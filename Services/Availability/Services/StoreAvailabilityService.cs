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
        private readonly IStoreLocationCoverageRepository _storeLocationCoverageRepository;
        private readonly IDeliveryAddressRepository _deliveryAddressRepository;
        private readonly IStoreProductRepository _storeProductRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreAvailabilityService> _logger;

        public StoreAvailabilityService(IStoreLocationCoverageRepository storeLocationCoverageRepository, IDeliveryAddressRepository deliveryAddressRepository, IStoreProductRepository storeProductRepository, IMapper mapper, ILogger<StoreAvailabilityService> logger)
        {
            _storeLocationCoverageRepository = storeLocationCoverageRepository;
            _deliveryAddressRepository = deliveryAddressRepository;
            _storeProductRepository = storeProductRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<AvailableStoreWithProductsDto>> GetAvailableStoresWithProductsByAddressAsync(int buyerId)
        {
            var availableStores = new List<AvailableStoreWithProductsDto>();

            try
            {
                var address = await _deliveryAddressRepository.GetDefaultAddressByBuyerIdAsync(buyerId);
                if (address == null)
                {
                    _logger.LogWarning("Teslimat adresi bulunamadı. BuyerId: {BuyerId}", buyerId);
                    return availableStores;
                }

                var allCoverages = await _storeLocationCoverageRepository.GetAllAsync();

                foreach (var coverage in allCoverages)
                {
                    bool isMatch =
                        (address.NeighborhoodId.HasValue && coverage.NeighborhoodIds.Contains(address.NeighborhoodId.Value)) ||
                        (address.DistrictId.HasValue && coverage.DistrictIds.Contains(address.DistrictId.Value)) ||
                        (address.ProvinceId.HasValue && coverage.ProvinceIds.Contains(address.ProvinceId.Value)) ||
                        (address.StateId.HasValue && coverage.StateIds.Contains(address.StateId.Value)) ||
                        (coverage.CountryIds.Contains(address.CountryId)) ||
                        (address.Country?.RegionId != null && coverage.RegionIds.Contains(address.Country.RegionId));

                    if (isMatch)
                    {
                        var products = await _storeProductRepository.GetProductsByStoreIdsAsync(new List<int> { coverage.StoreId });

                        var productDtos = _mapper.Map<List<StoreProductListDto>>(products);
                        var store = products.FirstOrDefault()?.Store;
                        if (store == null) continue;

                        var storeDto = new AvailableStoreWithProductsDto
                        {
                            StoreId = store.Id,
                            StoreName = store.StoreName,
                            StoreDescription = store.StoreDescription,
                            LogoUrl = store.ImageUrl,
                            RegionId = address.Country?.RegionId,
                            CountryId = address.CountryId,
                            StateId = address.StateId,
                            ProvinceId = address.ProvinceId,
                            DistrictId = address.DistrictId,
                            NeighborhoodId = address.NeighborhoodId,
                            Products = productDtos
                        };

                        availableStores.Add(storeDto);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza erişilebilirliği kontrol edilirken hata oluştu.");
            }

            return availableStores;
        }

        public async Task<List<AvailableStoreDto>> GetAvailableStoresByAddressAsync(int buyerId)
        {
            var storesWithProducts = await GetAvailableStoresWithProductsByAddressAsync(buyerId);
            return storesWithProducts.Select(x => new AvailableStoreDto
            {
                StoreId = x.StoreId,
                StoreName = x.StoreName,
                StoreDescription = x.StoreDescription,
                LogoUrl = x.LogoUrl,
                DeliveryTimeFrame = x.DeliveryTimeFrame,
                RegionId = x.RegionId,
                CountryId = x.CountryId,
                StateId = x.StateId,
                ProvinceId = x.ProvinceId,
                DistrictId = x.DistrictId,
                NeighborhoodId = x.NeighborhoodId,
            }).ToList();
        }
    }
}
