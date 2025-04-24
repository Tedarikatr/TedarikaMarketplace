using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Markets.Location;

namespace API.Controllers.Stores.Market
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    public class SellerLocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public SellerLocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries()
        {
            var countries = await _locationService.GetAllCountriesAsync();
            return Ok(countries);
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