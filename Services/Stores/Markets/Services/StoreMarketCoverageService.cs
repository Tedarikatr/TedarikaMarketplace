using AutoMapper;
using Data.Dtos.Stores.Markets;
using Entity.Markets.Locations;
using Entity.Stores.Markets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Stores.Markets.IRepositorys;
using Services.Markets.IServices;
using Services.Stores.Markets.IServices;

namespace Services.Stores.Markets.Services
{
    public class StoreMarketCoverageService : IStoreMarketCoverageService
    {
        private readonly IStoreMarketCoverageRepository _coverageRepo;
        private readonly IMarketLocationService _locationService;
        private readonly IMapper _mapper;
        private readonly ILogger<StoreMarketCoverageService> _logger;

        public StoreMarketCoverageService(
            IStoreMarketCoverageRepository coverageRepo,
            IMarketLocationService locationService,
            IMapper mapper,
            ILogger<StoreMarketCoverageService> logger)
        {
            _coverageRepo = coverageRepo;
            _locationService = locationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<StoreMarketCoverageDto>> GetMyCoveragesAsync(int storeId, bool? onlyActive = null)
        {
            try
            {
                _logger.LogInformation("Mağaza kapsamları listeleniyor. StoreId: {StoreId}", storeId);

                var query = _coverageRepo.GetQueryable()
                    .Include(x => x.Country)
                    .Include(x => x.Province)
                    .Include(x => x.District)
                    .Include(x => x.Neighborhood)
                    .Include(x => x.Region)
                    .Where(x => x.StoreId == storeId);

                if (onlyActive.HasValue)
                    query = query.Where(x => x.IsActive == onlyActive.Value);

                var list = await query.ToListAsync();
                return _mapper.Map<List<StoreMarketCoverageDto>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsamlar alınırken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task<List<StoreMarketCoverageDto>> GetSellerOwnCoveragesAsync(int sellerUserId)
        {
            try
            {
                _logger.LogInformation("Satıcı kapsamları alınıyor. SellerUserId: {SellerUserId}", sellerUserId);

                var coverages = await _coverageRepo.GetCoveragesBySellerUserIdAsync(sellerUserId);
                return _mapper.Map<List<StoreMarketCoverageDto>>(coverages);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcı kapsamları alınırken hata oluştu. SellerUserId: {SellerUserId}", sellerUserId);
                throw;
            }
        }

        public async Task AddCoverageAsync(StoreMarketCoverageCreateDto dto, int storeId)
        {
            try
            {
                _logger.LogInformation("Yeni kapsam ekleniyor. StoreId: {StoreId}", storeId);

                var entity = _mapper.Map<StoreMarketCoverage>(dto);
                entity.StoreId = storeId;
                entity.IsActive = true;

                await _coverageRepo.AddAsync(entity);
                _logger.LogInformation("Kapsam başarıyla eklendi. StoreId: {StoreId}", storeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam eklenirken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task AddCoveragesBulkAsync(StoreMarketCoverageBatchDto dto)
        {
            try
            {
                _logger.LogInformation("Toplu kapsam ekleniyor. StoreId: {StoreId}", dto.StoreId);

                var entities = _mapper.Map<List<StoreMarketCoverage>>(dto.Coverages);
                foreach (var entity in entities)
                {
                    entity.StoreId = dto.StoreId;
                    entity.IsActive = true;
                }

                await _coverageRepo.AddRangeAsync(entities);
                _logger.LogInformation("Toplu kapsam başarıyla eklendi. Adet: {Count}", entities.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu kapsam ekleme hatası. StoreId: {StoreId}", dto.StoreId);
                throw;
            }
        }

        public async Task UpdateCoverageAsync(StoreMarketCoverageUpdateDto dto, int storeId)
        {
            try
            {
                var entity = await _coverageRepo.GetByIdAsync(dto.Id);
                if (entity == null || entity.StoreId != storeId)
                {
                    _logger.LogWarning("Güncellenecek kapsam bulunamadı. CoverageId: {CoverageId}, StoreId: {StoreId}", dto.Id, storeId);
                    throw new Exception("Kapsam bulunamadı veya yetkisiz işlem.");
                }

                _mapper.Map(dto, entity);
                await _coverageRepo.UpdateAsync(entity);
                _logger.LogInformation("Kapsam güncellendi. CoverageId: {CoverageId}", dto.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam güncellenirken hata oluştu. CoverageId: {CoverageId}", dto.Id);
                throw;
            }
        }

        public async Task RemoveCoverageAsync(int coverageId, int storeId)
        {
            try
            {
                var entity = await _coverageRepo.GetByIdAsync(coverageId);
                if (entity == null || entity.StoreId != storeId)
                    throw new Exception("Silme yetkisi yok.");

                await _coverageRepo.RemoveAsync(entity);
                _logger.LogInformation("Kapsam silindi. CoverageId: {CoverageId}", coverageId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam silme hatası. CoverageId: {CoverageId}", coverageId);
                throw;
            }
        }

        public async Task ActivateCoverageAsync(int coverageId, int storeId)
        {
            try
            {
                var entity = await _coverageRepo.GetByIdAsync(coverageId);
                if (entity?.StoreId == storeId)
                {
                    entity.IsActive = true;
                    await _coverageRepo.UpdateAsync(entity);
                    _logger.LogInformation("Kapsam aktif hale getirildi. CoverageId: {CoverageId}", coverageId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam aktive edilirken hata oluştu. CoverageId: {CoverageId}", coverageId);
                throw;
            }
        }

        public async Task DeactivateCoverageAsync(int coverageId, int storeId)
        {
            try
            {
                var entity = await _coverageRepo.GetByIdAsync(coverageId);
                if (entity?.StoreId == storeId)
                {
                    entity.IsActive = false;
                    await _coverageRepo.UpdateAsync(entity);
                    _logger.LogInformation("Kapsam pasif hale getirildi. CoverageId: {CoverageId}", coverageId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam pasifleştirme hatası. CoverageId: {CoverageId}", coverageId);
                throw;
            }
        }

        public async Task AddCoverageMultiAsync(StoreMarketCoverageMultiCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Çoklu kapsam ekleme başlatıldı. StoreId: {StoreId}", dto.StoreId);

                var coverages = new List<StoreMarketCoverage>();

                coverages.AddRange((dto.CountryIds ?? new()).Select(id => new StoreMarketCoverage
                {
                    StoreId = dto.StoreId,
                    CoverageLevel = MarketCoverageLevel.Country,
                    CountryId = id,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                }));

                coverages.AddRange((dto.ProvinceIds ?? new()).Select(id => new StoreMarketCoverage
                {
                    StoreId = dto.StoreId,
                    CoverageLevel = MarketCoverageLevel.Province,
                    ProvinceId = id,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                }));

                coverages.AddRange((dto.DistrictIds ?? new()).Select(id => new StoreMarketCoverage
                {
                    StoreId = dto.StoreId,
                    CoverageLevel = MarketCoverageLevel.District,
                    DistrictId = id,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                }));

                coverages.AddRange((dto.NeighborhoodIds ?? new()).Select(id => new StoreMarketCoverage
                {
                    StoreId = dto.StoreId,
                    CoverageLevel = MarketCoverageLevel.Neighborhood,
                    NeighborhoodId = id,
                    DeliveryTimeFrame = dto.DeliveryTimeFrame,
                    IsActive = true
                }));

                if (!coverages.Any())
                    throw new Exception("En az bir lokasyon seçilmelidir.");

                await _coverageRepo.AddRangeAsync(coverages);
                _logger.LogInformation("Çoklu kapsam başarıyla eklendi. Adet: {Count}", coverages.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Çoklu kapsam eklenirken hata oluştu. StoreId: {StoreId}", dto.StoreId);
                throw;
            }
        }

        public async Task<List<StoreMarketCoverageDto>> GetAllCoveragesAsync()
        {
            try
            {
                _logger.LogInformation("Tüm kapsamlar listeleniyor (Admin).");

                var list = await _coverageRepo.GetQueryable()
                    .Include(x => x.Country)
                    .Include(x => x.Province)
                    .Include(x => x.District)
                    .Include(x => x.Neighborhood)
                    .Include(x => x.Region)
                    .ToListAsync();

                return _mapper.Map<List<StoreMarketCoverageDto>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm kapsamlar alınırken hata oluştu.");
                throw;
            }
        }

        public async Task<List<StoreMarketCoverageDto>> GetCoveragesByStoreIdAsync(int storeId)
        {
            try
            {
                _logger.LogInformation("Mağaza kapsamları getiriliyor. StoreId: {StoreId}", storeId);
                var data = await _coverageRepo.FindAsync(x => x.StoreId == storeId);
                return _mapper.Map<List<StoreMarketCoverageDto>>(data.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsamlar alınırken hata oluştu. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task AddCoverageAsAdminAsync(StoreMarketCoverageCreateDto dto, int storeId)
        {
            try
            {
                var entity = _mapper.Map<StoreMarketCoverage>(dto);
                entity.StoreId = storeId;
                entity.IsActive = true;

                await _coverageRepo.AddAsync(entity);
                _logger.LogInformation("Admin tarafından kapsam eklendi. StoreId: {StoreId}", storeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin kapsam ekleme hatası. StoreId: {StoreId}", storeId);
                throw;
            }
        }

        public async Task AddCoveragesAsAdminBulkAsync(StoreMarketCoverageBatchDto dto)
        {
            try
            {
                var entities = _mapper.Map<List<StoreMarketCoverage>>(dto.Coverages);
                foreach (var item in entities)
                {
                    item.StoreId = dto.StoreId;
                    item.IsActive = true;
                }

                await _coverageRepo.AddRangeAsync(entities);
                _logger.LogInformation("Admin toplu kapsam ekledi. Adet: {Count}", entities.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin toplu kapsam ekleme hatası.");
                throw;
            }
        }

        public async Task RemoveCoverageAsAdminAsync(int coverageId)
        {
            try
            {
                var entity = await _coverageRepo.GetByIdAsync(coverageId);
                if (entity != null)
                {
                    await _coverageRepo.RemoveAsync(entity);
                    _logger.LogInformation("Admin kapsam sildi. CoverageId: {CoverageId}", coverageId);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Admin kapsam silme hatası. CoverageId: {CoverageId}", coverageId);
                throw;
            }
        }

        public Task<List<StoreMarketCoverageDto>> GetCoveragesMatchingBuyerAddressAsync(int buyerId, int addressId)
        {
            _logger.LogWarning("Buyer kapsam eşleşme servisi henüz implement edilmedi.");
            throw new NotImplementedException("Buyer adresine uygun kapsam eşleşme servisi daha sonra eklenecek.");
        }
    }
}
