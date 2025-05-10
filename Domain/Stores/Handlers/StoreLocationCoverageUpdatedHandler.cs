using Entity.Stores.Locations;
using Microsoft.Extensions.Logging;
using Repository.Stores.Locations.IRepositorys;

namespace Domain.Stores.Handlers
{
    public class StoreLocationCoverageUpdatedHandler
    {
        private readonly IStoreLocationCountryRepository _countryRepo;
        private readonly IStoreLocationProvinceRepository _provinceRepo;
        private readonly IStoreLocationDistrictRepository _districtRepo;
        private readonly IStoreLocationNeighborhoodRepository _neighborhoodRepo;
        private readonly IStoreLocationRegionRepository _regionRepo;
        private readonly IStoreLocationStateRepository _stateRepo;
        private readonly IStoreLocationCoverageRepository _coverageRepo;
        private readonly ILogger<StoreLocationCoverageUpdatedHandler> _logger;

        public StoreLocationCoverageUpdatedHandler(
            IStoreLocationCountryRepository countryRepo,
            IStoreLocationProvinceRepository provinceRepo,
            IStoreLocationDistrictRepository districtRepo,
            IStoreLocationNeighborhoodRepository neighborhoodRepo,
            IStoreLocationRegionRepository regionRepo,
            IStoreLocationStateRepository stateRepo,
            IStoreLocationCoverageRepository coverageRepo,
            ILogger<StoreLocationCoverageUpdatedHandler> logger)
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

                coverage.RegionIds = (await _regionRepo.FindAsync(x => x.StoreId == storeId))
                    .Select(x => x.RegionId).ToHashSet();

                coverage.CountryIds = (await _countryRepo.FindAsync(x => x.StoreId == storeId))
                    .Select(x => x.CountryId).ToHashSet();

                coverage.StateIds = (await _stateRepo.FindAsync(x => x.StoreId == storeId))
                    .Select(x => x.StateId).ToHashSet();

                coverage.ProvinceIds = (await _provinceRepo.FindAsync(x => x.StoreId == storeId))
                    .Select(x => x.ProvinceId).ToHashSet();

                coverage.DistrictIds = (await _districtRepo.FindAsync(x => x.StoreId == storeId))
                    .Select(x => x.DistrictId).ToHashSet();

                coverage.NeighborhoodIds = (await _neighborhoodRepo.FindAsync(x => x.StoreId == storeId))
                    .Select(x => x.NeighborhoodId).ToHashSet();

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
