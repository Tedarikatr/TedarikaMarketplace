using Entity.Stores.Products;

namespace Entity.Anohter
{
    public class EstimatedExportCost
    {
        public int Id { get; set; }
        public int StoreProductId { get; set; }
        public StoreProduct StoreProduct { get; set; }

        public decimal ProductUnitPrice { get; set; }           // TL veya USD
        public decimal LogisticsCost { get; set; }              // Kargo + iç nakliye
        public decimal CustomsCost { get; set; }                // Gümrük çıkış vergileri
        public decimal CertificateCost { get; set; }            // CE, FDA vb. belgelerin maliyeti
        public decimal PackagingCost { get; set; }              // Ambalaj, koli, palet
        public decimal PlatformCommission { get; set; }         // Tedarika komisyonu
        public decimal FXMarginCost { get; set; }               // Kur farkı buffer’ı

        public decimal TotalExportCost => ProductUnitPrice + LogisticsCost + CustomsCost
                                         + CertificateCost + PackagingCost + PlatformCommission
                                         + FXMarginCost;

        public string Currency { get; set; } // "TRY", "USD", "EUR"
        public string Incoterm { get; set; } // "EXW", "FOB", "CIF"
        public string DestinationCountryCode { get; set; }

        public DateTime CalculatedAt { get; set; }
    }
}
