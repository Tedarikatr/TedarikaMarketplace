using AutoMapper;
using Data.Databases;
using Data.Dtos.Availability;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Stores;
using Services.Availability.IServices;

namespace Services.Availability.Services
{
    public class StoreAvailabilityService : IStoreAvailabilityService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreAvailabilityService> _logger;
        private readonly ApplicationDbContext _context;

        public async Task<List<AvailableStoreDto>> GetAvailableStoresByAddressAsync(StoreAvailabilityFilterDto dto)
        {
            try
            {
                _logger.LogInformation("Teslimat adresine göre uygun mağazalar filtreleniyor...");

                var stores = await _storeRepository.GetQueryable()
                    .Where(s => s.IsActive && s.IsApproved)
                    .Where(s =>
                        _context.StoreLocationRegions.Any(r => r.StoreId == s.Id && r.Region.Name == dto.Region) ||
                        _context.StoreLocationCountries.Any(c => c.StoreId == s.Id && c.Country.Name == dto.Country) ||
                        _context.StoreLocationStates.Any(st => st.StoreId == s.Id && st.State.Name == dto.State) ||
                        _context.StoreLocationProvinces.Any(p => p.StoreId == s.Id && p.Province.Name == dto.Province) ||
                        _context.StoreLocationDistricts.Any(d => d.StoreId == s.Id && d.District.Name == dto.District) ||
                        _context.StoreLocationNeighborhoods.Any(n => n.StoreId == s.Id && n.Neighborhood.Name == dto.Neighborhood)
                    )
                    .Include(s => s.Company)
                    .Include(s => s.StoreProducts.Where(p => p.IsActive && p.IsOnSale))
                    .ToListAsync();

                _logger.LogInformation("{Count} uygun mağaza bulundu.", stores.Count);

                return _mapper.Map<List<AvailableStoreDto>>(stores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza erişilebilirlik kontrolü yapılırken hata oluştu.");
                throw new ApplicationException("Mağaza erişilebilirlik kontrolü sırasında bir hata oluştu.", ex);
            }
        }
    }
}
