using Entity.Auth;
using Entity.Orders;

namespace Entity.Stores
{
    public class Store
    {
        public int StoreId { get; set; }
        public int OwnerId { get; set; }
        public SellerUser Owner { get; set; }
        public string StoreName { get; set; }
        public bool IsApproved { get; set; }
        public string AccountingIntegration { get; set; } 
        public ICollection<StoreMarket> StoreMarkets { get; set; }
        public ICollection<StoreProduct> StoreProducts { get; set; }
        public ICollection<StoreCarrier> StoreCarriers { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
