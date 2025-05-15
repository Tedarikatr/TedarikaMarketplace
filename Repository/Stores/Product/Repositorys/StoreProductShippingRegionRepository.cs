using Data.Databases;
using Data.Repository;
using Entity.Stores.Products;
using Repository.Stores.Product.IRepositorys;

namespace Repository.Stores.Product.Repositorys
{
    public class StoreProductShippingRegionRepository : GenericRepository<StoreProductShippingRegion>, IStoreProductShippingRegionRepository
    {
        public StoreProductShippingRegionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
