using Entity.Stores.Markets;
using Microsoft.Extensions.Logging;
using Repository.Stores.Markets.IRepositorys;
using Services.Stores.Markets.IServices;

namespace Services.Stores.Markets.Services
{
    public class StoreMarketRegionService : IStoreMarketRegionService
    {
        private readonly IStoreMarketRegionRepository _repository;
        private readonly ILogger<StoreMarketRegionService> _logger;

        public StoreMarketRegionService(IStoreMarketRegionRepository repository, ILogger<StoreMarketRegionService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<List<StoreMarketRegion>> GetRegionsByStoreIdAsync(int storeId)
        {
            return await _repository.GetByStoreIdAsync(storeId);
        }

        public async Task<bool> AddRegionAsync(StoreMarketRegion region)
        {
            try
            {
                if (await _repository.ExistsAsync(region.StoreId, region.Country, region.Province, region.District))
                {
                    _logger.LogWarning("Hizmet bölgesi zaten mevcut. StoreId: {StoreId}", region.StoreId);
                    return false;
                }

                await _repository.AddAsync(region);
                _logger.LogInformation("Hizmet bölgesi eklendi. StoreId: {StoreId}, Country: {Country}, Province: {Province}, District: {District}",
                    region.StoreId, region.Country, region.Province, region.District);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hizmet bölgesi eklenirken hata oluştu.");
                return false;
            }
        }

        public async Task<bool> RemoveRegionAsync(int regionId, int storeId)
        {
            var region = await _repository.GetByIdAsync(regionId);
            if (region == null || region.StoreId != storeId)
            {
                _logger.LogWarning("Silinmek istenen hizmet bölgesi bulunamadı. RegionId: {RegionId}", regionId);
                return false;
            }

            await _repository.RemoveAsync(region);
            _logger.LogInformation("Hizmet bölgesi silindi. RegionId: {RegionId}", regionId);
            return true;
        }

        public async Task<bool> RegionExistsAsync(int storeId, string country, string province, string district)
        {
            return await _repository.ExistsAsync(storeId, country, province, district);
        }
    }
}
