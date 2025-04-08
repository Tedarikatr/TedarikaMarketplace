using Data.Repository;
using Entity.Stores.Products;

namespace Repository.Stores.Product.IRepositorys
{
    public interface IStoreProductRequestRepository : IGenericRepository<StoreProductRequest>
    {
        Task<IEnumerable<StoreProductRequest>> GetPendingRequestsAsync();
    }
}
