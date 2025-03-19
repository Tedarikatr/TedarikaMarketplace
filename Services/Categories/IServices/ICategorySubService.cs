using Data.Dtos.Categories;

namespace Services.Categories.IServices
{
    public interface ICategorySubService
    {
        Task<string> AddCategorySubAsync(CategorySubCreateDto categorySubCreateDto);
        Task<string> UpdateCategorySubAsync(int id, CategorySubUpdateDto categorySubUpdateDto);
        Task<string> DeleteCategorySubAsync(int id);
        Task<IEnumerable<CategorySubDto>> GetAllCategorySubsAsync();
        Task<IEnumerable<CategorySubDto>> GetCategorySubsByMainCategoryIdAsync(int mainCategoryId);
        Task<CategorySubDto> GetCategorySubByIdAsync(int id);
    }
}
