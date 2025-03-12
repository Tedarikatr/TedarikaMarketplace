using Data.Dto.Auth;

namespace Services.Auth.IServices
{
    public interface ISellerUserService
    {
        Task<string> RegisterSellerUser(SellerRegisterDto sellerRegisterDto);
        Task<AuthResponseDto> LoginSellerUser(SellerLoginDto sellerLoginDto);
        Task<SellerUserDto> GetSellerByEmail(string email);
    }
}
