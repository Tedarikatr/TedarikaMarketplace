using Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Stores.Product.IRepositorys;

namespace Domain.Products.Handlers
{
    public class ProductUpdatedEventHandler : INotificationHandler<ProductUpdatedEvent>
    {
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly ILogger<ProductUpdatedEventHandler> _logger;

        public ProductUpdatedEventHandler(IStoreProductRepository storeProductRepository, ILogger<ProductUpdatedEventHandler> logger)
        {
            _storeProductRepository = storeProductRepository;
            _logger = logger;
        }

        public async Task Handle(ProductUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProductUpdatedEvent tetiklendi. ProductId: {ProductId}", notification.ProductId);

            var affectedStoreProducts = await _storeProductRepository.FindAsync(sp => sp.ProductId == notification.ProductId);
            foreach (var sp in affectedStoreProducts)
            {
                sp.Name = notification.NewName;
                sp.Brand = notification.NewBrand;
                sp.Description = notification.NewDescription;
                sp.ImageUrl = notification.NewImageUrl;
                sp.UnitTypes = Enum.TryParse(notification.NewUnitTypes, out Entity.Products.UnitType unitType)
                                ? (int)unitType : sp.UnitTypes;
                sp.UnitType = Enum.TryParse(notification.NewUnitTypes, out unitType)
                                ? unitType : sp.UnitType;
            }

            await _storeProductRepository.UpdateRangeAsync(affectedStoreProducts);
            _logger.LogInformation("StoreProduct kayıtları güncellendi. Etkilenen ürün sayısı: {Count}", affectedStoreProducts.Count());
        }
    }
}
