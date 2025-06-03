using AutoMapper;
using Data.Dtos.Stores.Locations;
using Domain.Stores.Events;
using Entity.Stores.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Locations.IRepositorys;
using Repository.Stores.Locations.IRepositorys;
using Services.Stores.Locations.IServices;

namespace Services.Stores.Locations.Services
{
    public class StoreLocationService : IStoreLocationService
    {

        private readonly IStoreLocationCoverageRepository _covargeRepo;
        private readonly IStoreLocationCountryRepository _countryRepo;
        private readonly IStoreLocationProvinceRepository _provinceRepo;
        private readonly IStoreLocationDistrictRepository _districtRepo;
        private readonly IStoreLocationNeighborhoodRepository _neighborhoodRepo;
        private readonly IStoreLocationRegionRepository _regionRepo;
        private readonly IStoreLocationStateRepository _stateRepo;
        private readonly ICountryRepository _masterCountryRepo;
        private readonly IProvinceRepository _masterProvinceRepo;
        private readonly IDistrictRepository _masterDistrictRepo;
        private readonly INeighborhoodRepository _masterNeighborhoodRepo;
        private readonly IStateRepository _masterStateRepo;
        private readonly IRegionRepository _masterRegionRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ILogger<StoreLocationService> _logger;

        public StoreLocationService(IStoreLocationCoverageRepository covargeRepo, IStoreLocationCountryRepository countryRepo, IStoreLocationProvinceRepository provinceRepo, IStoreLocationDistrictRepository districtRepo, IStoreLocationNeighborhoodRepository neighborhoodRepo, IStoreLocationRegionRepository regionRepo, IStoreLocationStateRepository stateRepo, ICountryRepository masterCountryRepo, IProvinceRepository masterProvinceRepo, IDistrictRepository masterDistrictRepo, INeighborhoodRepository masterNeighborhoodRepo, IStateRepository masterStateRepo, IRegionRepository masterRegionRepo, IMapper mapper, IMediator mediator, ILogger<StoreLocationService> logger)
        {
            _covargeRepo = covargeRepo;
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
            _mediator = mediator;
            _logger = logger;
        }

        public async Task<List<int>> AddCompositeCoverageAsync(StoreMarketCoverageCompositeCreateDto dto)
        {
            var addedIds = new List<int>();

            // 1️⃣ COUNTRY
            foreach (var countryId in dto.CountryIds.Distinct())
            {
                if (countryId <= 0) continue;

                var master = await _masterCountryRepo.GetByIdAsync(countryId);
                if (master is null) continue;

                bool exists = await _countryRepo.GetQueryable()
                    .AnyAsync(x => x.StoreId == dto.StoreId && x.CountryId == countryId);
                if (exists)
                {
                    _logger.LogInformation("Country already exists. StoreId: {StoreId}, CountryId: {CountryId}", dto.StoreId, countryId);
                    continue;
                }

                var entity = _mapper.Map<StoreLocationCountry>(new StoreMarketCountryCreateDto
                {
                    StoreId = dto.StoreId,
                    CountryId = countryId,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame
                });
                entity.CountryName = master.Name;

                await _countryRepo.AddAsync(entity);
                await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

                addedIds.Add(entity.Id);

           
            }

            // 2️⃣ PROVINCE
            foreach (var provinceId in dto.ProvinceIds.Distinct())
            {
                if (provinceId <= 0) continue;

                var master = await _masterProvinceRepo.GetByIdAsync(provinceId);
                if (master is null) continue;

                bool exists = await _provinceRepo.GetQueryable()
                    .AnyAsync(x => x.StoreId == dto.StoreId && x.ProvinceId == provinceId);
                if (exists)
                {
                    _logger.LogInformation("Province already exists. StoreId: {StoreId}, ProvinceId: {ProvinceId}", dto.StoreId, provinceId);
                    continue;
                }

                var entity = _mapper.Map<StoreLocationProvince>(new StoreMarketProvinceCreateDto
                {
                    StoreId = dto.StoreId,
                    ProvinceId = provinceId,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame
                });
                entity.ProvinceName = master.Name;

                await _provinceRepo.AddAsync(entity);
                await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

                addedIds.Add(entity.Id);

            }

            // 3️⃣ DISTRICT
            foreach (var districtId in dto.DistrictIds.Distinct())
            {
                if (districtId <= 0) continue;

                var master = await _masterDistrictRepo.GetByIdAsync(districtId);
                if (master is null) continue;

                bool exists = await _districtRepo.GetQueryable()
                    .AnyAsync(x => x.StoreId == dto.StoreId && x.DistrictId == districtId);
                if (exists)
                {
                    _logger.LogInformation("District already exists. StoreId: {StoreId}, DistrictId: {DistrictId}", dto.StoreId, districtId);
                    continue;
                }

                var entity = _mapper.Map<StoreLocationDistrict>(new StoreMarketDistrictCreateDto
                {
                    StoreId = dto.StoreId,
                    DistrictId = districtId,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame
                });
                entity.DistrictName = master.Name;

                await _districtRepo.AddAsync(entity);
                await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

                addedIds.Add(entity.Id);

            }

            // 4️⃣ NEIGHBORHOOD
            foreach (var neighborhoodId in dto.NeighborhoodIds.Distinct())
            {
                if (neighborhoodId <= 0) continue;

                var master = await _masterNeighborhoodRepo.GetByIdAsync(neighborhoodId);
                if (master is null) continue;

                bool exists = await _neighborhoodRepo.GetQueryable()
                    .AnyAsync(x => x.StoreId == dto.StoreId && x.NeighborhoodId == neighborhoodId);
                if (exists)
                {
                    _logger.LogInformation("Neighborhood already exists. StoreId: {StoreId}, NeighborhoodId: {NeighborhoodId}", dto.StoreId, neighborhoodId);
                    continue;
                }

                var entity = _mapper.Map<StoreLocationNeighborhood>(new StoreMarketNeighborhoodCreateDto
                {
                    StoreId = dto.StoreId,
                    NeighborhoodId = neighborhoodId,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame
                });
                entity.NeighborhoodName = master.Name;

                await _neighborhoodRepo.AddAsync(entity);
                await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

                addedIds.Add(entity.Id);
            }

            // 5️⃣ STATE
            foreach (var stateId in dto.StateIds.Distinct())
            {
                if (stateId <= 0) continue;

                var master = await _masterStateRepo.GetByIdAsync(stateId);
                if (master is null) continue;

                bool exists = await _stateRepo.GetQueryable()
                    .AnyAsync(x => x.StoreId == dto.StoreId && x.StateId == stateId);
                if (exists)
                {
                    _logger.LogInformation("State already exists. StoreId: {StoreId}, StateId: {StateId}", dto.StoreId, stateId);
                    continue;
                }

                var entity = _mapper.Map<StoreLocationState>(new StoreMarketStateCreateDto
                {
                    StoreId = dto.StoreId,
                    StateId = stateId,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame
                });
                entity.StateName = master.Name;

                await _stateRepo.AddAsync(entity);
                await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

                addedIds.Add(entity.Id);
            }

            // 6️⃣ REGION
            foreach (var regionId in dto.RegionIds.Distinct())
            {
                if (regionId <= 0) continue;

                var master = await _masterRegionRepo.GetByIdAsync(regionId);
                if (master is null) continue;

                bool exists = await _regionRepo.GetQueryable()
                    .AnyAsync(x => x.StoreId == dto.StoreId && x.RegionId == regionId);
                if (exists)
                {
                    _logger.LogInformation("Region already exists. StoreId: {StoreId}, RegionId: {RegionId}", dto.StoreId, regionId);
                    continue;
                }

                var entity = _mapper.Map<StoreLocationRegion>(new StoreMarketRegionCreateDto
                {
                    StoreId = dto.StoreId,
                    RegionId = regionId,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame
                });
                entity.RegionName = master.Name;

                await _regionRepo.AddAsync(entity);
                await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

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
                var countries = await _covargeRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.Countries = _mapper.Map<List<StoreMarketCountryDto>>(countries);
                _logger.LogInformation("✅ {Count} ülke getirildi.", hierarchyDto.Countries.Count);

                // İller
                var provinces = await _covargeRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.Provinces = _mapper.Map<List<StoreMarketProvinceDto>>(provinces);
                _logger.LogInformation("✅ {Count} il getirildi.", hierarchyDto.Provinces.Count);

                // İlçeler
                var districts = await _covargeRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.Districts = _mapper.Map<List<StoreMarketDistrictDto>>(districts);
                _logger.LogInformation("✅ {Count} ilçe getirildi.", hierarchyDto.Districts.Count);

                // Mahalleler
                var neighborhoods = await _covargeRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.Neighborhoods = _mapper.Map<List<StoreMarketNeighborhoodDto>>(neighborhoods);
                _logger.LogInformation("✅ {Count} mahalle getirildi.", hierarchyDto.Neighborhoods.Count);

                // Eyaletler
                var states = await _covargeRepo.FindAsync(x => x.StoreId == storeId);
                hierarchyDto.States = _mapper.Map<List<StoreMarketStateDto>>(states);
                _logger.LogInformation("✅ {Count} eyalet getirildi.", hierarchyDto.States.Count);

                // Bölgeler
                var regions = await _covargeRepo.FindAsync(x => x.StoreId == storeId);
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
                        await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

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
                        await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

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
                        await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

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
                        await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

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
                        await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

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
                        await _mediator.Publish(new StoreLocationCoverageChangedEvent(dto.StoreId));

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
