using AutoMapper;
using Data.Dtos.Auths;
using Entity.Auths;
using Microsoft.Extensions.Logging;
using Repository.Auths.IRepositorys;
using Services.Auths.Helper;
using Services.Auths.IServices;
using System;

namespace Services.Auths.Services
{
    public class BuyerUserService : IBuyerUserService
    {
        private readonly IBuyerUserRepository _buyerUserRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly ILogger<BuyerUserService> _logger;

        public BuyerUserService(IBuyerUserRepository buyerUserRepository, IMapper mapper, IJwtService jwtService, ILogger<BuyerUserService> logger)
        {
            _buyerUserRepository = buyerUserRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<string> RegisterBuyerUserAsync(BuyerUserCreateDto createUserDto)
        {
            try
            {
                _logger.LogInformation("Yeni alıcı kaydı yapılıyor: {Email}", createUserDto.Email);

                var existingUser = await _buyerUserRepository.FindAsync(u => u.Email == createUserDto.Email || u.Phone == createUserDto.Phone);
                if (existingUser.Any())
                {
                    throw new Exception("Bu e-posta veya telefon numarası zaten kayıtlı.");
                }

                var buyerUser = _mapper.Map<BuyerUser>(createUserDto);
                buyerUser.Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);
                buyerUser.UserGuidNumber = Guid.NewGuid();
                buyerUser.Status = true;
                buyerUser.UserType = UserType.Buyer;

                await _buyerUserRepository.AddAsync(buyerUser);
                _logger.LogInformation("Alıcı başarıyla kayıt oldu: {Email}", createUserDto.Email);

                return "Kayıt başarılı!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı kaydı sırasında hata oluştu: {Email}", createUserDto.Email);
                throw;
            }
        }

        public async Task<AuthResponseDto> AuthenticateBuyerUserAsync(string emailOrPhone, string password)
        {
            try
            {
                var buyerUser = await _buyerUserRepository.SingleOrDefaultAsync(u => u.Email == emailOrPhone || u.Phone == emailOrPhone);
                if (buyerUser == null || !BCrypt.Net.BCrypt.Verify(password, buyerUser.Password))
                {
                    _logger.LogWarning("Geçersiz giriş denemesi: {EmailOrPhone}", emailOrPhone);
                    throw new Exception("Geçersiz e-posta veya şifre.");
                }

                _logger.LogInformation("Alıcı giriş yaptı: {EmailOrPhone}", emailOrPhone);
                var token = _jwtService.GenerateBuyerToken(buyerUser);

                return new AuthResponseDto
                {
                    Token = token,
                    Email = buyerUser.Email,
                    UserNumber = buyerUser.UserNumber,
                    Role = UserType.Buyer,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı giriş sırasında hata oluştu: {EmailOrPhone}", emailOrPhone);
                throw;
            }
        }


        public async Task<bool> UpdateBuyerUserAsync(BuyerUserDto userDto)
        {
            try
            {
                var buyerUser = _mapper.Map<BuyerUser>(userDto);
                return await _buyerUserRepository.UpdateBoolAsync(buyerUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı güncelleme sırasında hata oluştu: {UserId}", userDto.Id);
                return false;
            }
        }

        public async Task<bool> DeleteBuyerUserAsync(int id)
        {
            try
            {
                var user = await _buyerUserRepository.GetByIdAsync(id);
                if (user == null)
                {
                    _logger.LogWarning("Silinmek istenen alıcı bulunamadı: {UserId}", id);
                    return false;
                }
                return await _buyerUserRepository.RemoveBoolAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı silme sırasında hata oluştu: {UserId}", id);
                return false;
            }
        }

        public async Task<BuyerUserDto> GetBuyerUserByIdAsync(int userId)
        {
            try
            {
                var user = await _buyerUserRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new KeyNotFoundException("Kullanıcı bulunamadı.");
                }

                return _mapper.Map<BuyerUserDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı bilgisi getirilirken hata oluştu: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            try
            {
                var user = await _buyerUserRepository.GetByIdAsync(userId);
                if (user == null || !BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
                {
                    return false;
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                await _buyerUserRepository.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı şifre değiştirme sırasında hata oluştu: {UserId}", userId);
                return false;
            }
        }

        public async Task<BuyerUserInfoDto> GetUserInfoAsync(int userId)
        {
            try
            {
                var user = await _buyerUserRepository.GetByIdAsync(userId);
                return _mapper.Map<BuyerUserInfoDto>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alıcı bilgisi getirme sırasında hata oluştu: {UserId}", userId);
                throw;
            }
        }
    }
}
