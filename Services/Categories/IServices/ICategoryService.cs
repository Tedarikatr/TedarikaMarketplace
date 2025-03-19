using Data.Dtos.Categories;
using Microsoft.AspNetCore.Http;

namespace Services.Categories.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int categoryId);
        Task<string> CreateCategoryAsync(CategoryCreateDto categoryCreateDto, IFormFile categoryImage);
        Task<string> UpdateCategoryAsync(int categoryId, CategoryUpdateDto categoryUpdateDto);
        Task<string> DeleteCategoryAsync(int categoryId);
        Task<string> UpdateCategoryImageAsync(int categoryId, IFormFile categoryImage);
    }
}
