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

        public Task<bool> AddAddressAsync(DeliveryAddressCreateDto dto, int buyerUserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAddressAsync(int id, int buyerUserId)
        {
            throw new NotImplementedException();
        }

        public Task<DeliveryAddressDto> GetAddressByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<DeliveryAddressDto>> GetAddressesByBuyerAsync(int buyerUserId)
        {
            throw new NotImplementedException();
        }

        public Task<DeliveryAddressDto> GetDefaultAddressAsync(int buyerUserId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsDefaultAsync(int buyerUserId, int addressId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAddressAsync(DeliveryAddressUpdateDto dto, int buyerUserId)
        {
            throw new NotImplementedException();
        }
    }
}
