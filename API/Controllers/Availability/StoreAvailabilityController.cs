using API.Helpers;
using Data.Dtos.Availability;
using Data.Dtos.Stores.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Availability.IServices;

namespace API.Controllers.Availability
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "buyer")]
    public class StoreAvailabilityController : ControllerBase
    {
        private readonly IStoreAvailabilityService _storeAvailabilityService;
        private readonly BuyerUserContextHelper _contextHelper;
        private readonly ILogger<StoreAvailabilityController> _logger;

        public StoreAvailabilityController(
            IStoreAvailabilityService storeAvailabilityService,
            BuyerUserContextHelper contextHelper,
            ILogger<StoreAvailabilityController> logger)
        {
            _storeAvailabilityService = storeAvailabilityService;
            _contextHelper = contextHelper;
            _logger = logger;
        }

        [HttpGet("stores")]
        public async Task<ActionResult<List<AvailableStoreDto>>> GetAvailableStores()
        {
            var buyerId = _contextHelper.GetBuyerId(User);
            var result = await _storeAvailabilityService.GetAvailableStoresByAddressAsync(buyerId);

            if (result == null || !result.Any())
            {
                _logger.LogInformation("BuyerId: {BuyerId} için uygun mağaza bulunamadı.", buyerId);
                return NotFound("Uygun mağaza bulunamadı.");
            }

            return Ok(result);
        }

        [HttpGet("stores-with-products")]
        public async Task<ActionResult<List<AvailableStoreWithProductsDto>>> GetAvailableStoresWithProducts()
        {
            var buyerId = _contextHelper.GetBuyerId(User);
            var result = await _storeAvailabilityService.GetAvailableStoresWithProductsByAddressAsync(buyerId);

            if (result == null || !result.Any())
            {
                _logger.LogInformation("BuyerId: {BuyerId} için ürünlü mağaza bulunamadı.", buyerId);
                return NotFound("Uygun mağaza ve ürün kombinasyonu bulunamadı.");
            }

            return Ok(result);
        }
    }
}
