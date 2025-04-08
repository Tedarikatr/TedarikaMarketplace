using Domain.Products.Events;
using Entity.Products;
using Entity.Stores.Products;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Product.IRepositorys;
using Repository.Stores.Product.IRepositorys;

namespace Domain.Products.Handlers
{
    public class StoreProductRequestApprovedEventHandler : INotificationHandler<StoreProductRequestApprovedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly ILogger<StoreProductRequestApprovedEventHandler> _logger;

        public StoreProductRequestApprovedEventHandler(
            IProductRepository productRepository,
            IStoreProductRepository storeProductRepository,
            ILogger<StoreProductRequestApprovedEventHandler> logger)
        {
            _productRepository = productRepository;
            _storeProductRepository = storeProductRepository;
            _logger = logger;
        }

        public async Task Handle(StoreProductRequestApprovedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("StoreProductRequest onaylandı. Ürün ve mağaza ürünü oluşturuluyor. Request ID: {RequestId}", notification.RequestId);

                var newProduct = new Product
                {
                    Name = notification.Name,
                    Description = notification.Description,
                    Brand = notification.Brand,
                    UnitTypes = notification.UnitTypes,
                    UnitType = (UnitType)notification.UnitType,
                    CategoryId = notification.CategoryId,
                    CategoryName = notification.CategoryName,
                    CategorySubId = notification.CategorySubId,
                    CategorySubName = notification.CategorySubName,
                    ImageUrl = notification.ImageUrl,
                    PreparationTime = notification.PreparationTime,
                    ExpirationDate = notification.ExpirationDate,
                    CreatedAt = DateTime.UtcNow
                };

                await _productRepository.AddAsync(newProduct);

                var newStoreProduct = new StoreProduct
                {
                    StoreId = notification.StoreId,
                    ProductId = newProduct.Id,
                    Name = newProduct.Name,
                    Description = newProduct.Description,
                    Brand = newProduct.Brand,
                    UnitTypes = (int)newProduct.UnitType,
                    UnitType = newProduct.UnitType,
                    ImageUrl = newProduct.ImageUrl,
                    CategoryName = newProduct.CategoryName,
                    CategorySubName = newProduct.CategorySubName,
                    IsActive = true,
                    IsOnSale = true,
                    AllowedDomestic = true,
                    AllowedInternational = false,
                    MinOrderQuantity = 1,
                    MaxOrderQuantity = 100,
                    Price = 0
                };

                await _storeProductRepository.AddAsync(newStoreProduct);

                _logger.LogInformation("Ürün ve mağaza ürünü başarıyla oluşturuldu. Product ID: {ProductId}, StoreProduct ID: {StoreProductId}", newProduct.Id, newStoreProduct.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "StoreProductRequest işlenirken bir hata oluştu. Request ID: {RequestId}", notification.RequestId);
                throw;
            }
        }
    } 
}
