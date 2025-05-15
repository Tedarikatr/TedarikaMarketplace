using Domain.Products.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Stores.Product.IRepositorys;

namespace Domain.Products.Handlers
{
    public class ProductExportBannedEventHandler : INotificationHandler<ProductExportBannedEvent>
    {
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly ILogger<ProductExportBannedEventHandler> _logger;

        public ProductExportBannedEventHandler(
            IStoreProductRepository storeProductRepository,
            ILogger<ProductExportBannedEventHandler> logger)
        {
            _storeProductRepository = storeProductRepository;
            _logger = logger;
        }

        public async Task Handle(ProductExportBannedEvent notification, CancellationToken cancellationToken)
        {
            var affectedProducts = await _storeProductRepository
                .GetQueryable()
                .Where(sp => sp.ProductId == notification.ProductId && sp.AllowedInternational)
                .ToListAsync();

            foreach (var item in affectedProducts)
            {
                item.IsActive = false;
                item.AllowedInternational = false;            
                item.BlockedByExportBan = true;
            }

            await _storeProductRepository.UpdateRangeAsync(affectedProducts);
            _logger.LogInformation("ProductId {ProductId} için {Count} mağaza ürününün uluslararası satışı durduruldu.", notification.ProductId, affectedProducts.Count);
        }
    }
}
