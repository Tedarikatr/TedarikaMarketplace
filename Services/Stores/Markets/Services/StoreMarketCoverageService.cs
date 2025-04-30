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

                var data = await _masterCountryRepo.GetByIdAsync(dto.CountryId);
                if (data == null)
                    throw new Exception($"Country bulunamadı. Id: {dto.CountryId}");

                var entity = _mapper.Map<StoreMarketCountry>(dto);
                entity.CountryName = data.Name;
                entity.IsActive = true;

                await _countryRepo.AddAsync(entity);
                _logger.LogInformation("Country kapsamı eklendi. Id: {Id}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Country kapsamı eklenirken hata oluştu.");
                throw;
            }
        }

        public async Task<List<int>> AddCountrysMultiAsync(StoreMarketCountryMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu Country kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var list = await _masterCountryRepo.FindAsync(x => dto.CountryIds.Contains(x.Id));
                var dict = list.ToDictionary(x => x.Id, x => x.Name);

                foreach (var id in dto.CountryIds)
                {
                    if (!dict.TryGetValue(id, out var name))
                        continue;

                    var itemDto = new StoreMarketCountryCreateDto
                    {
                        StoreId = dto.StoreId,
                        CountryId = id,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame
                    };

                    var entity = _mapper.Map<StoreMarketCountry>(itemDto);
                    entity.CountryName = name;
                    entity.IsActive = true;

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


        public async Task<int> AddProvinceAsync(StoreMarketProvinceCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni Province kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var data = await _masterProvinceRepo.GetByIdAsync(dto.ProvinceId);
                if (data == null)
                    throw new Exception($"Province bulunamadı. Id: {dto.ProvinceId}");

                var entity = _mapper.Map<StoreMarketProvince>(dto);
                entity.ProvinceName = data.Name;
                entity.IsActive = true;

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


        public async Task<List<int>> AddProvincesMultiAsync(StoreMarketProvinceMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu Province kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var list = await _masterProvinceRepo.FindAsync(x => dto.ProvinceIds.Contains(x.Id));
                var dict = list.ToDictionary(x => x.Id, x => x.Name);

                foreach (var id in dto.ProvinceIds)
                {
                    if (!dict.TryGetValue(id, out var name))
                        continue;

                    var itemDto = new StoreMarketProvinceCreateDto
                    {
                        StoreId = dto.StoreId,
                        ProvinceId = id,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame
                    };

                    var entity = _mapper.Map<StoreMarketProvince>(itemDto);
                    entity.ProvinceName = name;
                    entity.IsActive = true;

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


        public async Task<int> AddDistrictAsync(StoreMarketDistrictCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni District kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var data = await _masterDistrictRepo.GetByIdAsync(dto.DistrictId);
                if (data == null)
                    throw new Exception($"District bulunamadı. Id: {dto.DistrictId}");

                var entity = _mapper.Map<StoreMarketDistrict>(dto);
                entity.DistrictName = data.Name;
                entity.IsActive = true;

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


        public async Task<List<int>> AddDistrictsMultiAsync(StoreMarketDistrictMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu District kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var list = await _masterDistrictRepo.FindAsync(x => dto.DistrictIds.Contains(x.Id));
                var dict = list.ToDictionary(x => x.Id, x => x.Name);

                foreach (var id in dto.DistrictIds)
                {
                    if (!dict.TryGetValue(id, out var name))
                        continue;

                    var itemDto = new StoreMarketDistrictCreateDto
                    {
                        StoreId = dto.StoreId,
                        DistrictId = id,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame
                    };

                    var entity = _mapper.Map<StoreMarketDistrict>(itemDto);
                    entity.DistrictName = name;
                    entity.IsActive = true;

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


        public async Task<int> AddNeighborhoodAsync(StoreMarketNeighborhoodCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni Neighborhood kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var data = await _masterNeighborhoodRepo.GetByIdAsync(dto.NeighborhoodId);
                if (data == null)
                    throw new Exception($"Neighborhood bulunamadı. Id: {dto.NeighborhoodId}");

                var entity = _mapper.Map<StoreMarketNeighborhood>(dto);
                entity.NeighborhoodName = data.Name;
                entity.IsActive = true;

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


        public async Task<List<int>> AddNeighborhoodsMultiAsync(StoreMarketNeighborhoodMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu Neighborhood kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var list = await _masterNeighborhoodRepo.FindAsync(x => dto.NeighborhoodIds.Contains(x.Id));
                var dict = list.ToDictionary(x => x.Id, x => x.Name);

                foreach (var id in dto.NeighborhoodIds)
                {
                    if (!dict.TryGetValue(id, out var name))
                        continue;

                    var itemDto = new StoreMarketNeighborhoodCreateDto
                    {
                        StoreId = dto.StoreId,
                        NeighborhoodId = id,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame
                    };

                    var entity = _mapper.Map<StoreMarketNeighborhood>(itemDto);
                    entity.NeighborhoodName = name;
                    entity.IsActive = true;

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


        public async Task<int> AddRegionAsync(StoreMarketRegionCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni Region kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var data = await _masterRegionRepo.GetByIdAsync(dto.RegionId);
                if (data == null)
                    throw new Exception($"Region bulunamadı. Id: {dto.RegionId}");

                var entity = _mapper.Map<StoreMarketRegion>(dto);
                entity.RegionName = data.Name;
                entity.IsActive = true;

                await _regionRepo.AddAsync(entity);
                _logger.LogInformation("Region kapsamı eklendi. Id: {Id}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Region kapsamı eklenirken hata oluştu.");
                throw;
            }
        }


        public async Task<List<int>> AddRegionsMultiAsync(StoreMarketRegionMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu Region kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var list = await _masterRegionRepo.FindAsync(x => dto.RegionIds.Contains(x.Id));
                var dict = list.ToDictionary(x => x.Id, x => x.Name);

                foreach (var id in dto.RegionIds)
                {
                    if (!dict.TryGetValue(id, out var name))
                        continue;

                    var itemDto = new StoreMarketRegionCreateDto
                    {
                        StoreId = dto.StoreId,
                        RegionId = id,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame
                    };

                    var entity = _mapper.Map<StoreMarketRegion>(itemDto);
                    entity.RegionName = name;
                    entity.IsActive = true;

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


        public async Task<int> AddStateAsync(StoreMarketStateCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni State kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var data = await _masterStateRepo.GetByIdAsync(dto.StateId);
                if (data == null)
                    throw new Exception($"State bulunamadı. Id: {dto.StateId}");

                var entity = _mapper.Map<StoreMarketState>(dto);
                entity.StateName = data.Name;
                entity.IsActive = true;

                await _stateRepo.AddAsync(entity);
                _logger.LogInformation("State kapsamı eklendi. Id: {Id}", entity.Id);
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "State kapsamı eklenirken hata oluştu.");
                throw;
            }
        }


        public async Task<List<int>> AddStatesMultiAsync(StoreMarketStateMultiCreateDto dto)
        {
            var addedIds = new List<int>();
            try
            {
                _logger.LogInformation("Toplu State kapsamı ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var list = await _masterStateRepo.FindAsync(x => dto.StateIds.Contains(x.Id));
                var dict = list.ToDictionary(x => x.Id, x => x.Name);

                foreach (var id in dto.StateIds)
                {
                    if (!dict.TryGetValue(id, out var name))
                        continue;

                    var itemDto = new StoreMarketStateCreateDto
                    {
                        StoreId = dto.StoreId,
                        StateId = id,
                        DeliveryTimeFrame = dto.DeliveryTimeFrame
                    };

                    var entity = _mapper.Map<StoreMarketState>(itemDto);
                    entity.StateName = name;
                    entity.IsActive = true;

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
