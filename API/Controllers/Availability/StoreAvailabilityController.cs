using API.Helpers;
using Data.Dtos.Availability;
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

        public StoreAvailabilityController(IStoreAvailabilityService storeAvailabilityService, BuyerUserContextHelper contextHelper, ILogger<StoreAvailabilityController> logger)
        {
            _storeAvailabilityService = storeAvailabilityService;
            _contextHelper = contextHelper;
            _logger = logger;
        }

        [HttpGet("stores")]
        public async Task<ActionResult<List<AvailableStoreDto>>> GetAvailableStores()
        {
            try
            {
                int buyerId = _contextHelper.GetBuyerId(User);
                _logger.LogInformation("BuyerId: {BuyerId} için uygun mağazalar listeleniyor.", buyerId);

                var stores = await _storeAvailabilityService.GetAvailableStoresByAddressAsync(buyerId);

                if (stores == null || !stores.Any())
                {
                    _logger.LogWarning("BuyerId: {BuyerId} için uygun mağaza bulunamadı.", buyerId);
                    return NotFound("Uygun mağaza bulunamadı.");
                }

                return Ok(stores);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Uygun mağazalar listelenirken hata oluştu.");
                return StatusCode(500, "Sunucu hatası: mağaza verileri alınamadı.");
            }
        }

        [HttpGet("stores-with-products")]
        public async Task<ActionResult<List<AvailableStoreWithProductsDto>>> GetAvailableStoresWithProducts()
        {
            try
            {
                int buyerId = _contextHelper.GetBuyerId(User);
                _logger.LogInformation("BuyerId: {BuyerId} için ürünlü mağazalar listeleniyor.", buyerId);

                var storesWithProducts = await _storeAvailabilityService.GetAvailableStoresWithProductsByAddressAsync(buyerId);

                if (storesWithProducts == null || !storesWithProducts.Any())
                {
                    _logger.LogWarning("BuyerId: {BuyerId} için ürünlü mağaza bulunamadı.", buyerId);
                    return NotFound("Uygun mağaza ve ürün kombinasyonu bulunamadı.");
                }

                return Ok(storesWithProducts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürünlü mağazalar listelenirken hata oluştu.");
                return StatusCode(500, "Sunucu hatası: ürünlü mağaza verileri alınamadı.");
            }
        }
    }
}
