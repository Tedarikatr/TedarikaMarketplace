using API.Helpers;
using AutoMapper;
using Data.Dtos.Stores;
using Entity.Stores.Markets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Stores.Markets.IServices;

namespace API.Controllers.Stores.Market
{
    [Route("api/seller/store/regions")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "seller")]
    public class SellerStoreMarketRegionController : ControllerBase
    {
        private readonly IStoreMarketRegionService _regionService;
        private readonly IMapper _mapper;

        public SellerStoreMarketRegionController(IStoreMarketRegionService regionService, IMapper mapper)
        {
            _regionService = regionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyRegions()
        {
            int storeId = SellerUserContextHelper.GetStoreId(User);
            var regions = await _regionService.GetRegionsByStoreIdAsync(storeId);
            return Ok(regions);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegion([FromBody] StoreMarketRegionDto dto)
        {
            int storeId = SellerUserContextHelper.GetStoreId(User);

            var region = _mapper.Map<StoreMarketRegion>(dto);
            region.StoreId = storeId;

            var added = await _regionService.AddRegionAsync(region);

            if (!added)
                return Conflict("Bu hizmet bölgesi zaten eklenmiş.");

            return Ok("Hizmet bölgesi başarıyla eklendi.");
        }

        [HttpDelete("{regionId}")]
        public async Task<IActionResult> DeleteRegion(int regionId)
        {
            int storeId = SellerUserContextHelper.GetStoreId(User);

            var deleted = await _regionService.RemoveRegionAsync(regionId, storeId);

            if (!deleted)
                return NotFound("Hizmet bölgesi bulunamadı veya size ait değil.");

            return Ok("Hizmet bölgesi silindi.");
        }
    }
}
