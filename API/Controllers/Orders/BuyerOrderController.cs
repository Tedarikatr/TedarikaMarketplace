using API.Helpers;
using Data.Dtos.Orders;
using Entity.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Orders.IServices;

namespace API.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ApiExplorerSettings(GroupName = "buyer")]
    public class BuyerOrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly BuyerUserContextHelper _buyerContext;

        public BuyerOrderController(IOrderService orderService, BuyerUserContextHelper buyerContext)
        {
            _orderService = orderService;
            _buyerContext = buyerContext;
        }

        [HttpPost("create")]
        public async Task<ActionResult<OrderDto>> CreateOrder([FromBody] OrderCreateDto dto)
        {
            try
            {
                int buyerId = _buyerContext.GetBuyerId(User);
                var result = await _orderService.CreateOrderAsync(dto, buyerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sipariş oluşturulamadı: {ex.Message}");
            }
        }

        [HttpGet("my-orders")]
        public async Task<ActionResult<List<OrderListDto>>> GetMyOrders()
        {
            try
            {
                int buyerId = _buyerContext.GetBuyerId(User);
                var orders = await _orderService.GetOrdersByBuyerIdAsync(buyerId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Siparişler alınamadı: {ex.Message}");
            }
        }

        [HttpGet("detail/{orderId}")]
        public async Task<ActionResult<OrderDto>> GetOrderDetail(int orderId)
        {
            try
            {
                int buyerId = _buyerContext.GetBuyerId(User);
                var order = await _orderService.GetOrderByIdAsync(orderId, buyerId);
                return Ok(order);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sipariş detayına erişilemedi: {ex.Message}");
            }
        }

        [HttpGet("status/{orderId}")]
        public async Task<ActionResult<OrderStatus>> GetOrderStatus(int orderId)
        {
            try
            {
                int buyerId = _buyerContext.GetBuyerId(User);
                var status = await _orderService.GetOrderStatusAsync(orderId, buyerId);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Durum bilgisi alınamadı: {ex.Message}");
            }
        }

        [HttpPut("cancel/{orderId}")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            try
            {
                int buyerId = _buyerContext.GetBuyerId(User);
                var result = await _orderService.CancelOrderAsync(orderId, buyerId);
                if (!result)
                    return BadRequest("Sipariş iptal edilemedi veya bulunamadı.");

                return Ok("Sipariş iptal edildi.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"İptal işlemi başarısız: {ex.Message}");
            }
        }

        [HttpGet("is-paid/{orderId}")]
        public async Task<ActionResult<bool>> IsOrderPaid(int orderId)
        {
            try
            {
                var isPaid = await _orderService.IsOrderPaidAsync(orderId);
                return Ok(isPaid);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ödeme durumu kontrol edilemedi: {ex.Message}");
            }
        }
    }
}
