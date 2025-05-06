using API.Helpers;
using Data.Dtos.Stores.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Stores.Product.IServices;

namespace API.Controllers.Stores.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "seller")]
    [Authorize]
    public class SellerStoreProductRequestController : ControllerBase
    {
        private readonly IStoreProductRequestService _requestService;
        private readonly SellerUserContextHelper _userHelper;
        private readonly ILogger<SellerStoreProductRequestController> _logger;

        public SellerStoreProductRequestController(IStoreProductRequestService requestService, SellerUserContextHelper userHelper, ILogger<SellerStoreProductRequestController> logger)
        {
            _requestService = requestService;
            _userHelper = userHelper;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRequest([FromForm] StoreProductRequestCreateDto dto)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var result = await _requestService.CreateStoreProductRequestAsync(storeId, dto);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün başvurusu oluşturulurken hata oluştu.");
                return StatusCode(500, "Başvuru oluşturulurken bir hata meydana geldi.");
            }
        }

        [HttpGet("my-requests")]
        public async Task<IActionResult> GetMyRequests()
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var requests = await _requestService.GetMyRequestsAsync(storeId);
                return Ok(requests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kullanıcının başvuruları getirilirken hata oluştu.");
                return StatusCode(500, "Başvurular getirilirken bir hata meydana geldi.");
            }
        }

        [HttpGet("request-detail/{requestId}")]
        public async Task<IActionResult> GetRequestDetail(int requestId)
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var detail = await _requestService.GetRequestDetailAsync(requestId, storeId);
                if (detail == null)
                    return NotFound("Başvuru bulunamadı.");

                return Ok(detail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru detayı getirilirken hata oluştu.");
                return StatusCode(500, "Başvuru detayı getirilirken bir hata meydana geldi.");
            }
        }

        [HttpGet("request-summary")]
        public async Task<IActionResult> GetRequestSummary()
        {
            try
            {
                var storeId = await _userHelper.GetStoreId(User);
                var summary = await _requestService.GetRequestSummaryAsync(storeId);
                return Ok(summary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Başvuru özeti getirilirken hata oluştu.");
                return StatusCode(500, "Başvuru özeti getirilirken bir hata meydana geldi.");
            }
        }
    }
}
