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
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize]
    public class SellerCompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly SellerUserContextHelper _userHelper;
        private readonly ILogger<SellerCompanyController> _logger;

        public SellerCompanyController(ICompanyService companyService, SellerUserContextHelper userHelper, ILogger<SellerCompanyController> logger)
        {
            _companyService = companyService;
            _userHelper = userHelper;
            _logger = logger;
        }

        [HttpGet("my-company")]
        public async Task<IActionResult> GetMyCompany()
        {
            try
            {
                var sellerUserId = _userHelper.GetSellerId(User);
                var company = await _companyService.GetCompanyBySellerUserIdAsync(sellerUserId);

                if (company == null)
                {
                    _logger.LogWarning("Şirket bulunamadı. SellerUserId: {SellerUserId}", sellerUserId);
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
        public async Task<IActionResult> RegisterSellerCompany([FromBody] CompanyCreateDto companyCreateDto)
        {
            try
            {
                var sellerId = _userHelper.GetSellerId(User);
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

        [HttpPut("update-my-company")]
        public async Task<IActionResult> UpdateMyCompany([FromBody] CompanyUpdateDto companyUpdateDto)
        {
            try
            {
                var sellerUserId = _userHelper.GetSellerId(User);
                var company = await _companyService.GetCompanyBySellerUserIdAsync(sellerUserId);

                if (company == null)
                {
                    _logger.LogWarning("Güncellenecek şirket bulunamadı. SellerUserId: {SellerUserId}", sellerUserId);
                    return NotFound(new { message = "Şirket bilgisi bulunamadı." });
                }

                companyUpdateDto.Id = company.Id;
                var result = await _companyService.UpdateCompanyAsync(companyUpdateDto);

                if (!result)
                {
                    return StatusCode(500, new { error = "Şirket güncellemesi başarısız oldu." });
                }

                return Ok(new { message = "Şirket bilgisi başarıyla güncellendi." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Şirket bilgisi güncellenirken hata oluştu.");
                return StatusCode(500, new { error = "Şirket bilgisi güncellenemedi." });
            }
        }

        [HttpGet("has-company")]
        public async Task<IActionResult> HasCompany()
        {
            try
            {
                var sellerUserId = _userHelper.GetSellerId(User);

                var company = await _companyService.GetCompanyBySellerUserIdAsync(sellerUserId);

                bool hasCompany = company != null;

                return Ok(new { hasCompany });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcının şirketi olup olmadığı kontrol edilirken hata oluştu: {SellerId}", _userHelper.GetSellerId(User));
                return StatusCode(500, new { error = "Şirket kontrolü sırasında hata oluştu." });
            }
        }
    }
}
