namespace Entity.Forms
{
    public class SellerApplication
    {
        public int Id { get; set; }

        public Guid GuidId { get; set; }

        // Mağaza Bilgileri
        public string StoreName { get; set; }           
        public string BusinessType { get; set; }       
        public string TaxNumber { get; set; }            
        public string TaxOffice { get; set; }            
        public string GTIPFocusArea { get; set; }          

        // Yetkili Kişi Bilgileri
        public string RepresentativeFullName { get; set; } 
        public string RepresentativePosition { get; set; } 

        // İletişim Bilgileri
        public string Email { get; set; }                  
        public string PhoneNumber { get; set; }           
        public string City { get; set; }                   

        // Sistemsel
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
        public string Notes { get; set; }                          
    }
}
