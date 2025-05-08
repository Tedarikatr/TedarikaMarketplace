using System.Security.Claims;

namespace API.Helpers
{
    public class BuyerUserContextHelper
    {
        public  int GetBuyerId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Buyer ID bulunamadı.");

            if (!int.TryParse(claim.Value, out int userId))
                throw new UnauthorizedAccessException("Geçersiz Buyer ID.");

            return userId;
        }
    }
}
