using Entity.Stores.Markets;

namespace Services.Stores.Markets.IServices
{
    public interface IStoreMarketRegionService
    {
        Task<List<StoreMarketRegion>> GetRegionsByStoreIdAsync(int storeId);
        Task<bool> AddRegionAsync(StoreMarketRegion region);
        Task<bool> RemoveRegionAsync(int regionId, int storeId);
        Task<bool> RegionExistsAsync(int storeId, string country, string province, string district);
    }
}
