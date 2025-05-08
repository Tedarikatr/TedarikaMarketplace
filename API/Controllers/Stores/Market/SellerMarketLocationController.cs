using Microsoft.AspNetCore.Mvc;
using Services.Locations.IServices;

namespace API.Controllers.Stores.Market
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    public class SellerMarketLocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public SellerMarketLocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }


        [HttpGet("region")]
        public async Task<IActionResult> GetRegion()
        {
            try
            {
                var countries = await _locationService.GetRegions();
                if (countries == null || !countries.Any())
                    return NotFound(new { Message = "Bu bölgeye ait ülke bulunamadı." });

                return Ok(countries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Ülkeler getirilirken hata oluştu.", Details = ex.Message });
            }
        }

        [HttpGet("countries-by-region/{regionId}")]
        public async Task<IActionResult> GetCountriesByRegion(int regionId)
        {
            try
            {
                var countries = await _locationService.GetCountriesByRegionIdAsync(regionId);
                if (countries == null || !countries.Any())
                    return NotFound(new { Message = "Bu bölgeye ait ülke bulunamadı." });

                return Ok(countries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "Ülkeler getirilirken hata oluştu.", Details = ex.Message });
            }
        }


        [HttpGet("provinces/{countryId}")]
        public async Task<IActionResult> GetProvinces(int countryId)
        {
            var provinces = await _locationService.GetProvincesByCountryIdAsync(countryId);
            return Ok(provinces);
        }

        [HttpGet("districts/{provinceId}")]
        public async Task<IActionResult> GetDistricts(int provinceId)
        {
            var districts = await _locationService.GetDistrictsByProvinceIdAsync(provinceId);
            return Ok(districts);
        }

        [HttpGet("neighborhoods/{districtId}")]
        public async Task<IActionResult> GetNeighborhoods(int districtId)
        {
            var neighborhoods = await _locationService.GetNeighborhoodsByDistrictIdAsync(districtId);
            return Ok(neighborhoods);
        }

        [HttpGet("states/{countryId}")]
        public async Task<IActionResult> GetStates(int countryId)
        {
            var states = await _locationService.GetStatesByCountryIdAsync(countryId);
            return Ok(states);
        }
    }
}
