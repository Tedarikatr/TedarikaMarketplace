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
        private readonly SellerUserContextHelper _userHelper;
        private readonly ILogger<SellerMarketCoverageController> _logger;

        public SellerMarketCoverageController(IStoreMarketCoverageService coverageService, SellerUserContextHelper userHelper, ILogger<SellerMarketCoverageController> logger)
        {
            _coverageService = coverageService;
            _userHelper = userHelper;
            _logger = logger;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddCoverage([FromBody] StoreMarketCoverageCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                await _coverageService.AddCoverageAsync(dto, storeId);
                _logger.LogInformation("Kapsam eklendi. StoreId: {StoreId}", storeId);
                return Ok(new { message = "Kapsam başarıyla eklendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam ekleme hatası.");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("add-bulk")]
        public async Task<IActionResult> AddCoveragesBulk([FromBody] List<StoreMarketCoverageCreateDto> dtos)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var batchDto = new StoreMarketCoverageBatchDto
                {
                    StoreId = storeId,
                    Coverages = dtos
                };

                await _coverageService.AddCoveragesBulkAsync(batchDto);
                _logger.LogInformation("Toplu kapsam eklendi. Adet: {Count}", dtos.Count);
                return Ok(new { message = "Toplu kapsam başarıyla eklendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu kapsam ekleme hatası.");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("add-multi")]
        public async Task<IActionResult> AddCoverageMulti([FromBody] StoreMarketCoverageMultiCreateDto dto)
        {
            try
            {
                dto.StoreId = await _userHelper.GetStoreId(User);
                await _coverageService.AddCoverageMultiAsync(dto);
                _logger.LogInformation("Multi DTO ile kapsam eklendi. StoreId: {StoreId}", dto.StoreId);
                return Ok(new { message = "Kapsamlar başarıyla eklendi (multi)." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Multi DTO ekleme hatası.");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCoverage([FromBody] StoreMarketCoverageUpdateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                await _coverageService.UpdateCoverageAsync(dto, storeId);
                _logger.LogInformation("Kapsam güncellendi. ID: {CoverageId}", dto.Id);
                return Ok(new { message = "Kapsam başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam güncelleme hatası.");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("detail/{coverageId}")]
        public async Task<IActionResult> GetCoverageDetail(int coverageId)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var coverages = await _coverageService.GetMyCoveragesAsync(storeId);
                var detail = coverages.FirstOrDefault(x => x.Id == coverageId);

                if (detail == null)
                    return NotFound(new { error = "Kapsam kaydı bulunamadı." });

                return Ok(detail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam detayı getirilirken hata oluştu.");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("my-coverages")]
        public async Task<IActionResult> GetMyCoverages()
        {
            try
            {
                int sellerUserId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

                var coverages = await _coverageService.GetSellerOwnCoveragesAsync(sellerUserId);
                return Ok(coverages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsamlar alınırken hata oluştu.");
                return StatusCode(500, "Kapsamlar alınamadı.");
            }
        }
    }
}