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

        public async Task<List<AvailableStoreWithProductsDto>> GetAvailableStoresWithProductsByAddressAsync(int buyerId)
        {
            var availableStores = new List<AvailableStoreWithProductsDto>();

            try
            {
                var address = await _deliveryAddressRepository.GetDefaultWithLocationByBuyerIdAsync(buyerId);
                if (address == null)
                {
                    _logger.LogWarning("Teslimat adresi bulunamadı. BuyerId: {BuyerId}", buyerId);
                    return availableStores;
                }

                _logger.LogInformation(
                    "Buyer teslimat adresi -> BuyerId: {BuyerId}, CountryId: {CountryId}, StateId: {StateId}, ProvinceId: {ProvinceId}, DistrictId: {DistrictId}, NeighborhoodId: {NeighborhoodId}, RegionId: {RegionId}",
                    buyerId,
                    address.CountryId,
                    address.StateId,
                    address.ProvinceId,
                    address.DistrictId,
                    address.NeighborhoodId,
                    address.RegionId);

                var allCoverages = await _storeCoverageRepository.GetAllAsync();

                foreach (var coverage in allCoverages)
                {
                    _logger.LogInformation(
                        "Store Coverage kontrol ediliyor -> StoreId: {StoreId}, RegionIds: {RegionIds}, CountryIds: {CountryIds}, StateIds: {StateIds}, ProvinceIds: {ProvinceIds}, DistrictIds: {DistrictIds}, NeighborhoodIds: {NeighborhoodIds}",
                        coverage.StoreId,
                        string.Join(",", coverage.RegionIds),
                        string.Join(",", coverage.CountryIds),
                        string.Join(",", coverage.StateIds),
                        string.Join(",", coverage.ProvinceIds),
                        string.Join(",", coverage.DistrictIds),
                        string.Join(",", coverage.NeighborhoodIds));

                    bool isMatch =
                        (address.NeighborhoodId.HasValue && coverage.NeighborhoodIds.Contains(address.NeighborhoodId.Value)) ||
                        (address.DistrictId.HasValue && coverage.DistrictIds.Contains(address.DistrictId.Value)) ||
                        (address.ProvinceId.HasValue && coverage.ProvinceIds.Contains(address.ProvinceId.Value)) ||
                        (address.StateId.HasValue && coverage.StateIds.Contains(address.StateId.Value)) ||
                        (address.CountryId != default && coverage.CountryIds.Contains(address.CountryId)) ||
                        (address.RegionId.HasValue && coverage.RegionIds.Contains(address.RegionId.Value));

                    _logger.LogInformation("StoreId {StoreId} için eşleşme sonucu: {IsMatch}", coverage.StoreId, isMatch);

                    if (isMatch)
                    {
                        var products = await _storeProductRepository.GetProductsByStoreIdsAsync(new List<int> { coverage.StoreId });
                        _logger.LogInformation("StoreId {StoreId} için {ProductCount} ürün bulundu", coverage.StoreId, products.Count);

                        var productDtos = _mapper.Map<List<StoreProductListDto>>(products);
                        var store = products.FirstOrDefault()?.Store;
                        if (store == null)
                        {
                            _logger.LogWarning("StoreId {StoreId} için Store nesnesi null döndü", coverage.StoreId);
                            continue;
                        }

                        var storeDto = new AvailableStoreWithProductsDto
                        {
                            StoreId = store.Id,
                            StoreName = store.StoreName,
                            StoreDescription = store.StoreDescription,
                            LogoUrl = store.ImageUrl,
                            RegionId = address.RegionId,
                            CountryId = address.CountryId,
                            StateId = address.StateId,
                            ProvinceId = address.ProvinceId,
                            DistrictId = address.DistrictId,
                            NeighborhoodId = address.NeighborhoodId,
                            Products = productDtos
                        };

                        availableStores.Add(storeDto);
                        _logger.LogInformation("StoreId {StoreId} başarılı şekilde AvailableStore listesine eklendi.", store.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza erişilebilirliği kontrol edilirken hata oluştu.");
            }

            return availableStores;
        }
    }
}
