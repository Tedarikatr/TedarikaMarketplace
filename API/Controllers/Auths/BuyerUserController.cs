using Data.Dtos.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Auths.IServices;
using System.Security.Claims;

namespace API.Controllers.Auths
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "buyer")]
    public class BuyerUserController : ControllerBase
    {
        private readonly IBuyerUserService _buyerUserService;
        private readonly ILogger<BuyerUserController> _logger;

        public BuyerUserController(IBuyerUserService buyerUserService, ILogger<BuyerUserController> logger)
        {
            _buyerUserService = buyerUserService;
            _logger = logger;
        }

        [HttpPost("buyer-register")]
        public async Task<IActionResult> RegisterBuyer([FromBody] BuyerUserCreateDto createUserDto)
        {
            try
            {
                _logger.LogInformation("Yeni alıcı kaydı başlatıldı: {Email}", createUserDto.Email);
                var result = await _buyerUserService.RegisterBuyerUserAsync(createUserDto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı kaydı sırasında hata oluştu: {Email}", createUserDto.Email);
                return StatusCode(500, "Kayıt sırasında bir hata oluştu.");
            }
        }

        [HttpPost("buyer-login")]
        public async Task<IActionResult> BuyerLogin([FromBody] BuyerLoginDto buyerLoginDto)
        {
            try
            {
                _logger.LogInformation("Alıcı giriş yapıyor: {Email}", buyerLoginDto.EmailOrPhone);
                var token = await _buyerUserService.AuthenticateBuyerUserAsync(buyerLoginDto.EmailOrPhone, buyerLoginDto.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı giriş sırasında hata oluştu: {Email}", buyerLoginDto.EmailOrPhone);
                return Unauthorized("Geçersiz giriş.");
            }
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                var buyerId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
                _logger.LogInformation("Alıcı profili alınıyor. BuyerId: {BuyerId}", buyerId);

                var profile = await _buyerUserService.GetBuyerProfileAsync(buyerId);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı profil bilgisi alınırken hata oluştu.");
                return StatusCode(500, "Profil bilgisi alınamadı.");
            }
        }
    }
}
