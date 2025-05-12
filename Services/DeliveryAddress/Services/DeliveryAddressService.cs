using AutoMapper;
using Data.Dtos.DeliveryAddresses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.DeliveryAddresses.IRepositorys;
using Repository.Locations.IRepositorys;
using Services.DeliveryAddress.IService;

namespace Services.DeliveryAddress.Services
{
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private readonly IDeliveryAddressRepository _deliveryAddressRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly IDistrictRepository _districtRepository;
        private readonly INeighborhoodRepository _neighborhoodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeliveryAddressService> _logger;

        public DeliveryAddressService(IDeliveryAddressRepository deliveryAddressRepository, IRegionRepository regionRepository, ICountryRepository countryRepository, IStateRepository stateRepository, IProvinceRepository provinceRepository, IDistrictRepository districtRepository, INeighborhoodRepository neighborhoodRepository, IMapper mapper, ILogger<DeliveryAddressService> logger)
        {
            _deliveryAddressRepository = deliveryAddressRepository;
            _regionRepository = regionRepository;
            _countryRepository = countryRepository;
            _stateRepository = stateRepository;
            _provinceRepository = provinceRepository;
            _districtRepository = districtRepository;
            _neighborhoodRepository = neighborhoodRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> AddAddressAsync(DeliveryAddressCreateDto dto, int buyerUserId)
        {
            try
            {
                var valid = await ValidateLocationAsync(dto);
                if (!valid) throw new ArgumentException("Geçersiz lokasyon bilgileri.");

                if (dto.IsDefault)
                {
                    var allAddresses = await _deliveryAddressRepository.FindAsync(a => a.BuyerUserId == buyerUserId);
                    foreach (var addr in allAddresses)
                    {
                        addr.IsDefault = false;
                    }

                    await _deliveryAddressRepository.UpdateRangeAsync(allAddresses.ToList());
                }

                var entity = _mapper.Map<Entity.DeliveryAddresses.DeliveryAddress>(dto);
                entity.BuyerUserId = buyerUserId;
                await _deliveryAddressRepository.AddAsync(entity);
                _logger.LogInformation("Teslimat adresi eklendi. BuyerId: {BuyerId}", buyerUserId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Teslimat adresi eklenirken hata oluştu.");
                return false;
            }
        }

        public async Task<bool> UpdateAddressAsync(DeliveryAddressUpdateDto dto, int buyerUserId)
        {
            try
            {
                var address = await _deliveryAddressRepository.GetByIdAsync(dto.Id);
                if (address == null || address.BuyerUserId != buyerUserId)
                    return false;

                var valid = await ValidateLocationAsync(dto);
                if (!valid) throw new ArgumentException("Geçersiz lokasyon bilgileri.");

                _mapper.Map(dto, address);
                await _deliveryAddressRepository.UpdateAsync(address);
                _logger.LogInformation("Teslimat adresi güncellendi. AddressId: {AddressId}", dto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Teslimat adresi güncellenirken hata oluştu.");
                return false;
            }
        }

        public async Task<IEnumerable<DeliveryAddressDto>> GetAddressesByBuyerAsync(int buyerUserId)
        {
            try
            {
                var result = await _deliveryAddressRepository.GetAllWithLocationByBuyerIdAsync(buyerUserId);
                return _mapper.Map<IEnumerable<DeliveryAddressDto>>(result);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Teslimat adresleri listelenemedi. BuyerId: {BuyerId}", buyerUserId);
                return Enumerable.Empty<DeliveryAddressDto>();
            }
        }

        public async Task<DeliveryAddressDto> GetAddressByIdAsync(int id)
        {
            try
            {
                var address = await _deliveryAddressRepository.GetWithLocationByIdAsync(id);
                return _mapper.Map<DeliveryAddressDto>(address);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres detayı getirilemedi. AddressId: {AddressId}", id);
                return null;
            }
        }

        public async Task<DeliveryAddressDto> GetDefaultAddressAsync(int buyerUserId)
        {
            try
            {
                var address = await _deliveryAddressRepository.GetDefaultWithLocationByBuyerIdAsync(buyerUserId);
                return _mapper.Map<DeliveryAddressDto>(address);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Varsayılan adres getirilemedi. BuyerId: {BuyerId}", buyerUserId);
                return null;
            }
        }

        public async Task<bool> DeleteAddressAsync(int id, int buyerUserId)
        {
            try
            {
                var address = await _deliveryAddressRepository.GetByIdAsync(id);
                if (address == null || address.BuyerUserId != buyerUserId)
                    return false;

                return await _deliveryAddressRepository.RemoveBoolAsync(address);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Teslimat adresi silinirken hata oluştu. AddressId: {AddressId}", id);
                return false;
            }
        }

        public async Task<bool> SetAsDefaultAsync(int buyerUserId, int addressId)
        {
            try
            {
                var all = await _deliveryAddressRepository.FindAsync(a => a.BuyerUserId == buyerUserId);
                foreach (var item in all)
                    item.IsDefault = false;

                var defaultOne = all.FirstOrDefault(a => a.Id == addressId);
                if (defaultOne == null) return false;

                defaultOne.IsDefault = true;
                await _deliveryAddressRepository.UpdateRangeAsync(all);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Varsayılan adres ayarlanırken hata oluştu.");
                return false;
            }
        }

        private async Task<bool> ValidateLocationAsync(DeliveryAddressCreateDto dto)
        {
            _logger.LogInformation("Lokasyon doğrulama başlatıldı. DTO: {@Dto}", dto);

            var region = await _regionRepository.GetByIdAsync(dto.RegionId);
            if (region == null)
            {
                _logger.LogWarning("Region bulunamadı. RegionId: {RegionId}", dto.RegionId);
            }

            var country = await _countryRepository.GetByIdAsync(dto.CountryId);
            if (country == null)
            {
                _logger.LogWarning("Country bulunamadı. CountryId: {CountryId}", dto.CountryId);
            }

            var state = dto.StateId.HasValue ? await _stateRepository.GetByIdAsync(dto.StateId.Value) : null;
            if (dto.StateId.HasValue && state == null)
            {
                _logger.LogWarning("State bulunamadı. StateId: {StateId}", dto.StateId.Value);
            }

            var province = await _provinceRepository.GetByIdAsync(dto.ProvinceId);
            if (province == null)
            {
                _logger.LogWarning("Province bulunamadı. ProvinceId: {ProvinceId}", dto.ProvinceId);
            }

            var district = await _districtRepository.GetByIdAsync(dto.DistrictId);
            if (district == null)
            {
                _logger.LogWarning("District bulunamadı. DistrictId: {DistrictId}", dto.DistrictId);
            }

            var neighborhood = await _neighborhoodRepository.GetByIdAsync(dto.NeighborhoodId);
            if (neighborhood == null)
            {
                _logger.LogWarning("Neighborhood bulunamadı. NeighborhoodId: {NeighborhoodId}", dto.NeighborhoodId);
            }

            bool isValid = region != null && country != null && province != null &&
                           district != null && neighborhood != null &&
                           (!dto.StateId.HasValue || state != null);

            _logger.LogInformation("Lokasyon doğrulama sonucu: {IsValid}", isValid);

            return isValid;
        }

        private async Task<bool> ValidateLocationAsync(DeliveryAddressUpdateDto dto)
        {
            try
            {
                _logger.LogInformation("DeliveryAddressUpdateDto map işlemi başlatılıyor. DTO: {@Dto}", dto);
                var createDto = _mapper.Map<DeliveryAddressCreateDto>(dto);
                _logger.LogInformation("DTO map işlemi tamamlandı. Mapped DTO: {@CreateDto}", createDto);

                return await ValidateLocationAsync(createDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeliveryAddressUpdateDto mapping veya doğrulama sırasında hata oluştu.");
                return false;
            }
        }
    }
}
