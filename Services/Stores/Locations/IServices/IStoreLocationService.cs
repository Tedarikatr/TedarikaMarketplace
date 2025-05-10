using Data.Dtos.Stores.Markets;

namespace Services.Stores.Markets.IServices
{
    public interface IStoreLocationService
    {
        Task<List<int>> AddCompositeCoverageAsync(StoreMarketCoverageCompositeCreateDto dto);

        Task<StoreMarketCoverageHierarchyDto> GetCoverageHierarchyByStoreIdAsync(int storeId);

        Task<bool> UpdateCoverageAsync(StoreMarketCoverageUpdateBaseDto dto, CoverageType type);

        Task<int> DeleteCompositeCoverageAsync(StoreMarketCoverageCompositeDeleteDto dto);

    }
}
