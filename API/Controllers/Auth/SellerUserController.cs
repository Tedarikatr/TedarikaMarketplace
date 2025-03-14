using Data.Dtos.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Auths.IServices;

namespace API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    public class SellerUserController : ControllerBase
    {
        private readonly ISellerUserService _sellerUserService;
        private readonly ILogger<SellerUserController> _logger;

        public SellerUserController(ISellerUserService sellerUserService, ILogger<SellerUserController> logger)
        {
            _sellerUserService = sellerUserService;
            _logger = logger;
        }

        [HttpPost("seller-register")]
        public async Task<IActionResult> RegisterSeller([FromBody] SellerRegisterDto sellerRegisterDto)
        {
            try
            {
                _logger.LogInformation("Yeni satıcı kaydı başlatıldı: {Email}", sellerRegisterDto.Email);
                var result = await _sellerUserService.RegisterSellerUserAsync(sellerRegisterDto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı kaydı sırasında hata oluştu: {Email}", sellerRegisterDto.Email);
                return StatusCode(500, "Kayıt sırasında bir hata oluştu.");
            }
        }


        [HttpPost("seller-login")]
        public async Task<IActionResult> SellerLogin([FromBody] SellerLoginDto sellerLoginDto)
        {
            try
            {
                _logger.LogInformation("Satıcı giriş yapıyor: {Email}", sellerLoginDto.Email);
                var token = await _sellerUserService.AuthenticateSellerUserAsync(sellerLoginDto.Email, sellerLoginDto.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı giriş sırasında hata oluştu: {Email}", sellerLoginDto.Email);
                return Unauthorized("Geçersiz giriş.");
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetSellerById(int id)
        {
            try
            {
                _logger.LogInformation("Satıcı bilgisi alınıyor. ID: {UserId}", id);
                var user = await _sellerUserService.GetSellerUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı bilgisi alınırken hata oluştu. ID: {UserId}", id);
                return StatusCode(500, "Sunucu hatası.");
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateSeller(int id, [FromBody] SellerUserDto userDto)
        {
            try
            {
                _logger.LogInformation("Satıcı güncelleniyor. ID: {UserId}", id);
                userDto.Id = id;
                var result = await _sellerUserService.UpdateSellerUserAsync(userDto);
                return result ? Ok("Güncelleme başarılı.") : BadRequest("Güncelleme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı güncellenirken hata oluştu. ID: {UserId}", id);
                return StatusCode(500, "Sunucu hatası.");
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            try
            {
                _logger.LogInformation("Satıcı siliniyor. ID: {UserId}", id);
                var result = await _sellerUserService.DeleteSellerUserAsync(id);
                return result ? Ok("Kullanıcı silindi.") : NotFound("Kullanıcı bulunamadı.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı silinirken hata oluştu. ID: {UserId}", id);
                return StatusCode(500, "Sunucu hatası.");
            }
        }
    }
}
