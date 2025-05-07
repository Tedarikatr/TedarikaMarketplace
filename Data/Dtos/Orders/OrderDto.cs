using Entity.Orders;
using Entity.Payments;

namespace Data.Dtos.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public string Currency { get; set; }
        public string ShippingAddress { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string StoreName { get; set; }
        public string BuyerName { get; set; }
        public string? CarrierName { get; set; }

        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public string ProductName { get; set; }
        public string? StoreProductImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderCreateDto
    {
        public int StoreId { get; set; }
        public int DeliveryAddressId { get; set; }
        public int? SelectedCarrierId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public List<OrderItemCreateDto> Items { get; set; }
    }

    public class OrderItemCreateDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? StoreProductImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderListDto
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
