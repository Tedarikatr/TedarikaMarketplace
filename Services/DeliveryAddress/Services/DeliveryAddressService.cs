using AutoMapper;
using Data.Dtos.DeliveryAddresses;
using Microsoft.Extensions.Logging;
using Repository.DeliveryAddresses.IRepositorys;
using Services.DeliveryAddress.IService;

namespace Services.DeliveryAddress.Services
{
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private readonly IDeliveryAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly IAddressValidationService _addressValidationService;
        private readonly ILogger<DeliveryAddressService> _logger;

        public DeliveryAddressService(IDeliveryAddressRepository addressRepository, IMapper mapper, IAddressValidationService addressValidationService, ILogger<DeliveryAddressService> logger)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _addressValidationService = addressValidationService;
            _logger = logger;
        }

        public async Task<List<DeliveryAddressDto>> GetAddressesByBuyerIdAsync(int buyerId)
        {
            var addresses = await _addressRepository.FindAsync(x => x.BuyerUserId == buyerId);
            return _mapper.Map<List<DeliveryAddressDto>>(addresses);
        }

        public async Task<DeliveryAddressDto> AddAddressAsync(DeliveryAddressCreateDto dto)
        {
            var entity = _mapper.Map<Entity.DeliveryAddresses.DeliveryAddress>(dto);
            await _addressRepository.AddAsync(entity);
            return _mapper.Map<DeliveryAddressDto>(entity);
        }

        public async Task<bool> UpdateAddressAsync(DeliveryAddressUpdateDto dto)
        {
            var address = await _addressRepository.GetByIdAsync(dto.Id);
            if (address == null) return false;

            _mapper.Map(dto, address);
            return await _addressRepository.UpdateBoolAsync(address);
        }

        public async Task<bool> DeleteAddressAsync(int id)
        {
            var address = await _addressRepository.GetByIdAsync(id);
            if (address == null) return false;

            return await _addressRepository.RemoveBoolAsync(address);
        }

        public async Task<bool> SetDefaultAsync(int buyerId, int addressId)
        {
            var allAddresses = await _addressRepository.FindAsync(x => x.BuyerUserId == buyerId);
            foreach (var address in allAddresses)
            {
                address.IsDefault = address.Id == addressId;
            }

            await _addressRepository.UpdateRangeAsync(allAddresses);
            return true;
        }
    }
}
