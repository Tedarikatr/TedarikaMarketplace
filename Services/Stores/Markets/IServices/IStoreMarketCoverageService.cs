using Data.Dtos.Stores;

namespace Services.Stores.Markets.IServices
{
    public interface IStoreMarketCoverageService
    {
        Task<int> AddCoverageAsync(StoreMarketCoverageCreateDto dto);
        Task<bool> RemoveCoverageAsync(int id);
        Task<bool> UpdateCoverageStatusAsync(int id, bool isActive);
        Task<List<StoreMarketCoverageDto>> GetStoreCoveragesAsync(int storeMarketId, bool? onlyActive = null);
        Task<bool> CoverageExistsAsync(StoreMarketCoverageCreateDto dto);
    }
}
