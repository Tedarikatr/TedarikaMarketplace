using Entity.Auths;
using Entity.Locations;

namespace Entity.DeliveryAddresses
{
    public class DeliveryAddress
    {
        public int Id { get; set; }

        public int BuyerUserId { get; set; }
        public BuyerUser BuyerUser { get; set; }

        public int? RegionId { get; set; }
        public Region Region { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int? StateId { get; set; }
        public State State { get; set; }

        public int? ProvinceId { get; set; }
        public Province Province { get; set; }

        public int? DistrictId { get; set; }
        public District District { get; set; }

        public int? NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

        public string AddressLine { get; set; }
        public string PostalCode { get; set; }

        public bool IsDefault { get; set; }
    }
}
