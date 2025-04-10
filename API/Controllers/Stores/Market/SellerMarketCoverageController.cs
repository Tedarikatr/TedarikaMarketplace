using API.Helpers;
using Data.Dtos.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Stores.Markets.IServices;

namespace API.Controllers.Stores.Market
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize]
    public class SellerMarketCoverageController : ControllerBase
    {
        private readonly IStoreMarketCoverageService _coverageService;
        private readonly ILogger<SellerMarketCoverageController> _logger;

        public SellerMarketCoverageController(
            IStoreMarketCoverageService coverageService,
            ILogger<SellerMarketCoverageController> logger)
        {
            _coverageService = coverageService;
            _logger = logger;
        }

        [HttpPost("add-coverage")]
        public async Task<IActionResult> AddCoverage([FromBody] StoreMarketCoverageCreateDto dto)
        {
            try
            {
                var sellerId = SellerUserContextHelper.GetSellerId(User);

                var coverageId = await _coverageService.AddCoverageAsync(dto);
                return Ok(new { Message = "Hizmet bölgesi başarıyla eklendi.", CoverageId = coverageId });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Kapsama tekrar eklenmeye çalışıldı.");
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hizmet bölgesi eklenirken hata oluştu.");
                return StatusCode(500, new { Error = "Hizmet bölgesi eklenemedi." });
            }
        }

        [HttpDelete("remove-coverage/{id}")]
        public async Task<IActionResult> RemoveCoverage(int id)
        {
            try
            {
                var result = await _coverageService.RemoveCoverageAsync(id);
                if (!result) return NotFound(new { Error = "Kapsam bilgisi bulunamadı." });

                return Ok(new { Message = "Hizmet bölgesi başarıyla silindi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hizmet bölgesi silinirken hata oluştu.");
                return StatusCode(500, new { Error = "Silme işlemi başarısız." });
            }
        }

        [HttpPut("toggle-coverage-status/{id}")]
        public async Task<IActionResult> ToggleCoverageStatus(int id, [FromQuery] bool isActive)
        {
            try
            {
                var result = await _coverageService.UpdateCoverageStatusAsync(id, isActive);
                if (!result) return NotFound(new { Error = "Kapsam bilgisi bulunamadı." });

                return Ok(new { Message = "Hizmet bölgesi durumu güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Durum güncelleme sırasında hata oluştu.");
                return StatusCode(500, new { Error = "Güncelleme işlemi başarısız." });
            }
        }

        [HttpGet("store-coverages/{storeMarketId}")]
        public async Task<IActionResult> GetStoreCoverages(int storeMarketId, [FromQuery] bool? onlyActive = null)
        {
            try
            {
                var result = await _coverageService.GetStoreCoveragesAsync(storeMarketId, onlyActive);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam bilgileri alınırken hata oluştu.");
                return StatusCode(500, new { Error = "Listeleme işlemi başarısız." });
            }
        }
    }
}