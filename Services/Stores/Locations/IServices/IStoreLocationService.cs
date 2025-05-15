using Data.Dtos.Stores.Locations;

namespace Services.Stores.Locations.IServices
{
    public interface IStoreLocationService
    {
        Task<List<int>> AddCompositeCoverageAsync(StoreMarketCoverageCompositeCreateDto dto);

        Task<StoreMarketCoverageHierarchyDto> GetCoverageHierarchyByStoreIdAsync(int storeId);

        Task<bool> UpdateCoverageAsync(StoreMarketCoverageUpdateBaseDto dto, CoverageType type);

        Task<int> DeleteCompositeCoverageAsync(StoreMarketCoverageCompositeDeleteDto dto);

    }
}
