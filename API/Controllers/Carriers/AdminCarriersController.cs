using API.Helpers;
using Data.Dtos.Carriers;
using Microsoft.AspNetCore.Mvc;
using Services.Carriers.IServices;

namespace API.Controllers.Carriers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    //[Authorize]
    public class AdminCarriersController : ControllerBase
    {
        private readonly ICarrierService _carrierService;
        private readonly AdminUserContextHelper _adminHelper;
        private readonly ILogger<AdminCarriersController> _logger;

        public AdminCarriersController(
            ICarrierService carrierService,
            AdminUserContextHelper adminHelper,
            ILogger<AdminCarriersController> logger)
        {
            _carrierService = carrierService;
            _adminHelper = adminHelper;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var adminId = _adminHelper.GetAdminId(User);
                _logger.LogInformation("AdminId {AdminId} tüm kargo firmalarını listelemek istiyor.", adminId);

                var result = await _carrierService.GetAllCarriersAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm kargo firmaları listelenirken hata oluştu.");
                return StatusCode(500, "Kargo firmaları listelenemedi.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var adminId = _adminHelper.GetAdminId(User);
                _logger.LogInformation("AdminId {AdminId}, CarrierId {CarrierId} detayını istiyor.", adminId, id);

                var result = await _carrierService.GetCarrierByIdAsync(id);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("CarrierId {CarrierId} bulunamadı.", id);
                return NotFound("Kargo firması bulunamadı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması detayları getirilirken hata oluştu. Id: {CarrierId}", id);
                return StatusCode(500, "Kargo firması getirilemedi.");
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CarrierCreateDto dto)
        {
            try
            {
                var adminId = _adminHelper.GetAdminId(User);

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("AdminId {AdminId} tarafından gönderilen model geçersiz.", adminId);
                    return BadRequest(ModelState);
                }

                await _carrierService.CreateCarrierAsync(dto);
                _logger.LogInformation("AdminId {AdminId} yeni kargo firması oluşturdu. Ad: {Name}", adminId, dto.Name);
                return Ok("Kargo firması başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması oluşturulurken hata oluştu.");
                return StatusCode(500, "Kargo firması eklenemedi.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var adminId = _adminHelper.GetAdminId(User);
                var success = await _carrierService.DeleteCarrierAsync(id);

                if (!success)
                {
                    _logger.LogWarning("AdminId {AdminId} geçersiz CarrierId {CarrierId} ile silme denemesi yaptı.", adminId, id);
                    return NotFound("Kargo firması bulunamadı.");
                }

                _logger.LogInformation("AdminId {AdminId}, CarrierId {CarrierId} başarıyla sildi.", adminId, id);
                return Ok("Kargo firması silindi.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması silinirken hata oluştu. Id: {CarrierId}", id);
                return StatusCode(500, "Kargo firması silinemedi.");
            }
        }
    }
}
