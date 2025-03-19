using Data.Databases;
using Data.Repository;
using Entity.Categories;
using Repository.Categories.IRepositorys;

namespace Repository.Categories.Repositorys
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context) { }
    }
}
