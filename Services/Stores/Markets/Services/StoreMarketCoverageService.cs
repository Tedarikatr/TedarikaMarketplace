using AutoMapper;
using Data.Dtos.Stores.Markets;
using Entity.Stores.Markets;
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

        public async Task<int> AddCountryAsync(StoreMarketCountryCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni Country kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var country = await _masterCountryRepo.GetByIdAsync(dto.CountryId);
                if (country == null)
                    throw new Exception($"Country bulunamadı. Id: {dto.CountryId}");

                var entity = new StoreMarketCountry
                {
                    StoreId = dto.StoreId,
                    CountryId = dto.CountryId,
                    CountryName = country.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _countryRepo.AddAsync(entity);
                _logger.LogInformation("Country kapsamı başarıyla eklendi. Id: {Id}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Country kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<int> AddProvinceAsync(StoreMarketProvinceCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni Province kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var province = await _masterProvinceRepo.GetByIdAsync(dto.ProvinceId);
                if (province == null)
                    throw new Exception($"Province bulunamadı. Id: {dto.ProvinceId}");

                var entity = new StoreMarketProvince
                {
                    StoreId = dto.StoreId,
                    ProvinceId = dto.ProvinceId,
                    ProvinceName = province.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _provinceRepo.AddAsync(entity);
                _logger.LogInformation("Province kapsamı eklendi. Id: {Id}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Province kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<int> AddDistrictAsync(StoreMarketDistrictCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni District kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var district = await _masterDistrictRepo.GetByIdAsync(dto.DistrictId);
                if (district == null)
                    throw new Exception($"District bulunamadı. Id: {dto.DistrictId}");

                var entity = new StoreMarketDistrict
                {
                    StoreId = dto.StoreId,
                    DistrictId = dto.DistrictId,
                    DistrictName = district.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _districtRepo.AddAsync(entity);
                _logger.LogInformation("District kapsamı eklendi. Id: {Id}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "District kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<int> AddNeighborhoodAsync(StoreMarketNeighborhoodCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni Neighborhood kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var neighborhood = await _masterNeighborhoodRepo.GetByIdAsync(dto.NeighborhoodId);
                if (neighborhood == null)
                    throw new Exception($"Neighborhood bulunamadı. Id: {dto.NeighborhoodId}");

                var entity = new StoreMarketNeighborhood
                {
                    StoreId = dto.StoreId,
                    NeighborhoodId = dto.NeighborhoodId,
                    NeighborhoodName = neighborhood.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _neighborhoodRepo.AddAsync(entity);
                _logger.LogInformation("Neighborhood kapsamı eklendi. Id: {Id}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Neighborhood kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<int> AddRegionAsync(StoreMarketRegionCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni Region kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var region = await _masterRegionRepo.GetByIdAsync(dto.RegionId);
                if (region == null)
                    throw new Exception($"Region bulunamadı. Id: {dto.RegionId}");

                var entity = new StoreMarketRegion
                {
                    StoreId = dto.StoreId,
                    RegionId = dto.RegionId,
                    RegionName = region.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _regionRepo.AddAsync(entity);
                _logger.LogInformation("Region kapsamı başarıyla eklendi. Id: {Id}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Region kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<int> AddStateAsync(StoreMarketStateCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni State kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var state = await _masterStateRepo.GetByIdAsync(dto.StateId);
                if (state == null)
                    throw new Exception($"State bulunamadı. Id: {dto.StateId}");

                var entity = new StoreMarketState
                {
                    StoreId = dto.StoreId,
                    StateId = dto.StateId,
                    StateName = state.Name,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                };

                await _stateRepo.AddAsync(entity);
                _logger.LogInformation("State kapsamı başarıyla eklendi. Id: {Id}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "State kapsamı eklenirken hata oluştu.");
                throw;
            }
        }



        // Mutli

        public async Task<List<int>> AddCountriesMultiAsync(StoreMarketCountryMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu Country kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var countries = await _masterCountryRepo.FindAsync(x => dto.CountryIds.Contains(x.Id));
                var countryDict = countries.ToDictionary(x => x.Id, x => x.Name);

                foreach (var countryId in dto.CountryIds)
                {
                    if (!countryDict.TryGetValue(countryId, out var countryName))
                        continue;

                    var entity = new StoreMarketCountry
                    {
                        StoreId = dto.StoreId,
                        CountryId = countryId,
                        CountryName = countryName,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame,
                        IsActive = true
                    };

                    await _countryRepo.AddAsync(entity);
                    addedIds.Add(entity.Id);
                    _logger.LogInformation("Country kapsamı eklendi. Id: {Id}", entity.Id);
                }

                return addedIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu Country kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<List<int>> AddProvincesMultiAsync(StoreMarketProvinceMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu Province kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var provinces = await _masterProvinceRepo.FindAsync(x => dto.ProvinceIds.Contains(x.Id));
                var provinceDict = provinces.ToDictionary(x => x.Id, x => x.Name);

                foreach (var provinceId in dto.ProvinceIds)
                {
                    if (!provinceDict.TryGetValue(provinceId, out var provinceName))
                        continue;

                    var entity = new StoreMarketProvince
                    {
                        StoreId = dto.StoreId,
                        ProvinceId = provinceId,
                        ProvinceName = provinceName,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame,
                        IsActive = true
                    };

                    await _provinceRepo.AddAsync(entity);
                    addedIds.Add(entity.Id);
                    _logger.LogInformation("Province kapsamı eklendi. Id: {Id}", entity.Id);
                }

                return addedIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu Province kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<List<int>> AddDistrictsMultiAsync(StoreMarketDistrictMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu District kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var districts = await _masterDistrictRepo.FindAsync(x => dto.DistrictIds.Contains(x.Id));
                var districtDict = districts.ToDictionary(x => x.Id, x => x.Name);

                foreach (var districtId in dto.DistrictIds)
                {
                    if (!districtDict.TryGetValue(districtId, out var districtName))
                        continue;

                    var entity = new StoreMarketDistrict
                    {
                        StoreId = dto.StoreId,
                        DistrictId = districtId,
                        DistrictName = districtName,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame,
                        IsActive = true
                    };

                    await _districtRepo.AddAsync(entity);
                    addedIds.Add(entity.Id);
                    _logger.LogInformation("District kapsamı eklendi. Id: {Id}", entity.Id);
                }

                return addedIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu District kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<List<int>> AddNeighborhoodsMultiAsync(StoreMarketNeighborhoodMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu Neighborhood kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var neighborhoods = await _masterNeighborhoodRepo.FindAsync(x => dto.NeighborhoodIds.Contains(x.Id));
                var neighborhoodDict = neighborhoods.ToDictionary(x => x.Id, x => x.Name);

                foreach (var neighborhoodId in dto.NeighborhoodIds)
                {
                    if (!neighborhoodDict.TryGetValue(neighborhoodId, out var name))
                        continue;

                    var entity = new StoreMarketNeighborhood
                    {
                        StoreId = dto.StoreId,
                        NeighborhoodId = neighborhoodId,
                        NeighborhoodName = name,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame,
                        IsActive = true
                    };

                    await _neighborhoodRepo.AddAsync(entity);
                    addedIds.Add(entity.Id);
                    _logger.LogInformation("Neighborhood kapsamı eklendi. Id: {Id}", entity.Id);
                }

                return addedIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu Neighborhood kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<List<int>> AddRegionsMultiAsync(StoreMarketRegionMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu Region kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var regions = await _masterRegionRepo.FindAsync(x => dto.RegionIds.Contains(x.Id));
                var regionDict = regions.ToDictionary(x => x.Id, x => x.Name);

                foreach (var regionId in dto.RegionIds)
                {
                    if (!regionDict.TryGetValue(regionId, out var name))
                        continue;

                    var entity = new StoreMarketRegion
                    {
                        StoreId = dto.StoreId,
                        RegionId = regionId,
                        RegionName = name,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame,
                        IsActive = true
                    };

                    await _regionRepo.AddAsync(entity);
                    addedIds.Add(entity.Id);
                    _logger.LogInformation("Region kapsamı eklendi. Id: {Id}", entity.Id);
                }

                return addedIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu Region kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<List<int>> AddStatesMultiAsync(StoreMarketStateMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu State kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var states = await _masterStateRepo.FindAsync(x => dto.StateIds.Contains(x.Id));
                var stateDict = states.ToDictionary(x => x.Id, x => x.Name);

                foreach (var stateId in dto.StateIds)
                {
                    if (!stateDict.TryGetValue(stateId, out var name))
                        continue;

                    var entity = new StoreMarketState
                    {
                        StoreId = dto.StoreId,
                        StateId = stateId,
                        StateName = name,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame,
                        IsActive = true
                    };

                    await _stateRepo.AddAsync(entity);
                    addedIds.Add(entity.Id);
                    _logger.LogInformation("State kapsamı eklendi. Id: {Id}", entity.Id);
                }

                return addedIds;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu State kapsamı eklenirken hata oluştu.");
                throw;
            }
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
