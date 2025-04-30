using Data.Databases;
using Data.Repository;
using Entity.Stores.Markets;
using Repository.Stores.Markets.IRepositorys;

namespace Repository.Stores.Markets.Repositorys
{
    public class StoreMarketCountryRepository : GenericRepository<StoreMarketCountry>, IStoreMarketCountryRepository
    {
        public StoreMarketCountryRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreMarketProvinceRepository : GenericRepository<StoreMarketProvince>, IStoreMarketProvinceRepository
    {
        public StoreMarketProvinceRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreMarketDistrictRepository : GenericRepository<StoreMarketDistrict>, IStoreMarketDistrictRepository
    {
        public StoreMarketDistrictRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreMarketNeighborhoodRepository : GenericRepository<StoreMarketNeighborhood>, IStoreMarketNeighborhoodRepository
    {
        public StoreMarketNeighborhoodRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreMarketRegionRepository : GenericRepository<StoreMarketRegion>, IStoreMarketRegionRepository
    {
        public StoreMarketRegionRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StoreMarketStateRepository : GenericRepository<StoreMarketState>, IStoreMarketStateRepository
    {
        public StoreMarketStateRepository(ApplicationDbContext context) : base(context) { }
    }

}
