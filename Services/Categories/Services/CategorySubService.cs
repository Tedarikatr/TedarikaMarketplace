using AutoMapper;
using Data.Dtos.Categories;
using Domain.Categories.Events;
using Entity.Categories;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Categories.IRepositorys;
using Services.Categories.IServices;

namespace Services.Categories.Services
{
    public class CategorySubService : ICategorySubService
    {
        private readonly ICategorySubRepository _categorySubRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategorySubService> _logger;
        private readonly IMediator _mediator;

        public CategorySubService(ICategorySubRepository categorySubRepository, IMapper mapper, ILogger<CategorySubService> logger, IMediator mediator)
        {
            _categorySubRepository = categorySubRepository;
            _mapper = mapper;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<string> AddCategorySubAsync(CategorySubCreateDto categorySubCreateDto)
        {
            try
            {
                _logger.LogInformation("Yeni alt kategori ekleme işlemi başladı. Ad: {CategorySubName}", categorySubCreateDto.Name);

                var newCategorySub = _mapper.Map<CategorySub>(categorySubCreateDto);
                await _categorySubRepository.AddAsync(newCategorySub);

                _logger.LogInformation("Alt kategori eklendi. Ad: {CategorySubName}", newCategorySub.Name);
                return "Alt kategori başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategori ekleme sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<string> UpdateCategorySubAsync(int id, CategorySubUpdateDto categorySubUpdateDto)
        {
            try
            {
                _logger.LogInformation("Alt kategori güncelleniyor. ID: {CategorySubId}", id);

                var categorySub = await _categorySubRepository.GetByIdAsync(id);
                if (categorySub == null)
                {
                    throw new Exception("Alt kategori bulunamadı.");
                }

                var oldName = categorySub.Name;

                _mapper.Map(categorySubUpdateDto, categorySub);
                await _categorySubRepository.UpdateAsync(categorySub);

                _logger.LogInformation("Alt kategori güncellendi. ID: {CategorySubId}", id);

                if (!string.Equals(oldName, categorySub.Name, StringComparison.OrdinalIgnoreCase))
                {
                    await _mediator.Publish(new ProductCategorySubUpdatedEvent
                    {
                        CategorySubId = categorySub.Id,
                        NewSubCategoryName = categorySub.Name
                    });

                    _logger.LogInformation("Alt kategori adı değiştiği için ProductCategorySubUpdatedEvent tetiklendi.");
                }

                return "Alt kategori başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategori güncelleme sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<string> DeleteCategorySubAsync(int id)
        {
            try
            {
                _logger.LogInformation("Alt kategori siliniyor. ID: {CategorySubId}", id);

                var categorySub = await _categorySubRepository.GetByIdAsync(id);
                if (categorySub == null)
                {
                    throw new Exception("Alt kategori bulunamadı.");
                }

                await _categorySubRepository.RemoveAsync(categorySub);

                _logger.LogInformation("Alt kategori silindi. ID: {CategorySubId}", id);
                return "Alt kategori başarıyla silindi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategori silme sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<IEnumerable<CategorySubDto>> GetAllCategorySubsAsync()
        {
            try
            {
                _logger.LogInformation("Tüm alt kategoriler listeleniyor.");
                var categorySubs = await _categorySubRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CategorySubDto>>(categorySubs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategorileri listeleme sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<IEnumerable<CategorySubDto>> GetCategorySubsByMainCategoryIdAsync(int mainCategoryId)
        {
            try
            {
                _logger.LogInformation("Ana kategoriye ait alt kategoriler listeleniyor. Ana Kategori ID: {MainCategoryId}", mainCategoryId);
                var categorySubs = await _categorySubRepository.GetCategorySubsByMainCategoryIdAsync(mainCategoryId);
                return _mapper.Map<IEnumerable<CategorySubDto>>(categorySubs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ana kategoriye ait alt kategorileri listeleme sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<CategorySubDto> GetCategorySubByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Alt kategori detayları getiriliyor. ID: {CategorySubId}", id);
                var categorySub = await _categorySubRepository.GetByIdAsync(id);
                if (categorySub == null)
                {
                    throw new Exception("Alt kategori bulunamadı.");
                }
                return _mapper.Map<CategorySubDto>(categorySub);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alt kategori detaylarını getirirken hata oluştu.");
                throw;
            }
        }
    }
}
