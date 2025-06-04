using AutoMapper;
using Data.Dtos.Stores.Locations;
using Data.Repository;
using Domain.Stores.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Locations.IRepositorys;
using Repository.Stores.Locations.IRepositorys;
using Services.Stores.Locations.IServices;

namespace Services.Stores.Locations.Services
{
    public class StoreCoverageService : IStoreCoverageService
    {
        private readonly IStoreCoverageRepository _coverageRepo;
        private readonly ICountryRepository _countryRepo;
        private readonly IProvinceRepository _provinceRepo;
        private readonly IDistrictRepository _districtRepo;
        private readonly IRegionRepository _regionRepo;
        private readonly IStateRepository _stateRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<StoreCoverageService> _logger;

        public StoreCoverageService(IStoreCoverageRepository coverageRepo,
            ICountryRepository countryRepo,
            IProvinceRepository provinceRepo,
            IDistrictRepository districtRepo,
            IRegionRepository regionRepo,
            IStateRepository stateRepo,
            INeighborhoodRepository neighborhoodRepo,
            IMapper mapper,
            IMediator mediator,
            ILogger<StoreCoverageService> logger)
        {
            _coverageRepo = coverageRepo;
            _countryRepo = countryRepo;
            _provinceRepo = provinceRepo;
            _districtRepo = districtRepo;
            _regionRepo = regionRepo;
            _stateRepo = stateRepo;
            _neighborhoodRepo = neighborhoodRepo;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }
        public async Task<int> AddCoverageAsync(StoreCoverageCreateDto dto, int storeId)
        {
            var coverages = await _coverageRepo.GetByStoreIdAsync(storeId);
            var coverage = coverages.FirstOrDefault();

            if (coverage == null)
            {
                coverage = new Entity.Stores.Locations.StoreCoverage
                {
                    StoreId = storeId
                };
            }

            foreach (var regionId in dto.RegionIds.Distinct())
            {
                if (!coverage.RegionIds.Contains(regionId))
                {
                    var region = await _regionRepo.GetByIdAsync(regionId);
                    if (region != null)
                    {
                        coverage.RegionIds.Add(regionId);
                        coverage.RegionNames.Add(region.Name);
                    }
                }
            }

            foreach (var countryId in dto.CountryIds.Distinct())
            {
                if (!coverage.CountryIds.Contains(countryId))
                {
                    var country = await _countryRepo.GetByIdAsync(countryId);
                    if (country != null)
                    {
                        coverage.CountryIds.Add(countryId);
                        coverage.CountryNames.Add(country.Name);

                        if (!coverage.RegionIds.Contains(country.RegionId))
                        {
                            var reg = await _regionRepo.GetByIdAsync(country.RegionId);
                            if (reg != null)
                            {
                                coverage.RegionIds.Add(reg.Id);
                                coverage.RegionNames.Add(reg.Name);
                            }
                        }
                    }
                }
            }

            coverage.LastUpdatedAt = DateTime.UtcNow;

            if (coverages.Any())
                await _coverageRepo.UpdateAsync(coverage);
            else
                await _coverageRepo.AddAsync(coverage);

            await _mediator.Publish(new StoreCoverageChangedEvent(storeId));
            return coverage.Id;
        }

        public async Task<bool> DeleteCoverageAsync(StoreCoverageDeleteDto dto, int storeId)
        {
            var coverages = await _coverageRepo.GetByStoreIdAsync(storeId);
            var coverage = coverages.FirstOrDefault();
            if (coverage == null) return false;

            foreach (var regionId in dto.RegionIds)
            {
                int index = coverage.RegionIds.IndexOf(regionId);
                if (index >= 0)
                {
                    coverage.RegionIds.RemoveAt(index);
                    coverage.RegionNames.RemoveAt(index);
                }
            }

            foreach (var countryId in dto.CountryIds)
            {
                int index = coverage.CountryIds.IndexOf(countryId);
                if (index >= 0)
                {
                    coverage.CountryIds.RemoveAt(index);
                    coverage.CountryNames.RemoveAt(index);
                }
            }

            coverage.LastUpdatedAt = DateTime.UtcNow;

            await _coverageRepo.UpdateAsync(coverage);
            await _mediator.Publish(new StoreCoverageChangedEvent(storeId));
            return true;
        }

        public async Task<List<StoreCoverageDto>> GetCoverageByStoreIdAsync(int storeId)
        {
            var coverage = await _coverageRepo.GetByStoreIdAsync(storeId);
            return coverage.Select(c => _mapper.Map<StoreCoverageDto>(c)).ToList();
        }

        private string BuildHash(int storeId, int? regionId, int? countryId, int? provinceId, int? districtId)
        {
            return $"{storeId}-{regionId}-{countryId}-{provinceId}-{districtId}";
        }

    }
}

