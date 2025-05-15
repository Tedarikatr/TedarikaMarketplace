using Data.Databases;
using Data.Repository;
using Entity.Stores.Products;
using Repository.Stores.Product.IRepositorys;

namespace Repository.Stores.Product.Repositorys
{
    public class StoreProductShowroomRepository : GenericRepository<StoreProductShowroom>, IStoreProductShowroomRepository
    {
        public StoreProductShowroomRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
