using Data.Databases;
using Data.Repository;
using Entity.Categories;
using Microsoft.EntityFrameworkCore;
using Repository.Categories.IRepositorys;

namespace Repository.Categories.Repositorys
{
    public class CategorySubRepository : GenericRepository<CategorySub>, ICategorySubRepository
    {
        public CategorySubRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<CategorySub>> GetCategorySubsByMainCategoryIdAsync(int mainCategoryId)
        {
            return await _dbSet.Where(c => c.MainCategoryId == mainCategoryId).ToListAsync();
        }
    }
}
