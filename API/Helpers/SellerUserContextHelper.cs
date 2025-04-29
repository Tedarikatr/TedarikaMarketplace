using Repository.Stores;
using System.Security.Claims;

namespace API.Helpers
{
    public class SellerUserContextHelper
    {
        private readonly IStoreRepository _storeRepository;

        public SellerUserContextHelper(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public int GetSellerId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
            if (claim == null)
                throw new UnauthorizedAccessException("Seller ID bulunamadı.");

            if (!int.TryParse(claim.Value, out int userId))
                throw new UnauthorizedAccessException("Geçersiz Seller ID.");

            return userId;
        }

        public  async Task<int> GetStoreId(ClaimsPrincipal user)
        {
            if (_storeRepository == null)
                throw new InvalidOperationException("StoreRepository yapılandırılmadı.");

            var sellerId = GetSellerId(user);

            var store = await _storeRepository.GetStoreBySellerIdAsync(sellerId);
            if (store == null)
                throw new UnauthorizedAccessException("Bu kullanıcıya ait mağaza bulunamadı.");

            return store.Id;
        }

        public  string GetSellerNumber(ClaimsPrincipal user) =>
            user.FindFirst("UserNumber")?.Value ?? throw new UnauthorizedAccessException("Seller numarası bulunamadı.");

        public  Guid GetSellerGuid(ClaimsPrincipal user) =>
            Guid.Parse(user.FindFirst("UserGuid")?.Value ?? throw new UnauthorizedAccessException("Seller GUID bulunamadı."));

        public  bool IsSellerActive(ClaimsPrincipal user) =>
            bool.Parse(user.FindFirst("IsActive")?.Value ?? throw new UnauthorizedAccessException("Seller aktiflik durumu bulunamadı."));

        public int? GetCompanyId(ClaimsPrincipal user)
        {
            var claim = user.FindFirst("CompanyId");
            return claim != null ? int.Parse(claim.Value) : null;
        }
    }
}
