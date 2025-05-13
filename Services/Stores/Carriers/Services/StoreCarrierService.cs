using AutoMapper;
using Data.Dtos.Stores.Carriers;
using Entity.Stores.Carriers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Stores.Carriers.IRepositorys;
using Services.Stores.Carriers.IServices;

namespace Services.Stores.Carriers.Services
{
    public class StoreCarrierService : IStoreCarrierService
    {
        private readonly IStoreCarrierRepository _storeCarrierRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreCarrierService> _logger;

        public StoreCarrierService(
            IStoreCarrierRepository storeCarrierRepository,
            IMapper mapper,
            ILogger<StoreCarrierService> logger)
        {
            _storeCarrierRepository = storeCarrierRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StoreCarrierDto>> GetStoreCarriersAsync(int storeId)
        {
            try
            {
                var carriers = await _storeCarrierRepository.GetQueryable()
                    .Where(sc => sc.StoreId == storeId)
                    .Include(sc => sc.Carrier)
                    .ToListAsync();

                _logger.LogInformation("StoreId {StoreId} için kargo firmaları listelendi.", storeId);
                return _mapper.Map<List<StoreCarrierDto>>(carriers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza kargo firmaları listelenirken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task<bool> AddCarrierToStoreAsync(StoreCarrierCreateDto dto)
        {
            try
            {
                var exists = await _storeCarrierRepository.GetQueryable()
                    .AnyAsync(sc => sc.StoreId == dto.StoreId && sc.CarrierId == dto.CarrierId);

                if (exists)
                {
                    _logger.LogWarning("StoreId {StoreId} için CarrierId {CarrierId} zaten atanmış.", dto.StoreId, dto.CarrierId);
                    return false;
                }

                var entity = _mapper.Map<StoreCarrier>(dto);
                await _storeCarrierRepository.AddAsync(entity);

                _logger.LogInformation("StoreId {StoreId} için CarrierId {CarrierId} başarıyla eklendi.", dto.StoreId, dto.CarrierId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "StoreCarrier eklenirken hata oluştu. StoreId: {StoreId}, CarrierId: {CarrierId}", dto.StoreId, dto.CarrierId);
                throw;
            }
        }

        public async Task<bool> RemoveCarrierFromStoreAsync(int storeCarrierId)
        {
            try
            {
                var entity = await _storeCarrierRepository.GetByIdAsync(storeCarrierId);
                if (entity == null)
                {
                    _logger.LogWarning("StoreCarrier bulunamadı. Id: {StoreCarrierId}", storeCarrierId);
                    return false;
                }

                await _storeCarrierRepository.RemoveAsync(entity);
                _logger.LogInformation("StoreCarrier silindi. Id: {StoreCarrierId}", storeCarrierId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "StoreCarrier silinirken hata oluştu. Id: {StoreCarrierId}", storeCarrierId);
                throw;
            }
        }

        public async Task<bool> EnableCarrierAsync(int storeCarrierId)
        {
            try
            {
                var entity = await _storeCarrierRepository.GetByIdAsync(storeCarrierId);
                if (entity == null)
                {
                    _logger.LogWarning("Aktif edilecek StoreCarrier bulunamadı. Id: {StoreCarrierId}", storeCarrierId);
                    return false;
                }

                entity.IsEnabled = true;
                await _storeCarrierRepository.UpdateAsync(entity);
                _logger.LogInformation("StoreCarrier aktif hale getirildi. Id: {StoreCarrierId}", storeCarrierId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "StoreCarrier aktif edilirken hata oluştu. Id: {StoreCarrierId}", storeCarrierId);
                throw;
            }
        }

        public async Task<bool> DisableCarrierAsync(int storeCarrierId)
        {
            try
            {
                var entity = await _storeCarrierRepository.GetByIdAsync(storeCarrierId);
                if (entity == null)
                {
                    _logger.LogWarning("Pasif edilecek StoreCarrier bulunamadı. Id: {StoreCarrierId}", storeCarrierId);
                    return false;
                }

                entity.IsEnabled = false;
                await _storeCarrierRepository.UpdateAsync(entity);
                _logger.LogInformation("StoreCarrier pasif hale getirildi. Id: {StoreCarrierId}", storeCarrierId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "StoreCarrier pasif edilirken hata oluştu. Id: {StoreCarrierId}", storeCarrierId);
                throw;
            }
        }
    }
}
