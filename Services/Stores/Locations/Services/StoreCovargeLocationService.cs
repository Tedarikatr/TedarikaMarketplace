using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Stores.Locations.IRepositorys;
using Services.Stores.Locations.IServices;

namespace Services.Stores.Locations.Services
{
    public class StoreCovargeLocationService : IStoreCovargeLocationService
    {

        private readonly IStoreLocationCoverageRepository _covargeRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<StoreCovargeLocationService> _logger;

        public StoreCovargeLocationService(IStoreLocationCoverageRepository covargeRepo, IMapper mapper, IMediator mediator, ILogger<StoreCovargeLocationService> logger)
        {
            _covargeRepo = covargeRepo;
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
        }

        public Task<List<int>> AddCompositeCoverageAsync(StoreMarketCoverageCompositeCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteCompositeCoverageAsync(StoreMarketCoverageCompositeDeleteDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<StoreMarketCoverageHierarchyDto> GetCoverageHierarchyByStoreIdAsync(int storeId)
        {
            throw new NotImplementedException();
        }

    }
}
