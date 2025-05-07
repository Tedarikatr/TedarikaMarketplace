using API.Helpers;
using Data.Dtos.Baskets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Baskets.IServices;

namespace API.Controllers.Baskets
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "buyer")]
    public class BuyerBasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly BuyerUserContextHelper _userHelper;

        public BuyerBasketController(IBasketService basketService, BuyerUserContextHelper userHelper)
        {
            _basketService = basketService;
            _userHelper = userHelper;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetBasket()
        {
            try
            {
                int userId = _userHelper.GetBuyerId(User);
                var basket = await _basketService.GetBasketAsync(userId);

                if (basket == null)
                {
                    Log.Warning("Kullanıcının sepeti bulunamadı. BuyerId: {UserId}", userId);
                    return NotFound("Sepet bulunamadı.");
                }

                return Ok(basket);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Sepet getirme işlemi sırasında hata oluştu.");
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItem([FromBody] BasketAddToDto dto)
        {
            try
            {
                int userId = _userHelper.GetBuyerId(User);
                await _basketService.AddItemAsync(userId, dto);
                return Ok("Ürün sepete eklendi.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Sepete ürün eklenemedi. DTO: {@Dto}", dto);
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("increase/{productId}")]
        public async Task<IActionResult> IncreaseQuantity(int productId)
        {
            try
            {
                int userId = _userHelper.GetBuyerId(User);
                var basket = await _basketService.IncreaseQuantityAsync(userId, productId);
                return Ok(basket);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Sepet ürünü miktarı artırılamadı. ProductId: {ProductId}", productId);
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("decrease/{productId}")]
        public async Task<IActionResult> DecreaseQuantity(int productId)
        {
            try
            {
                int userId = _userHelper.GetBuyerId(User);
                var basket = await _basketService.DecreaseQuantityAsync(userId, productId);
                return Ok(basket);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Sepet ürünü miktarı azaltılamadı. ProductId: {ProductId}", productId);
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            try
            {
                int userId = _userHelper.GetBuyerId(User);
                await _basketService.RemoveItemAsync(userId, productId);
                return Ok("Ürün sepetten silindi.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ürün sepetten silinemedi. ProductId: {ProductId}", productId);
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearBasket()
        {
            try
            {
                int userId = _userHelper.GetBuyerId(User);
                await _basketService.ClearBasketAsync(userId);
                return Ok("Sepet temizlendi.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Sepet temizlenemedi.");
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotal()
        {
            try
            {
                int userId = _userHelper.GetBuyerId(User);
                var total = await _basketService.GetTotalAsync(userId);
                return Ok(new { TotalAmount = total });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Sepet toplamı getirilemedi.");
                return StatusCode(500, new { Error = ex.Message });
            }
        }
    }
}
