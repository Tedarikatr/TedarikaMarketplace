using Data.Dtos.Stores.Markets;

namespace Services.Stores.Markets.IServices
{
    public interface IStoreMarketCoverageService
    {
        Task<int> AddCoverageAsync(StoreMarketCoverageCreateDto dto);
        Task<bool> RemoveCoverageAsync(int id);
        Task<bool> UpdateCoverageStatusAsync(int id, bool isActive);
        Task<List<StoreMarketCoverageDto>> GetStoreCoveragesByStoreIdAsync(int storeId);
        Task<bool> CoverageExistsAsync(StoreMarketCoverageCreateDto dto);
    }
}
