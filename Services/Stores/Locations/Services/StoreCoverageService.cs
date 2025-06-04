using AutoMapper;
using Data.Dtos.Stores.Locations;
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
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<StoreCoverageService> _logger;

        public StoreCoverageService(IStoreCoverageRepository coverageRepo,
            ICountryRepository countryRepo,
            IProvinceRepository provinceRepo,
            IDistrictRepository districtRepo,
            IMapper mapper,
            IMediator mediator,
            ILogger<StoreCoverageService> logger)
        {
            _coverageRepo = coverageRepo;
            _countryRepo = countryRepo;
            _provinceRepo = provinceRepo;
            _districtRepo = districtRepo;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public Task<int> AddCoverageAsync(StoreCoverageCreateDto dto, int storeId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCoverageAsync(StoreCoverageDeleteDto dto, int storeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<StoreCoverageDto>> GetCoverageByStoreIdAsync(int storeId)
        {
            throw new NotImplementedException();
        }

        private string BuildHash(int storeId, int? regionId, int? countryId, int? provinceId, int? districtId)
        {
            return $"{storeId}-{regionId}-{countryId}-{provinceId}-{districtId}";
        }
      
    }
}
