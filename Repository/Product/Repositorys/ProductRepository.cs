using Data.Databases;
using Data.Repository;
using Repository.Product.IRepositorys;

namespace Repository.Product.Repositorys
{
    public class ProductRepository : GenericRepository<Entity.Products.Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
