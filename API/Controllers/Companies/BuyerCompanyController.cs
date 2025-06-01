using API.Helpers;
using Data.Dtos.Companies;
using Entity.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Companys.IServices;

namespace API.Controllers.Companies
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "buyer")]
    [Authorize]
    public class BuyerCompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly BuyerUserContextHelper _buyerUserContextHelper;
        private readonly ILogger<BuyerCompanyController> _logger;

        public BuyerCompanyController(ICompanyService companyService, BuyerUserContextHelper buyerUserContextHelper, ILogger<BuyerCompanyController> logger)
        {
            _companyService = companyService;
            _buyerUserContextHelper = buyerUserContextHelper;
            _logger = logger;
        }

        [HttpGet("my-company")]
        public async Task<IActionResult> GetMyCompany()
        {
            try
            {
                var buyerUserId = _buyerUserContextHelper.GetBuyerId(User);
                var company = await _companyService.GetCompanyByBuyerUserIdAsync(buyerUserId);

                if (company == null)
                {
                    _logger.LogWarning("Şirket bulunamadı. BuyerUserId: {BuyerUserId}", buyerUserId);
                    return NotFound(new { message = "Şirket bilgisi bulunamadı." });
                }

                return Ok(company);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket bilgisi alınırken hata oluştu.");
                return StatusCode(500, new { error = "Şirket bilgisi alınamadı." });
            }
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterBuyerCompany([FromBody] CompanyCreateDto companyCreateDto)
        {
            try
            {
                var buyerId = _buyerUserContextHelper.GetBuyerId(User);
                _logger.LogInformation("Yeni alıcı şirket kaydı başlatıldı: {CompanyNumber}", companyCreateDto.CompanyNumber);

                var result = await _companyService.RegisterCompanyAsync(companyCreateDto, buyerId, UserType.Buyer);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı şirket kaydı sırasında hata oluştu: {CompanyNumber}", companyCreateDto.CompanyNumber);
                return StatusCode(500, "Kayıt sırasında bir hata oluştu.");
            }
        }

    }
}
