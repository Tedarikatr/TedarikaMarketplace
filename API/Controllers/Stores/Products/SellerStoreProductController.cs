using API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Stores.Product.IServices;

namespace API.Controllers.Stores.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Seller")]
    [ApiExplorerSettings(GroupName = "seller")]
    public class SellerStoreProductController : ControllerBase
    {
        private readonly IStoreProductService _storeProductService;
        private readonly ILogger<SellerStoreProductController> _logger;

        public SellerStoreProductController(IStoreProductService storeProductService, ILogger<SellerStoreProductController> logger)
        {
            _storeProductService = storeProductService;
            _logger = logger;
        }

        [HttpPost("{productId}/add")]
        public async Task<IActionResult> AddProductToStore(int productId)
        {
            try
            {
                var storeId = SellerUserContextHelper.GetSellerId(User);
                _logger.LogInformation("Mağaza ürünü ekliyor. StoreId: {StoreId}, ProductId: {ProductId}", storeId, productId);

                var result = await _storeProductService.AddProductToStoreAsync(storeId, productId);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün mağazaya eklenemedi.");
                return StatusCode(500, new { error = "Ürün mağazaya eklenirken bir hata oluştu." });
            }
        }

        [HttpPatch("{productId}/price")]
        public async Task<IActionResult> UpdatePrice(int productId, [FromQuery] decimal price)
        {
            try
            {
                var storeId = SellerUserContextHelper.GetSellerId(User);
                var result = await _storeProductService.UpdateStoreProductPriceAsync(storeId, productId, price);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün fiyatı güncellenemedi.");
                return StatusCode(500, new { error = "Fiyat güncellenirken bir hata oluştu." });
            }
        }

        [HttpPatch("{productId}/status")]
        public async Task<IActionResult> SetActiveStatus(int productId, [FromQuery] bool isActive)
        {
            try
            {
                var storeId = SellerUserContextHelper.GetSellerId(User);
                var result = await _storeProductService.SetProductActiveStatusAsync(storeId, productId, isActive);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün aktiflik durumu değiştirilemedi.");
                return StatusCode(500, new { error = "Aktiflik durumu güncellenemedi." });
            }
        }

        [HttpPatch("{productId}/sale")]
        public async Task<IActionResult> SetSaleStatus(int productId, [FromQuery] bool isOnSale)
        {
            try
            {
                var storeId = SellerUserContextHelper.GetSellerId(User);
                var result = await _storeProductService.SetProductOnSaleStatusAsync(storeId, productId, isOnSale);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış durumu güncellenemedi.");
                return StatusCode(500, new { error = "Satış durumu güncellenirken hata oluştu." });
            }
        }

        [HttpPatch("{productId}/quantity")]
        public async Task<IActionResult> SetMinMaxQuantity(int productId, [FromQuery] int minQty, [FromQuery] int maxQty)
        {
            try
            {
                var storeId = SellerUserContextHelper.GetSellerId(User);
                var result = await _storeProductService.SetMinMaxQuantityAsync(storeId, productId, minQty, maxQty);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Min/Max sipariş miktarları güncellenemedi.");
                return StatusCode(500, new { error = "Sipariş sınırları güncellenirken hata oluştu." });
            }
        }

        [HttpPatch("{productId}/regions")]
        public async Task<IActionResult> SetAllowedRegions(int productId, [FromQuery] bool allowedDomestic, [FromQuery] bool allowedInternational)
        {
            try
            {
                var storeId = SellerUserContextHelper.GetSellerId(User);
                var result = await _storeProductService.SetAllowedRegionsAsync(storeId, productId, allowedDomestic, allowedInternational);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölgesel izinler güncellenemedi.");
                return StatusCode(500, new { error = "Bölgesel izinler güncellenirken hata oluştu." });
            }
        }

        [HttpPatch("{productId}/image")]
        public async Task<IActionResult> UpdateStoreImage(int productId, [FromQuery] string imageUrl)
        {
            try
            {
                var storeId = SellerUserContextHelper.GetSellerId(User);
                var result = await _storeProductService.UpdateStoreImageAsync(storeId, productId, imageUrl);
                return Ok(new { message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Mağaza görseli güncellenemedi.");
                return StatusCode(500, new { error = "Mağaza görseli güncellenirken bir hata oluştu." });
            }
        }
    }
}
