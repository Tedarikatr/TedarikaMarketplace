using Data.Dtos.Stores.Markets;

namespace Services.Stores.Markets.IServices
{
    public interface IStoreMarketCoverageService
    {
        Task<List<int>> AddCompositeCoverageAsync(StoreMarketCoverageCompositeCreateDto dto);

        Task<StoreMarketCoverageHierarchyDto> GetCoverageHierarchyByStoreIdAsync(int storeId);

        Task<bool> UpdateCountryAsync(StoreMarketCountryUpdateDto dto);
        Task<bool> UpdateProvinceAsync(StoreMarketProvinceUpdateDto dto);
        Task<bool> UpdateDistrictAsync(StoreMarketDistrictUpdateDto dto);
        Task<bool> UpdateNeighborhoodAsync(StoreMarketNeighborhoodUpdateDto dto);
        Task<bool> UpdateRegionAsync(StoreMarketRegionUpdateDto dto);
        Task<bool> UpdateStateAsync(StoreMarketStateUpdateDto dto);

        Task<int> DeleteCompositeCoverageAsync(StoreMarketCoverageCompositeDeleteDto dto);

    }
}
