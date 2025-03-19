using Data.Repository;
using Entity.Categories;

namespace Repository.Categories.IRepositorys
{
    public interface ICategorySubRepository : IGenericRepository<CategorySub>
    {
        Task<IEnumerable<CategorySub>> GetCategorySubsByMainCategoryIdAsync(int mainCategoryId);
    }

}
