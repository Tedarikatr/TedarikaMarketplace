using Entity.Auths;
using Entity.Carriers;
using Entity.Companies;
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

        public int StoreId { get; set; }
        public Store Store { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; }

        public int? SelectedCarrierId { get; set; }
        public Carrier SelectedCarrier { get; set; }

        public OrderStatus Status { get; set; } 

        public ICollection<OrderItem> OrderItems { get; set; }
    }

    public enum OrderStatus
    {
        Pending, // Bekliyor
        Processing, // İşleniyor
        Shipped, // Kargolandı
        Delivered, // Teslim Edildi
        Canceled // İptal Edildi
    }

}
