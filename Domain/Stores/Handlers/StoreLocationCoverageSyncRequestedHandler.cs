using Domain.Stores.Events;
using Entity.Stores.Locations;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Stores.Locations.IRepositorys;

namespace Domain.Stores.Handlers
{
    public class StoreLocationCoverageSyncRequestedHandler : INotificationHandler<StoreLocationCoverageSyncRequestedEvent>
    {
        private readonly IStoreLocationRegionRepository _regionRepo;
        private readonly IStoreLocationCountryRepository _countryRepo;
        private readonly IStoreLocationStateRepository _stateRepo;
        private readonly IStoreLocationProvinceRepository _provinceRepo;
        private readonly IStoreLocationDistrictRepository _districtRepo;
        private readonly IStoreLocationNeighborhoodRepository _neighborhoodRepo;
        private readonly IStoreLocationCoverageRepository _coverageRepo;
        private readonly ILogger<StoreLocationCoverageSyncRequestedHandler> _logger;

        public StoreLocationCoverageSyncRequestedHandler(
            IStoreLocationRegionRepository regionRepo,
            IStoreLocationCountryRepository countryRepo,
            IStoreLocationStateRepository stateRepo,
            IStoreLocationProvinceRepository provinceRepo,
            IStoreLocationDistrictRepository districtRepo,
            IStoreLocationNeighborhoodRepository neighborhoodRepo,
            IStoreLocationCoverageRepository coverageRepo,
            ILogger<StoreLocationCoverageSyncRequestedHandler> logger)
        {
            _regionRepo = regionRepo;
            _countryRepo = countryRepo;
            _stateRepo = stateRepo;
            _provinceRepo = provinceRepo;
            _districtRepo = districtRepo;
            _neighborhoodRepo = neighborhoodRepo;
            _coverageRepo = coverageRepo;
            _logger = logger;
        }

        public async Task Handle(StoreLocationCoverageSyncRequestedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("📦 StoreLocationCoverage senkronizasyonu başlatıldı. StoreId: {StoreId}", notification.StoreId);

            var coverage = await _coverageRepo.GetByStoreIdAsync(notification.StoreId)
                           ?? new StoreLocationCoverage { StoreId = notification.StoreId };

            coverage.RegionIds = (await _regionRepo.FindAsync(x => x.StoreId == notification.StoreId))
                .Select(x => x.RegionId).ToHashSet();

            coverage.CountryIds = (await _countryRepo.FindAsync(x => x.StoreId == notification.StoreId))
                .Select(x => x.CountryId).ToHashSet();

            coverage.StateIds = (await _stateRepo.FindAsync(x => x.StoreId == notification.StoreId))
                .Select(x => x.StateId).ToHashSet();

            coverage.ProvinceIds = (await _provinceRepo.FindAsync(x => x.StoreId == notification.StoreId))
                .Select(x => x.ProvinceId).ToHashSet();

            coverage.DistrictIds = (await _districtRepo.FindAsync(x => x.StoreId == notification.StoreId))
                .Select(x => x.DistrictId).ToHashSet();

            coverage.NeighborhoodIds = (await _neighborhoodRepo.FindAsync(x => x.StoreId == notification.StoreId))
                .Select(x => x.NeighborhoodId).ToHashSet();

            await _coverageRepo.UpsertAsync(coverage);

            _logger.LogInformation("✅ StoreLocationCoverage güncellendi. StoreId: {StoreId}", notification.StoreId);
        }
    }
}
