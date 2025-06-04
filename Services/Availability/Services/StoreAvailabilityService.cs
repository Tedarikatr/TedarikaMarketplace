using AutoMapper;
using Data.Dtos.Availability;
using Data.Dtos.Stores.Products;
using Microsoft.Extensions.Logging;
using Repository.DeliveryAddresses.IRepositorys;
using Repository.Stores.Locations.IRepositorys;
using Repository.Stores.Product.IRepositorys;
using Services.Availability.IServices;

namespace Services.Availability.Services
{
    public class StoreAvailabilityService : IStoreAvailabilityService
    {
        private readonly IStoreCoverageRepository _storeCoverageRepository;
        private readonly IDeliveryAddressRepository _deliveryAddressRepository;
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreAvailabilityService> _logger;

        public StoreAvailabilityService(IStoreCoverageRepository storeCoverageRepository, IDeliveryAddressRepository deliveryAddressRepository, IStoreProductRepository storeProductRepository, IMapper mapper, ILogger<StoreAvailabilityService> logger)
        {
            _storeCoverageRepository = storeCoverageRepository;
            _deliveryAddressRepository = deliveryAddressRepository;
            _storeProductRepository = storeProductRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public Task<List<AvailableStoreDto>> GetAvailableStoresByAddressAsync(int buyerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<AvailableStoreWithProductsDto>> GetAvailableStoresWithProductsByAddressAsync(int buyerId)
        {
            throw new NotImplementedException();
        }
    }
}

