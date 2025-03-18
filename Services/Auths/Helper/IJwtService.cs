using Entity.Auths;
using System.Security.Claims;

namespace Services.Auths.Helper
{
    public interface IJwtService
    {
        string GenerateBuyerToken(BuyerUser buyerUser);
        string GenerateSellerToken(SellerUser sellerUser);
        string GenerateAdminToken(AdminUser adminUser);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
