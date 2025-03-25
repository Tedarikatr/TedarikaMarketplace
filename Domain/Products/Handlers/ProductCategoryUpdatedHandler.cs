using Domain.Categories.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Product.IRepositorys;

namespace Domain.Categories.Handlers
{
    public class ProductCategoryUpdatedHandler : INotificationHandler<ProductCategoryUpdatedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductCategoryUpdatedHandler> _logger;

        public ProductCategoryUpdatedHandler(IProductRepository productRepository, ILogger<ProductCategoryUpdatedHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task Handle(ProductCategoryUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            var affectedProducts = products.Where(p => p.CategoryId == notification.CategoryId).ToList();

            foreach (var product in affectedProducts)
            {
                product.CategoryName = notification.NewCategoryName;
            }

            await _productRepository.UpdateRangeAsync(affectedProducts);

            _logger.LogInformation("Kategori güncellendiği için {Count} ürünün CategoryName alanı güncellendi.", affectedProducts.Count);
        }
    }
}
