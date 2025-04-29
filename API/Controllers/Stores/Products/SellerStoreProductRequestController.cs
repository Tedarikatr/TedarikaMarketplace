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
        public async Task<IActionResult> CreateStoreProductRequest([FromForm] StoreProductRequestCreateDto dto)
        {
            try
            {
                dto.StoreId = _userHelper.GetSellerId(User);

                var result = await _requestService.CreateStoreProductRequestAsync(dto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün başvurusu oluşturulurken hata oluştu.");
                return StatusCode(500, new { Error = "Ürün başvurusu eklenemedi." });
            }
        }
    }
}
