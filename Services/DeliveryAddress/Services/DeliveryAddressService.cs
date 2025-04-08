using AutoMapper;
using Data.Dtos.DeliveryAddresses;
using Microsoft.Extensions.Logging;
using Repository.DeliveryAddresses.IRepositorys;
using Services.DeliveryAddress.IService;

namespace Services.DeliveryAddress.Services
{
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private readonly IDeliveryAddressRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeliveryAddressService> _logger;

        public DeliveryAddressService(IDeliveryAddressRepository repository, IMapper mapper, ILogger<DeliveryAddressService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<DeliveryAddressDto>> GetAddressesByBuyerAsync(int buyerUserId)
        {
            var addresses = await _repository.GetAddressesByBuyerIdAsync(buyerUserId);
            return _mapper.Map<IEnumerable<DeliveryAddressDto>>(addresses);
        }

        public async Task<DeliveryAddressDto> GetAddressByIdAsync(int id)
        {
            var address = await _repository.GetByIdAsync(id);
            return _mapper.Map<DeliveryAddressDto>(address);
        }

        public async Task<DeliveryAddressDto> GetDefaultAddressAsync(int buyerUserId)
        {
            var address = await _repository.GetDefaultAddressAsync(buyerUserId);
            return _mapper.Map<DeliveryAddressDto>(address);
        }

        public async Task<bool> AddAddressAsync(DeliveryAddressCreateDto dto, int buyerUserId)
        {
            try
            {
                var address = _mapper.Map<Entity.DeliveryAddresses.DeliveryAddress>(dto);
                address.BuyerUserId = buyerUserId;

                await _repository.AddAsync(address);
                _logger.LogInformation("Adres eklendi. BuyerId: {BuyerId}, Country: {Country}", buyerUserId, dto.Country);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres eklenirken hata oluştu.");
                return false;
            }
        }

        public async Task<bool> UpdateAddressAsync(DeliveryAddressUpdateDto dto, int buyerUserId)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(dto.Id);
                if (existing == null || existing.BuyerUserId != buyerUserId)
                    return false;

                _mapper.Map(dto, existing);
                await _repository.UpdateAsync(existing);

                _logger.LogInformation("Adres güncellendi. AddressId: {Id}", dto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres güncellenirken hata oluştu.");
                return false;
            }
        }

        public async Task<bool> DeleteAddressAsync(int id, int buyerUserId)
        {
            try
            {
                var address = await _repository.GetByIdAsync(id);
                if (address == null || address.BuyerUserId != buyerUserId)
                    return false;

                await _repository.RemoveAsync(address);
                _logger.LogInformation("Adres silindi. AddressId: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Adres silinirken hata oluştu.");
                return false;
            }
        }

        public async Task<bool> SetAsDefaultAsync(int buyerUserId, int addressId)
        {
            try
            {
                var addresses = await _repository.GetAddressesByBuyerIdAsync(buyerUserId);

                foreach (var addr in addresses)
                    addr.IsDefault = addr.Id == addressId;

                await _repository.UpdateRangeAsync(addresses);
                _logger.LogInformation("Varsayılan adres ayarlandı. BuyerId: {BuyerId}, AddressId: {AddressId}", buyerUserId, addressId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Varsayılan adres güncellenirken hata oluştu.");
                return false;
            }
        }
    }
}
