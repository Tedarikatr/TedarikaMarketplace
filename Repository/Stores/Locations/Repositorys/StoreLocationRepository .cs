using Data.Databases;
using Data.Repository;
using Entity.Stores.Locations;
using Microsoft.EntityFrameworkCore;
using Repository.Stores.Locations.IRepositorys;

namespace Repository.Stores.Locations.Repositorys
{
    public class StoreLocationCoverageRepository : GenericRepository<StoreLocationCoverage>, IStoreLocationCoverageRepository
    {
        public StoreLocationCoverageRepository(ApplicationDbContext context) : base(context) { }

        public async Task<StoreLocationCoverage> GetByStoreIdAsync(int storeId)
        {
            return await _context.StoreLocationCoverages
                .FirstOrDefaultAsync(c => c.StoreId == storeId);
        }
    }

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
