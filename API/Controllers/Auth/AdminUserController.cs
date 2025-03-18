using Data.Dtos.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Auths.IServices;
using System;
using System.Threading.Tasks;

namespace API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;
        private readonly ILogger<AdminUserController> _logger;

        public AdminUserController(IAdminUserService adminUserService, ILogger<AdminUserController> logger)
        {
            _adminUserService = adminUserService;
            _logger = logger;
        }

        [HttpPost("login-admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] AdminLoginDto adminLoginDto)
        {
            try
            {
                _logger.LogInformation("Admin giriş işlemi başlatıldı. Email: {Email}", adminLoginDto.EmailOrPhone);

                var result = await _adminUserService.AuthenticateAdminAsync(adminLoginDto.EmailOrPhone, adminLoginDto.Password);

                _logger.LogInformation("Admin giriş başarılı. Email: {Email}", adminLoginDto.EmailOrPhone);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin giriş işlemi başarısız. Email: {Email}", adminLoginDto.EmailOrPhone);
                return Unauthorized(new { Error = "Geçersiz email veya şifre." });
            }
        }

        [HttpPost("register-super-admin")]
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] AdminRegisterDto adminRegisterDto)
        {
            try
            {
                _logger.LogInformation("SuperAdmin kaydı başlatıldı. Email: {Email}", adminRegisterDto.Email);

                var result = await _adminUserService.RegisterSuperAdminAsync(adminRegisterDto);

                _logger.LogInformation("SuperAdmin kaydı başarılı. Email: {Email}", adminRegisterDto.Email);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SuperAdmin kaydı başarısız. Email: {Email}", adminRegisterDto.Email);
                return StatusCode(500, new { Error = "SuperAdmin kaydı sırasında hata oluştu." });
            }
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterDto adminRegisterDto, [FromQuery] string superAdminPassword)
        {
            try
            {
                _logger.LogInformation("Yeni admin kaydı başlatıldı. Email: {Email}", adminRegisterDto.Email);

                var result = await _adminUserService.RegisterAdminAsync(adminRegisterDto, superAdminPassword);

                _logger.LogInformation("Admin kaydı başarılı. Email: {Email}", adminRegisterDto.Email);
                return Ok(new { Message = result });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "SuperAdmin şifresi hatalı. Yeni admin kaydı başarısız. Email: {Email}", adminRegisterDto.Email);
                return Unauthorized(new { Error = "SuperAdmin şifresi hatalı." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Yeni admin kaydı sırasında hata oluştu. Email: {Email}", adminRegisterDto.Email);
                return StatusCode(500, new { Error = "Yeni admin kaydı sırasında hata oluştu." });
            }
        }
    }
}
