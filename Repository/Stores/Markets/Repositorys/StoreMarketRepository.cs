using Data.Databases;
using Data.Repository;
using Entity.Stores.Markets;
using Repository.Stores.Markets.IRepositorys;

namespace Repository.Stores.Markets.Repositorys
{
    public class StoreMarketRepository : GenericRepository<StoreMarket>, IStoreMarketRepository
    {
        public StoreMarketRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
