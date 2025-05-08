using Data.Repository;
using Entity.Locations;

namespace Repository.Locations.IRepositorys
{
    public interface ICountryRepository : IGenericRepository<Country> { }
    public interface IProvinceRepository : IGenericRepository<Province> { }
    public interface IDistrictRepository : IGenericRepository<District> { }
    public interface INeighborhoodRepository : IGenericRepository<Neighborhood> { }
    public interface IStateRepository : IGenericRepository<State> { }
    public interface IRegionRepository : IGenericRepository<Region> { }
}
