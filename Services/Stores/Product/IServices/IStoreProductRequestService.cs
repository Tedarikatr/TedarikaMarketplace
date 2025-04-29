using Data.Dtos.Stores.Products;

namespace Services.Stores.Product.IServices
{
    public interface IStoreProductRequestService
    {
        Task<string> CreateStoreProductRequestAsync(StoreProductRequestCreateDto dto);
        Task<IEnumerable<StoreProductRequestDto>> GetPendingRequestsAsync();
        Task<string> ApproveStoreProductRequestAsync(int requestId);
    }
}
