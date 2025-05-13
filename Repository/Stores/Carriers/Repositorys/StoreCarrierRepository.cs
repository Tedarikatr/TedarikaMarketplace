using Data.Databases;
using Data.Repository;
using Entity.Stores.Carriers;
using Repository.Stores.Carriers.IRepositorys;

namespace Repository.Stores.Carriers.Repositorys
{
    public class StoreCarrierRepository : GenericRepository<StoreCarrier>, IStoreCarrierRepository
    {
        public StoreCarrierRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
