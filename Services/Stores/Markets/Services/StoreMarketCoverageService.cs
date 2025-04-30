using AutoMapper;
using Microsoft.Extensions.Logging;
using Repository.Stores.Markets.IRepositorys;
using Services.Markets.IServices;
using Services.Stores.Markets.IServices;

namespace Services.Stores.Markets.Services
{
    public class StoreMarketCoverageService : IStoreMarketCoverageService
    {
        private readonly IStoreMarketCoverageRepository _coverageRepo;
        private readonly IMarketLocationService _locationService;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreMarketCoverageService> _logger;

        public StoreMarketCoverageService(
            IStoreMarketCoverageRepository coverageRepo,
            IMarketLocationService locationService,
            IMapper mapper,
            ILogger<StoreMarketCoverageService> logger)
        {
            _coverageRepo = coverageRepo;
            _locationService = locationService;
            _mapper = mapper;
            _logger = logger;
        }

       
    }
}
