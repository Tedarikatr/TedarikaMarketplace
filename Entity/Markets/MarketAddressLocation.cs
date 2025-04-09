using Entity.Markets.Locations;

namespace Entity.Markets
{
    public class MarketAddressLocation
    {
        public int Id { get; set; }

        public MarketCoverageLevel CoverageLevel { get; set; }

        public int MarketId { get; set; }
        public Market Market { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }


        public bool IsActive { get; set; }

    }

}



//public bool IsBuyerCovered(Location buyerLocation, List<MarketAddressLocation> sellerMarketLocations)
//{
//    foreach (var loc in sellerMarketLocations)
//    {
//        switch (loc.CoverageLevel)
//        {
//            case MarketCoverageLevel.Country:
//                if (loc.CountryId == buyerLocation.CountryId) return true;
//                break;
//            case MarketCoverageLevel.Province:
//                if (loc.ProvinceId == buyerLocation.ProvinceId) return true;
//                break;
//            case MarketCoverageLevel.District:
//                if (loc.DistrictId == buyerLocation.DistrictId) return true;
//                break;
//            case MarketCoverageLevel.Neighborhood:
//                if (loc.NeighborhoodId == buyerLocation.NeighborhoodId) return true;
//                break;
//        }
//    }

//    return false;
//}
//    }