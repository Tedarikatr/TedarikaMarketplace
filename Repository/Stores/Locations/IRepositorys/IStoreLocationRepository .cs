using Data.Repository;
using Entity.Stores.Locations;

namespace Repository.Stores.Locations.IRepositorys
{
    public interface IStoreLocationCoverageRepository : IGenericRepository<StoreCovargeLocation>
    {
        Task<StoreCovargeLocation> GetByStoreIdAsync(int storeId);
    }

}
