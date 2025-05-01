using Data.Dtos.Stores.Markets;

namespace Services.Stores.Markets.IServices
{
    public interface IStoreMarketCoverageService
    {
        Task<List<int>> AddCompositeCoverageAsync(StoreMarketCoverageCompositeCreateDto dto);

        Task<List<StoreMarketCountryDto>> GetCountrysByStoreIdAsync(int storeId);
        Task<List<StoreMarketProvinceDto>> GetProvincesByStoreIdAsync(int storeId);
        Task<List<StoreMarketDistrictDto>> GetDistrictsByStoreIdAsync(int storeId);
        Task<List<StoreMarketNeighborhoodDto>> GetNeighborhoodsByStoreIdAsync(int storeId);
        Task<List<StoreMarketRegionDto>> GetRegionsByStoreIdAsync(int storeId);
        Task<List<StoreMarketStateDto>> GetStatesByStoreIdAsync(int storeId);

        Task<bool> UpdateCountryAsync(StoreMarketCountryUpdateDto dto);
        Task<bool> UpdateProvinceAsync(StoreMarketProvinceUpdateDto dto);
        Task<bool> UpdateDistrictAsync(StoreMarketDistrictUpdateDto dto);
        Task<bool> UpdateNeighborhoodAsync(StoreMarketNeighborhoodUpdateDto dto);
        Task<bool> UpdateRegionAsync(StoreMarketRegionUpdateDto dto);
        Task<bool> UpdateStateAsync(StoreMarketStateUpdateDto dto);

        Task<int> DeleteCompositeCoverageAsync(StoreMarketCoverageCompositeDeleteDto dto);

    }
}
