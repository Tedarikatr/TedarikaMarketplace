using AutoMapper;
using Data.Dtos.Stores;
using Entity.Stores.Markets;
using Microsoft.Extensions.Logging;
using Repository.Stores.Markets.IRepositorys;
using Services.Markets.Location;
using Services.Stores.Markets.IServices;

namespace Services.Stores.Markets.Services
{
    public class StoreMarketCoverageService : IStoreMarketCoverageService
    {
        private readonly IStoreMarketCoverageRepository _coverageRepo;
        private readonly ILocationService _locationService;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreMarketCoverageService> _logger;

        public StoreMarketCoverageService(
            IStoreMarketCoverageRepository coverageRepo,
            ILocationService locationService,
            IMapper mapper,
            ILogger<StoreMarketCoverageService> logger)
        {
            _coverageRepo = coverageRepo;
            _locationService = locationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddCoverageAsync(StoreMarketCoverageCreateDto dto)
        {
            await ValidateLocationAsync(dto); 

            if (await CoverageExistsAsync(dto))
            {
                _logger.LogWarning("Aynı kapsama alanı daha önce tanımlanmış.");
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
                x.NeighborhoodId == dto.NeighborhoodId &&
                x.IsActive
            );

            return existing.Any();
        }

        private async Task ValidateLocationAsync(StoreMarketCoverageCreateDto dto)
        {
            if (dto.CountryId.HasValue)
            {
                var countries = await _locationService.GetAllCountriesAsync();
                if (!countries.Any(c => c.Id == dto.CountryId && c.IsActive))
                    throw new InvalidOperationException("Seçilen ülke aktif değil veya bulunamadı.");
            }

            if (dto.ProvinceId.HasValue)
            {
                var provinces = await _locationService.GetProvincesByCountryIdAsync(dto.CountryId ?? 0);
                if (!provinces.Any(p => p.Id == dto.ProvinceId && p.IsActive))
                    throw new InvalidOperationException("Seçilen il aktif değil veya bulunamadı.");
            }

            if (dto.DistrictId.HasValue)
            {
                var districts = await _locationService.GetDistrictsByProvinceIdAsync(dto.ProvinceId ?? 0);
                if (!districts.Any(d => d.Id == dto.DistrictId && d.IsActive))
                    throw new InvalidOperationException("Seçilen ilçe aktif değil veya bulunamadı.");
            }

            if (dto.NeighborhoodId.HasValue)
            {
                var neighborhoods = await _locationService.GetNeighborhoodsByDistrictIdAsync(dto.DistrictId ?? 0);
                if (!neighborhoods.Any(n => n.Id == dto.NeighborhoodId && n.IsActive))
                    throw new InvalidOperationException("Seçilen mahalle aktif değil veya bulunamadı.");
            }
        }
    }
}
