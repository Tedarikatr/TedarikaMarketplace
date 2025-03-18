using AutoMapper;
using Data.Dtos.Auths;
using Entity.Auths;
using Microsoft.Extensions.Logging;
using Repository.Auths.IRepositorys;
using Services.Auths.Helper;
using Services.Auths.IServices;
using System;
using System.Threading.Tasks;
using BCrypt.Net;

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
            try
            {
                _logger.LogInformation("SuperAdmin kaydı başlatıldı: {Email}", adminRegisterDto.Email);

                if (await _adminUserRepository.IsSuperAdminExistsAsync())
                {
                    _logger.LogWarning("SuperAdmin zaten kayıtlı: {Email}", adminRegisterDto.Email);
                    throw new Exception("Super Admin zaten mevcut.");
                }

                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(adminRegisterDto.Password, salt);

                var superAdmin = new AdminUser
                {
                    Name = adminRegisterDto.Name,
                    LastName = adminRegisterDto.LastName,
                    Email = adminRegisterDto.Email,
                    Phone = adminRegisterDto.Phone,
                    PasswordHash = hashedPassword,
                    PasswordSalt = salt,
                    IsSuperAdmin = true,
                    UserType = UserType.Admin,
                    Role = AdminRole.SuperAdmin
                };

                await _adminUserRepository.AddAsync(superAdmin);

                _logger.LogInformation("SuperAdmin başarıyla oluşturuldu: {Email}", adminRegisterDto.Email);
                return "Super Admin başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SuperAdmin kaydı sırasında hata oluştu: {Email}", adminRegisterDto.Email);
                throw;
            }
        }

        public async Task<string> RegisterAdminAsync(AdminRegisterDto adminRegisterDto, string superAdminPassword)
        {
            try
            {
                _logger.LogInformation("Admin kaydı başlatıldı: {Email}", adminRegisterDto.Email);

                var superAdmin = await _adminUserRepository.GetSuperAdminAsync();
                if (superAdmin == null || !BCrypt.Net.BCrypt.Verify(superAdminPassword, superAdmin.PasswordHash))
                {
                    _logger.LogWarning("SuperAdmin doğrulaması başarısız: {Email}", adminRegisterDto.Email);
                    throw new Exception("SuperAdmin doğrulaması başarısız.");
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
                    UserType = UserType.Admin,
                    Role = AdminRole.StandardAdmin
                };

                await _adminUserRepository.AddAsync(admin);

                _logger.LogInformation("Admin başarıyla oluşturuldu: {Email}", adminRegisterDto.Email);
                return "Admin başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin kaydı sırasında hata oluştu: {Email}", adminRegisterDto.Email);
                throw;
            }
        }

        public async Task<AuthResponseDto> AuthenticateAdminAsync(string email, string password)
        {
            try
            {
                _logger.LogInformation("Admin girişi başlatıldı: {Email}", email);

                var admin = await _adminUserRepository.GetAdminByEmailAsync(email);

                if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
                {
                    _logger.LogWarning("Geçersiz giriş denemesi: {Email}", email);
                    throw new Exception("Geçersiz e-posta veya şifre.");
                }

                var token = _jwtService.GenerateAdminToken(admin);

                _logger.LogInformation("Admin girişi başarılı: {Email}", email);

                return new AuthResponseDto
                {
                    Token = token,
                    Email = admin.Email,
                    Role = UserType.Admin
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin giriş sırasında hata oluştu: {Email}", email);
                throw;
            }
        }
    }
}
