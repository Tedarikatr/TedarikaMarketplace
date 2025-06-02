using Data.Dtos.Forms;
using Microsoft.AspNetCore.Mvc;
using Services.Forms.IServices;

namespace API.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    public class BuyerApplicationController : ControllerBase
    {
        private readonly IBuyerApplicationService _buyerApplicationService;
        private readonly ILogger<BuyerApplicationController> _logger;

        public BuyerApplicationController(IBuyerApplicationService buyerApplicationService, ILogger<BuyerApplicationController> logger)
        {
            _buyerApplicationService = buyerApplicationService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BuyerApplicationCreateDto dto)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var result = await _buyerApplicationService.CreateBuyerApplicationAsync(dto, ipAddress);

                if (result)
                {
                    _logger.LogInformation("Tedarik talebi başarıyla oluşturuldu. Ürün: {ProductName}", dto.ProductName);
                    return Ok(new { message = "Tedarik talebiniz başarıyla oluşturuldu." });
                }

                _logger.LogWarning("Tedarik talebi oluşturulamadı. Ürün: {ProductName}", dto.ProductName);
                return BadRequest(new { error = "Tedarik talebi oluşturulamadı." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarik talebi oluşturulurken hata oluştu.");
                return StatusCode(500, "Sunucu hatası.");
            }
        }
    }
}

