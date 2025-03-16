using AutoMapper;
using Data.Dtos.Auths;
using Entity.Auths;
using Microsoft.Extensions.Logging;
using Repository.Auths.IRepositorys;
using Services.Auths.Helper;
using Services.Auths.IServices;

namespace Services.Auths.Services
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminUserService> _logger;

        public AdminUserService(
            IAdminUserRepository adminUserRepository,
            IJwtService jwtService,
            IMapper mapper,
            ILogger<AdminUserService> logger)
        {
            _adminUserRepository = adminUserRepository;
            _jwtService = jwtService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> RegisterSuperAdminAsync(AdminRegisterDto adminRegisterDto)
        {
            if (await _adminUserRepository.IsSuperAdminExistsAsync())
            {
                throw new Exception("Super Admin zaten kayıtlı.");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminRegisterDto.Password, salt);

            var admin = new AdminUser
            {
                Name = adminRegisterDto.Name,
                LastName = adminRegisterDto.LastName,
                Email = adminRegisterDto.Email,
                Phone = adminRegisterDto.Phone,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                IsSuperAdmin = true,
                Role = AdminRole.SuperAdmin
            };

            await _adminUserRepository.AddAsync(admin);
            return "Super Admin başarıyla oluşturuldu.";
        }

        public async Task<string> RegisterAdminAsync(AdminRegisterDto adminRegisterDto, string superAdminPassword)
        {
            var superAdmin = await _adminUserRepository.GetAdminByEmailAsync(adminRegisterDto.SuperAdminEmail);

            if (superAdmin == null || !BCrypt.Net.BCrypt.Verify(superAdminPassword, superAdmin.PasswordHash))
            {
                throw new Exception("Super Admin doğrulaması başarısız.");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminRegisterDto.Password, salt);

            var admin = new AdminUser
            {
                Name = adminRegisterDto.Name,
                LastName = adminRegisterDto.LastName,
                Email = adminRegisterDto.Email,
                Phone = adminRegisterDto.Phone,
                PasswordHash = hashedPassword,
                PasswordSalt = salt,
                IsSuperAdmin = false,
                Role = AdminRole.StandardAdmin
            };

            await _adminUserRepository.AddAsync(admin);
            return "Admin başarıyla oluşturuldu.";
        }

        public async Task<AuthResponseDto> AuthenticateAdminAsync(string email, string password)
        {
            var admin = await _adminUserRepository.GetAdminByEmailAsync(email);

            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                throw new Exception("Geçersiz e-posta veya şifre.");
            }

            var token = _jwtService.GenerateAdminToken(admin);

            return new AuthResponseDto
            {
                Token = token,
                Email = admin.Email
            };
        }
    }
}