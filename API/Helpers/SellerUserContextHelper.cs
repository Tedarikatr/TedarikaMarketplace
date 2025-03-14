using System.Security.Claims;

namespace API.Helpers
{
    public class SellerUserContextHelper
    {
        public static int GetSellerId(ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("Seller ID bulunamadı."));
        }

        public static string GetSellerNumber(ClaimsPrincipal user)
        {
            return user.FindFirst("UserNumber")?.Value ?? throw new UnauthorizedAccessException("Seller numarası bulunamadı.");
        }

        public static Guid GetSellerGuid(ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst("UserGuid")?.Value ?? throw new UnauthorizedAccessException("Seller GUID bulunamadı."));
        }

        public static bool IsSellerActive(ClaimsPrincipal user)
        {
            return bool.Parse(user.FindFirst("IsActive")?.Value ?? throw new UnauthorizedAccessException("Seller aktiflik durumu bulunamadı."));
        }

        public static int? GetCompanyId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst("CompanyId");
            return claim != null ? int.Parse(claim.Value) : null;
        }
    }
}
