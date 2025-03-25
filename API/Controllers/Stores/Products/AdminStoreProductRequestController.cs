using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Stores.Product.IServices;

namespace API.Controllers.Stores.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    [Authorize(Roles = "Admin")]
    public class AdminStoreProductRequestController : ControllerBase
    {
        private readonly IStoreProductRequestService _requestService;
        private readonly ILogger<AdminStoreProductRequestController> _logger;

        public AdminStoreProductRequestController(IStoreProductRequestService requestService, ILogger<AdminStoreProductRequestController> logger)
        {
            _requestService = requestService;
            _logger = logger;
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingRequests()
        {
            try
            {
                var result = await _requestService.GetPendingRequestsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bekleyen başvurular listelenirken hata oluştu.");
                return StatusCode(500, new { Error = "Başvurular listelenemedi." });
            }
        }

        [HttpPost("approve/{requestId}")]
        public async Task<IActionResult> ApproveRequest(int requestId)
        {
            try
            {
                var result = await _requestService.ApproveStoreProductRequestAsync(requestId);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru onaylanırken hata oluştu.");
                return StatusCode(500, new { Error = "Başvuru onaylanamadı." });
            }
        }
    }
}
