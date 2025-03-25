using Data.Dtos.Stores;

namespace Services.Stores.Markets.IServices
{
    public interface IStoreMarketService
    {
        Task<IEnumerable<StoreMarketAddDto>> GetAvailableMarketsForStoreAsync(int storeId);
        Task<string> AddMarketToStoreAsync(int storeId, int marketId);
        Task<string> RemoveMarketFromStoreAsync(int storeId, int marketId);
        Task<string> SetStoreMarketStatusAsync(int storeId, int marketId, bool isActive);
    }
}
