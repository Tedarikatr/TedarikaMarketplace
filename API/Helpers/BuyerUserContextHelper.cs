using System.Security.Claims;

namespace API.Helpers
{
    public class BuyerUserContextHelper
    {
        public static int GetBuyerId(ClaimsPrincipal user)
        {
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("Buyer ID bulunamadı."));
        }

        public static string GetBuyerNumber(ClaimsPrincipal user)
        {
            return user.FindFirst("UserNumber")?.Value ?? throw new UnauthorizedAccessException("Buyer numarası bulunamadı.");
        }

        public static Guid GetBuyerGuid(ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirst("UserGuid")?.Value ?? throw new UnauthorizedAccessException("Buyer GUID bulunamadı."));
        }

        public static bool IsBuyerActive(ClaimsPrincipal user)
        {
            return bool.Parse(user.FindFirst("IsActive")?.Value ?? throw new UnauthorizedAccessException("Buyer aktiflik durumu bulunamadı."));
        }

        public static int? GetCompanyId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst("CompanyId");
            return claim != null ? int.Parse(claim.Value) : null;
        }
    }
}
