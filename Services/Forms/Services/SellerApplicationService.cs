using AutoMapper;
using Data.Dtos.Forms;
using Entity.Forms;
using Microsoft.Extensions.Logging;
using Repository.Forms.IRepositorys;
using Services.Forms.IServices;

namespace Services.Forms.Services
{
    public class SellerApplicationService : ISellerApplicationService
    {
        private readonly ISellerApplicationRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<SellerApplicationService> _logger;

        public SellerApplicationService(
            ISellerApplicationRepository repository,
            IMapper mapper,
            ILogger<SellerApplicationService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateAsync(SellerApplicationCreateDto dto)
        {
            try
            {
                var entity = _mapper.Map<SellerApplication>(dto);
                entity.GuidId = Guid.NewGuid();
                await _repository.AddAsync(entity);

                _logger.LogInformation("Yeni satıcı başvurusu oluşturuldu. StoreName: {StoreName}, GuidId: {GuidId}",
                    entity.StoreName, entity.GuidId);

                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı başvurusu oluşturulurken hata oluştu. StoreName: {StoreName}", dto.StoreName);
                throw;
            }
        }

        public async Task<SellerApplicationDetailDto?> GetByGuidAsync(Guid guidId)
        {
            try
            {
                var entity = await _repository.GetByGuidAsync(guidId);

                if (entity == null)
                {
                    _logger.LogWarning("Belirtilen GUID ile satıcı başvurusu bulunamadı. GuidId: {GuidId}", guidId);
                    return null;
                }

                return _mapper.Map<SellerApplicationDetailDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Guid ile başvuru getirilirken hata oluştu. GuidId: {GuidId}", guidId);
                throw;
            }
        }

        public async Task<List<SellerApplicationListDto>> GetAllAsync()
        {
            try
            {
                var list = await _repository.GetAllAsync();
                return list.Select(_mapper.Map<SellerApplicationListDto>).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm satıcı başvuruları getirilirken hata oluştu.");
                throw;
            }
        }

        public async Task<SellerApplicationDetailDto?> GetDetailByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);

                if (entity == null)
                {
                    _logger.LogWarning("ID ile satıcı başvurusu bulunamadı. Id: {Id}", id);
                    return null;
                }

                return _mapper.Map<SellerApplicationDetailDto>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ID ile başvuru detayları getirilirken hata oluştu. Id: {Id}", id);
                throw;
            }
        }

        public async Task<bool> UpdateApprovalAsync(SellerApplicationUpdateApprovalDto dto)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    _logger.LogWarning("Onaylanmak istenen başvuru bulunamadı. Id: {Id}", dto.Id);
                    return false;
                }

                entity.IsApproved = dto.IsApproved;
                entity.Notes = dto.Notes;

                var result = await _repository.UpdateBoolAsync(entity);

                _logger.LogInformation("Başvuru onayı güncellendi. Id: {Id}, IsApproved: {IsApproved}", dto.Id, dto.IsApproved);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru onayı güncellenirken hata oluştu. Id: {Id}", dto.Id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Silinmek istenen başvuru bulunamadı. Id: {Id}", id);
                    return false;
                }

                var result = await _repository.RemoveBoolAsync(entity);

                _logger.LogInformation("Satıcı başvurusu silindi. Id: {Id}", id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı başvurusu silinirken hata oluştu. Id: {Id}", id);
                throw;
            }
        }
    }
}
