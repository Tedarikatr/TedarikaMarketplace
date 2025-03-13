using Data.Dto.Auth;
using Entity.Auth;
using Microsoft.Extensions.Logging;
using Repository.Auth.IRepositorys;
using Services.Auth.Helper;
using Services.Auth.IServices;
using System.Security.Cryptography;
using System.Text;

namespace Services.Auth.Services
{
    public class SellerUserService : ISellerUserService
    {
        private readonly ISellerUserRepository _sellerUserRepository;
        private readonly IJwtService _jwtService;
        private readonly ILogger<SellerUserService> _logger;

        public SellerUserService(ISellerUserRepository sellerUserRepository, IJwtService jwtService, ILogger<SellerUserService> logger)
        {
            _sellerUserRepository = sellerUserRepository;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<string> RegisterSellerUser(SellerRegisterDto sellerRegisterDto)
        {
            _logger.LogInformation("Yeni satıcı kaydı yapılıyor: " + sellerRegisterDto.Email);

            if (await _sellerUserRepository.EmailExistsAsync(sellerRegisterDto.Email))
            {
                throw new Exception("Bu e-posta adresi zaten kayıtlı.");
            }

            var sellerUser = new SellerUser
            {
                Name = sellerRegisterDto.Name,
                LastName = sellerRegisterDto.LastName,
                Email = sellerRegisterDto.Email,
                Phone = sellerRegisterDto.Phone,
                Password = HashPassword(sellerRegisterDto.Password),
                UserNumber = GenerateUserNumber(),
                GuidNumber = Guid.NewGuid(),
                Status = true
            };

            await _sellerUserRepository.AddAsync(sellerUser);

            _logger.LogInformation("Satıcı başarıyla kayıt oldu: " + sellerRegisterDto.Email);
            return "Kayıt başarılı!";
        }

        public async Task<AuthResponseDto> LoginSellerUser(SellerLoginDto sellerLoginDto)
        {
            var sellerUser = await _sellerUserRepository.GetUserByEmailAsync(sellerLoginDto.Email);

            if (sellerUser == null || !VerifyPassword(sellerLoginDto.Password, sellerUser.Password))
            {
                _logger.LogWarning("Geçersiz giriş denemesi: " + sellerLoginDto.Email);
                throw new Exception("Geçersiz e-posta veya şifre.");
            }

            _logger.LogInformation("Satıcı giriş yaptı: " + sellerLoginDto.Email);
            var token = _jwtService.GenerateJwtToken(sellerUser);

            return new AuthResponseDto
            {
                Token = token
            };
        }

        public async Task<SellerUserDto> GetSellerByEmail(string email)
        {
            var sellerUser = await _sellerUserRepository.GetUserByEmailAsync(email);
            if (sellerUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            return new SellerUserDto
            {
                Id = sellerUser.Id,
                Name = sellerUser.Name,
                LastName = sellerUser.LastName,
                Email = sellerUser.Email,
                Phone = sellerUser.Phone,
                UserNumber = sellerUser.UserNumber
            };
        }

        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }

        private string GenerateUserNumber()
        {
            return "SU-" + new Random().Next(100000, 999999);
        }
    }
}
