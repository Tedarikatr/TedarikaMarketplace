using Data.Dtos.Forms;
using Microsoft.AspNetCore.Mvc;
using Services.Forms.IServices;

namespace API.Controllers.Forms
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    public class AdminSellerApplicationController : ControllerBase
    {
        private readonly ISellerApplicationService _service;
        private readonly ILogger<AdminSellerApplicationController> _logger;

        public AdminSellerApplicationController(
            ISellerApplicationService service,
            ILogger<AdminSellerApplicationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var list = await _service.GetAllAsync();
                return Ok(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı başvuruları listelenirken hata oluştu.");
                return StatusCode(500, "Bir hata oluştu.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            try
            {
                var detail = await _service.GetDetailByIdAsync(id);
                if (detail == null)
                    return NotFound($"Başvuru bulunamadı (Id: {id})");

                return Ok(detail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru detayları alınırken hata oluştu. Id: {Id}", id);
                return StatusCode(500, "Bir hata oluştu.");
            }
        }

        [HttpPut("approve")]
        public async Task<IActionResult> UpdateApproval([FromBody] SellerApplicationUpdateApprovalDto dto)
        {
            try
            {
                var result = await _service.UpdateApprovalAsync(dto);
                if (!result)
                    return NotFound($"Onaylanacak başvuru bulunamadı (Id: {dto.Id})");

                return Ok("Başvuru durumu güncellendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru durumu güncellenirken hata oluştu. Id: {Id}", dto.Id);
                return StatusCode(500, "Bir hata oluştu.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _service.DeleteAsync(id);
                if (!result)
                    return NotFound($"Silinecek başvuru bulunamadı (Id: {id})");

                return Ok("Başvuru silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru silinirken hata oluştu. Id: {Id}", id);
                return StatusCode(500, "Bir hata oluştu.");
            }
        }
    }
}
