using Data.Dtos.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Auths.IServices;

namespace API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }

        [HttpPost("register-super-admin")]
        public async Task<IActionResult> RegisterSuperAdmin([FromBody] AdminRegisterDto adminRegisterDto)
        {
            var result = await _adminUserService.RegisterSuperAdminAsync(adminRegisterDto);
            return Ok(new { Message = result });
        }

        [HttpPost("register-admin")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterDto adminRegisterDto, [FromQuery] string superAdminPassword)
        {
            var result = await _adminUserService.RegisterAdminAsync(adminRegisterDto, superAdminPassword);
            return Ok(new { Message = result });
        }
    }
}
