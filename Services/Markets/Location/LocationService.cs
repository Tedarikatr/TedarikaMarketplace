using Data.Databases;
using Data.Dtos.Markets;
using Entity.Markets.Locations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Services.Markets.Location
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LocationService> _logger;

        public LocationService(ApplicationDbContext context, ILogger<LocationService> logger)
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

        public async Task<bool> ToggleCountryStatusAsync(int id, bool isActive)
        {
            var locations = await _context.MarketAddressLocations
                .Where(x => x.Location.CountryId == id)
            .ToListAsync();

            locations.ForEach(x => x.IsActive = isActive);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Ülkeye bağlı tüm market lokasyonları {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleProvinceStatusAsync(int id, bool isActive)
        {
            var locations = await _context.MarketAddressLocations
                .Where(x => x.Location.ProvinceId == id)
            .ToListAsync();

            locations.ForEach(x => x.IsActive = isActive);
            await _context.SaveChangesAsync();

            _logger.LogInformation("İle bağlı tüm market lokasyonları {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleDistrictStatusAsync(int id, bool isActive)
        {
            var locations = await _context.MarketAddressLocations
                .Where(x => x.Location.DistrictId == id)
            .ToListAsync();

            locations.ForEach(x => x.IsActive = isActive);
            await _context.SaveChangesAsync();

            _logger.LogInformation("İlçeye bağlı tüm market lokasyonları {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }

        public async Task<bool> ToggleNeighborhoodStatusAsync(int id, bool isActive)
        {
            var locations = await _context.MarketAddressLocations
                .Where(x => x.Location.NeighborhoodId == id)
            .ToListAsync();

            locations.ForEach(x => x.IsActive = isActive);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Mahalleye bağlı tüm market lokasyonları {status} yapıldı", isActive ? "aktif" : "pasif");
            return true;
        }
    }
}
