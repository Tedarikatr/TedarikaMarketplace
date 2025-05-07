using AutoMapper;
using Data.Dtos.Stores.Products;
using Entity.Stores.Products;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository.Product.IRepositorys;
using Repository.Stores.Product.IRepositorys;
using Services.Files.IServices;
using Services.Stores.Product.IServices;

namespace Services.Stores.Product.Services
{
    public class StoreProductService : IStoreProductService
    {
        private readonly IStoreProductRepository _storeProductRepo;
        private readonly IProductRepository _productRepo;
        private readonly IFilesService _filesService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreProductService> _logger;

        public StoreProductService(IStoreProductRepository storeProductRepo, IProductRepository productRepo, IFilesService filesService, IMediator mediator, IMapper mapper, ILogger<StoreProductService> logger)
        {
            _storeProductRepo = storeProductRepo;
            _productRepo = productRepo;
            _filesService = filesService;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<StoreProductDto>> GetAllProductsByShopDirectIdAsync(int storeId)
        {
            _logger.LogInformation("Mağazaya ait ürünler listeleniyor. ShopDirectId: {ShopDirectId}", storeId);
            try
            {
                var products = await _storeProductRepo.FindAsync(p => p.StoreId == storeId);
                _logger.LogInformation("{Count} ürün başarıyla getirildi. ShopDirectId: {ShopDirectId}", products.Count(), storeId);
                return _mapper.Map<IEnumerable<StoreProductDto>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağazaya ait ürünler listelenirken bir hata oluştu. ShopDirectId: {ShopDirectId}", storeId);
                throw new ApplicationException("Mağazaya ait ürünler listelenirken bir hata oluştu.", ex);
            }
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
                    ProductNumber = product.ProductNumber,
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

                storeProduct.UnitPrice = price;
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

        public async Task<string> UploadAndSetStoreImageAsync(int storeId, int productId, IFormFile file)
        {
            try
            {
                var sp = await _storeProductRepo.GetByStoreAndProductIdAsync(storeId, productId);
                if (sp == null)
                    return "Ürün mağazada bulunamadı.";

                var uploadResult = await _filesService.UploadFileAsync(file, "storeproduct-images");
                sp.StoreProductImageUrl = uploadResult.Url;

                await _storeProductRepo.UpdateAsync(sp);
                _logger.LogInformation("Görsel yüklendi ve ürün güncellendi. StoreId: {storeId}, ProductId: {productId}", storeId, productId);

                return "Görsel başarıyla yüklendi ve ürünle ilişkilendirildi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Görsel yükleme ve ilişkilendirme hatası.");
                throw new ApplicationException("Görsel yüklenirken bir hata oluştu.");
            }
        }


        public async Task<bool> UpdateMinMaxOrderQuantityAsync(int storeId, int productId, int minOrderQuantity, int maxOrderQuantity)
        {
            _logger.LogInformation("Min/Max sipariş miktarı güncelleniyor. ShopDirectId: {ShopDirectId}, ProductId: {ProductId}, MinOrderQuantity: {MinOrderQuantity}, MaxOrderQuantity: {MaxOrderQuantity}",
                storeId, productId, minOrderQuantity, maxOrderQuantity);

            try
            {
                var result = await _storeProductRepo.UpdateMinMaxOrderQuantityAsync(storeId, productId, minOrderQuantity, maxOrderQuantity);

                if (result)
                {
                    _logger.LogInformation("Min/Max sipariş miktarı başarıyla güncellendi. ShopDirectId: {ShopDirectId}, ProductId: {ProductId}", storeId, productId);
                }
                else
                {
                    _logger.LogWarning("Min/Max sipariş miktarı güncellenemedi. Ürün bulunamadı. ShopDirectId: {ShopDirectId}, ProductId: {ProductId}", storeId, productId);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Min/Max sipariş miktarı güncellenirken bir hata oluştu. ShopDirectId: {ShopDirectId}, ProductId: {ProductId}, MinOrderQuantity: {MinOrderQuantity}, MaxOrderQuantity: {MaxOrderQuantity}",
                    storeId, productId, minOrderQuantity, maxOrderQuantity);
                throw new ApplicationException($"Min/Max sipariş miktarı güncellenirken bir hata oluştu: {ex.Message}");
            }
        }


    }
}