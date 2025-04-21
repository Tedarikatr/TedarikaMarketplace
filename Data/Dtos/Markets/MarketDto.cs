using Entity.Markets.Locations;

namespace Data.Dtos.Markets
{
    public class MarketDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string RegionCode { get; set; }
        public bool IsActive { get; set; }
    }

    public class MarketCreateDto
    {
        public string Name { get; set; }
        public string RegionCode { get; set; }
        public MarketType MarketType { get; set; }

    }

    public class MarketUpdateDto
    {
        public string Name { get; set; }
        public string RegionCode { get; set; }
        public bool IsActive { get; set; }
        public MarketType MarketType { get; set; }

    }
}
