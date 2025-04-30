using Data.Dtos.Stores.Markets;

namespace Services.Stores.Markets.IServices
{
    public interface IStoreMarketCoverageService
    {
        Task<int> AddCountryAsync(StoreMarketCountryCreateDto dto);
        Task<int> AddProvinceAsync(StoreMarketProvinceCreateDto dto);
        Task<int> AddDistrictAsync(StoreMarketDistrictCreateDto dto);
        Task<int> AddNeighborhoodAsync(StoreMarketNeighborhoodCreateDto dto);
        Task<int> AddRegionAsync(StoreMarketRegionCreateDto dto);
        Task<int> AddStateAsync(StoreMarketStateCreateDto dto);

        //Multi

        Task<List<int>> AddCountrysMultiAsync(StoreMarketCountryMultiCreateDto dto);
        Task<List<int>> AddProvincesMultiAsync(StoreMarketProvinceMultiCreateDto dto);
        Task<List<int>> AddDistrictsMultiAsync(StoreMarketDistrictMultiCreateDto dto);
        Task<List<int>> AddNeighborhoodsMultiAsync(StoreMarketNeighborhoodMultiCreateDto dto);
        Task<List<int>> AddRegionsMultiAsync(StoreMarketRegionMultiCreateDto dto);
        Task<List<int>> AddStatesMultiAsync(StoreMarketStateMultiCreateDto dto);

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

        Task<bool> DeleteCountryAsync(int id);
        Task<bool> DeleteProvinceAsync(int id);
        Task<bool> DeleteDistrictAsync(int id);
        Task<bool> DeleteNeighborhoodAsync(int id);
        Task<bool> DeleteRegionAsync(int id);
        Task<bool> DeleteStateAsync(int id);
    }
}
