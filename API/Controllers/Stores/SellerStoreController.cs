using Data.Dtos.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Stores;
using System.Security.Claims;

namespace API.Controllers.Stores
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize(Roles = "Seller")]
    public class SellerStoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ILogger<SellerStoreController> _logger;

        public SellerStoreController(IStoreService storeService, ILogger<SellerStoreController> logger)
        {
            _storeService = storeService;
            _logger = logger;
        }

        [HttpPost("create-store")]
        public async Task<IActionResult> CreateStore([FromBody] StoreCreateDto storeCreateDto)
        {
            try
            {
                var sellerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (sellerId == 0)
                {
                    _logger.LogWarning("Geçersiz seller ID.");
                    return Unauthorized(new { Error = "Yetkilendirme başarısız." });
                }

                var result = await _storeService.CreateStoreAsync(storeCreateDto, sellerId);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza oluşturulurken hata oluştu.");
                return StatusCode(500, new { Error = "Mağaza oluşturulurken hata oluştu." });
            }
        }

        [HttpPut("update-store/{storeId}")]
        public async Task<IActionResult> UpdateStore(int storeId, [FromBody] StoreUpdateDto storeUpdateDto)
        {
            try
            {
                var sellerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (sellerId == 0)
                {
                    _logger.LogWarning("Geçersiz seller ID.");
                    return Unauthorized(new { Error = "Yetkilendirme başarısız." });
                }

                var result = await _storeService.UpdateStoreAsync(storeUpdateDto, storeId, sellerId);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza güncellenirken hata oluştu.");
                return StatusCode(500, new { Error = "Mağaza güncellenirken hata oluştu." });
            }
        }

        [HttpPut("set-active/{storeId}")]
        public async Task<IActionResult> SetStoreStatus(int storeId, [FromQuery] bool isActive)
        {
            try
            {
                var sellerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
                if (sellerId == 0)
                {
                    _logger.LogWarning("Geçersiz seller ID.");
                    return Unauthorized(new { Error = "Yetkilendirme başarısız." });
                }

                var result = await _storeService.SetStoreStatusAsync(storeId, isActive, sellerId);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza durumu değiştirilirken hata oluştu.");
                return StatusCode(500, new { Error = "Mağaza durumu değiştirilirken hata oluştu." });
            }
        }
    }
}
