using Data.Databases;
using Data.Repository;
using Entity.Stores.Products;
using Repository.Stores.Product.IRepositorys;

namespace Repository.Stores.Product.Repositorys
{
    public class StoreProductRequestRepository : GenericRepository<StoreProductRequest>, IStoreProductRequestRepository
    {
        public StoreProductRequestRepository(ApplicationDbContext context) : base(context)
        {
        }

     
    }
}
