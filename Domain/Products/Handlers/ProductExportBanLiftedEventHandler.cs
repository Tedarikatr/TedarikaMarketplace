using Domain.Products.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Stores.Product.IRepositorys;

namespace Domain.Products.Handlers
{
    public class ProductExportBanLiftedEventHandler : INotificationHandler<ProductExportBanLiftedEvent>
    {
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly ILogger<ProductExportBanLiftedEventHandler> _logger;

        public ProductExportBanLiftedEventHandler(
            IStoreProductRepository storeProductRepository,
            ILogger<ProductExportBanLiftedEventHandler> logger)
        {
            _storeProductRepository = storeProductRepository;
            _logger = logger;
        }

        public async Task Handle(ProductExportBanLiftedEvent notification, CancellationToken cancellationToken)
        {
            var affectedProducts = await _storeProductRepository
                .GetQueryable()
                .Where(sp => sp.ProductId == notification.ProductId && !sp.AllowedInternational)
                .ToListAsync();

            foreach (var item in affectedProducts)
            {
                item.AllowedInternational = true;
                item.IsActive = true;
                item.BlockedByExportBan = false;  
            }

            await _storeProductRepository.UpdateRangeAsync(affectedProducts);
            _logger.LogInformation("ProductId {ProductId} için yasağı kaldırılan ürünler yeniden aktif hale getirildi.", notification.ProductId);
        }
    }
}
