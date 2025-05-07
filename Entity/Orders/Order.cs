using Entity.Auths;
using Entity.Carriers;
using Entity.Companies;
using Entity.DeliveryAddresses;
using Entity.Payments;
using Entity.Stores;

namespace Entity.Orders
{
    public class Order
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }
        public BuyerUser Buyer { get; set; }

        public int? BuyerCompanyId { get; set; }
        public Company BuyerCompany { get; set; }

        public int? PaymentId { get; set; }
        public Payment Payment { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public int? SelectedCarrierId { get; set; }
        public Carrier SelectedCarrier { get; set; }

        public int DeliveryAddressId { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }

        public string OrderNumber { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }

        public OrderStatus Status { get; set; } 

        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        Created = 1,
        AwaitingPayment = 2,
        Paid = 3,
        Shipped = 4,
        Delivered = 5,
        Cancelled = 6
    }

}
