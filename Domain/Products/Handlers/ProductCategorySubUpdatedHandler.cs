using Domain.Categories.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Product.IRepositorys;

namespace Domain.Categories.Handlers
{
    public class ProductCategorySubUpdatedHandler : INotificationHandler<ProductCategorySubUpdatedEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductCategorySubUpdatedHandler> _logger;

        public ProductCategorySubUpdatedHandler(IProductRepository productRepository, ILogger<ProductCategorySubUpdatedHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task Handle(ProductCategorySubUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();
            var affectedProducts = products.Where(p => p.CategorySubId == notification.CategorySubId).ToList();

            foreach (var product in affectedProducts)
            {
                product.CategorySubName = notification.NewSubCategoryName;
            }

            await _productRepository.UpdateRangeAsync(affectedProducts);

            _logger.LogInformation("Alt kategori güncellendiği için {Count} ürünün CategorySubName alanı güncellendi.", affectedProducts.Count);
        }
    }
}
