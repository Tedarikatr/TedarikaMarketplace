using AutoMapper;
using Data.Dtos.Store;
using Entity.Stores;
using Microsoft.Extensions.Logging;
using Repository.Store.IRepositorys;
using Services.Store.IServices;

namespace Services.Store.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreService> _logger;

        public StoreService(IStoreRepository storeRepository, IMapper mapper, ILogger<StoreService> logger)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> CreateStoreAsync(StoreCreateDto storeCreateDto, int sellerId)
        {
            try
            {
                _logger.LogInformation("Yeni mağaza oluşturma işlemi başlatıldı. Mağaza Adı: {StoreName}", storeCreateDto.StoreName);

                var existingStore = await _storeRepository.GetStoreBySellerIdAsync(sellerId);
                if (existingStore != null)
                {
                    throw new Exception("Bu seller zaten bir mağaza açmış.");
                }

                var newStore = _mapper.Map<Entity.Stores.Store>(storeCreateDto);
                newStore.OwnerId = sellerId;
                newStore.IsApproved = false;

                await _storeRepository.AddAsync(newStore);
                _logger.LogInformation("Yeni mağaza oluşturuldu. Mağaza ID: {StoreId}", newStore.Id);
                return "Mağaza başarıyla oluşturuldu.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza oluşturma sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<string> UpdateStoreAsync(StoreUpdateDto storeUpdateDto, int storeId, int sellerId)
        {
            try
            {
                _logger.LogInformation("Mağaza güncelleme işlemi başlatıldı. Mağaza ID: {StoreId}", storeId);

                var store = await _storeRepository.GetByIdAsync(storeId);
                if (store == null || store.OwnerId != sellerId)
                {
                    throw new UnauthorizedAccessException("Bu mağazayı güncelleme yetkiniz yok.");
                }

                _mapper.Map(storeUpdateDto, store);
                await _storeRepository.UpdateAsync(store);

                _logger.LogInformation("Mağaza bilgileri güncellendi. Mağaza ID: {StoreId}", storeId);
                return "Mağaza bilgileri başarıyla güncellendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza güncelleme sırasında hata oluştu.");
                throw;
            }
        }

        public async Task<string> SetStoreStatusAsync(int storeId, bool isActive, int sellerId)
        {
            try
            {
                _logger.LogInformation("Mağaza durumu değiştiriliyor. Mağaza ID: {StoreId}, Yeni Durum: {IsActive}", storeId, isActive);

                var store = await _storeRepository.GetByIdAsync(storeId);
                if (store == null || store.OwnerId != sellerId)
                {
                    throw new UnauthorizedAccessException("Bu mağazayı yönetme yetkiniz yok.");
                }

                store.IsApproved = isActive;
                await _storeRepository.UpdateAsync(store);

                _logger.LogInformation("Mağaza durumu değiştirildi. Mağaza ID: {StoreId}", storeId);
                return "Mağaza durumu başarıyla değiştirildi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza durumu değiştirirken hata oluştu.");
                throw;
            }
        }

        public async Task<string> AddPaymentMethodAsync(StorePaymentMethodDto paymentMethodDto, int storeId)
        {
            try
            {
                _logger.LogInformation("Mağazaya ödeme yöntemi ekleniyor. Mağaza ID: {StoreId}", storeId);

                var store = await _storeRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    throw new Exception("Mağaza bulunamadı.");
                }

                var newPaymentMethod = _mapper.Map<StorePaymentMethod>(paymentMethodDto);
                newPaymentMethod.StoreId = storeId;

                await _storeRepository.AddPaymentMethodAsync(newPaymentMethod);
                _logger.LogInformation("Ödeme yöntemi eklendi. Mağaza ID: {StoreId}", storeId);
                return "Ödeme yöntemi başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ödeme yöntemi eklerken hata oluştu.");
                throw;
            }
        }

        public async Task<string> AddDeliveryOptionAsync(StoreDeliveryOptionDto deliveryOptionDto, int storeId)
        {
            try
            {
                _logger.LogInformation("Teslimat seçeneği ekleniyor. Mağaza ID: {StoreId}", storeId);

                var store = await _storeRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    throw new Exception("Mağaza bulunamadı.");
                }

                var newDeliveryOption = _mapper.Map<StoreCarrier>(deliveryOptionDto);
                newDeliveryOption.StoreId = storeId;

                await _storeRepository.AddDeliveryOptionAsync(newDeliveryOption);
                _logger.LogInformation("Teslimat seçeneği eklendi. Mağaza ID: {StoreId}", storeId);
                return "Teslimat seçeneği başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Teslimat seçeneği eklerken hata oluştu.");
                throw;
            }
        }

        public async Task<string> AddMarketAsync(StoreMarketDto marketDto, int storeId)
        {
            try
            {
                _logger.LogInformation("Mağazaya market ekleniyor. Mağaza ID: {StoreId}", storeId);

                var store = await _storeRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    throw new Exception("Mağaza bulunamadı.");
                }

                var newMarket = _mapper.Map<StoreMarket>(marketDto);
                newMarket.StoreId = storeId;

                await _storeRepository.AddMarketAsync(newMarket);
                _logger.LogInformation("Market eklendi. Mağaza ID: {StoreId}", storeId);
                return "Market başarıyla eklendi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Market eklerken hata oluştu.");
                throw;
            }
        }
    }
}
