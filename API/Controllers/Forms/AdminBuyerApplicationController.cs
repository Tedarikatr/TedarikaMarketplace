using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Forms.IServices;

namespace API.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    public class AdminBuyerApplicationController : ControllerBase
    {
        private readonly IBuyerApplicationService _buyerApplicationService;
        private readonly ILogger<AdminBuyerApplicationController> _logger;

        public AdminBuyerApplicationController(IBuyerApplicationService buyerApplicationService, ILogger<AdminBuyerApplicationController> logger)
        {
            _buyerApplicationService = buyerApplicationService;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _buyerApplicationService.GetAllApplicationsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm başvurular getirilirken hata oluştu.");
                return StatusCode(500, "Sunucu hatası.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _buyerApplicationService.GetByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("ID {id} ile başvuru bulunamadı.", id);
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru detayları alınırken hata oluştu.");
                return StatusCode(500, "Sunucu hatası.");
            }
        }

        [HttpPost("fulfill/{id}")]
        public async Task<IActionResult> MarkAsFulfilled(int id)
        {
            try
            {
                var result = await _buyerApplicationService.MarkAsFulfilledAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Başvuru tamamlanamadı. ID: {id}", id);
                    return BadRequest(new { error = "Tamamlama işlemi başarısız oldu." });
                }

                _logger.LogInformation("Başvuru tamamlandı. ID: {id}", id);
                return Ok(new { message = "Başvuru tamamlandı." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru tamamlanırken hata oluştu.");
                return StatusCode(500, "Sunucu hatası.");
            }
        }
    }
}

