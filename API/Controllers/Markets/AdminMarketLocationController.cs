using Data.Dtos.Markets;
using Microsoft.AspNetCore.Mvc;
using Services.Markets.IServices;

namespace API.Controllers.Markets
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    //[Authorize]
    public class AdminMarketLocationController : ControllerBase
    {
        private readonly IMarketLocationService _locationService;

        public AdminMarketLocationController(IMarketLocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost("countries")]
        public async Task<IActionResult> AddCountry([FromBody] CountryCreateDto dto)
        {
            var id = await _locationService.AddCountryAsync(dto);
            return Ok(new { Id = id, Message = "Ülke başarıyla eklendi." });
        }

        [HttpPost("provinces")]
        public async Task<IActionResult> AddProvince([FromBody] ProvinceCreateDto dto)
        {
            var id = await _locationService.AddProvinceAsync(dto);
            return Ok(new { Id = id, Message = "İl başarıyla eklendi." });
        }

        [HttpPost("districts")]
        public async Task<IActionResult> AddDistrict([FromBody] DistrictCreateDto dto)
        {
            var id = await _locationService.AddDistrictAsync(dto);
            return Ok(new { Id = id, Message = "İlçe başarıyla eklendi." });
        }

        [HttpPost("neighborhoods")]
        public async Task<IActionResult> AddNeighborhood([FromBody] NeighborhoodCreateDto dto)
        {
            var id = await _locationService.AddNeighborhoodAsync(dto);
            return Ok(new { Id = id, Message = "Mahalle başarıyla eklendi." });
        }

        [HttpPost("state")]
        public async Task<IActionResult> AddState([FromBody] StateCreateDto dto)
        {
            var id = await _locationService.AddStateAsync(dto);
            return Ok(new { StateId = id });
        }

        [HttpPut("toggle-country/{id}")]
        public async Task<IActionResult> ToggleCountry(int id, [FromQuery] bool isActive)
        {
            var result = await _locationService.ToggleCountryStatusAsync(id, isActive);
            return result ? Ok("Ülke durumu güncellendi.") : NotFound("Ülke bulunamadı.");
        }

        [HttpPut("toggle-province/{id}")]
        public async Task<IActionResult> ToggleProvince(int id, [FromQuery] bool isActive)
        {
            var result = await _locationService.ToggleProvinceStatusAsync(id, isActive);
            return result ? Ok("İl durumu güncellendi.") : NotFound("İl bulunamadı.");
        }

        [HttpPut("toggle-district/{id}")]
        public async Task<IActionResult> ToggleDistrict(int id, [FromQuery] bool isActive)
        {
            var result = await _locationService.ToggleDistrictStatusAsync(id, isActive);
            return result ? Ok("İlçe durumu güncellendi.") : NotFound("İlçe bulunamadı.");
        }

        [HttpPut("toggle-neighborhood/{id}")]
        public async Task<IActionResult> ToggleNeighborhood(int id, [FromQuery] bool isActive)
        {
            var result = await _locationService.ToggleNeighborhoodStatusAsync(id, isActive);
            return result ? Ok("Mahalle durumu güncellendi.") : NotFound("Mahalle bulunamadı.");
        }

        [HttpPost("state/{id}/toggle")]
        public async Task<IActionResult> ToggleStateStatus(int id, [FromQuery] bool isActive)
        {
            var result = await _locationService.ToggleStateStatusAsync(id, isActive);
            return result ? Ok() : BadRequest();
        }

        // Listeleme
        [HttpGet("countries")]
        public async Task<IActionResult> GetCountries() => Ok(await _locationService.GetAllCountriesAsync());

        [HttpGet("provinces/{countryId}")]
        public async Task<IActionResult> GetProvinces(int countryId) => Ok(await _locationService.GetProvincesByCountryIdAsync(countryId));

        [HttpGet("districts/{provinceId}")]
        public async Task<IActionResult> GetDistricts(int provinceId) => Ok(await _locationService.GetDistrictsByProvinceIdAsync(provinceId));

        [HttpGet("neighborhoods/{districtId}")]
        public async Task<IActionResult> GetNeighborhoods(int districtId) => Ok(await _locationService.GetNeighborhoodsByDistrictIdAsync(districtId));

        [HttpGet("country/{countryId}/states")]
        public async Task<IActionResult> GetStatesByCountry(int countryId)
        {
            var states = await _locationService.GetStatesByCountryIdAsync(countryId);
            return Ok(states);
        }

        // Silme
        [HttpDelete("countries/{id}")]
        public async Task<IActionResult> DeleteCountry(int id) =>
            await _locationService.DeleteCountryAsync(id) ? Ok("Ülke silindi.") : NotFound("Ülke bulunamadı.");

        [HttpDelete("provinces/{id}")]
        public async Task<IActionResult> DeleteProvince(int id) =>
            await _locationService.DeleteProvinceAsync(id) ? Ok("İl silindi.") : NotFound("İl bulunamadı.");

        [HttpDelete("districts/{id}")]
        public async Task<IActionResult> DeleteDistrict(int id) =>
            await _locationService.DeleteDistrictAsync(id) ? Ok("İlçe silindi.") : NotFound("İlçe bulunamadı.");

        [HttpDelete("neighborhoods/{id}")]
        public async Task<IActionResult> DeleteNeighborhood(int id) =>
            await _locationService.DeleteNeighborhoodAsync(id) ? Ok("Mahalle silindi.") : NotFound("Mahalle bulunamadı.");

        [HttpDelete("state/{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            var success = await _locationService.DeleteStateAsync(id);
            return success ? Ok() : NotFound();
        }

        // Güncelleme
        [HttpPut("countries/{id}")]
        public async Task<IActionResult> UpdateCountry(int id, [FromBody] CountryCreateDto dto) =>
            await _locationService.UpdateCountryAsync(id, dto) ? Ok("Ülke güncellendi.") : NotFound("Ülke bulunamadı.");

        [HttpPut("provinces/{id}")]
        public async Task<IActionResult> UpdateProvince(int id, [FromBody] ProvinceCreateDto dto) =>
            await _locationService.UpdateProvinceAsync(id, dto) ? Ok("İl güncellendi.") : NotFound("İl bulunamadı.");

        [HttpPut("districts/{id}")]
        public async Task<IActionResult> UpdateDistrict(int id, [FromBody] DistrictCreateDto dto) =>
            await _locationService.UpdateDistrictAsync(id, dto) ? Ok("İlçe güncellendi.") : NotFound("İlçe bulunamadı.");

        [HttpPut("neighborhoods/{id}")]
        public async Task<IActionResult> UpdateNeighborhood(int id, [FromBody] NeighborhoodCreateDto dto) =>
            await _locationService.UpdateNeighborhoodAsync(id, dto) ? Ok("Mahalle güncellendi.") : NotFound("Mahalle bulunamadı.");

        [HttpPut("state/{id}")]
        public async Task<IActionResult> UpdateState(int id, [FromBody] StateCreateDto dto)
        {
            var success = await _locationService.UpdateStateAsync(id, dto);
            return success ? Ok() : NotFound();
        }
    }
}
