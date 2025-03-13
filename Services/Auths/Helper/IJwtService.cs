using Entity.Auths;

namespace Services.Auths.Helper
{
    public interface IJwtService
    {
        string GenerateJwtToken(UserBase user);
    }
}
