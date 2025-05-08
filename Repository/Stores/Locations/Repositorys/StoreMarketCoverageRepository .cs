using Data.Databases;
using Data.Repository;
using Entity.Stores.Locations;
using Repository.Stores.Locations.IRepositorys;

namespace Repository.Stores.Locations.Repositorys
{
    public class StoreLocationCountryRepository : GenericRepository<StoreLocationCountry>, IStoreLocationCountryRepository
    {
        public StoreLocationCountryRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreLocationProvinceRepository : GenericRepository<StoreLocationProvince>, IStoreLocationProvinceRepository
    {
        public StoreLocationProvinceRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreLocationDistrictRepository : GenericRepository<StoreLocationDistrict>, IStoreLocationDistrictRepository
    {
        public StoreLocationDistrictRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreLocationNeighborhoodRepository : GenericRepository<StoreLocationNeighborhood>, IStoreLocationNeighborhoodRepository
    {
        public StoreLocationNeighborhoodRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreLocationRegionRepository : GenericRepository<StoreLocationRegion>, IStoreLocationRegionRepository
    {
        public StoreLocationRegionRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreLocationStateRepository : GenericRepository<StoreLocationState>, IStoreLocationStateRepository
    {
        public StoreLocationStateRepository(ApplicationDbContext context) : base(context) { }
    }

}
