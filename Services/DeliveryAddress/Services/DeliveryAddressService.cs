using Microsoft.Extensions.Logging;
using Repository.DeliveryAddresses.IRepositorys;
using Services.DeliveryAddress.IService;

namespace Services.DeliveryAddress.Services
{
    public class DeliveryAddressService : IDeliveryAddressService
    {
        private readonly IDeliveryAddressRepository _repository;
        private readonly ILogger<DeliveryAddressService> _logger;

        public DeliveryAddressService(IDeliveryAddressRepository repository, ILogger<DeliveryAddressService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Entity.DeliveryAddresses.DeliveryAddress>> GetAddressesByBuyerAsync(int buyerUserId)
        {
            return await _repository.GetAddressesByBuyerIdAsync(buyerUserId);
        }

        public async Task<Entity.DeliveryAddresses.DeliveryAddress> GetAddressByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Entity.DeliveryAddresses.DeliveryAddress> GetDefaultAddressAsync(int buyerUserId)
        {
            return await _repository.GetDefaultAddressAsync(buyerUserId);
        }

        public async Task AddAddressAsync(Entity.DeliveryAddresses.DeliveryAddress address)
        {
            await _repository.AddAsync(address);
            _logger.LogInformation("Yeni teslimat adresi eklendi. BuyerId: {BuyerId}", address.BuyerUserId);
        }

        public async Task UpdateAddressAsync(Entity.DeliveryAddresses.DeliveryAddress address)
        {
            await _repository.UpdateAsync(address);
            _logger.LogInformation("Teslimat adresi güncellendi. AddressId: {Id}", address.Id);
        }

        public async Task DeleteAddressAsync(int id)
        {
            var address = await _repository.GetByIdAsync(id);
            if (address != null)
            {
                await _repository.RemoveAsync(address);
                _logger.LogInformation("Teslimat adresi silindi. AddressId: {Id}", id);
            }
        }

        public async Task SetAsDefaultAsync(int buyerUserId, int addressId)
        {
            var addresses = await _repository.GetAddressesByBuyerIdAsync(buyerUserId);

            foreach (var addr in addresses)
                addr.IsDefault = addr.Id == addressId;

            await _repository.UpdateRangeAsync(addresses);
            _logger.LogInformation("Varsayılan adres güncellendi. BuyerId: {BuyerId}, AddressId: {AddressId}", buyerUserId, addressId);
        }

    }
}
