using API.Helpers;
using Data.Dtos.Stores.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Product.IServices;
using Services.Stores.Product.IServices;

namespace API.Controllers.Stores.Products
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "seller")]
    public class SellerStoreProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStoreProductService _storeProductService;
        private readonly SellerUserContextHelper _userHelper;
        private readonly ILogger<SellerStoreProductController> _logger;

        public SellerStoreProductController(IProductService productService, IStoreProductService storeProductService, SellerUserContextHelper userHelper, ILogger<SellerStoreProductController> logger)
        {
            _productService = productService;
            _storeProductService = storeProductService;
            _userHelper = userHelper;
            _logger = logger;
        }

        [HttpGet("product-database-list-all")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("Tüm ürünler listeleniyor.");
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tüm ürünleri listelerken bir hata oluştu.");
                return StatusCode(500, new { Error = "Ürün listesi alınırken bir hata oluştu." });
            }
        }

        [HttpGet("my-products")]
        public async Task<IActionResult> GetAllStoreProducts()
        {
            try
            {
                var storeId = _userHelper.GetSellerId(User);
                _logger.LogInformation("Satıcıya ait ürünler listeleniyor. StoreId: {StoreId}", storeId);

                var result = await _storeProductService.GetAllProductsByShopDirectIdAsync(storeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satıcıya ait ürünler listelenirken bir hata oluştu.");
                return StatusCode(500, new { error = "Mağaza ürünleri listelenirken bir hata oluştu." });
            }
        }


        [HttpPost("{productId}/add")]
        public async Task<IActionResult> AddProductToStore(int productId)
        {
            try
            {
                var storeId = _userHelper.GetSellerId(User);
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

        [HttpPut("update-price-productId={productId}&price={price}")]
        public async Task<IActionResult> UpdatePriceAsync(int productId, decimal price)
        {
            try
            {
                int storeId = await _userHelper.GetStoreId(User);
                var result = await _storeProductService.UpdateStoreProductPriceAsync(storeId, productId, price);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fiyat güncelleme hatası.");
                return StatusCode(500, "Fiyat güncellenemedi.");
            }
        }


        [HttpPut("set-active-status")]
        public async Task<IActionResult> SetActiveStatusAsync([FromBody] SetActiveStatusRequest request)
        {
            try
            {
                int storeId = await _userHelper.GetStoreId(User);
                var result = await _storeProductService.SetProductActiveStatusAsync(storeId, request.ProductId, request.IsActive);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aktiflik durumu değiştirilemedi.");
                return StatusCode(500, "İşlem başarısız.");
            }
        }

        [HttpPut("set-on-sale")]
        public async Task<IActionResult> SetOnSaleStatusAsync([FromBody] SetOnSaleStatusRequest request)
        {
            try
            {
                int storeId = await _userHelper.GetStoreId(User);
                var result = await _storeProductService.SetProductOnSaleStatusAsync(storeId, request.ProductId, request.IsOnSale);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Satış durumu güncellenemedi.");
                return StatusCode(500, "İşlem başarısız.");
            }
        }


        [HttpPut("set-quantity-limits-productId={productId}&minQty={minQty}&maxQty={maxQty}")]
        public async Task<IActionResult> SetQuantityLimitsAsync(int productId, int minQty, int maxQty)
        {
            try
            {
                int storeId = await _userHelper.GetStoreId(User);
                var result = await _storeProductService.SetMinMaxQuantityAsync(storeId, productId, minQty, maxQty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Min/Max miktar güncellemesi başarısız.");
                return StatusCode(500, "İşlem başarısız.");
            }
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImageAsync(int productId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Geçerli bir dosya yükleyiniz.");

            try
            {
                int storeId = await _userHelper.GetStoreId(User);
                var result = await _storeProductService.UploadAndSetStoreImageAsync(storeId, productId, file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Görsel yükleme işlemi başarısız.");
                return StatusCode(500, "Görsel yüklenirken bir hata oluştu.");
            }
        }
    }
}
