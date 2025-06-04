using Data.Repository;
using Entity.Stores.Locations;

namespace Repository.Stores.Locations.IRepositorys
{
    public interface IStoreCoverageRepository : IGenericRepository<StoreCoverage>
    {
        Task<List<StoreCoverage>> GetByStoreIdAsync(int storeId);
        Task DeleteCascadeForStoreAsync(int storeId, int? countryId, int? provinceId);
    }
}
