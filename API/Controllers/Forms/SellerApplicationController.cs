using Data.Dtos.Forms;
using Microsoft.AspNetCore.Mvc;
using Services.Forms.IServices;

namespace API.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    public class SellerApplicationController : ControllerBase
    {
        private readonly ISellerApplicationService _service;
        private readonly ILogger<SellerApplicationController> _logger;

        public SellerApplicationController(
            ISellerApplicationService service,
            ILogger<SellerApplicationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SellerApplicationCreateDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var id = await _service.CreateAsync(dto);
                return Ok(new { Message = "Başvurunuz başarıyla alınmıştır.", Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı başvurusu oluşturulurken hata.");
                return StatusCode(500, "Başvuru oluşturulurken bir hata oluştu.");
            }
        }

        [HttpGet("{guid}")]
        public async Task<IActionResult> GetDetail(Guid guid)
        {
            try
            {
                var detail = await _service.GetByGuidAsync(guid);

                if (detail == null)
                {
                    return NotFound("Başvuru bulunamadı. GUID geçersiz olabilir.");
                }

                return Ok(detail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru detayları getirilirken hata. Guid: {Guid}", guid);
                return StatusCode(500, "Başvuru bilgileri alınırken bir hata oluştu.");
            }
        }
    }
}
