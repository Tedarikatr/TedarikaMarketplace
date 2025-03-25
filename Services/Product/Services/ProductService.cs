using AutoMapper;
using Data.Dtos.Product;
using Domain.Products.Events;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository.Categories.IRepositorys;
using Repository.Product.IRepositorys;
using Services.Files.IServices;
using Services.Product.IServices;

namespace Services.Product.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICategorySubRepository _categorySubRepository;
        private readonly IFilesService _filesService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, ICategorySubRepository categorySubRepository, IFilesService filesService, IMediator mediator, IMapper mapper, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _categorySubRepository = categorySubRepository;
            _filesService = filesService;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> CreateProductAsync(ProductCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni ürün oluşturma işlemi başlatıldı.");

                var product = _mapper.Map<Entity.Products.Product>(dto);
                product.CreatedAt = DateTime.UtcNow;

                if (dto.Image != null && dto.Image.Length > 0)
                {
                    var uploadResult = await _filesService.UploadFileAsync(dto.Image, "product-images");
                    product.ImageUrl = uploadResult.Url;
                    _logger.LogInformation("Ürün görseli başarıyla yüklendi. URL: {ImageUrl}", uploadResult.Url);
                }

                if (dto.CategoryId.HasValue)
                {
                    var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
                    if (category == null)
                        throw new Exception("Geçerli bir kategori bulunamadı.");
                    product.CategoryName = category.CategoryName;
                }

                if (dto.CategorySubId.HasValue)
                {
                    var sub = await _categorySubRepository.GetByIdAsync(dto.CategorySubId.Value);
                    if (sub == null)
                        throw new Exception("Geçerli bir alt kategori bulunamadı.");
                    product.CategorySubName = sub.Name;
                }

                await _productRepository.AddAsync(product);
                _logger.LogInformation("Ürün başarıyla eklendi: {ProductName}", product.Name);

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
                _logger.LogInformation("Ürün güncelleme işlemi başlatıldı. ProductId: {ProductId}", productId);

                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    _logger.LogWarning("Ürün bulunamadı. ID: {ProductId}", productId);
                    throw new Exception("Ürün bulunamadı.");
                }

                var oldName = product.Name;
                var oldBrand = product.Brand;
                var oldDescription = product.Description;
                var oldImageUrl = product.ImageUrl;
                var oldUnitTypes = product.UnitTypes;

                _mapper.Map(dto, product);
                product.UpdatedAt = DateTime.UtcNow;

                if (dto.CategoryId.HasValue)
                {
                    var category = await _categoryRepository.GetByIdAsync(dto.CategoryId.Value);
                    if (category == null)
                        throw new Exception("Geçerli bir kategori bulunamadı.");
                    product.CategoryName = category.CategoryName;
                }

                if (dto.CategorySubId.HasValue)
                {
                    var subCategory = await _categorySubRepository.GetByIdAsync(dto.CategorySubId.Value);
                    if (subCategory == null)
                        throw new Exception("Geçerli bir alt kategori bulunamadı.");
                    product.CategorySubName = subCategory.Name;
                }

                await _productRepository.UpdateAsync(product);
                _logger.LogInformation("Ürün başarıyla güncellendi. ProductId: {ProductId}", productId);

                bool shouldTriggerEvent =
                    !string.Equals(oldName, product.Name, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(oldBrand, product.Brand, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(oldDescription, product.Description, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(oldImageUrl, product.ImageUrl, StringComparison.OrdinalIgnoreCase) ||
                    !string.Equals(oldUnitTypes, product.UnitTypes, StringComparison.OrdinalIgnoreCase);

                if (shouldTriggerEvent)
                {
                    await _mediator.Publish(new ProductUpdatedEvent
                    {
                        ProductId = product.Id,
                        NewName = product.Name,
                        NewBrand = product.Brand,
                        NewDescription = product.Description,
                        NewImageUrl = product.ImageUrl,
                        NewUnitTypes = product.UnitTypes
                    });

                    _logger.LogInformation("ProductUpdatedEvent yayınlandı. ProductId: {ProductId}", product.Id);
                }

                return "Ürün başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün güncelleme sırasında hata oluştu. ProductId: {ProductId}", productId);
                throw new Exception("Ürün güncellenirken bir hata oluştu.");
            }
        }

        public async Task<string> UpdateProductImageAsync(int productId, IFormFile imageFile)
        {
            try
            {
                _logger.LogInformation("Ürün görseli güncelleme işlemi başladı. ProductId: {ProductId}", productId);

                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                    throw new Exception("Ürün bulunamadı.");

                if (imageFile == null || imageFile.Length == 0)
                    throw new Exception("Geçerli bir görsel dosyası yüklenmedi.");

                var uploadResult = await _filesService.UploadFileAsync(imageFile, "product-images");
                product.ImageUrl = uploadResult.Url;
                product.UpdatedAt = DateTime.UtcNow;

                await _productRepository.UpdateAsync(product);

                _logger.LogInformation("Ürün görseli başarıyla güncellendi. Yeni URL: {ImageUrl}", product.ImageUrl);

                await _mediator.Publish(new ProductImageUpdatedEvent
                {
                    ProductId = productId,
                    NewImageUrl = product.ImageUrl
                });

                return "Ürün görseli başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün görseli güncellenirken hata oluştu.");
                throw new Exception("Görsel güncellenirken bir hata oluştu.");
            }
        }

        public async Task<string> DeleteProductAsync(int productId)
        {
            _logger.LogInformation("Ürün silme işlemi başlatıldı. ID: {ProductId}", productId);

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Ürün bulunamadı.");

            await _productRepository.RemoveAsync(product);
            _logger.LogInformation("Ürün silindi. ID: {ProductId}", productId);

            return "Ürün başarıyla silindi.";
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            _logger.LogInformation("Tüm ürünler listeleniyor.");
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByIdAsync(int productId)
        {
            _logger.LogInformation("Ürün bilgisi alınıyor. ID: {ProductId}", productId);
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new Exception("Ürün bulunamadı.");

            return _mapper.Map<ProductDto>(product);
        }
    }
}
