using Data.Dtos.Markets;

namespace Services.Markets.Location
{
    public interface ILocationService
    {
        Task<int> AddCountryAsync(CountryCreateDto dto);
        Task<int> AddProvinceAsync(ProvinceCreateDto dto);
        Task<int> AddDistrictAsync(DistrictCreateDto dto);
        Task<int> AddNeighborhoodAsync(NeighborhoodCreateDto dto);

        Task<bool> ToggleCountryStatusAsync(int countryId, bool isActive);
        Task<bool> ToggleProvinceStatusAsync(int provinceId, bool isActive);
        Task<bool> ToggleDistrictStatusAsync(int districtId, bool isActive);
        Task<bool> ToggleNeighborhoodStatusAsync(int neighborhoodId, bool isActive);

        Task<List<CountryDto>> GetAllCountriesAsync();
        Task<List<ProvinceDto>> GetProvincesByCountryIdAsync(int countryId);
        Task<List<DistrictDto>> GetDistrictsByProvinceIdAsync(int provinceId);
        Task<List<NeighborhoodDto>> GetNeighborhoodsByDistrictIdAsync(int districtId);

        Task<bool> DeleteCountryAsync(int id);
        Task<bool> DeleteProvinceAsync(int id);
        Task<bool> DeleteDistrictAsync(int id);
        Task<bool> DeleteNeighborhoodAsync(int id);

        Task<bool> UpdateCountryAsync(int id, CountryCreateDto dto);
        Task<bool> UpdateProvinceAsync(int id, ProvinceCreateDto dto);
        Task<bool> UpdateDistrictAsync(int id, DistrictCreateDto dto);
        Task<bool> UpdateNeighborhoodAsync(int id, NeighborhoodCreateDto dto);
    }
}
