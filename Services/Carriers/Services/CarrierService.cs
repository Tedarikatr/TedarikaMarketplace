using AutoMapper;
using Data.Dtos.Carriers;
using Entity.Carriers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Carriers.IRepositorys;
using Services.Carriers.IServices;

namespace Services.Carriers.Services
{

    public class CarrierService : ICarrierService
    {
        private readonly ICarrierRepository _carrierRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CarrierService> _logger;

        public CarrierService(
            ICarrierRepository carrierRepository,
            IMapper mapper,
            ILogger<CarrierService> logger)
        {
            _carrierRepository = carrierRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CarrierDto>> GetAllCarriersAsync()
        {
            try
            {
                var carriers = await _carrierRepository.GetQueryable()
                    .Where(c => c.IsActive)
                    .ToListAsync();

                _logger.LogInformation("Tüm aktif kargo firmaları listelendi. Toplam: {Count}", carriers.Count);

                return _mapper.Map<List<CarrierDto>>(carriers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firmaları listelenirken hata oluştu.");
                throw;
            }
        }

        public async Task<CarrierDto> GetCarrierByIdAsync(int id)
        {
            try
            {
                var carrier = await _carrierRepository.GetByIdAsync(id);
                if (carrier == null)
                {
                    _logger.LogWarning("CarrierId {CarrierId} ile kargo firması bulunamadı.", id);
                    throw new KeyNotFoundException("Kargo firması bulunamadı.");
                }

                _logger.LogInformation("CarrierId {CarrierId} ile kargo firması getirildi.", id);
                return _mapper.Map<CarrierDto>(carrier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması detayları alınırken hata oluştu. Id: {CarrierId}", id);
                throw;
            }
        }

        public async Task CreateCarrierAsync(CarrierCreateDto dto)
        {
            try
            {
                var entity = _mapper.Map<Carrier>(dto);
                await _carrierRepository.AddAsync(entity);

                _logger.LogInformation("Yeni kargo firması eklendi. Ad: {CarrierName}, Kod: {CarrierCode}", dto.Name, dto.CarrierCode);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<bool> DeleteCarrierAsync(int id)
        {
            try
            {
                var carrier = await _carrierRepository.GetByIdAsync(id);
                if (carrier == null)
                {
                    _logger.LogWarning("Silinecek kargo firması bulunamadı. Id: {CarrierId}", id);
                    return false;
                }

                await _carrierRepository.RemoveAsync(carrier);
                _logger.LogInformation("Kargo firması silindi. Id: {CarrierId}", id);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kargo firması silinirken hata oluştu. Id: {CarrierId}", id);
                throw;
            }
        }
    } 
 }
