using AutoMapper;
using Data.Databases;
using Data.Dtos.Availability;
using Data.Dtos.Stores.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Stores;
using Services.Availability.IServices;

namespace Services.Availability.Services
{
    public class StoreAvailabilityService : IStoreAvailabilityService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreAvailabilityService> _logger;
        private readonly ApplicationDbContext _context;

        public StoreAvailabilityService(
            IStoreRepository storeRepository,
            IMapper mapper,
            ILogger<StoreAvailabilityService> logger,
            ApplicationDbContext context)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }


        public async Task<List<AvailableStoreDto>> GetAvailableStoresByAddressAsync(StoreAvailabilityFilterDto dto)
        {
            _logger.LogInformation("Store filtreleme basladi.");

            var query = _storeRepository.GetQueryable()
                .Where(s => s.IsActive && s.IsApproved)
                .Where(s =>
                    _context.StoreLocationRegions.Any(r => r.StoreId == s.Id && r.RegionId == dto.RegionId) ||
                    _context.StoreLocationCountries.Any(c => c.StoreId == s.Id && c.CountryId == dto.CountryId) ||
                    _context.StoreLocationStates.Any(st => st.StoreId == s.Id && st.StateId == dto.StateId) ||
                    _context.StoreLocationProvinces.Any(p => p.StoreId == s.Id && p.ProvinceId == dto.ProvinceId) ||
                    _context.StoreLocationDistricts.Any(d => d.StoreId == s.Id && d.DistrictId == dto.DistrictId) ||
                    _context.StoreLocationNeighborhoods.Any(n => n.StoreId == s.Id && n.NeighborhoodId == dto.NeighborhoodId)
                )
                .Include(s => s.Company);

            var stores = await query.ToListAsync();
            _logger.LogInformation("{Count} uygun store bulundu.", stores.Count);

            return _mapper.Map<List<AvailableStoreDto>>(stores);
        }

        public async Task<List<AvailableStoreWithProductsDto>> GetAvailableStoresWithProductsByAddressAsync(StoreAvailabilityFilterDto dto)
        {
            _logger.LogInformation("Store+Urun filtreleme basladi.");

            var stores = await _storeRepository.GetQueryable()
                .Where(s => s.IsActive && s.IsApproved)
                .Where(s =>
                    _context.StoreLocationRegions.Any(r => r.StoreId == s.Id && r.RegionId == dto.RegionId) ||
                    _context.StoreLocationCountries.Any(c => c.StoreId == s.Id && c.CountryId == dto.CountryId) ||
                    _context.StoreLocationStates.Any(st => st.StoreId == s.Id && st.StateId == dto.StateId) ||
                    _context.StoreLocationProvinces.Any(p => p.StoreId == s.Id && p.ProvinceId == dto.ProvinceId) ||
                    _context.StoreLocationDistricts.Any(d => d.StoreId == s.Id && d.DistrictId == dto.DistrictId) ||
                    _context.StoreLocationNeighborhoods.Any(n => n.StoreId == s.Id && n.NeighborhoodId == dto.NeighborhoodId)
                )
                .Include(s => s.Company)
                .Include(s => s.StoreProducts.Where(p => p.IsActive && p.IsOnSale))
                .ToListAsync();

            _logger.LogInformation("{Count} uygun store (urunleriyle birlikte) bulundu.", stores.Count);

            return _mapper.Map<List<AvailableStoreWithProductsDto>>(stores);
        }

        public async Task<List<StoreProductDto>> GetAvailableProductsByAddressAsync(StoreAvailabilityFilterDto dto)
        {
            _logger.LogInformation("Adres bazli uygun urunler filtreleniyor...");

            var storeIds = await _storeRepository.GetQueryable()
                .Where(s => s.IsActive && s.IsApproved)
                .Where(s =>
                    _context.StoreLocationRegions.Any(r => r.StoreId == s.Id && r.RegionId == dto.RegionId) ||
                    _context.StoreLocationCountries.Any(c => c.StoreId == s.Id && c.CountryId == dto.CountryId) ||
                    _context.StoreLocationStates.Any(st => st.StoreId == s.Id && st.StateId == dto.StateId) ||
                    _context.StoreLocationProvinces.Any(p => p.StoreId == s.Id && p.ProvinceId == dto.ProvinceId) ||
                    _context.StoreLocationDistricts.Any(d => d.StoreId == s.Id && d.DistrictId == dto.DistrictId) ||
                    _context.StoreLocationNeighborhoods.Any(n => n.StoreId == s.Id && n.NeighborhoodId == dto.NeighborhoodId)
                )
                .Select(s => s.Id)
                .ToListAsync();

            var products = await _context.StoreProducts
                .Where(p => storeIds.Contains(p.StoreId) && p.IsActive && p.IsOnSale)
                .ToListAsync();

            _logger.LogInformation("{Count} uygun urun bulundu.", products.Count);

            return _mapper.Map<List<StoreProductDto>>(products);
        }
    }
}

