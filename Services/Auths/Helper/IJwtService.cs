using Entity.Auths;

namespace Services.Auths.Helper
{
    public interface IJwtService
    {
        string GenerateBuyerToken(BuyerUser user);
        string GenerateSellerToken(SellerUser user);
        string GenerateAdminToken(AdminUser user);
    }
}
