using AutoMapper;
using Data.Dtos.Availability;
using Data.Dtos.Stores.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.DeliveryAddresses.IRepositorys;
using Repository.Stores;
using Repository.Stores.Locations.IRepositorys;
using Repository.Stores.Product.IRepositorys;
using Services.Availability.IServices;
using System.Linq;

namespace Services.Availability.Services
{
    public class StoreAvailabilityService : IStoreAvailabilityService
    {
        private readonly IStoreCoverageRepository _storeCoverageRepository;
        private readonly IDeliveryAddressRepository _deliveryAddressRepository;
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreAvailabilityService> _logger;

        public StoreAvailabilityService(IStoreCoverageRepository storeCoverageRepository, IDeliveryAddressRepository deliveryAddressRepository, IStoreProductRepository storeProductRepository, IStoreRepository storeRepository, IMapper mapper, ILogger<StoreAvailabilityService> logger)
        {
            _storeCoverageRepository = storeCoverageRepository;
            _deliveryAddressRepository = deliveryAddressRepository;
            _storeProductRepository = storeProductRepository;
            _storeRepository = storeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<AvailableStoreDto>> GetAvailableStoresByAddressAsync(int buyerId)
        {
            var address = await _deliveryAddressRepository.GetDefaultWithLocationByBuyerIdAsync(buyerId);
            if (address == null) return new List<AvailableStoreDto>();

            var coverages = await _storeCoverageRepository.GetQueryable().ToListAsync();

            var filtered = coverages.Where(c =>
                (c.RegionIds.Count == 0 || c.RegionIds.Contains(address.RegionId)) &&
                (c.CountryIds.Count == 0 || c.CountryIds.Contains(address.CountryId)));

            var result = new List<AvailableStoreDto>();

            foreach (var cov in filtered)
            {
                var store = await _storeRepository.GetByIdAsync(cov.StoreId);
                if (store == null) continue;

                var dto = _mapper.Map<AvailableStoreDto>(store);
                dto.RegionId = address.RegionId;
                dto.CountryId = address.CountryId;
                dto.StateId = address.StateId;
                dto.ProvinceId = address.ProvinceId;
                dto.DistrictId = address.DistrictId;
                dto.NeighborhoodId = address.NeighborhoodId;
                result.Add(dto);
            }

            return result;
        }

        public async Task<List<AvailableStoreWithProductsDto>> GetAvailableStoresWithProductsByAddressAsync(int buyerId)
        {
            var stores = await GetAvailableStoresByAddressAsync(buyerId);
            var storeIds = stores.Select(s => s.StoreId).ToList();
            if (!storeIds.Any()) return new List<AvailableStoreWithProductsDto>();

            var products = await _storeProductRepository.GetProductsByStoreIdsAsync(storeIds);

            var grouped = products.GroupBy(p => p.StoreId).ToDictionary(g => g.Key, g => g.ToList());

            var result = new List<AvailableStoreWithProductsDto>();
            foreach (var store in stores)
            {
                var dto = _mapper.Map<AvailableStoreWithProductsDto>(store);
                if (grouped.TryGetValue(store.StoreId, out var prods))
                {
                    dto.Products = prods.Select(p => _mapper.Map<StoreProductListDto>(p)).ToList();
                }
                result.Add(dto);
            }

            return result;
        }
    }
}