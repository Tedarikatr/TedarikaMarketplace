using AutoMapper;
using Data.Dtos.Baskets;
using Entity.Baskets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Baskets.IRepositorys;
using Repository.Stores.Product.IRepositorys;
using Services.Baskets.IServices;

namespace Services.Baskets.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketService> _logger;

        public BasketService(
            IBasketRepository basketRepository,
            IStoreProductRepository storeProductRepository,
            IMapper mapper,
            ILogger<BasketService> logger)
        {
            _basketRepository = basketRepository;
            _storeProductRepository = storeProductRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BasketDto> GetBasketAsync(int userId)
        {
            var basket = await GetBasketWithItems(userId);
            return _mapper.Map<BasketDto>(basket);
        }

        public async Task AddItemAsync(int userId, AddToBasketDto dto)
        {
            var product = await _storeProductRepository.GetByIdAsync(dto.ProductId);
            if (product == null)
            {
                _logger.LogWarning("Sepete eklenmek istenen ürün bulunamadı. ProductId: {ProductId}", dto.ProductId);
                throw new KeyNotFoundException("Ürün bulunamadı.");
            }

            if (!product.IsActive)
            {
                _logger.LogWarning("Pasif ürün sepete eklenmek istendi. ProductId: {ProductId}", dto.ProductId);
                throw new InvalidOperationException("Bu ürün şu anda aktif değil.");
            }

            if (dto.Quantity < product.MinOrderQuantity || dto.Quantity > product.MaxOrderQuantity)
            {
                _logger.LogWarning("Sipariş miktarı sınırlar dışında. ProductId: {ProductId}, Quantity: {Quantity}, AllowedMin: {Min}, Max: {Max}",
                    dto.ProductId, dto.Quantity, product.MinOrderQuantity, product.MaxOrderQuantity);
                throw new InvalidOperationException("Sipariş miktarı izin verilen aralıkta değil.");
            }

            var basket = await EnsureBasketExists(userId);
            var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                int newQuantity = existingItem.Quantity + dto.Quantity;

                if (newQuantity > product.StockQuantity)
                {
                    _logger.LogWarning("Stok yetersiz. İstenen: {Quantity}, Stok: {Stock}", newQuantity, product.StockQuantity);
                    throw new InvalidOperationException("Yeterli stok yok.");
                }

                existingItem.Quantity = newQuantity;
                existingItem.TotalPrice = existingItem.Quantity * existingItem.UnitPrice;
            }
            else
            {
                if (dto.Quantity > product.StockQuantity)
                {
                    _logger.LogWarning("Stok yetersiz. İstenen: {Quantity}, Stok: {Stock}", dto.Quantity, product.StockQuantity);
                    throw new InvalidOperationException("Yeterli stok yok.");
                }

                basket.Items.Add(new BasketItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    UnitPrice = product.UnitPrice,
                    Quantity = dto.Quantity,
                    TotalPrice = dto.Quantity * product.UnitPrice
                });
            }

            basket.TotalAmount = basket.Items.Sum(i => i.TotalPrice);
            basket.UpdatedAt = DateTime.UtcNow;

            await _basketRepository.UpdateAsync(basket);
            _logger.LogInformation("Sepete ürün eklendi. UserId: {UserId}, ProductId: {ProductId}, Quantity: {Quantity}", userId, dto.ProductId, dto.Quantity);
        }

        public async Task<BasketDto> IncreaseQuantityAsync(int userId, int productId)
        {
            try
            {
                var basket = await GetBasketWithItems(userId);
                var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
                if (item == null) throw new KeyNotFoundException("Ürün sepette bulunamadı.");

                var product = await _storeProductRepository.GetByIdAsync(productId);
                if (product == null) throw new KeyNotFoundException("Ürün sistemde bulunamadı.");

                if (item.Quantity + 1 > product.MaxOrderQuantity)
                    throw new InvalidOperationException($"Maksimum sipariş sınırı aşıldı. Max: {product.MaxOrderQuantity}");

                if (item.Quantity + 1 > product.StockQuantity)
                    throw new InvalidOperationException($"Stok sınırı aşıldı. Stok: {product.StockQuantity}");

                item.Quantity += 1;
                item.TotalPrice = item.Quantity * item.UnitPrice;
                basket.TotalAmount = basket.Items.Sum(i => i.TotalPrice);
                basket.UpdatedAt = DateTime.UtcNow;

                await _basketRepository.UpdateAsync(basket);
                return _mapper.Map<BasketDto>(basket);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Sepet eşzamanlı güncellenemedi. Retry: IncreaseQuantityAsync");
                return await IncreaseQuantityAsync(userId, productId);
            }
        }

        public async Task<BasketDto> DecreaseQuantityAsync(int userId, int productId)
        {
            try
            {
                var basket = await GetBasketWithItems(userId);
                var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
                if (item == null) throw new KeyNotFoundException("Ürün sepette bulunamadı.");

                if (item.Quantity <= 1)
                {
                    basket.Items.Remove(item);
                    _logger.LogInformation("Sepetten ürün tamamen çıkarıldı. ProductId: {ProductId}", productId);
                }
                else
                {
                    item.Quantity -= 1;
                    item.TotalPrice = item.Quantity * item.UnitPrice;
                }

                basket.TotalAmount = basket.Items.Sum(i => i.TotalPrice);
                basket.UpdatedAt = DateTime.UtcNow;

                await _basketRepository.UpdateAsync(basket);
                return _mapper.Map<BasketDto>(basket);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency hatası: DecreaseQuantityAsync tekrar deneniyor.");
                return await DecreaseQuantityAsync(userId, productId); 
            }
        }

        public async Task RemoveItemAsync(int userId, int productId)
        {
            var basket = await GetBasketWithItems(userId);
            var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null) return;

            basket.Items.Remove(item);
            basket.TotalAmount = basket.Items.Sum(i => i.TotalPrice);
            basket.UpdatedAt = DateTime.UtcNow;

            await _basketRepository.UpdateAsync(basket);
            _logger.LogInformation("Sepetten ürün silindi. ProductId: {ProductId}, UserId: {UserId}", productId, userId);
        }

        public async Task ClearBasketAsync(int userId)
        {
            var basket = await GetBasketWithItems(userId);
            basket.Items.Clear();
            basket.TotalAmount = 0;
            basket.UpdatedAt = DateTime.UtcNow;

            await _basketRepository.UpdateAsync(basket);
            _logger.LogInformation("Sepet temizlendi. UserId: {UserId}", userId);
        }

        public async Task<decimal> GetTotalAsync(int userId)
        {
            var basket = await GetBasketWithItems(userId);
            return basket.TotalAmount;
        }

        private async Task<Basket> EnsureBasketExists(int userId)
        {
            var basket = await _basketRepository
                .GetQueryable()
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.BuyerId == userId);

            if (basket == null)
            {
                basket = new Basket
                {
                    BuyerId = userId,
                    Items = new List<BasketItem>(),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _basketRepository.AddAsync(basket);
                _logger.LogInformation("Yeni sepet oluşturuldu. UserId: {UserId}", userId);
            }

            return basket;
        }

        private async Task<Basket> GetBasketWithItems(int userId)
        {
            var basket = await _basketRepository
                .GetQueryable()
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.BuyerId == userId);

            if (basket == null)
            {
                _logger.LogWarning("Sepet bulunamadı. UserId: {UserId}", userId);
                throw new KeyNotFoundException("Sepet bulunamadı.");
            }

            return basket;
        }
    }
}
