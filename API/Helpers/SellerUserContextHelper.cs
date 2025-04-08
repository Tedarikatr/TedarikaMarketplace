using System.Security.Claims;

namespace API.Helpers
{
    public class SellerUserContextHelper
    {
        public static int GetSellerId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Seller ID bulunamadı.");

            if (!int.TryParse(claim.Value, out int userId))
                throw new UnauthorizedAccessException("Geçersiz Seller ID.");

            return userId;
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

        public static int GetStoreId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst("StoreId");
            if (claim == null) throw new UnauthorizedAccessException("StoreId bulunamadı.");
            return int.Parse(claim.Value);
        }

    }
}
