using Data.Dto.Auth;

namespace Services.Auth.IServices
{
    public interface ISellerUserService
    {
        Task<string> RegisterSellerUserAsync(SellerRegisterDto sellerRegisterDto);
        Task<AuthResponseDto> AuthenticateSellerUserAsync(string emailOrPhone, string password);
        Task<bool> UpdateSellerUserAsync(SellerUserDto userDto);
        Task<bool> DeleteSellerUserAsync(int id);
        Task<SellerUserDto> GetSellerUserByIdAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task<BuyerUserInfoDto> GetUserInfoAsync(int userId);
    }
}
