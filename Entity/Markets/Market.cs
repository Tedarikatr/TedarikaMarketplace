using Entity.Stores.Markets;

namespace Entity.Markets
{
    public class Market
    {
        public int Id { get; set; }
        public string Name { get; set; }  
        public string RegionCode { get; set; }  

        public bool IsActive { get; set; } 
        public bool IsLocal { get; set; } 
        public bool IsRegional { get; set; } 
        public bool IsGlobal { get; set; } 

        public int DeliveryTimeFrame { get; set; } 

        public ICollection<StoreMarket> StoreMarkets { get; set; } 
    }
}
