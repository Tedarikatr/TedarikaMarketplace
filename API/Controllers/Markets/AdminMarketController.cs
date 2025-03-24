using Data.Dtos.Markets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Markets.IServices;

namespace API.Controllers.Markets
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    //[Authorize(Roles = "SuperAdmin")]
    public class AdminMarketController : ControllerBase
    {
        private readonly IMarketService _marketService;
        private readonly ILogger<AdminMarketController> _logger;

        public AdminMarketController(IMarketService marketService, ILogger<AdminMarketController> logger)
        {
            _marketService = marketService;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllMarkets()
        {
            try
            {
                _logger.LogInformation("Tüm marketler listeleniyor.");
                var markets = await _marketService.GetAllMarketsAsync();
                return Ok(markets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm marketleri listelerken hata oluştu.");
                return StatusCode(500, new { Error = "Market listesi yüklenirken bir hata oluştu." });
            }
        }

        [HttpGet("{marketId}")]
        public async Task<IActionResult> GetMarketById(int marketId)
        {
            try
            {
                _logger.LogInformation("Market bilgisi alınıyor. Market ID: {MarketId}", marketId);
                var market = await _marketService.GetMarketByIdAsync(marketId);
                return Ok(market);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Market bilgisi alınırken hata oluştu. Market ID: {MarketId}", marketId);
                return StatusCode(500, new { Error = "Market bilgisi alınırken bir hata oluştu." });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMarket([FromBody] MarketCreateDto marketDto)
        {
            try
            {
                _logger.LogInformation("Yeni market oluşturma işlemi başlatıldı.");
                var result = await _marketService.CreateMarketAsync(marketDto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Market oluşturma sırasında hata oluştu.");
                return StatusCode(500, new { Error = "Market oluşturulurken bir hata oluştu." });
            }
        }

        [HttpPut("update/{marketId}")]
        public async Task<IActionResult> UpdateMarket(int marketId, [FromBody] MarketUpdateDto marketDto)
        {
            try
            {
                _logger.LogInformation("Market güncelleme işlemi başlatıldı. Market ID: {MarketId}", marketId);
                var result = await _marketService.UpdateMarketAsync(marketId, marketDto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Market güncelleme sırasında hata oluştu. Market ID: {MarketId}", marketId);
                return StatusCode(500, new { Error = "Market güncellenirken bir hata oluştu." });
            }
        }

        [HttpPatch("status/{marketId}")]
        public async Task<IActionResult> SetMarketStatus(int marketId, [FromQuery] bool isActive)
        {
            try
            {
                _logger.LogInformation("Market durumu değiştiriliyor. Market ID: {MarketId}, Yeni Durum: {IsActive}", marketId, isActive);
                var result = await _marketService.SetMarketStatusAsync(marketId, isActive);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Market durumu değiştirilirken hata oluştu. Market ID: {MarketId}", marketId);
                return StatusCode(500, new { Error = "Market durumu değiştirilemedi." });
            }
        }
    }
}
