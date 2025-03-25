using Microsoft.AspNetCore.Mvc;
using Services.Stores;

namespace API.Controllers.Stores
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminStoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly ILogger<AdminStoreController> _logger;

        public AdminStoreController(IStoreService storeService, ILogger<AdminStoreController> logger)
        {
            _storeService = storeService;
            _logger = logger;
        }

        [HttpGet("list-all-stores")]
        public async Task<IActionResult> GetAllStores()
        {
            try
            {
                var result = await _storeService.GetAllStoresAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağazalar getirilirken hata oluştu.");
                return StatusCode(500, new { Error = "Mağazalar getirilirken hata oluştu." });
            }
        }

        [HttpPut("approve-store/{storeId}")]
        public async Task<IActionResult> ApproveStore(int storeId, [FromQuery] bool isApproved)
        {
            try
            {
                var result = await _storeService.ApproveStoreAsync(storeId, isApproved);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza onay durumu değiştirilirken hata oluştu.");
                return StatusCode(500, new { Error = "Mağaza onay durumu değiştirilirken hata oluştu." });
            }
        }
    }
}
