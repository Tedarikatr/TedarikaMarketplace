using Data.Repository;
using Entity.Stores.Locations;

namespace Repository.Stores.Locations.IRepositorys
{
    public interface IStoreLocationCoverageRepository : IGenericRepository<StoreLocationCoverage>
    {
        Task<StoreLocationCoverage> GetByStoreIdAsync(int storeId);
    }

    public interface IStoreLocationCountryRepository : IGenericRepository<StoreLocationCountry> { }
    public interface IStoreLocationProvinceRepository : IGenericRepository<StoreLocationProvince> { }
    public interface IStoreLocationDistrictRepository : IGenericRepository<StoreLocationDistrict> { }
    public interface IStoreLocationNeighborhoodRepository : IGenericRepository<StoreLocationNeighborhood> { }
    public interface IStoreLocationRegionRepository : IGenericRepository<StoreLocationRegion> { }
    public interface IStoreLocationStateRepository : IGenericRepository<StoreLocationState> { }

}

