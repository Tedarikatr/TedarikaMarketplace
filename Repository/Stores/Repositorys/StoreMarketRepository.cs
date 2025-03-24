using Data.Databases;
using Data.Repository;
using Entity.Stores;
using Repository.Stores.IRepositorys;

namespace Repository.Stores.Repositorys
{
    public class StoreMarketRepository : GenericRepository<StoreMarket>, IStoreMarketRepository
    {
        public StoreMarketRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
