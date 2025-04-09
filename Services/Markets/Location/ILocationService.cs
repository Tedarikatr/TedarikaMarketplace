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
    }
}
