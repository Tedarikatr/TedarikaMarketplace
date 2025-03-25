using AutoMapper;
using Data.Dtos.Categories;
using Domain.Products.Events;
using Entity.Categories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository.Categories.IRepositorys;
using Services.Categories.IServices;
using Services.Files.IServices;

namespace Services.Categories.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFilesService _filesService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMediator _mediator;

        public CategoryService(ICategoryRepository categoryRepository, IFilesService filesService, IMapper mapper, ILogger<CategoryService> logger, IMediator mediator)
        {
            _categoryRepository = categoryRepository;
            _filesService = filesService;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            try
            {
                _logger.LogInformation("Tüm kategoriler listeleniyor.");
                var categories = await _categoryRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CategoryDto>>(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm kategorileri getirirken hata oluştu.");
                throw;
            }
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                _logger.LogInformation("Kategori detayları alınıyor. Kategori ID: {CategoryId}", categoryId);
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null) throw new Exception("Kategori bulunamadı.");
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori bilgilerini alırken hata oluştu. ID: {CategoryId}", categoryId);
                throw;
            }
        }

        public async Task<string> CreateCategoryAsync(CategoryCreateDto categoryCreateDto, IFormFile categoryImage)
        {
            try
            {
                _logger.LogInformation("Yeni kategori ekleniyor. Kategori Adı: {CategoryName}", categoryCreateDto.CategoryName);

                var category = _mapper.Map<Category>(categoryCreateDto);

                if (categoryImage != null)
                {
                    var uploadResult = await _filesService.UploadFileAsync(categoryImage, "category-images");
                    category.CategoryImage = uploadResult.Url;
                }

                await _categoryRepository.AddAsync(category);
                _logger.LogInformation("Kategori başarıyla eklendi. ID: {CategoryId}", category.Id);
                return "Kategori başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori ekleme işlemi sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<string> UpdateCategoryAsync(int categoryId, CategoryUpdateDto categoryUpdateDto)
        {
            try
            {
                _logger.LogInformation("Kategori güncelleniyor. ID: {CategoryId}", categoryId);
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null) throw new Exception("Kategori bulunamadı.");

                var oldCategoryName = category.CategoryName;

                _mapper.Map(categoryUpdateDto, category);
                await _categoryRepository.UpdateAsync(category);

                _logger.LogInformation("Kategori başarıyla güncellendi. ID: {CategoryId}", categoryId);

                if (!string.Equals(oldCategoryName, category.CategoryName, StringComparison.OrdinalIgnoreCase))
                {
                    await _mediator.Publish(new ProductCategoryUpdatedEvent
                    {
                        CategoryId = categoryId,
                        NewCategoryName = category.CategoryName
                    });

                    _logger.LogInformation("Kategori adı değiştiği için ProductCategoryUpdatedEvent tetiklendi.");
                }

                return "Kategori başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori güncelleme sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<string> DeleteCategoryAsync(int categoryId)
        {
            try
            {
                _logger.LogInformation("Kategori siliniyor. ID: {CategoryId}", categoryId);
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null) throw new Exception("Kategori bulunamadı.");

                await _categoryRepository.RemoveAsync(category);

                _logger.LogInformation("Kategori başarıyla silindi. ID: {CategoryId}", categoryId);
                return "Kategori başarıyla silindi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori silme işlemi sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<string> UpdateCategoryImageAsync(int categoryId, IFormFile categoryImage)
        {
            try
            {
                _logger.LogInformation("Kategori resmi güncelleniyor. ID: {CategoryId}", categoryId);
                var category = await _categoryRepository.GetByIdAsync(categoryId);
                if (category == null) throw new Exception("Kategori bulunamadı.");

                var uploadResult = await _filesService.UploadFileAsync(categoryImage, "category-images");
                category.CategoryImage = uploadResult.Url;

                await _categoryRepository.UpdateAsync(category);

                _logger.LogInformation("Kategori resmi başarıyla güncellendi. ID: {CategoryId}", categoryId);
                return "Kategori resmi başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kategori resmi güncellenirken hata oluştu.");
                throw;
            }
        }
    }
}