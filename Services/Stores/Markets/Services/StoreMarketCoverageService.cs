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

        public async Task<StoreMarketCoverageHierarchyDto> GetCoverageHierarchyByStoreIdAsync(int storeId)
        {
            _logger.LogInformation("📥 Store kapsam hiyerarşisi alınmaya çalışılıyor. StoreId: {StoreId}", storeId);

            var hierarchyDto = new StoreMarketCoverageHierarchyDto();

            try
            {
                // Ülkeler
                var countries = await _countryRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.Countries = _mapper.Map<List<StoreMarketCountryDto>>(countries);
                _logger.LogInformation("✅ {Count} ülke getirildi.", hierarchyDto.Countries.Count);

                // İller
                var provinces = await _provinceRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.Provinces = _mapper.Map<List<StoreMarketProvinceDto>>(provinces);
                _logger.LogInformation("✅ {Count} il getirildi.", hierarchyDto.Provinces.Count);

                // İlçeler
                var districts = await _districtRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.Districts = _mapper.Map<List<StoreMarketDistrictDto>>(districts);
                _logger.LogInformation("✅ {Count} ilçe getirildi.", hierarchyDto.Districts.Count);

                // Mahalleler
                var neighborhoods = await _neighborhoodRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.Neighborhoods = _mapper.Map<List<StoreMarketNeighborhoodDto>>(neighborhoods);
                _logger.LogInformation("✅ {Count} mahalle getirildi.", hierarchyDto.Neighborhoods.Count);

                // Eyaletler
                var states = await _stateRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.States = _mapper.Map<List<StoreMarketStateDto>>(states);
                _logger.LogInformation("✅ {Count} eyalet getirildi.", hierarchyDto.States.Count);

                // Bölgeler
                var regions = await _regionRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.Regions = _mapper.Map<List<StoreMarketRegionDto>>(regions);
                _logger.LogInformation("✅ {Count} bölge getirildi.", hierarchyDto.Regions.Count);

                _logger.LogInformation("🎯 Store kapsam verileri başarıyla yüklendi. StoreId: {StoreId}", storeId);
                return hierarchyDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Store kapsam hiyerarşisi getirilirken hata oluştu. StoreId: {StoreId}", storeId);
                throw new ApplicationException("Mağaza kapsam bilgileri alınırken bir hata oluştu.", ex);
            }
        }


        public async Task<bool> UpdateCoverageAsync(StoreMarketCoverageUpdateBaseDto dto, CoverageType type)
        {
            try
            {
                _logger.LogInformation("🔧 Kapsam güncelleme işlemi başlatıldı. Tip: {Type}, Id: {Id}", type, dto.Id);

                switch (type)
                {
                    case CoverageType.Country:
                        var country = await _countryRepo.GetByIdAsync(dto.Id);
                        if (country == null)
                        {
                            _logger.LogWarning("❗ Country bulunamadı. Id: {Id}", dto.Id);
                            return false;
                        }
                        country.DeliveryTimeFrame = dto.DeliveryTimeFrame;
                        country.IsActive = dto.IsActive;
                        await _countryRepo.UpdateAsync(country);
                        break;

                    case CoverageType.Province:
                        var province = await _provinceRepo.GetByIdAsync(dto.Id);
                        if (province == null)
                        {
                            _logger.LogWarning("❗ Province bulunamadı. Id: {Id}", dto.Id);
                            return false;
                        }
                        province.DeliveryTimeFrame = dto.DeliveryTimeFrame;
                        province.IsActive = dto.IsActive;
                        await _provinceRepo.UpdateAsync(province);
                        break;

                    case CoverageType.District:
                        var district = await _districtRepo.GetByIdAsync(dto.Id);
                        if (district == null)
                        {
                            _logger.LogWarning("❗ District bulunamadı. Id: {Id}", dto.Id);
                            return false;
                        }
                        district.DeliveryTimeFrame = dto.DeliveryTimeFrame;
                        district.IsActive = dto.IsActive;
                        await _districtRepo.UpdateAsync(district);
                        break;

                    case CoverageType.Neighborhood:
                        var neighborhood = await _neighborhoodRepo.GetByIdAsync(dto.Id);
                        if (neighborhood == null)
                        {
                            _logger.LogWarning("❗ Neighborhood bulunamadı. Id: {Id}", dto.Id);
                            return false;
                        }
                        neighborhood.DeliveryTimeFrame = dto.DeliveryTimeFrame;
                        neighborhood.IsActive = dto.IsActive;
                        await _neighborhoodRepo.UpdateAsync(neighborhood);
                        break;

                    case CoverageType.Region:
                        var region = await _regionRepo.GetByIdAsync(dto.Id);
                        if (region == null)
                        {
                            _logger.LogWarning("❗ Region bulunamadı. Id: {Id}", dto.Id);
                            return false;
                        }
                        region.DeliveryTimeFrame = dto.DeliveryTimeFrame;
                        region.IsActive = dto.IsActive;
                        await _regionRepo.UpdateAsync(region);
                        break;

                    case CoverageType.State:
                        var state = await _stateRepo.GetByIdAsync(dto.Id);
                        if (state == null)
                        {
                            _logger.LogWarning("❗ State bulunamadı. Id: {Id}", dto.Id);
                            return false;
                        }
                        state.DeliveryTimeFrame = dto.DeliveryTimeFrame;
                        state.IsActive = dto.IsActive;
                        await _stateRepo.UpdateAsync(state);
                        break;

                    default:
                        _logger.LogWarning("⚠️ Geçersiz coverage tipi girildi: {Type}", type);
                        return false;
                }

                _logger.LogInformation("✅ Kapsam güncelleme başarılı. Tip: {Type}, Id: {Id}", type, dto.Id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Kapsam güncellenirken hata oluştu. Tip: {Type}, Id: {Id}", type, dto.Id);
                throw new ApplicationException("Kapsam güncelleme işlemi başarısız oldu.", ex);
            }
        }


        public async Task<int> DeleteCompositeCoverageAsync(StoreMarketCoverageCompositeDeleteDto dto)
        {
            var totalDeleted = 0;

            try
            {
                _logger.LogInformation("📦 Composite silme işlemi başlatıldı. StoreId: {StoreId}", dto.StoreId);

                // 1️⃣ Ülkeler
                if (dto.CountryIds?.Any() == true)
                {
                    var entities = await _countryRepo.FindAsync(x => x.StoreId == dto.StoreId && dto.CountryIds.Contains(x.CountryId));
                    if (entities.Any())
                    {
                        await _countryRepo.RemoveRangeAsync(entities);
                        totalDeleted += entities.Count();
                        _logger.LogInformation("✅ {Count} ülke kapsamı silindi. StoreId: {StoreId}", entities.Count(), dto.StoreId);
                    }
                }

                // 2️⃣ İller
                if (dto.ProvinceIds?.Any() == true)
                {
                    var entities = await _provinceRepo.FindAsync(x => x.StoreId == dto.StoreId && dto.ProvinceIds.Contains(x.ProvinceId));
                    if (entities.Any())
                    {
                        await _provinceRepo.RemoveRangeAsync(entities);
                        totalDeleted += entities.Count();
                        _logger.LogInformation("✅ {Count} il kapsamı silindi. StoreId: {StoreId}", entities.Count(), dto.StoreId);
                    }
                }

                // 3️⃣ İlçeler
                if (dto.DistrictIds?.Any() == true)
                {
                    var entities = await _districtRepo.FindAsync(x => x.StoreId == dto.StoreId && dto.DistrictIds.Contains(x.DistrictId));
                    if (entities.Any())
                    {
                        await _districtRepo.RemoveRangeAsync(entities);
                        totalDeleted += entities.Count();
                        _logger.LogInformation("✅ {Count} ilçe kapsamı silindi. StoreId: {StoreId}", entities.Count(), dto.StoreId);
                    }
                }

                // 4️⃣ Mahalleler
                if (dto.NeighborhoodIds?.Any() == true)
                {
                    var entities = await _neighborhoodRepo.FindAsync(x => x.StoreId == dto.StoreId && dto.NeighborhoodIds.Contains(x.NeighborhoodId));
                    if (entities.Any())
                    {
                        await _neighborhoodRepo.RemoveRangeAsync(entities);
                        totalDeleted += entities.Count();
                        _logger.LogInformation("✅ {Count} mahalle kapsamı silindi. StoreId: {StoreId}", entities.Count(), dto.StoreId);
                    }
                }

                // 5️⃣ Eyaletler
                if (dto.StateIds?.Any() == true)
                {
                    var entities = await _stateRepo.FindAsync(x => x.StoreId == dto.StoreId && dto.StateIds.Contains(x.StateId));
                    if (entities.Any())
                    {
                        await _stateRepo.RemoveRangeAsync(entities);
                        totalDeleted += entities.Count();
                        _logger.LogInformation("✅ {Count} eyalet kapsamı silindi. StoreId: {StoreId}", entities.Count(), dto.StoreId);
                    }
                }

                // 6️⃣ Bölgeler
                if (dto.RegionIds?.Any() == true)
                {
                    var entities = await _regionRepo.FindAsync(x => x.StoreId == dto.StoreId && dto.RegionIds.Contains(x.RegionId));
                    if (entities.Any())
                    {
                        await _regionRepo.RemoveRangeAsync(entities);
                        totalDeleted += entities.Count();
                        _logger.LogInformation("✅ {Count} bölge kapsamı silindi. StoreId: {StoreId}", entities.Count(), dto.StoreId);
                    }
                }

                _logger.LogInformation("🧾 Composite silme işlemi tamamlandı. Toplam silinen kayıt: {TotalDeleted}. StoreId: {StoreId}", totalDeleted, dto.StoreId);
                return totalDeleted;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Composite silme sırasında beklenmeyen hata oluştu. StoreId: {StoreId}", dto.StoreId);
                throw new InvalidOperationException("Kapsam silme işlemi sırasında bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.", ex);
            }
        }

    }
}
