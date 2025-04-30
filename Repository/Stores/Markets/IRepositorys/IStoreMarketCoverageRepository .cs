using Data.Repository;
using Entity.Stores.Markets;

namespace Repository.Stores.Markets.IRepositorys
{
    public interface IStoreMarketCoverageRepository : IGenericRepository<StoreMarketCoverage>
    {
        Task<List<StoreMarketCoverage>> GetCoveragesBySellerUserIdAsync(int sellerUserId);

    }

    public interface IStoreMarketCountryRepository : IGenericRepository<StoreMarketCountry> { }
    public interface IStoreMarketProvinceRepository : IGenericRepository<StoreMarketProvince> { }
    public interface IStoreMarketDistrictRepository : IGenericRepository<StoreMarketDistrict> { }
    public interface IStoreMarketNeighborhoodRepository : IGenericRepository<StoreMarketNeighborhood> { }
    public interface IStoreMarketRegionRepository : IGenericRepository<StoreMarketRegion> { }
    public interface IStoreMarketStateRepository : IGenericRepository<StoreMarketState> { }

}
