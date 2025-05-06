using Data.Dtos.Stores.Products;

namespace Services.Stores.Product.IServices
{
    public interface IStoreProductRequestService
    {
        Task<string> CreateStoreProductRequestAsync(int storeId, StoreProductRequestCreateDto dto);
        Task<List<StoreProductRequestDto>> GetMyRequestsAsync(int storeId); // Seller: kendi başvurularını listele
        Task<StoreProductRequestDetailDto> GetRequestDetailAsync(int requestId, int storeId); // Seller: detay görüntüleme
        Task<RequestSummaryDto> GetRequestSummaryAsync(int storeId); // Özet bilgi: kaç başvuru yapıldı vs.


        Task<List<StoreProductRequestDto>> GetAllPendingRequestsAsync(); // Admin: tüm bekleyen başvuruları listele
        Task<StoreProductRequestDetailDto> GetRequestDetailAsync(int requestId); // Admin: detay görüntüleme

        Task<string> ApproveRequestAsync(int requestId); // Admin: onay işlemi
        Task<string> RejectRequestAsync(int requestId, string adminNote); // Admin: red ve açıklama

        Task<bool> CreateProductFromApprovedRequestAsync(int requestId); // Onaylanmış başvurudan Product üret


    }
}
