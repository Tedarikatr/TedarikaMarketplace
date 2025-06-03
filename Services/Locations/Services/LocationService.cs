using Data.Dtos.Locations;
using Microsoft.Extensions.Logging;
using Repository.Locations.IRepositorys;
using Services.Locations.IServices;

namespace Services.Locations.Services
{
    public class LocationService : ILocationService
    {
        private readonly ICountryRepository _countryRepo;
        private readonly IProvinceRepository _provinceRepo;
        private readonly IDistrictRepository _districtRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;
        private readonly IStateRepository _stateRepo;
        private readonly IRegionRepository _regionRepo;
        private readonly ILogger<LocationService> _logger;

        public LocationService(
            ICountryRepository countryRepo,
            IProvinceRepository provinceRepo,
            IDistrictRepository districtRepo,
            INeighborhoodRepository neighborhoodRepo,
            IStateRepository stateRepo,
            IRegionRepository regionRepo,
            ILogger<LocationService> logger)
        {
            _countryRepo = countryRepo;
            _provinceRepo = provinceRepo;
            _districtRepo = districtRepo;
            _neighborhoodRepo = neighborhoodRepo;
            _stateRepo = stateRepo;
            _regionRepo = regionRepo;
            _logger = logger;
        }


        public async Task<int> AddCountryAsync(CountryCreateDto dto)
        {
            var country = new Entity.Locations.Country
            {
                Name = dto.Name,
                Code = dto.Code,
                RegionId = dto.RegionId,
                IsActive = true
            };

            await _countryRepo.AddAsync(country);
            _logger.LogInformation("Yeni ülke eklendi: {Name}", dto.Name);
            return country.Id;
        }

        public async Task<int> AddProvinceAsync(ProvinceCreateDto dto)
        {
            var province = new Entity.Locations.Province
            {
                Name = dto.Name,
                CountryId = dto.CountryId,
                StateId = dto.StateId,
                IsActive = true
            };

            await _provinceRepo.AddAsync(province);
            _logger.LogInformation("Yeni il eklendi: {Name}", dto.Name);
            return province.Id;
        }

        public async Task<int> AddDistrictAsync(DistrictCreateDto dto)
        {
            var district = new Entity.Locations.District
            {
                Name = dto.Name,
                ProvinceId = dto.ProvinceId,
                IsActive = true
            };

            await _districtRepo.AddAsync(district);
            _logger.LogInformation("Yeni ilçe eklendi: {Name}", dto.Name);
            return district.Id;
        }

        public async Task<int> AddNeighborhoodAsync(NeighborhoodCreateDto dto)
        {
            var neighborhood = new Entity.Locations.Neighborhood
            {
                Name = dto.Name,
                DistrictId = dto.DistrictId,
                PostalCode = dto.PostalCode,
                IsActive = true
            };

            await _neighborhoodRepo.AddAsync(neighborhood);
            _logger.LogInformation("Yeni mahalle eklendi: {Name}", dto.Name);
            return neighborhood.Id;
        }

        public async Task<int> AddStateAsync(StateCreateDto dto)
        {
            var state = new Entity.Locations.State
            {
                Name = dto.Name,
                CountryId = dto.CountryId,
                IsActive = true
            };

            await _stateRepo.AddAsync(state);
            _logger.LogInformation("Yeni eyalet eklendi: {Name}", dto.Name);
            return state.Id;
        }

        public async Task<bool> ToggleCountryStatusAsync(int id, bool isActive)
        {
            var country = await _countryRepo.GetByIdAsync(id);
            if (country == null) return false;

            country.IsActive = isActive;
            await _countryRepo.UpdateAsync(country);

            _logger.LogInformation("Ülke durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleProvinceStatusAsync(int id, bool isActive)
        {
            var province = await _provinceRepo.GetByIdAsync(id);
            if (province == null) return false;

            province.IsActive = isActive;
            await _provinceRepo.UpdateAsync(province);

            _logger.LogInformation("İl durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleDistrictStatusAsync(int id, bool isActive)
        {
            var district = await _districtRepo.GetByIdAsync(id);
            if (district == null) return false;

            district.IsActive = isActive;
            await _districtRepo.UpdateAsync(district);

            _logger.LogInformation("İlçe durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleNeighborhoodStatusAsync(int id, bool isActive)
        {
            var neighborhood = await _neighborhoodRepo.GetByIdAsync(id);
            if (neighborhood == null) return false;

            neighborhood.IsActive = isActive;
            await _neighborhoodRepo.UpdateAsync(neighborhood);

            _logger.LogInformation("Mahalle durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleStateStatusAsync(int id, bool isActive)
        {
            var state = await _stateRepo.GetByIdAsync(id);
            if (state == null) return false;

            state.IsActive = isActive;
            await _stateRepo.UpdateAsync(state);

            _logger.LogInformation("Eyalet durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<List<RegionDto>> GetRegions()
        {
            var regions = await _regionRepo.GetAllAsync();
            return regions.Select(r => new RegionDto()
            {
                Id = r.Id,
                Name = r.Name,
                Code = r.Code,
                IsActive = r.IsActive,
            }).ToList();
        }

        public async Task<List<CountryDto>> GetCountriesByRegionIdAsync(int regionId)
        {
            var countries = await _countryRepo.FindAsync(c => c.RegionId == regionId);
            return countries.Select(c => new CountryDto
            {
                Id = c.Id,
                Name = c.Name,
                Code = c.Code,
                IsActive = c.IsActive
            }).ToList();
        }

        public async Task<List<ProvinceDto>> GetProvincesByCountryIdAsync(int countryId)
        {
            var provinces = await _provinceRepo.FindAsync(p => p.CountryId == countryId);
            return provinces.Select(p => new ProvinceDto
            {
                Id = p.Id,
                Name = p.Name,
                CountryId = p.CountryId,
            }).ToList();
        }

        public async Task<List<DistrictDto>> GetDistrictsByProvinceIdAsync(int provinceId)
        {
            var districts = await _districtRepo.FindAsync(d => d.ProvinceId == provinceId);
            return districts.Select(d => new DistrictDto
            {
                Id = d.Id,
                Name = d.Name,
                ProvinceId = d.ProvinceId
            }).ToList();
        }

        public async Task<List<NeighborhoodDto>> GetNeighborhoodsByDistrictIdAsync(int districtId)
        {
            var neighborhoods = await _neighborhoodRepo.FindAsync(n => n.DistrictId == districtId);
            return neighborhoods.Select(n => new NeighborhoodDto
            {
                Id = n.Id,
                Name = n.Name,
                DistrictId = n.DistrictId,
                PostalCode = n.PostalCode
            }).ToList();
        }

        public async Task<List<StateDto>> GetStatesByCountryIdAsync(int countryId)
        {
            var states = await _stateRepo.FindAsync(s => s.CountryId == countryId);
            return states.Select(s => new StateDto
            {
                Id = s.Id,
                Name = s.Name,
                IsActive = s.IsActive,
                CountryId = s.CountryId
            }).ToList();
        }




        public async Task<bool> UpdateCountryAsync(int id, CountryCreateDto dto)
        {
            var entity = await _countryRepo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.Code = dto.Code;
            await _countryRepo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateProvinceAsync(int id, ProvinceCreateDto dto)
        {
            var entity = await _provinceRepo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.CountryId = dto.CountryId;
            entity.StateId = dto.StateId;
            await _provinceRepo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateDistrictAsync(int id, DistrictCreateDto dto)
        {
            var entity = await _districtRepo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.ProvinceId = dto.ProvinceId;
            await _districtRepo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateNeighborhoodAsync(int id, NeighborhoodCreateDto dto)
        {
            var entity = await _neighborhoodRepo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.DistrictId = dto.DistrictId;
            entity.PostalCode = dto.PostalCode;
            await _neighborhoodRepo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> UpdateStateAsync(int id, StateCreateDto dto)
        {
            var entity = await _stateRepo.GetByIdAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.CountryId = dto.CountryId;
            await _stateRepo.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var entity = await _countryRepo.GetByIdAsync(id);
            if (entity == null) return false;

            await _countryRepo.RemoveAsync(entity);
            return true;
        }

        public async Task<bool> DeleteProvinceAsync(int id)
        {
            var entity = await _provinceRepo.GetByIdAsync(id);
            if (entity == null) return false;

            await _provinceRepo.RemoveAsync(entity);
            return true;
        }

        public async Task<bool> DeleteDistrictAsync(int id)
        {
            var entity = await _districtRepo.GetByIdAsync(id);
            if (entity == null) return false;

            await _districtRepo.RemoveAsync(entity);
            return true;
        }

        public async Task<bool> DeleteNeighborhoodAsync(int id)
        {
            var entity = await _neighborhoodRepo.GetByIdAsync(id);
            if (entity == null) return false;

            await _neighborhoodRepo.RemoveAsync(entity);
            return true;
        }

        public async Task<bool> DeleteStateAsync(int id)
        {
            var entity = await _stateRepo.GetByIdAsync(id);
            if (entity == null) return false;

            await _stateRepo.RemoveAsync(entity);
            return true;
        }

        public async Task<bool> DeleteRegionAsync(int id)
        {
            var entity = await _regionRepo.GetByIdAsync(id);
            if (entity == null) return false;

            await _regionRepo.RemoveAsync(entity);
            return true;
        }
    }
}
