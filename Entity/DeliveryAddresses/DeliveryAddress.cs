using Entity.Auth;

namespace Entity.DeliveryAddresses
{
    public class DeliveryAddress
    {
        public int Id { get; set; }
        public int BuyerUserId { get; set; }
        public BuyerUser BuyerUser { get; set; }

        public string BuyerUserNumber { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Neighborhood { get; set; }
        public string Address { get; set; }
        public bool IsDefault { get; set; }

    }
}
