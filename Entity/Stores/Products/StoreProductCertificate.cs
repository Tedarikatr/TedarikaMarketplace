using Entity.Locations;

namespace Entity.Stores.Products
{
    public class StoreProductCertificate
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public int StoreProductId { get; set; }
        public StoreProduct StoreProduct { get; set; }

        public CertificateType CertificateType { get; set; } 

        public int CountryId { get; set; } 
        public string CountryCode { get; set; } 
        public ICollection<Country> Countrys { get; set; }

        public int RegionId { get; set; } 
        public string RegionCode { get; set; }
        public ICollection<Region> Regions { get; set; }

        public string FilePath { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public bool IsApproved { get; set; } = false;
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public string Notes { get; set; } 
    }

    public enum CertificateType
    {
        // Ürün Belgeleri
        CE = 1,
        FDA = 2,
        MSDS = 3,
        RoHS = 4,
        EN71 = 5,
        TestReport = 6,
        UserManual = 7,
        TechnicalDataSheet = 8,
        MaterialOriginCertificate = 9,
        OrganicCertificate = 10,

        // Firma/Mağaza Belgeleri
        ISO9001 = 100,
        ISO22000 = 101,
        ISO14001 = 102,
        GMP = 103,
        Halal = 104,
        Sedex = 105,
        BSCI = 106,
        TradeRegistryCertificate = 107,
        TaxCertificate = 108,
        SignatureCircular = 109,
        ExportUnionMembership = 110
    }
}
