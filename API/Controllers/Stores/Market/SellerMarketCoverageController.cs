using API.Helpers;
using Data.Dtos.Stores.Markets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Locations.IServices;
using Services.Stores.Markets.IServices;

namespace API.Controllers.Stores.Market
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize]
    public class SellerMarketCoverageController : ControllerBase
    {
        private readonly IStoreLocationService _coverageService;
        private readonly ILocationService _marketLocationService;
        private readonly SellerUserContextHelper _userHelper;
        private readonly ILogger<SellerMarketCoverageController> _logger;

        public SellerMarketCoverageController(IStoreLocationService coverageService, ILocationService marketLocationService, SellerUserContextHelper userHelper, ILogger<SellerMarketCoverageController> logger)
        {
            _coverageService = coverageService;
            _marketLocationService = marketLocationService;
            _userHelper = userHelper;
            _logger = logger;
        }

        [HttpPost("add-coverage")]
        public async Task<IActionResult> AddCoverage([FromBody] StoreMarketCoverageCompositeCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                dto.StoreId = storeId;

                var addedIds = await _coverageService.AddCompositeCoverageAsync(dto);

                return Ok(new
                {
                    Message = "Kapsamlar başarıyla eklendi.",
                    AddedCount = addedIds.Count,
                    AddedIds = addedIds
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toplu coverage eklenirken hata oluştu.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("my-coverage-hierarchy")]
        public async Task<IActionResult> GetMyCoverageHierarchy()
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var data = await _coverageService.GetCoverageHierarchyByStoreIdAsync(storeId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kapsam hiyerarşisi getirilirken hata.");
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPut("update-coverage/{type}")]
        public async Task<IActionResult> UpdateCoverage([FromRoute] CoverageType type, [FromBody] StoreMarketCoverageUpdateBaseDto dto)
        {
            try
            {
                _logger.LogInformation("🛠️ Coverage güncelleme isteği alındı. Type: {Type}, Id: {Id}", type, dto.Id);

                var result = await _coverageService.UpdateCoverageAsync(dto, type);

                if (result)
                {
                    return Ok(new
                    {
                        Message = $"{type} kapsamı başarıyla güncellendi.",
                        CoverageType = type.ToString(),
                        CoverageId = dto.Id
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        Message = $"{type} kapsamı bulunamadı.",
                        CoverageType = type.ToString(),
                        CoverageId = dto.Id
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Coverage güncelleme sırasında hata oluştu. Type: {Type}, Id: {Id}", type, dto.Id);
                return StatusCode(500, new
                {
                    Error = "Kapsam güncellenirken beklenmeyen bir hata oluştu.",
                    Exception = ex.Message
                });
            }
        }

        [HttpPost("delete-coverage")]
        public async Task<IActionResult> DeleteCompositeCoverage([FromBody] StoreMarketCoverageCompositeDeleteDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                dto.StoreId = storeId;

                var deletedCount = await _coverageService.DeleteCompositeCoverageAsync(dto);

                if (deletedCount == 0)
                {
                    return NotFound(new
                    {
                        Message = "Silinecek kapsam bulunamadı.",
                        DeletedCount = 0
                    });
                }

                return Ok(new
                {
                    Message = "Kapsamlar başarıyla silindi.",
                    DeletedCount = deletedCount
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "⚠️ Composite silme işleminde uygulama hatası.");
                return BadRequest(new
                {
                    Message = "Kapsam silinirken bir hata oluştu.",
                    Error = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Composite silme işleminde beklenmeyen hata.");
                return StatusCode(500, new
                {
                    Message = "Sunucu hatası. Lütfen daha sonra tekrar deneyin.",
                    Error = ex.Message
                });
            }
        }
    }
}