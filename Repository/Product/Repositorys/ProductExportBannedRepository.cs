using Data.Databases;
using Data.Repository;
using Entity.Products;
using Repository.Product.IRepositorys;

namespace Repository.Product.Repositorys
{
    public class ProductExportBannedRepository : GenericRepository<ProductExportBanned>, IProductExportBannedRepository
    {
        public ProductExportBannedRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
