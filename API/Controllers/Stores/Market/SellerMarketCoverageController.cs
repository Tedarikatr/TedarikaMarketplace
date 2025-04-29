using API.Helpers;
using Data.Dtos.Stores.Markets;
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

        [HttpPost("add")]
        public async Task<IActionResult> AddCoverage([FromBody] StoreMarketCoverageCreateDto dto)
        {
            try
            {
                var sellerStoreId = SellerUserContextHelper.GetStoreId(User);
                dto.StoreMarketId = sellerStoreId;

                var coverageId = await _coverageService.AddCoverageAsync(dto);
                _logger.LogInformation("Satıcı hizmet bölgesi eklendi. StoreMarketId: {StoreMarketId}, CoverageId: {CoverageId}", sellerStoreId, coverageId);
                return Ok(new { message = "Hizmet bölgesi başarıyla eklendi.", id = coverageId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hizmet bölgesi eklenirken hata oluştu.");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetMyCoverages([FromQuery] bool? onlyActive = null)
        {
            try
            {
                var storeMarketId = SellerUserContextHelper.GetStoreId(User);
                var coverages = await _coverageService.GetStoreCoveragesAsync(storeMarketId, onlyActive);
                return Ok(coverages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hizmet bölgeleri listelenemedi.");
                return StatusCode(500, new { error = "Hizmet bölgeleri alınırken hata oluştu." });
            }
        }

        [HttpDelete("{coverageId}")]
        public async Task<IActionResult> DeleteCoverage(int coverageId)
        {
            try
            {
                var success = await _coverageService.RemoveCoverageAsync(coverageId);
                if (!success) return NotFound("Hizmet bölgesi bulunamadı.");
                return Ok(new { message = "Hizmet bölgesi silindi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hizmet bölgesi silinirken hata oluştu.");
                return StatusCode(500, new { error = "Silme sırasında bir hata oluştu." });
            }
        }

        [HttpPatch("{coverageId}/status")]
        public async Task<IActionResult> ToggleCoverageStatus(int coverageId, [FromQuery] bool isActive)
        {
            try
            {
                var success = await _coverageService.UpdateCoverageStatusAsync(coverageId, isActive);
                if (!success) return NotFound("Hizmet bölgesi bulunamadı.");
                return Ok(new { message = "Hizmet bölgesi durumu güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hizmet bölgesi durumu güncellenemedi.");
                return StatusCode(500, new { error = "Durum güncelleme sırasında bir hata oluştu." });
            }
        }
    }
}