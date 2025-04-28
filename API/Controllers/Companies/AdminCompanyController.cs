using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Companys.IServices;

namespace API.Controllers.Companies
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize]
    public class AdminCompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<AdminCompanyController> _logger;

        public AdminCompanyController(ICompanyService companyService, ILogger<AdminCompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllCompanies()
        {
            try
            {
                int adminId = AdminUserContextHelper.GetAdminId(User);

                _logger.LogInformation("Şirket listesi alınıyor...");

                var companies = await _companyService.GetAllCompaniesAsync();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket listesi alınırken hata oluştu.");
                return StatusCode(500, "Sunucu hatası.");
            }
        }

        [HttpPut("verify/{id}")]
        public async Task<IActionResult> VerifyCompany(int id, bool value)
        {
            try
            {
                int adminId = AdminUserContextHelper.GetAdminId(User);
                _logger.LogInformation("Şirket onaylanıyor. ID: {CompanyId}", id);

                var result = await _companyService.VerifyCompanyAsync(id, value);
                return result ? Ok("Şirket onaylandı.") : NotFound("Şirket bulunamadı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket onaylama sırasında hata oluştu. ID: {CompanyId}", id);
                return StatusCode(500, "Sunucu hatası.");
            }
        }

        [HttpPut("toggle-status/{id}")]
        public async Task<IActionResult> ToggleCompanyStatus(int id)
        {
            try
            {
                int adminId = AdminUserContextHelper.GetAdminId(User);
                _logger.LogInformation("Şirket aktif/pasif durumu değiştiriliyor. ID: {CompanyId}", id);

                var result = await _companyService.ToggleCompanyStatusAsync(id);
                return result ? Ok("Şirket durumu değiştirildi.") : NotFound("Şirket bulunamadı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket durumu değiştirilirken hata oluştu. ID: {CompanyId}", id);
                return StatusCode(500, "Sunucu hatası.");
            }
        }
    }
}