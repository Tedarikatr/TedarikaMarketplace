using Entity.Stores;

namespace Entity.Orders
{
    public class Order
    {
        public int OrderId { get; set; }
        public int BuyerId { get; set; }
        public User Buyer { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }
        public int? SelectedCarrierId { get; set; }
        public Carrier SelectedCarrier { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }

}
