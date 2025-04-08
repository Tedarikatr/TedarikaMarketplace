using Domain.Products.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Stores.Product.IRepositorys;

namespace Domain.Products.Handlers
{
    public class ProductImageUpdatedEventHandler : INotificationHandler<ProductImageUpdatedEvent>
    {
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly ILogger<ProductImageUpdatedEventHandler> _logger;

        public ProductImageUpdatedEventHandler(IStoreProductRepository storeProductRepository, ILogger<ProductImageUpdatedEventHandler> logger)
        {
            _storeProductRepository = storeProductRepository;
            _logger = logger;
        }

        public async Task Handle(ProductImageUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ProductImageUpdatedEvent tetiklendi. ProductId: {ProductId}", notification.ProductId);

            var relatedStoreProducts = await _storeProductRepository.FindAsync(x => x.ProductId == notification.ProductId);
            if (!relatedStoreProducts.Any())
            {
                _logger.LogWarning("Bu ürüne bağlı mağaza ürünü bulunamadı.");
                return;
            }

            foreach (var storeProduct in relatedStoreProducts)
            {
                storeProduct.ImageUrl = notification.NewImageUrl;
            }

            await _storeProductRepository.UpdateRangeAsync(relatedStoreProducts);
            _logger.LogInformation("{Count} adet StoreProduct görseli güncellendi.", relatedStoreProducts.Count());
        }
    }
}
