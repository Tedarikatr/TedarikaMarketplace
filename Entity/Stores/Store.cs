using Entity.Auths;
using Entity.Companies;
using Entity.Orders;
using Entity.Stores.Carriers;
using Entity.Stores.Markets;
using Entity.Stores.Products;

namespace Entity.Stores
{
    public class Store
    {
        public int Id { get; set; }

        public string StoreName { get; set; }

        public int OwnerId { get; set; }
        public SellerUser Owner { get; set; }

        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }

        public int CompanyId { get; set; } 
        public virtual Company Company { get; set; }

        public string Country { get; set; }
        public string City { get; set; }

        public ICollection<StoreMarket> StoreMarkets { get; set; }
        public ICollection<StoreProduct> StoreProducts { get; set; }
        public ICollection<StoreCarrier> StoreCarriers { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
