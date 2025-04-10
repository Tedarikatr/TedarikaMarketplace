using AutoMapper;
using Data.Dtos.Stores;
using Entity.Stores.Markets;
using Microsoft.Extensions.Logging;
using Repository.Stores.Markets.IRepositorys;
using Services.Stores.Markets.IServices;

namespace Services.Stores.Markets.Services
{
    public class StoreMarketCoverageService : IStoreMarketCoverageService
    {
        private readonly IStoreMarketCoverageRepository _coverageRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreMarketCoverageService> _logger;

        public StoreMarketCoverageService(
            IStoreMarketCoverageRepository coverageRepo,
            IMapper mapper,
            ILogger<StoreMarketCoverageService> logger)
        {
            _coverageRepo = coverageRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddCoverageAsync(StoreMarketCoverageCreateDto dto)
        {
            if (await CoverageExistsAsync(dto))
            {
                _logger.LogWarning("Aynı kapsama daha önce eklenmiş.");
                throw new InvalidOperationException("Bu hizmet bölgesi zaten tanımlanmış.");
            }

            var entity = _mapper.Map<StoreMarketCoverage>(dto);
            entity.IsActive = true;

            await _coverageRepo.AddAsync(entity);
            _logger.LogInformation("StoreMarketCoverage eklendi. ID: {Id}", entity.Id);
            return entity.Id;
        }

        public async Task<bool> RemoveCoverageAsync(int id)
        {
            var entity = await _coverageRepo.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Silinmek istenen coverage bulunamadı. ID: {Id}", id);
                return false;
            }

            return await _coverageRepo.RemoveBoolAsync(entity);
        }

        public async Task<bool> UpdateCoverageStatusAsync(int id, bool isActive)
        {
            var entity = await _coverageRepo.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Durumu güncellenecek coverage bulunamadı. ID: {Id}", id);
                return false;
            }

            entity.IsActive = isActive;
            return await _coverageRepo.UpdateBoolAsync(entity);
        }

        public async Task<List<StoreMarketCoverageDto>> GetStoreCoveragesAsync(int storeMarketId, bool? onlyActive = null)
        {
            var coverages = await _coverageRepo.FindAsync(x =>
                x.StoreMarketId == storeMarketId &&
                (onlyActive == null || x.IsActive == onlyActive.Value)
            );

            return _mapper.Map<List<StoreMarketCoverageDto>>(coverages);
        }

        public async Task<bool> CoverageExistsAsync(StoreMarketCoverageCreateDto dto)
        {
            var existing = await _coverageRepo.FindAsync(x =>
                x.StoreMarketId == dto.StoreMarketId &&
                x.CoverageLevel == dto.CoverageLevel &&
                x.CountryId == dto.CountryId &&
                x.ProvinceId == dto.ProvinceId &&
                x.DistrictId == dto.DistrictId &&
                x.NeighborhoodId == dto.NeighborhoodId
            );

            return existing.Any();
        }
    }
}
