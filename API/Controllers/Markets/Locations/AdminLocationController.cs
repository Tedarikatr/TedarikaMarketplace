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
    }
}
