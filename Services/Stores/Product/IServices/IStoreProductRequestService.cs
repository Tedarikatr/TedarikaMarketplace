using Data.Dtos.Stores.Products;

namespace Services.Stores.Product.IServices
{
    public interface IStoreProductRequestService
    {
        Task<string> CreateStoreProductRequestAsync(int storeId, StoreProductRequestCreateDto dto);
        Task<List<StoreProductRequestDto>> GetMyRequestsAsync(int storeId);
        Task<StoreProductRequestDetailDto> GetRequestDetailAsync(int requestId, int storeId); 
        Task<RequestSummaryDto> GetRequestSummaryAsync(int storeId); 

        Task<List<StoreProductRequestDto>> GetAllPendingRequestsAsync();
        Task<StoreProductRequestDetailDto> GetRequestDetailAsync(int requestId);

        Task<string> ApproveRequestAsync(int requestId);
        Task<string> RejectRequestAsync(int requestId, string adminNote); 

        Task<bool> CreateProductFromApprovedRequestAsync(int requestId); 
    }
}
