using Data.Dtos.Stores.Locations;

namespace Services.Stores.Locations.IServices
{
    public interface IStoreCoverageService
    {
        Task<int> AddCoverageAsync(StoreCoverageCreateDto dto, int storeId);
        Task<bool> DeleteCoverageAsync(StoreCoverageDeleteDto dto, int storeId);
        Task<List<StoreCoverageHierarchyDto>> GetCoverageByStoreIdAsync(int storeId);

    }
}
