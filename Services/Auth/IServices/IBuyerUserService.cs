using Data.Dto.Auth;

namespace Services.Auth.IServices
{
    public interface IBuyerUserService
    {
        Task<string> RegisterBuyerUserAsync(BuyerUserCreateDto createUserDto);
        Task<AuthResponseDto> AuthenticateBuyerUserAsync(string emailOrPhone, string password);
        Task<bool> UpdateBuyerUserAsync(BuyerUserDto userDto);
        Task<bool> DeleteBuyerUserAsync(int id);
        Task<BuyerUserDto> GetBuyerUserByIdAsync(int userId);
        Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task<BuyerUserInfoDto> GetUserInfoAsync(int userId);
    }
}
