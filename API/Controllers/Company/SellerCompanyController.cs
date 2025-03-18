using API.Helpers;
using Data.Dtos.Companys;
using Entity.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Companys.IServices;

namespace API.Controllers.Company
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize]
    public class SellerCompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly ILogger<SellerCompanyController> _logger;

        public SellerCompanyController(ICompanyService companyService, ILogger<SellerCompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterSellerCompany([FromBody] CompanyCreateDto companyCreateDto)
        {
            try
            {
                var sellerId = SellerUserContextHelper.GetSellerId(User);
                _logger.LogInformation("Yeni satıcı şirket kaydı başlatıldı: {CompanyNumber}", companyCreateDto.CompanyNumber);

                var result = await _companyService.RegisterCompanyAsync(companyCreateDto, sellerId, UserType.Seller);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı şirket kaydı sırasında hata oluştu: {CompanyNumber}", companyCreateDto.CompanyNumber);
                return StatusCode(500, "Kayıt sırasında bir hata oluştu.");
            }
        }

    }
}
