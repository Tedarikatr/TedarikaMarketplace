using Entity.Auth;

namespace Services.Auth.Helper
{
    public interface IJwtService
    {
        string GenerateJwtToken(UserBase user);
    }
}
