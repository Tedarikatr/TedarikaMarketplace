using Data.Dtos.Orders;
using Entity.Orders;

namespace Services.Orders.IServices
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderCreateDto createDto, int buyerId);

        Task<OrderDto> GetOrderByIdAsync(int orderId, int buyerId);

        Task<List<OrderListDto>> GetOrdersByBuyerIdAsync(int buyerId);

        Task<OrderStatus> GetOrderStatusAsync(int orderId, int buyerId);

        Task<List<OrderListDto>> GetOrdersByStoreIdAsync(int storeId);

        Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);

        Task<bool> CancelOrderAsync(int orderId, int buyerId);

        Task<bool> IsOrderPaidAsync(int orderId);
    }
}
