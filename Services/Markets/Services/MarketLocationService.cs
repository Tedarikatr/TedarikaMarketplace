using Data.Databases;
using Data.Dtos.Markets;
using Entity.Markets.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Services.Markets.IServices;

namespace Services.Markets.Services
{
    public class MarketLocationService : IMarketLocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MarketLocationService> _logger;

        public MarketLocationService(ApplicationDbContext context, ILogger<MarketLocationService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> AddCountryAsync(CountryCreateDto dto)
        {
            var country = new Country { Name = dto.Name, Code = dto.Code };
            await _context.Countries.AddAsync(country);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Yeni ülke eklendi: {Name}", dto.Name);
            return country.Id;
        }

        public async Task<int> AddProvinceAsync(ProvinceCreateDto dto)
        {
            var province = new Province { Name = dto.Name, CountryId = dto.CountryId };
            await _context.Provinces.AddAsync(province);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Ülkeye il eklendi: {Name}", dto.Name);
            return province.Id;
        }

        public async Task<int> AddDistrictAsync(DistrictCreateDto dto)
        {
            var district = new District { Name = dto.Name, ProvinceId = dto.ProvinceId };
            await _context.Districts.AddAsync(district);
            await _context.SaveChangesAsync();
            _logger.LogInformation("İle ilçe eklendi: {Name}", dto.Name);
            return district.Id;
        }

        public async Task<int> AddNeighborhoodAsync(NeighborhoodCreateDto dto)
        {
            var neighborhood = new Neighborhood
            {
                Name = dto.Name,
                DistrictId = dto.DistrictId,
                PostalCode = dto.PostalCode
            };

            await _context.Neighborhoods.AddAsync(neighborhood);
            await _context.SaveChangesAsync();
            _logger.LogInformation("İlçeye mahalle eklendi: {Name}", dto.Name);
            return neighborhood.Id;
        }

        public async Task<int> AddStateAsync(StateCreateDto dto)
        {
            var state = new State { Name = dto.Name, CountryId = dto.CountryId };
            await _context.States.AddAsync(state);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Ülkeye eyalet eklendi: {Name}", dto.Name);
            return state.Id;
        }

        public async Task<bool> ToggleCountryStatusAsync(int id, bool isActive)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null) return false;

            country.IsActive = isActive;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Ülke durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleProvinceStatusAsync(int id, bool isActive)
        {
            var province = await _context.Provinces.FindAsync(id);
            if (province == null) return false;

            province.IsActive = isActive;
            await _context.SaveChangesAsync();

            _logger.LogInformation("İl durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleDistrictStatusAsync(int id, bool isActive)
        {
            var district = await _context.Districts.FindAsync(id);
            if (district == null) return false;

            district.IsActive = isActive;
            await _context.SaveChangesAsync();

            _logger.LogInformation("İlçe durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleNeighborhoodStatusAsync(int id, bool isActive)
        {
            var neighborhood = await _context.Neighborhoods.FindAsync(id);
            if (neighborhood == null) return false;

            neighborhood.IsActive = isActive;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Mahalle durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleStateStatusAsync(int id, bool isActive)
        {
            var state = await _context.States.FindAsync(id);
            if (state == null) return false;

            state.IsActive = isActive;
            await _context.SaveChangesAsync();

            _logger.LogInformation("Eyalet durumu {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<List<CountryDto>> GetAllCountriesAsync()
        {
            return await _context.Countries
                .Select(c => new CountryDto { Id = c.Id, Name = c.Name, Code = c.Code })
                .ToListAsync();
        }

        public async Task<List<ProvinceDto>> GetProvincesByCountryIdAsync(int countryId)
        {
            return await _context.Provinces
                .Where(p => p.CountryId == countryId)
                .Select(p => new ProvinceDto { Id = p.Id, Name = p.Name, CountryId = p.CountryId })
                .ToListAsync();
        }

        public async Task<List<DistrictDto>> GetDistrictsByProvinceIdAsync(int provinceId)
        {
            return await _context.Districts
                .Where(d => d.ProvinceId == provinceId)
                .Select(d => new DistrictDto { Id = d.Id, Name = d.Name, ProvinceId = d.ProvinceId })
                .ToListAsync();
        }

        public async Task<List<NeighborhoodDto>> GetNeighborhoodsByDistrictIdAsync(int districtId)
        {
            return await _context.Neighborhoods
                .Where(n => n.DistrictId == districtId)
                .Select(n => new NeighborhoodDto { Id = n.Id, Name = n.Name, DistrictId = n.DistrictId, PostalCode = n.PostalCode })
                .ToListAsync();
        }

        public async Task<List<StateDto>> GetStatesByCountryIdAsync(int countryId)
        {
            return await _context.States
                .Where(s => s.CountryId == countryId)
                .Select(s => new StateDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    IsActive = s.IsActive,
                    CountryId = s.CountryId,
                    CountryName = s.Country.Name
                }).ToListAsync();
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var entity = await _context.Countries.FindAsync(id);
            if (entity == null) return false;

            _context.Countries.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProvinceAsync(int id)
        {
            var entity = await _context.Provinces.FindAsync(id);
            if (entity == null) return false;

            _context.Provinces.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteDistrictAsync(int id)
        {
            var entity = await _context.Districts.FindAsync(id);
            if (entity == null) return false;

            _context.Districts.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNeighborhoodAsync(int id)
        {
            var entity = await _context.Neighborhoods.FindAsync(id);
            if (entity == null) return false;

            _context.Neighborhoods.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStateAsync(int id)
        {
            var entity = await _context.States.FindAsync(id);
            if (entity == null) return false;

            _context.States.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCountryAsync(int id, CountryCreateDto dto)
        {
            var entity = await _context.Countries.FindAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.Code = dto.Code;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProvinceAsync(int id, ProvinceCreateDto dto)
        {
            var entity = await _context.Provinces.FindAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.CountryId = dto.CountryId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateDistrictAsync(int id, DistrictCreateDto dto)
        {
            var entity = await _context.Districts.FindAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.ProvinceId = dto.ProvinceId;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateNeighborhoodAsync(int id, NeighborhoodCreateDto dto)
        {
            var entity = await _context.Neighborhoods.FindAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.DistrictId = dto.DistrictId;
            entity.PostalCode = dto.PostalCode;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStateAsync(int id, StateCreateDto dto)
        {
            var entity = await _context.States.FindAsync(id);
            if (entity == null) return false;

            entity.Name = dto.Name;
            entity.CountryId = dto.CountryId;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
