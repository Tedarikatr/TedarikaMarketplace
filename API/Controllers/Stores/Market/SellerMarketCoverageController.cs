using API.Helpers;
using Data.Dtos.Stores.Markets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Stores.Markets.IServices;

namespace API.Controllers.Stores.Market
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize]
    public class SellerMarketCoverageController : ControllerBase
    {
        private readonly IStoreMarketCoverageService _coverageService;
        private readonly SellerUserContextHelper _userHelper;
        private readonly ILogger<SellerMarketCoverageController> _logger;

        public SellerMarketCoverageController(IStoreMarketCoverageService coverageService, SellerUserContextHelper userHelper, ILogger<SellerMarketCoverageController> logger)
        {
            _coverageService = coverageService;
            _userHelper = userHelper;
            _logger = logger;
        }

        [HttpPost("add-country")]
        public async Task<IActionResult> AddCountry([FromBody] StoreMarketCountryCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                dto.StoreId = storeId;
                var id = await _coverageService.AddCountryAsync(dto);
                return Ok(new { Message = "Ülke kapsamı başarıyla eklendi.", Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ülke kapsamı eklenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("add-province")]
        public async Task<IActionResult> AddProvince([FromBody] StoreMarketProvinceCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                dto.StoreId = storeId;
                var id = await _coverageService.AddProvinceAsync(dto);
                return Ok(new { Message = "İl kapsamı başarıyla eklendi.", Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İl kapsamı eklenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("add-district")]
        public async Task<IActionResult> AddDistrict([FromBody] StoreMarketDistrictCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                dto.StoreId = storeId;
                var id = await _coverageService.AddDistrictAsync(dto);
                return Ok(new { Message = "İlçe kapsamı başarıyla eklendi.", Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İlçe kapsamı eklenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("add-neighborhood")]
        public async Task<IActionResult> AddNeighborhood([FromBody] StoreMarketNeighborhoodCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                dto.StoreId = storeId;
                var id = await _coverageService.AddNeighborhoodAsync(dto);
                return Ok(new { Message = "Mahalle kapsamı başarıyla eklendi.", Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mahalle kapsamı eklenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("add-region")]
        public async Task<IActionResult> AddRegion([FromBody] StoreMarketRegionCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                dto.StoreId = storeId;
                var id = await _coverageService.AddRegionAsync(dto);
                return Ok(new { Message = "Bölge kapsamı başarıyla eklendi.", Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölge kapsamı eklenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("add-state")]
        public async Task<IActionResult> AddState([FromBody] StoreMarketStateCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                dto.StoreId = storeId;
                var id = await _coverageService.AddStateAsync(dto);
                return Ok(new { Message = "Eyalet kapsamı başarıyla eklendi.", Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eyalet kapsamı eklenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("add-multi-country")]
        public async Task<IActionResult> AddMultiCountry([FromBody] StoreMarketCountryMultiCreateDto dto)
        {
            var storeId = await _userHelper.GetStoreId(User);
            dto.StoreId = storeId;
            await _coverageService.AddCountrysMultiAsync(dto);
            return Ok(new { message = "Ülkeler başarıyla eklendi." });
        }

        [HttpPost("add-multi-province")]
        public async Task<IActionResult> AddMultiProvince([FromBody] StoreMarketProvinceMultiCreateDto dto)
        {
            var storeId = await _userHelper.GetStoreId(User);
            dto.StoreId = storeId;
            await _coverageService.AddProvincesMultiAsync(dto);
            return Ok(new { message = "İller başarıyla eklendi." });
        }

        [HttpPost("add-multi-district")]
        public async Task<IActionResult> AddMultiDistrict([FromBody] StoreMarketDistrictMultiCreateDto dto)
        {
            var storeId = await _userHelper.GetStoreId(User);
            dto.StoreId = storeId;
            await _coverageService.AddDistrictsMultiAsync(dto);
            return Ok(new { message = "İlçeler başarıyla eklendi." });
        }

        [HttpPost("add-multi-neighborhood")]
        public async Task<IActionResult> AddMultiNeighborhood([FromBody] StoreMarketNeighborhoodMultiCreateDto dto)
        {
            var storeId = await _userHelper.GetStoreId(User);
            dto.StoreId = storeId;
            await _coverageService.AddNeighborhoodsMultiAsync(dto);
            return Ok(new { message = "Mahalleler başarıyla eklendi." });
        }

        [HttpPost("add-multi-region")]
        public async Task<IActionResult> AddMultiRegion([FromBody] StoreMarketRegionMultiCreateDto dto)
        {
            var storeId = await _userHelper.GetStoreId(User);
            dto.StoreId = storeId;
            await _coverageService.AddRegionsMultiAsync(dto);
            return Ok(new { message = "Bölgeler başarıyla eklendi." });
        }

        [HttpPost("add-multi-state")]
        public async Task<IActionResult> AddMultiState([FromBody] StoreMarketStateMultiCreateDto dto)
        {
            var storeId = await _userHelper.GetStoreId(User);
            dto.StoreId = storeId;

            await _coverageService.AddStatesMultiAsync(dto);
            return Ok(new { message = "Eyaletler başarıyla eklendi." });
        }

        [HttpGet("my-countries")]
        public async Task<IActionResult> GetMyCountries()
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var result = await _coverageService.GetCountrysByStoreIdAsync(storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ülke kapsamları getirilirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("my-provinces")]
        public async Task<IActionResult> GetMyProvinces()
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var result = await _coverageService.GetProvincesByStoreIdAsync(storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İl kapsamları getirilirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("my-districts")]
        public async Task<IActionResult> GetMyDistricts()
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var result = await _coverageService.GetDistrictsByStoreIdAsync(storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İlçe kapsamları getirilirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("my-neighborhoods")]
        public async Task<IActionResult> GetMyNeighborhoods()
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var result = await _coverageService.GetNeighborhoodsByStoreIdAsync(storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mahalle kapsamları getirilirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("my-regions")]
        public async Task<IActionResult> GetMyRegions()
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var result = await _coverageService.GetRegionsByStoreIdAsync(storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölge kapsamları getirilirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("my-states")]
        public async Task<IActionResult> GetMyStates()
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var result = await _coverageService.GetStatesByStoreIdAsync(storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eyalet kapsamları getirilirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("update-country")]
        public async Task<IActionResult> UpdateCountry([FromBody] StoreMarketCountryUpdateDto dto)
        {
            try
            {
                var result = await _coverageService.UpdateCountryAsync(dto);
                return result ? Ok(new { Message = "Ülke kapsamı güncellendi." }) : BadRequest("Güncelleme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ülke kapsamı güncellenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("update-province")]
        public async Task<IActionResult> UpdateProvince([FromBody] StoreMarketProvinceUpdateDto dto)
        {
            try
            {
                var result = await _coverageService.UpdateProvinceAsync(dto);
                return result ? Ok(new { Message = "İl kapsamı güncellendi." }) : BadRequest("Güncelleme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İl kapsamı güncellenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("update-district")]
        public async Task<IActionResult> UpdateDistrict([FromBody] StoreMarketDistrictUpdateDto dto)
        {
            try
            {
                var result = await _coverageService.UpdateDistrictAsync(dto);
                return result ? Ok(new { Message = "İlçe kapsamı güncellendi." }) : BadRequest("Güncelleme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İlçe kapsamı güncellenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("update-neighborhood")]
        public async Task<IActionResult> UpdateNeighborhood([FromBody] StoreMarketNeighborhoodUpdateDto dto)
        {
            try
            {
                var result = await _coverageService.UpdateNeighborhoodAsync(dto);
                return result ? Ok(new { Message = "Mahalle kapsamı güncellendi." }) : BadRequest("Güncelleme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mahalle kapsamı güncellenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("update-region")]
        public async Task<IActionResult> UpdateRegion([FromBody] StoreMarketRegionUpdateDto dto)
        {
            try
            {
                var result = await _coverageService.UpdateRegionAsync(dto);
                return result ? Ok(new { Message = "Bölge kapsamı güncellendi." }) : BadRequest("Güncelleme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölge kapsamı güncellenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("update-state")]
        public async Task<IActionResult> UpdateState([FromBody] StoreMarketStateUpdateDto dto)
        {
            try
            {
                var result = await _coverageService.UpdateStateAsync(dto);
                return result ? Ok(new { Message = "Eyalet kapsamı güncellendi." }) : BadRequest("Güncelleme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eyalet kapsamı güncellenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpDelete("delete-country/{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            try
            {
                var result = await _coverageService.DeleteCountryAsync(id);
                return result ? Ok(new { Message = "Ülke kapsamı silindi." }) : BadRequest("Silme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ülke kapsamı silinirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("delete-province/{id}")]
        public async Task<IActionResult> DeleteProvince(int id)
        {
            try
            {
                var result = await _coverageService.DeleteProvinceAsync(id);
                return result ? Ok(new { Message = "İl kapsamı silindi." }) : BadRequest("Silme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İl kapsamı silinirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("delete-district/{id}")]
        public async Task<IActionResult> DeleteDistrict(int id)
        {
            try
            {
                var result = await _coverageService.DeleteDistrictAsync(id);
                return result ? Ok(new { Message = "İlçe kapsamı silindi." }) : BadRequest("Silme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "İlçe kapsamı silinirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("delete-neighborhood/{id}")]
        public async Task<IActionResult> DeleteNeighborhood(int id)
        {
            try
            {
                var result = await _coverageService.DeleteNeighborhoodAsync(id);
                return result ? Ok(new { Message = "Mahalle kapsamı silindi." }) : BadRequest("Silme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mahalle kapsamı silinirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("delete-region/{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            try
            {
                var result = await _coverageService.DeleteRegionAsync(id);
                return result ? Ok(new { Message = "Bölge kapsamı silindi." }) : BadRequest("Silme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölge kapsamı silinirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("delete-state/{id}")]
        public async Task<IActionResult> DeleteState(int id)
        {
            try
            {
                var result = await _coverageService.DeleteStateAsync(id);
                return result ? Ok(new { Message = "Eyalet kapsamı silindi." }) : BadRequest("Silme başarısız.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Eyalet kapsamı silinirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

    }
}