using AutoMapper;
using Data.Dtos.Forms;
using Entity.Forms;
using Microsoft.Extensions.Logging;
using Repository.Forms.IRepositorys;
using Services.Forms.IServices;

namespace Services.Forms.Services
{
    public class BuyerApplicationService : IBuyerApplicationService
    {
        private readonly IBuyerApplicationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<BuyerApplicationService> _logger;

        public BuyerApplicationService(IBuyerApplicationRepository repository, IMapper mapper, ILogger<BuyerApplicationService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> CreateBuyerApplicationAsync(BuyerApplicationCreateDto dto, string ipAddress)
        {
            try
            {
                if (dto == null)
                {
                    _logger.LogWarning("Tedarik talebi oluşturulamadı: DTO null.");
                    return false;
                }

                var entity = _mapper.Map<BuyerApplication>(dto);
                entity.GuidId = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;
                entity.UserIpAddress = ipAddress;

                await _repository.AddAsync(entity);

                _logger.LogInformation("Yeni tedarik talebi oluşturuldu. TalepNo: {GuidId}, IP: {Ip}", entity.GuidId, ipAddress);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarik talebi oluşturulurken hata oluştu. IP: {Ip}", ipAddress);
                return false;
            }
        }

        public async Task<IEnumerable<BuyerApplicationDto>> GetApplicationsByBuyerAsync(Guid buyerUserId)
        {
            try
            {
                var result = await _repository.FindAsync(x => x.GuidId == buyerUserId);
                _logger.LogInformation("{Count} adet tedarik talebi bulundu. BuyerId: {BuyerId}", result.Count(), buyerUserId);

                return _mapper.Map<IEnumerable<BuyerApplicationDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "BuyerId bazlı tedarik talepleri alınırken hata oluştu. BuyerId: {BuyerId}", buyerUserId);
                return Enumerable.Empty<BuyerApplicationDto>();
            }
        }

        public async Task<IEnumerable<BuyerApplicationDto>> GetAllApplicationsAsync()
        {
            try
            {
                var result = await _repository.GetAllAsync();
                _logger.LogInformation("Tüm tedarik talepleri listelendi. Toplam: {Count}", result.Count());

                return _mapper.Map<IEnumerable<BuyerApplicationDto>>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm tedarik talepleri listelenirken hata oluştu.");
                return Enumerable.Empty<BuyerApplicationDto>();
            }
        }

        public async Task<BuyerApplicationDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Belirtilen ID ile tedarik talebi bulunamadı. ID: {Id}", id);
                    return null;
                }

                _logger.LogInformation("Tedarik talebi detayları getirildi. ID: {Id}", id);
                return _mapper.Map<BuyerApplicationDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarik talebi getirilirken hata oluştu. ID: {Id}", id);
                return null;
            }
        }

        public async Task<bool> MarkAsFulfilledAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Fulfilled olarak işaretlenmek istenen talep bulunamadı. ID: {Id}", id);
                    return false;
                }

                entity.IsFulfilled = true;
                var result = await _repository.UpdateBoolAsync(entity);

                _logger.LogInformation("Tedarik talebi fulfilled olarak işaretlendi. ID: {Id}", id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tedarik talebi fulfilled olarak işaretlenirken hata oluştu. ID: {Id}", id);
                return false;
            }
        }
    }
}
