using Data.Databases;
using Data.Repository;
using Entity.Markets.Locations;
using Repository.Markets.IRepositorys;

namespace Repository.Markets.Repositorys
{
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        public CountryRepository(ApplicationDbContext context) : base(context) { }
    }

    public class ProvinceRepository : GenericRepository<Province>, IProvinceRepository
    {
        public ProvinceRepository(ApplicationDbContext context) : base(context) { }
    }

    public class DistrictRepository : GenericRepository<District>, IDistrictRepository
    {
        public DistrictRepository(ApplicationDbContext context): base(context) { }
    }

    public class NeighborhoodRepository : GenericRepository<Neighborhood>, INeighborhoodRepository
    {
        public NeighborhoodRepository(ApplicationDbContext context) : base(context) { }
    }

    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        public StateRepository(ApplicationDbContext context) : base(context) { }
    }

    public class RegionRepository : GenericRepository<Region>, IRegionRepository
    {
        public RegionRepository(ApplicationDbContext context) : base(context) { }
    }
}
