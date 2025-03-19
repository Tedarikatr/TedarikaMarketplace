namespace Entity.Markets
{
    public class MarketCity
    {
        public int Id { get; set; }

        public int MarketId { get; set; }
        public virtual Market Market { get; set; }

        public string Country { get; set; } 
        public string State { get; set; } 
        public string City { get; set; } 
    }
}
