using Data.Repository;
using Entity.Markets.Locations;

namespace Repository.Markets.IRepositorys
{
    public interface ICountryRepository : IGenericRepository<Country> { }
    public interface IProvinceRepository : IGenericRepository<Province> { }
    public interface IDistrictRepository : IGenericRepository<District> { }
    public interface INeighborhoodRepository : IGenericRepository<Neighborhood> { }
    public interface IStateRepository : IGenericRepository<State> { }
    public interface IRegionRepository : IGenericRepository<Region> { }
}
