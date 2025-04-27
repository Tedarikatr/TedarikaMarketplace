using AutoMapper;
using Data.Dtos.Stores;
using Domain.Store.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Repository.Companys.IRepositorys;
using Repository.Stores;

namespace Services.Stores
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreService> _logger;

        public StoreService(IStoreRepository storeRepository, ICompanyRepository companyRepository, IMediator mediator, IMapper mapper, ILogger<StoreService> logger)
        {
            _storeRepository = storeRepository;
            _companyRepository = companyRepository;
            _mediator = mediator;
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

                var company = await _companyRepository.GetCompanyBySellerIdAsync(sellerId);
                if (company == null)
                    throw new Exception("Seller'a ait geçerli bir şirket ataması yapılmamış.");

                var newStore = _mapper.Map<Entity.Stores.Store>(storeCreateDto);
                newStore.OwnerId = sellerId;
                newStore.CompanyId = company.Id;
                newStore.IsApproved = false;
                newStore.IsActive = false;

                await _storeRepository.AddAsync(newStore);
                _logger.LogInformation("Yeni mağaza oluşturuldu. Mağaza ID: {StoreId}", newStore.Id);
                await _mediator.Publish(new StoreCreatedEvent(newStore.Id, sellerId));
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

                store.IsActive = isActive;
                await _storeRepository.UpdateAsync(store);

                _logger.LogInformation("Mağaza durumu değiştirildi. Mağaza ID: {StoreId}", storeId);
                return "Mağaza durumu başarıyla değiştirildi.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza durumu değiştirilirken hata oluştu.");
                throw;
            }
        }

        public async Task<IEnumerable<StoreDto>> GetAllStoresAsync()
        {
            try
            {
                _logger.LogInformation("Tüm mağazalar getiriliyor.");

                var stores = await _storeRepository.GetAllAsync();
                var storeDtos = _mapper.Map<IEnumerable<StoreDto>>(stores);

                _logger.LogInformation("Tüm mağazalar başarıyla getirildi.");
                return storeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm mağazalar getirilirken hata oluştu.");
                throw;
            }
        }

        public async Task<string> ApproveStoreAsync(int storeId, bool isApproved)
        {
            try
            {
                _logger.LogInformation("Mağaza onay durumu değiştiriliyor. Mağaza ID: {StoreId}, Yeni Durum: {IsApproved}", storeId, isApproved);

                var store = await _storeRepository.GetByIdAsync(storeId);
                if (store == null)
                {
                    throw new Exception("Mağaza bulunamadı.");
                }

                store.IsApproved = isApproved;
                await _storeRepository.UpdateAsync(store);

                _logger.LogInformation("Mağaza onay durumu değiştirildi. Mağaza ID: {StoreId}", storeId);
                return isApproved ? "Mağaza onaylandı." : "Mağaza onayı kaldırıldı.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza onay durumu değiştirilirken hata oluştu.");
                throw;
            }
        }
    }
}
