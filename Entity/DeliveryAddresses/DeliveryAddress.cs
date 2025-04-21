using Entity.Auths;

namespace Entity.DeliveryAddresses
{
    public class DeliveryAddress
    {
        public int Id { get; set; }

        public int BuyerUserId { get; set; }
        public BuyerUser BuyerUser { get; set; }

        public string Country { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string Neighborhood { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public bool IsDefault { get; set; }

    }
}
