using Data.Dtos.Auths;

namespace Services.Auths.IServices
{
    public interface IAdminUserService
    {
        Task<string> RegisterSuperAdminAsync(AdminRegisterDto adminRegisterDto);
        Task<string> RegisterAdminAsync(AdminRegisterDto adminRegisterDto, string superAdminPassword);
        Task<AuthResponseDto> AuthenticateAdminAsync(string email, string password);
    }
}
