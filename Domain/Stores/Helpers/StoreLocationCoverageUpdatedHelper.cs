using Entity.Stores.Locations;
using Microsoft.Extensions.Logging;
using Repository.Stores.Locations.IRepositorys;

namespace Domain.Stores.Helpers
{
    public class StoreLocationCoverageUpdatedHelper
    {
        private readonly IStoreLocationCountryRepository _countryRepo;
        private readonly IStoreLocationProvinceRepository _provinceRepo;
        private readonly IStoreLocationDistrictRepository _districtRepo;
        private readonly IStoreLocationNeighborhoodRepository _neighborhoodRepo;
        private readonly IStoreLocationRegionRepository _regionRepo;
        private readonly IStoreLocationStateRepository _stateRepo;
        private readonly IStoreLocationCoverageRepository _coverageRepo;
        private readonly ILogger<StoreLocationCoverageUpdatedHelper> _logger;

        public StoreLocationCoverageUpdatedHelper(IStoreLocationCountryRepository countryRepo, IStoreLocationProvinceRepository provinceRepo, IStoreLocationDistrictRepository districtRepo, IStoreLocationNeighborhoodRepository neighborhoodRepo, IStoreLocationRegionRepository regionRepo, IStoreLocationStateRepository stateRepo, IStoreLocationCoverageRepository coverageRepo, ILogger<StoreLocationCoverageUpdatedHelper> logger)
        {
            _countryRepo = countryRepo;
            _provinceRepo = provinceRepo;
            _districtRepo = districtRepo;
            _neighborhoodRepo = neighborhoodRepo;
            _regionRepo = regionRepo;
            _stateRepo = stateRepo;
            _coverageRepo = coverageRepo;
            _logger = logger;
        }

        public async Task<bool> UpdateCoverageAsync(int storeId)
        {
            try
            {
                var coverage = await _coverageRepo.GetByStoreIdAsync(storeId) ?? new StoreLocationCoverage { StoreId = storeId };

                var regions = await _regionRepo.FindAsync(x => x.StoreId == storeId);
                coverage.RegionIds = regions.Select(x => x.RegionId).ToList();
                coverage.RegionNames = regions.Select(x => x.RegionName).ToList();

                var countries = await _countryRepo.FindAsync(x => x.StoreId == storeId);
                coverage.CountryIds = countries.Select(x => x.CountryId).ToList();
                coverage.CountryNames = countries.Select(x => x.CountryName).ToList();

                var states = await _stateRepo.FindAsync(x => x.StoreId == storeId);
                coverage.StateIds = states.Select(x => x.StateId).ToList();
                coverage.StateNames = states.Select(x => x.StateName).ToList();

                var provinces = await _provinceRepo.FindAsync(x => x.StoreId == storeId);
                coverage.ProvinceIds = provinces.Select(x => x.ProvinceId).ToList();
                coverage.ProvinceNames = provinces.Select(x => x.ProvinceName).ToList();

                var districts = await _districtRepo.FindAsync(x => x.StoreId == storeId);
                coverage.DistrictIds = districts.Select(x => x.DistrictId).ToList();
                coverage.DistrictNames = districts.Select(x => x.DistrictName).ToList();

                var neighborhoods = await _neighborhoodRepo.FindAsync(x => x.StoreId == storeId);
                coverage.NeighborhoodIds = neighborhoods.Select(x => x.NeighborhoodId).ToList();
                coverage.NeighborhoodNames = neighborhoods.Select(x => x.NeighborhoodName).ToList();

                if (coverage.Id > 0)
                    await _coverageRepo.UpdateAsync(coverage);
                else
                    await _coverageRepo.AddAsync(coverage);

                _logger.LogInformation("✅ StoreLocationCoverage güncellendi. StoreId: {StoreId}", storeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ StoreLocationCoverage güncellenemedi. StoreId: {StoreId}", storeId);
                return false;
            }
        }
    }
}
