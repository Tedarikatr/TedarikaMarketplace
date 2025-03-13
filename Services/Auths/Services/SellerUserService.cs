using AutoMapper;
using Data.Dtos.Auths;
using Entity.Auths;
using Microsoft.Extensions.Logging;
using Repository.Auths.IRepositorys;
using Services.Auths.Helper;
using Services.Auths.IServices;

namespace Services.Auths.Services
{
    public class SellerUserService : ISellerUserService
    {
        private readonly ISellerUserRepository _sellerUserRepository;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly ILogger<SellerUserService> _logger;

        public SellerUserService(ISellerUserRepository sellerUserRepository, IMapper mapper, IJwtService jwtService, ILogger<SellerUserService> logger)
        {
            _sellerUserRepository = sellerUserRepository;
            _mapper = mapper;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<string> RegisterSellerUserAsync(SellerRegisterDto sellerRegisterDto)
        {
            try
            {
                _logger.LogInformation("Yeni satıcı kaydı yapılıyor: {Email}", sellerRegisterDto.Email);

                var existingUser = await _sellerUserRepository.FindAsync(u => u.Email == sellerRegisterDto.Email);
                if (existingUser.Any())
                {
                    throw new Exception("Bu e-posta adresi zaten kayıtlı.");
                }

                var sellerUser = _mapper.Map<SellerUser>(sellerRegisterDto);
                sellerUser.Password = BCrypt.Net.BCrypt.HashPassword(sellerRegisterDto.Password);
                sellerUser.UserGuidNumber = Guid.NewGuid();
                sellerUser.Status = true;

                await _sellerUserRepository.AddAsync(sellerUser);
                _logger.LogInformation("Satıcı başarıyla kayıt oldu: {Email}", sellerRegisterDto.Email);

                return "Kayıt başarılı!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı kaydı sırasında hata oluştu: {Email}", sellerRegisterDto.Email);
                throw;
            }
        }

        public async Task<AuthResponseDto> AuthenticateSellerUserAsync(string emailOrPhone, string password)
        {
            try
            {
                var sellerUser = await _sellerUserRepository.SingleOrDefaultAsync(u => u.Email == emailOrPhone || u.Phone == emailOrPhone);
                if (sellerUser == null || !BCrypt.Net.BCrypt.Verify(password, sellerUser.Password))
                {
                    _logger.LogWarning("Geçersiz giriş denemesi: {Email}", emailOrPhone);
                    throw new Exception("Geçersiz e-posta veya şifre.");
                }

                _logger.LogInformation("Satıcı giriş yaptı: {Email}", emailOrPhone);
                var token = _jwtService.GenerateJwtToken(sellerUser);

                return new AuthResponseDto
                {
                    Token = token,
                    Email = sellerUser.Email,
                    UserNumber = sellerUser.UserNumber
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı giriş sırasında hata oluştu: {Email}", emailOrPhone);
                throw;
            }
        }

        public async Task<bool> UpdateSellerUserAsync(SellerUserDto userDto)
        {
            try
            {
                var sellerUser = await _sellerUserRepository.GetByIdAsync(userDto.Id);
                if (sellerUser == null)
                {
                    _logger.LogWarning("Güncellenmek istenen satıcı bulunamadı: {UserId}", userDto.Id);
                    return false;
                }

                _mapper.Map(userDto, sellerUser);
                var isUpdated = await _sellerUserRepository.UpdateBoolAsync(sellerUser);

                if (isUpdated)
                {
                    _logger.LogInformation("Satıcı başarıyla güncellendi: {UserId}", userDto.Id);
                }
                else
                {
                    _logger.LogWarning("Satıcı güncelleme başarısız: {UserId}", userDto.Id);
                }

                return isUpdated;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı güncelleme sırasında hata oluştu: {UserId}", userDto.Id);
                return false;
            }
        }

        public async Task<bool> DeleteSellerUserAsync(int id)
        {
            try
            {
                var sellerUser = await _sellerUserRepository.GetByIdAsync(id);
                if (sellerUser == null)
                {
                    _logger.LogWarning("Silinmek istenen satıcı bulunamadı: {UserId}", id);
                    return false;
                }

                var isDeleted = await _sellerUserRepository.RemoveBoolAsync(sellerUser);
                if (isDeleted)
                {
                    _logger.LogInformation("Satıcı başarıyla silindi: {UserId}", id);
                }
                else
                {
                    _logger.LogWarning("Satıcı silme işlemi başarısız: {UserId}", id);
                }

                return isDeleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı silme sırasında hata oluştu: {UserId}", id);
                return false;
            }
        }

        public async Task<SellerUserDto> GetSellerUserByIdAsync(int userId)
        {
            try
            {
                var sellerUser = await _sellerUserRepository.GetByIdAsync(userId);
                if (sellerUser == null)
                {
                    _logger.LogWarning("Kullanıcı bulunamadı: {UserId}", userId);
                    throw new Exception("Satıcı bulunamadı.");
                }

                return _mapper.Map<SellerUserDto>(sellerUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı bilgisi getirilirken hata oluştu: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            try
            {
                var user = await _sellerUserRepository.GetByIdAsync(userId);
                if (user == null || !BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
                {
                    return false;
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
                await _sellerUserRepository.UpdateAsync(user);
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
                var user = await _sellerUserRepository.GetByIdAsync(userId);
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
