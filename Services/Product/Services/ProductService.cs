using AutoMapper;
using Data.Dtos.Products;
using Entity.Products;
using Microsoft.Extensions.Logging;
using Repository.Product.IRepositorys;
using Services.Products.IServices;

namespace Services.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> CreateProductAsync(ProductCreateDto dto)
        {
            try
            {
                var product = _mapper.Map<Product>(dto);
                product.CreatedAt = DateTime.UtcNow;

                await _productRepository.AddAsync(product);

                _logger.LogInformation("Yeni ürün eklendi: {ProductName}", product.Name);
                return "Ürün başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün ekleme sırasında hata oluştu.");
                throw new Exception("Ürün eklenirken bir hata oluştu.");
            }
        }

        public async Task<string> UpdateProductAsync(int productId, ProductUpdateDto dto)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                    throw new Exception("Ürün bulunamadı.");

                _mapper.Map(dto, product);
                product.UpdatedAt = DateTime.UtcNow;

                await _productRepository.UpdateAsync(product);

                _logger.LogInformation("Ürün güncellendi: ID {ProductId}", productId);
                return "Ürün başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün güncelleme sırasında hata oluştu.");
                throw new Exception("Ürün güncellenirken bir hata oluştu.");
            }
        }

        public async Task<string> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                    throw new Exception("Ürün bulunamadı.");

                await _productRepository.RemoveAsync(product);

                _logger.LogInformation("Ürün silindi: ID {ProductId}", productId);
                return "Ürün başarıyla silindi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün silme sırasında hata oluştu.");
                throw new Exception("Ürün silinirken bir hata oluştu.");
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Ürün bulunamadı.");

            return _mapper.Map<ProductDto>(product);
        }
    }
}
