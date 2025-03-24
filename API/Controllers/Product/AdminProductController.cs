using Data.Dtos.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Product.IServices;

namespace API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "admin")]
    //[Authorize(Roles = "SuperAdmin, Admin")]
    public class AdminProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<AdminProductController> _logger;

        public AdminProductController(IProductService productService, ILogger<AdminProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpGet("all")]
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

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            try
            {
                _logger.LogInformation("Ürün bilgisi alınıyor. Ürün ID: {ProductId}", productId);
                var product = await _productService.GetProductByIdAsync(productId);
                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün bilgisi alınırken bir hata oluştu. Ürün ID: {ProductId}", productId);
                return StatusCode(500, new { Error = "Ürün bilgisi alınırken bir hata oluştu." });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromForm] ProductCreateDto dto)
        {
            try
            {
                _logger.LogInformation("Yeni ürün oluşturma isteği alındı.");
                var result = await _productService.CreateProductAsync(dto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün oluşturulurken hata oluştu.");
                return StatusCode(500, new { Error = "Ürün oluşturulurken bir hata oluştu." });
            }
        }

        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromForm] ProductUpdateDto dto)
        {
            try
            {
                _logger.LogInformation("Ürün güncelleme işlemi başlatıldı. Ürün ID: {ProductId}", productId);
                var result = await _productService.UpdateProductAsync(productId, dto);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün güncellenirken hata oluştu. Ürün ID: {ProductId}", productId);
                return StatusCode(500, new { Error = "Ürün güncellenirken bir hata oluştu." });
            }
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try
            {
                _logger.LogInformation("Ürün silme işlemi başlatıldı. Ürün ID: {ProductId}", productId);
                var result = await _productService.DeleteProductAsync(productId);
                return Ok(new { Message = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ürün silinirken hata oluştu. Ürün ID: {ProductId}", productId);
                return StatusCode(500, new { Error = "Ürün silinirken bir hata oluştu." });
            }
        }
    }
}
