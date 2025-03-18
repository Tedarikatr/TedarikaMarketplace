using Data.Dtos.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Auths.IServices;

namespace API.Controllers.Auth
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

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBuyerById(int id)
        {
            try
            {
                _logger.LogInformation("Alıcı bilgisi alınıyor. ID: {UserId}", id);
                var user = await _buyerUserService.GetBuyerUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı bilgisi alınırken hata oluştu. ID: {UserId}", id);
                return StatusCode(500, "Sunucu hatası.");
            }
        }
    }
}
