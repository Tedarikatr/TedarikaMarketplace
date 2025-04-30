using Data.Databases;
using Data.Repository;
using Entity.Stores.Markets;
using Repository.Stores.Markets.IRepositorys;

namespace Repository.Stores.Markets.Repositorys
{
    public class StoreMarketCoverageRepository : GenericRepository<StoreMarketCoverage>, IStoreMarketCoverageRepository
    {
        public StoreMarketCoverageRepository(ApplicationDbContext context) : base(context)
        {
        }

       
    }
}
