using Entity.Stores.Products;
using Microsoft.Extensions.Logging;
using Repository.Product.IRepositorys;
using Repository.Stores.IRepositorys;
using Services.Stores.Product.IServices;

namespace Services.Stores.Product.Services
{
    public class StoreProductService : IStoreProductService
    {
        private readonly IStoreProductRepository _storeProductRepo;
        private readonly IProductRepository _productRepo;
        private readonly ILogger<StoreProductService> _logger;

        public StoreProductService(IStoreProductRepository storeProductRepo, IProductRepository productRepo, ILogger<StoreProductService> logger)
        {
            _storeProductRepo = storeProductRepo;
            _productRepo = productRepo;
            _logger = logger;
        }

        public async Task<string> AddProductToStoreAsync(int storeId, int productId)
        {
            try
            {
                var existing = await _storeProductRepo.GetByStoreAndProductIdAsync(storeId, productId);
                if (existing != null)
                    return "Bu ürün zaten mağazanıza eklenmiş.";

                var product = await _productRepo.GetByIdAsync(productId);
                if (product == null) throw new Exception("Ürün bulunamadı.");

                var storeProduct = new StoreProduct
                {
                    StoreId = storeId,
                    ProductId = productId,
                    Name = product.Name,
                    Description = product.Description,
                    Brand = product.Brand,
                    UnitType = product.UnitType,
                    UnitTypes = (int)product.UnitType,
                    ImageUrl = product.ImageUrl,
                    CategoryName = product.CategoryName,
                    CategorySubName = product.CategorySubName,
                };

                await _storeProductRepo.AddAsync(storeProduct);
                _logger.LogInformation("Ürün mağazaya eklendi. StoreId: {storeId}, ProductId: {productId}", storeId, productId);
                return "Ürün mağazaya başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün mağazaya eklenemedi.");
                throw new Exception("Ürün mağazaya eklenirken hata oluştu.");
            }
        }

        public async Task<string> UpdateStoreProductPriceAsync(int storeId, int productId, decimal price)
        {
            try
            {
                var storeProduct = await _storeProductRepo.GetByStoreAndProductIdAsync(storeId, productId);
                if (storeProduct == null) return "Ürün mağazanıza ekli değil.";

                storeProduct.Price = price;
                await _storeProductRepo.UpdateAsync(storeProduct);

                return "Ürün fiyatı güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fiyat güncelleme hatası.");
                throw;
            }
        }

        public async Task<string> SetProductActiveStatusAsync(int storeId, int productId, bool isActive)
        {
            try
            {
                var sp = await _storeProductRepo.GetByStoreAndProductIdAsync(storeId, productId);
                if (sp == null) return "Ürün mağazada bulunamadı.";

                sp.IsActive = isActive;
                await _storeProductRepo.UpdateAsync(sp);

                return $"Ürün {(isActive ? "aktifleştirildi" : "pasifleştirildi")}.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aktiflik durumu güncellenemedi.");
                throw;
            }
        }

        public async Task<string> SetProductOnSaleStatusAsync(int storeId, int productId, bool isOnSale)
        {
            try
            {
                var sp = await _storeProductRepo.GetByStoreAndProductIdAsync(storeId, productId);
                if (sp == null) return "Ürün mağazada bulunamadı.";

                sp.IsOnSale = isOnSale;
                await _storeProductRepo.UpdateAsync(sp);

                return $"Ürün {(isOnSale ? "satışa çıkarıldı" : "satıştan kaldırıldı")}.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış durumu güncellenemedi.");
                throw;
            }
        }

        public async Task<string> SetMinMaxQuantityAsync(int storeId, int productId, int minQty, int maxQty)
        {
            try
            {
                var sp = await _storeProductRepo.GetByStoreAndProductIdAsync(storeId, productId);
                if (sp == null) return "Ürün mağazada bulunamadı.";

                sp.MinOrderQuantity = minQty;
                sp.MaxOrderQuantity = maxQty;
                await _storeProductRepo.UpdateAsync(sp);

                return "Minimum ve maksimum sipariş miktarları güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Min/Max sipariş güncellemesi hatası.");
                throw;
            }
        }

        public async Task<string> SetAllowedRegionsAsync(int storeId, int productId, bool allowedDomestic, bool allowedInternational)
        {
            try
            {
                var sp = await _storeProductRepo.GetByStoreAndProductIdAsync(storeId, productId);
                if (sp == null) return "Ürün mağazada bulunamadı.";

                sp.AllowedDomestic = allowedDomestic;
                sp.AllowedInternational = allowedInternational;

                await _storeProductRepo.UpdateAsync(sp);
                return "Bölgesel izinler güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölge güncelleme hatası.");
                throw;
            }
        }

        public async Task<string> UpdateStoreImageAsync(int storeId, int productId, string imageUrl)
        {
            try
            {
                var sp = await _storeProductRepo.GetByStoreAndProductIdAsync(storeId, productId);
                if (sp == null) return "Ürün mağazada bulunamadı.";

                sp.StoreImageUrl = imageUrl;
                await _storeProductRepo.UpdateAsync(sp);
                return "Mağaza görseli güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Görsel güncelleme hatası.");
                throw;
            }
        }
    }
}