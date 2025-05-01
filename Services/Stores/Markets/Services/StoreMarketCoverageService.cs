using AutoMapper;
using Data.Dtos.Stores.Markets;
using Entity.Stores.Markets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Markets.IRepositorys;
using Repository.Stores.Markets.IRepositorys;
using Services.Stores.Markets.IServices;

namespace Services.Stores.Markets.Services
{
    public class StoreMarketCoverageService : IStoreMarketCoverageService
    {

        private readonly IStoreMarketCountryRepository _countryRepo;
        private readonly IStoreMarketProvinceRepository _provinceRepo;
        private readonly IStoreMarketDistrictRepository _districtRepo;
        private readonly IStoreMarketNeighborhoodRepository _neighborhoodRepo;
        private readonly IStoreMarketRegionRepository _regionRepo;
        private readonly IStoreMarketStateRepository _stateRepo;
        private readonly ICountryRepository _masterCountryRepo;
        private readonly IProvinceRepository _masterProvinceRepo;
        private readonly IDistrictRepository _masterDistrictRepo;
        private readonly INeighborhoodRepository _masterNeighborhoodRepo;
        private readonly IStateRepository _masterStateRepo;
        private readonly IRegionRepository _masterRegionRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreMarketCoverageService> _logger;

        public StoreMarketCoverageService(IStoreMarketCountryRepository countryRepo, IStoreMarketProvinceRepository provinceRepo, IStoreMarketDistrictRepository districtRepo, IStoreMarketNeighborhoodRepository neighborhoodRepo, IStoreMarketRegionRepository regionRepo, IStoreMarketStateRepository stateRepo, ICountryRepository masterCountryRepo, IProvinceRepository masterProvinceRepo, IDistrictRepository masterDistrictRepo, INeighborhoodRepository masterNeighborhoodRepo, IStateRepository masterStateRepo, IRegionRepository masterRegionRepo, IMapper mapper, ILogger<StoreMarketCoverageService> logger)
        {
            _countryRepo = countryRepo;
            _provinceRepo = provinceRepo;
            _districtRepo = districtRepo;
            _neighborhoodRepo = neighborhoodRepo;
            _regionRepo = regionRepo;
            _stateRepo = stateRepo;
            _masterCountryRepo = masterCountryRepo;
            _masterProvinceRepo = masterProvinceRepo;
            _masterDistrictRepo = masterDistrictRepo;
            _masterNeighborhoodRepo = masterNeighborhoodRepo;
            _masterStateRepo = masterStateRepo;
            _masterRegionRepo = masterRegionRepo;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<int>> AddCompositeCoverageAsync(StoreMarketCoverageCompositeCreateDto dto)
        {
            var addedIds = new List<int>();

            // 1️⃣ Ülkeler
            foreach (var countryId in dto.CountryIds.Distinct())
            {
                bool exists = await _countryRepo.GetQueryable().AnyAsync(x => x.StoreId == dto.StoreId && x.CountryId == countryId);
                if (exists)
                {
                    _logger.LogInformation("Country zaten eklenmiş. StoreId: {StoreId}, CountryId: {CountryId}", dto.StoreId, countryId);
                    continue;
                }

                var country = await _masterCountryRepo.GetByIdAsync(countryId);
                if (country == null) continue;

                var entity = new StoreMarketCountry
                {
                    StoreId = dto.StoreId,
                    CountryId = countryId,
                    CountryName = country.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _countryRepo.AddAsync(entity);
                addedIds.Add(entity.Id);

                if (dto.CascadeProvinceFromCountry)
                {
                    var provinces = await _masterProvinceRepo.FindAsync(p => p.CountryId == countryId);
                    dto.ProvinceIds.AddRange(provinces.Select(p => p.Id));
                }
            }

            // 2️⃣ İller
            foreach (var provinceId in dto.ProvinceIds.Distinct())
            {
                bool exists = await _provinceRepo.GetQueryable().AnyAsync(x => x.StoreId == dto.StoreId && x.ProvinceId == provinceId);
                if (exists)
                {
                    _logger.LogInformation("Province zaten eklenmiş. StoreId: {StoreId}, ProvinceId: {ProvinceId}", dto.StoreId, provinceId);
                    continue;
                }

                var province = await _masterProvinceRepo.GetByIdAsync(provinceId);
                if (province == null) continue;

                var entity = new StoreMarketProvince
                {
                    StoreId = dto.StoreId,
                    ProvinceId = provinceId,
                    ProvinceName = province.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _provinceRepo.AddAsync(entity);
                addedIds.Add(entity.Id);

                if (dto.CascadeDistrictFromProvince)
                {
                    var districts = await _masterDistrictRepo.FindAsync(d => d.ProvinceId == provinceId);
                    dto.DistrictIds.AddRange(districts.Select(d => d.Id));
                }
            }

            // 3️⃣ İlçeler
            foreach (var districtId in dto.DistrictIds.Distinct())
            {
                bool exists = await _districtRepo.GetQueryable().AnyAsync(x => x.StoreId == dto.StoreId && x.DistrictId == districtId);
                if (exists)
                {
                    _logger.LogInformation("District zaten eklenmiş. StoreId: {StoreId}, DistrictId: {DistrictId}", dto.StoreId, districtId);
                    continue;
                }

                var district = await _masterDistrictRepo.GetByIdAsync(districtId);
                if (district == null) continue;

                var entity = new StoreMarketDistrict
                {
                    StoreId = dto.StoreId,
                    DistrictId = districtId,
                    DistrictName = district.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _districtRepo.AddAsync(entity);
                addedIds.Add(entity.Id);

                if (dto.CascadeNeighborhoodFromDistrict)
                {
                    var neighborhoods = await _masterNeighborhoodRepo.FindAsync(n => n.DistrictId == districtId);
                    dto.NeighborhoodIds.AddRange(neighborhoods.Select(n => n.Id));
                }
            }

            // 4️⃣ Mahalleler
            foreach (var neighborhoodId in dto.NeighborhoodIds.Distinct())
            {
                bool exists = await _neighborhoodRepo.GetQueryable().AnyAsync(x => x.StoreId == dto.StoreId && x.NeighborhoodId == neighborhoodId);
                if (exists)
                {
                    _logger.LogInformation("Neighborhood zaten eklenmiş. StoreId: {StoreId}, NeighborhoodId: {NeighborhoodId}", dto.StoreId, neighborhoodId);
                    continue;
                }

                var neighborhood = await _masterNeighborhoodRepo.GetByIdAsync(neighborhoodId);
                if (neighborhood == null) continue;

                var entity = new StoreMarketNeighborhood
                {
                    StoreId = dto.StoreId,
                    NeighborhoodId = neighborhoodId,
                    NeighborhoodName = neighborhood.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _neighborhoodRepo.AddAsync(entity);
                addedIds.Add(entity.Id);
            }

            // 5️⃣ Eyaletler
            foreach (var stateId in dto.StateIds.Distinct())
            {
                bool exists = await _stateRepo.GetQueryable().AnyAsync(x => x.StoreId == dto.StoreId && x.StateId == stateId);
                if (exists)
                {
                    _logger.LogInformation("State zaten eklenmiş. StoreId: {StoreId}, StateId: {StateId}", dto.StoreId, stateId);
                    continue;
                }

                var state = await _masterStateRepo.GetByIdAsync(stateId);
                if (state == null) continue;

                var entity = new StoreMarketState
                {
                    StoreId = dto.StoreId,
                    StateId = stateId,
                    StateName = state.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _stateRepo.AddAsync(entity);
                addedIds.Add(entity.Id);
            }

            // 6️⃣ Bölgeler
            foreach (var regionId in dto.RegionIds.Distinct())
            {
                bool exists = await _regionRepo.GetQueryable().AnyAsync(x => x.StoreId == dto.StoreId && x.RegionId == regionId);
                if (exists)
                {
                    _logger.LogInformation("Region zaten eklenmiş. StoreId: {StoreId}, RegionId: {RegionId}", dto.StoreId, regionId);
                    continue;
                }

                var region = await _masterRegionRepo.GetByIdAsync(regionId);
                if (region == null) continue;

                var entity = new StoreMarketRegion
                {
                    StoreId = dto.StoreId,
                    RegionId = regionId,
                    RegionName = region.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _regionRepo.AddAsync(entity);
                addedIds.Add(entity.Id);
            }

            return addedIds;
        }

        public async Task<List<StoreMarketCountryDto>> GetCountrysByStoreIdAsync(int storeId)
        {
            try
            {
                var data = await _countryRepo.FindAsync(x => x.StoreId == storeId);
                return _mapper.Map<List<StoreMarketCountryDto>>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Country kapsamları getirilirken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task<List<StoreMarketProvinceDto>> GetProvincesByStoreIdAsync(int storeId)
        {
            try
            {
                var data = await _provinceRepo.FindAsync(x => x.StoreId == storeId);
                return _mapper.Map<List<StoreMarketProvinceDto>>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Province kapsamları getirilirken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task<List<StoreMarketDistrictDto>> GetDistrictsByStoreIdAsync(int storeId)
        {
            try
            {
                var data = await _districtRepo.FindAsync(x => x.StoreId == storeId);
                return _mapper.Map<List<StoreMarketDistrictDto>>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "District kapsamları getirilirken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task<List<StoreMarketNeighborhoodDto>> GetNeighborhoodsByStoreIdAsync(int storeId)
        {
            try
            {
                var data = await _neighborhoodRepo.FindAsync(x => x.StoreId == storeId);
                return _mapper.Map<List<StoreMarketNeighborhoodDto>>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Neighborhood kapsamları getirilirken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task<List<StoreMarketRegionDto>> GetRegionsByStoreIdAsync(int storeId)
        {
            try
            {
                var data = await _regionRepo.FindAsync(x => x.StoreId == storeId);
                return _mapper.Map<List<StoreMarketRegionDto>>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Region kapsamları getirilirken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task<List<StoreMarketStateDto>> GetStatesByStoreIdAsync(int storeId)
        {
            try
            {
                var data = await _stateRepo.FindAsync(x => x.StoreId == storeId);
                return _mapper.Map<List<StoreMarketStateDto>>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "State kapsamları getirilirken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task<bool> UpdateCountryAsync(StoreMarketCountryUpdateDto dto)
        {
            try
            {
                var entity = await _countryRepo.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    _logger.LogWarning("Country kapsamı bulunamadı. Id: {Id}", dto.Id);
                    return false;
                }
                _mapper.Map(dto, entity);
                await _countryRepo.UpdateAsync(entity);
                _logger.LogInformation("Country kapsamı güncellendi. Id: {Id}", dto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Country kapsamı güncellenirken hata oluştu. Id: {Id}", dto.Id);
                throw;
            }
        }

        public async Task<bool> UpdateProvinceAsync(StoreMarketProvinceUpdateDto dto)
        {
            try
            {
                var entity = await _provinceRepo.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    _logger.LogWarning("Province kapsamı bulunamadı. Id: {Id}", dto.Id);
                    return false;
                }
                _mapper.Map(dto, entity);
                await _provinceRepo.UpdateAsync(entity);
                _logger.LogInformation("Province kapsamı güncellendi. Id: {Id}", dto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Province kapsamı güncellenirken hata oluştu. Id: {Id}", dto.Id);
                throw;
            }
        }

        public async Task<bool> UpdateDistrictAsync(StoreMarketDistrictUpdateDto dto)
        {
            try
            {
                var entity = await _districtRepo.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    _logger.LogWarning("District kapsamı bulunamadı. Id: {Id}", dto.Id);
                    return false;
                }
                _mapper.Map(dto, entity);
                await _districtRepo.UpdateAsync(entity);
                _logger.LogInformation("District kapsamı güncellendi. Id: {Id}", dto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "District kapsamı güncellenirken hata oluştu. Id: {Id}", dto.Id);
                throw;
            }
        }

        public async Task<bool> UpdateNeighborhoodAsync(StoreMarketNeighborhoodUpdateDto dto)
        {
            try
            {
                var entity = await _neighborhoodRepo.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    _logger.LogWarning("Neighborhood kapsamı bulunamadı. Id: {Id}", dto.Id);
                    return false;
                }
                _mapper.Map(dto, entity);
                await _neighborhoodRepo.UpdateAsync(entity);
                _logger.LogInformation("Neighborhood kapsamı güncellendi. Id: {Id}", dto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Neighborhood kapsamı güncellenirken hata oluştu. Id: {Id}", dto.Id);
                throw;
            }
        }

        public async Task<bool> UpdateRegionAsync(StoreMarketRegionUpdateDto dto)
        {
            try
            {
                var entity = await _regionRepo.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    _logger.LogWarning("Region kapsamı bulunamadı. Id: {Id}", dto.Id);
                    return false;
                }
                _mapper.Map(dto, entity);
                await _regionRepo.UpdateAsync(entity);
                _logger.LogInformation("Region kapsamı güncellendi. Id: {Id}", dto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Region kapsamı güncellenirken hata oluştu. Id: {Id}", dto.Id);
                throw;
            }
        }

        public async Task<bool> UpdateStateAsync(StoreMarketStateUpdateDto dto)
        {
            try
            {
                var entity = await _stateRepo.GetByIdAsync(dto.Id);
                if (entity == null)
                {
                    _logger.LogWarning("State kapsamı bulunamadı. Id: {Id}", dto.Id);
                    return false;
                }
                _mapper.Map(dto, entity);
                await _stateRepo.UpdateAsync(entity);
                _logger.LogInformation("State kapsamı güncellendi. Id: {Id}", dto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "State kapsamı güncellenirken hata oluştu. Id: {Id}", dto.Id);
                throw;
            }
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            try
            {
                var entity = await _countryRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Country kapsamı bulunamadı. Id: {Id}", id);
                    return false;
                }
                await _countryRepo.RemoveAsync(entity);
                _logger.LogInformation("Country kapsamı silindi. Id: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Country kapsamı silinirken hata oluştu. Id: {Id}", id);
                throw;
            }
        }
        public async Task<bool> DeleteProvinceAsync(int id)
        {
            try
            {
                var entity = await _provinceRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Province kapsamı bulunamadı. Id: {Id}", id);
                    return false;
                }
                await _provinceRepo.RemoveAsync(entity);
                _logger.LogInformation("Province kapsamı silindi. Id: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Province kapsamı silinirken hata oluştu. Id: {Id}", id);
                throw;
            }
        }
        public async Task<bool> DeleteDistrictAsync(int id)
        {
            try
            {
                var entity = await _districtRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("District kapsamı bulunamadı. Id: {Id}", id);
                    return false;
                }
                await _districtRepo.RemoveAsync(entity);
                _logger.LogInformation("District kapsamı silindi. Id: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "District kapsamı silinirken hata oluştu. Id: {Id}", id);
                throw;
            }
        }
        public async Task<bool> DeleteNeighborhoodAsync(int id)
        {
            try
            {
                var entity = await _neighborhoodRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Neighborhood kapsamı bulunamadı. Id: {Id}", id);
                    return false;
                }
                await _neighborhoodRepo.RemoveAsync(entity);
                _logger.LogInformation("Neighborhood kapsamı silindi. Id: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Neighborhood kapsamı silinirken hata oluştu. Id: {Id}", id);
                throw;
            }
        }
        public async Task<bool> DeleteRegionAsync(int id)
        {
            try
            {
                var entity = await _regionRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("Region kapsamı bulunamadı. Id: {Id}", id);
                    return false;
                }
                await _regionRepo.RemoveAsync(entity);
                _logger.LogInformation("Region kapsamı silindi. Id: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Region kapsamı silinirken hata oluştu. Id: {Id}", id);
                throw;
            }
        }
        public async Task<bool> DeleteStateAsync(int id)
        {
            try
            {
                var entity = await _stateRepo.GetByIdAsync(id);
                if (entity == null)
                {
                    _logger.LogWarning("State kapsamı bulunamadı. Id: {Id}", id);
                    return false;
                }
                await _stateRepo.RemoveAsync(entity);
                _logger.LogInformation("State kapsamı silindi. Id: {Id}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "State kapsamı silinirken hata oluştu. Id: {Id}", id);
                throw;
            }
        }
    }
}
