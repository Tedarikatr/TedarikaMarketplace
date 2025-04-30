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
        public string StoreDescription { get; set; }
        public string ImageUrl { get; set; }
        public string StorePhone { get; set; }
        public string StoreMail { get; set; }
        public string StoreProvince { get; set; }
        public string StoreDistrict { get; set; }

        public int SellerId { get; set; }
        public SellerUser SellerUser { get; set; }

        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }

        public int CompanyId { get; set; } 
        public virtual Company Company { get; set; }

        public ICollection<StoreProduct> StoreProducts { get; set; }
        public ICollection<StoreCarrier> StoreCarriers { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<StoreMarketCountry> MarketCountries { get; set; }
        public ICollection<StoreMarketProvince> MarketProvinces { get; set; }
        public ICollection<StoreMarketDistrict> MarketDistricts { get; set; }
        public ICollection<StoreMarketNeighborhood> MarketNeighborhoods { get; set; }
        public ICollection<StoreMarketRegion> MarketRegions { get; set; }
        public ICollection<StoreMarketState> MarketStates { get; set; }


    }
}
