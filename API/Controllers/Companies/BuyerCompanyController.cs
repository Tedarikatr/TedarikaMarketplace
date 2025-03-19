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
        private readonly ILogger<BuyerCompanyController> _logger;

        public BuyerCompanyController(ICompanyService companyService, ILogger<BuyerCompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterBuyerCompany([FromBody] CompanyCreateDto companyCreateDto)
        {
            try
            {
                var buyerId = BuyerUserContextHelper.GetBuyerId(User);
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
