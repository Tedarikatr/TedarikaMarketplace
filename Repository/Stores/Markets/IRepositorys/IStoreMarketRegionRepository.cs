using Data.Repository;
using Entity.Stores.Markets;

namespace Repository.Stores.Markets.IRepositorys
{
    public interface IStoreMarketRegionRepository : IGenericRepository<StoreMarketRegion>
    {
        Task<List<StoreMarketRegion>> GetByStoreIdAsync(int storeId);
        Task<bool> ExistsAsync(int storeId, string country, string province, string district);
    }
}
