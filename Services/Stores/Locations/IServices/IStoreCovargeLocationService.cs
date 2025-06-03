using Data.Dtos.Stores.Locations;

namespace Services.Stores.Locations.IServices
{
    public interface IStoreCovargeLocationService
    {
        Task<List<int>> AddCompositeCoverageAsync(StoreMarketCoverageCompositeCreateDto dto);

        Task<StoreMarketCoverageHierarchyDto> GetCoverageHierarchyByStoreIdAsync(int storeId);

        Task<int> DeleteCompositeCoverageAsync(StoreMarketCoverageCompositeDeleteDto dto);

    }
}
