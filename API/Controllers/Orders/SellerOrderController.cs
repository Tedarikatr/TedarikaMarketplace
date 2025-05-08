using API.Helpers;
using Data.Dtos.Orders;
using Entity.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Orders.IServices;

namespace API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "seller")]
    public class SellerOrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly SellerUserContextHelper _sellerContext;

        public SellerOrderController(IOrderService orderService, SellerUserContextHelper sellerContext)
        {
            _orderService = orderService;
            _sellerContext = sellerContext;
        }

        [HttpGet("store-orders")]
        public async Task<ActionResult<List<OrderListDto>>> GetOrders()
        {
            try
            {
                int storeId = await _sellerContext.GetStoreId(User);
                var orders = await _orderService.GetOrdersByStoreIdAsync(storeId);
                return Ok(orders);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }

        [HttpGet("detail/{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderDetail(int orderId)
        {
            try
            {
                int sellerId = _sellerContext.GetSellerId(User);
                var order = await _orderService.GetOrderByIdAsync(orderId, sellerId);
                return Ok(order);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sipariş bilgisi alınamadı: {ex.Message}");
            }
        }

        [HttpGet("paged")]
        public async Task<ActionResult<List<OrderListDto>>> GetPaginatedOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                int storeId = await _sellerContext.GetStoreId(User);
                var orders = await _orderService.GetOrdersByStoreIdAsync(storeId);

                var paginated = orders
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Ok(paginated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sayfalama başarısız: {ex.Message}");
            }
        }

        [HttpPut("update-status/{orderId}")]
        public async Task<IActionResult> UpdateStatus(int orderId, [FromBody] OrderStatus newStatus)
        {
            try
            {
                bool updated = await _orderService.UpdateOrderStatusAsync(orderId, newStatus);
                if (!updated)
                    return NotFound("Sipariş bulunamadı veya güncellenemedi.");

                return Ok("Sipariş durumu güncellendi.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"İşlem başarısız: {ex.Message}");
            }
        }

        [HttpPut("cancel/{orderId}")]
        public async Task<IActionResult> Cancel(int orderId)
        {
            try
            {
                int sellerId = _sellerContext.GetSellerId(User);
                bool result = await _orderService.CancelOrderAsync(orderId, sellerId);

                if (!result)
                    return BadRequest("İptal işlemi başarısız. Sipariş bulunamadı veya iptal edilemez.");

                return Ok("Sipariş iptal edildi.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sunucu hatası: {ex.Message}");
            }
        }
    }
}
