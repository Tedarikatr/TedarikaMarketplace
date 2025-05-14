using Entity.Locations;
using Entity.Stores.Products;

namespace Entity.Stores
{
    public class StoreCertificate
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public int StoreId { get; set; }
        public Store Store { get; set; }

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
}
