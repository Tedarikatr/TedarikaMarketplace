using Entity.Stores.Carriers;

namespace Entity.Carriers
{
    public class Carrier
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public string CarrierCode { get; set; }   
        public string ApiEndpoint { get; set; }   
        public string ApiKey { get; set; }       
        public string CarrierLogoUrl { get; set; } 

        public bool IsActive { get; set; } = true;

        public CarrierIntegrationType IntegrationType { get; set; }

        public ICollection<StoreCarrier> StoreCarriers { get; set; }

    }
    public enum CarrierIntegrationType
    {
        None = 0,
        Aras = 1,
        Yurtiçi = 2,
        MNG = 3,
        UPS = 4,
        CustomWebhook = 5
    }
}
