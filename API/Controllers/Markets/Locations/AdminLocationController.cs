using Data.Dtos.Markets;
using Microsoft.AspNetCore.Mvc;
using Services.Markets.Location;

namespace API.Controllers.Markets.Locations
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLocationController : ControllerBase
    {

        private readonly ILocationService _locationService;

        public AdminLocationController(ILocationService locationService)
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

        [HttpPut("countries/{id}/status")]
        public async Task<IActionResult> ToggleCountryStatus(int id, [FromQuery] bool isActive)
        {
            await _locationService.ToggleCountryStatusAsync(id, isActive);
            return Ok(new { Message = $"Ülkeye bağlı marketler {(isActive ? "aktif" : "pasif")} yapıldı." });
        }

        [HttpPut("provinces/{id}/status")]
        public async Task<IActionResult> ToggleProvinceStatus(int id, [FromQuery] bool isActive)
        {
            await _locationService.ToggleProvinceStatusAsync(id, isActive);
            return Ok(new { Message = $"İle bağlı marketler {(isActive ? "aktif" : "pasif")} yapıldı." });
        }

        [HttpPut("districts/{id}/status")]
        public async Task<IActionResult> ToggleDistrictStatus(int id, [FromQuery] bool isActive)
        {
            await _locationService.ToggleDistrictStatusAsync(id, isActive);
            return Ok(new { Message = $"İlçeye bağlı marketler {(isActive ? "aktif" : "pasif")} yapıldı." });
        }

        [HttpPut("neighborhoods/{id}/status")]
        public async Task<IActionResult> ToggleNeighborhoodStatus(int id, [FromQuery] bool isActive)
        {
            await _locationService.ToggleNeighborhoodStatusAsync(id, isActive);
            return Ok(new { Message = $"Mahalleye bağlı marketler {(isActive ? "aktif" : "pasif")} yapıldı." });
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

    }
}
