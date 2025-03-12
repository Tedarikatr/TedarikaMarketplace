using Entity.Stores;

namespace Entity.Companys
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } // Şirket Adı
        public string TaxNumber { get; set; } // Vergi Numarası
        public string Country { get; set; } // Şirketin Bulunduğu Ülke
        public string City { get; set; } // Şirketin Bulunduğu Şehir
        public string Address { get; set; } // Açık Adres
        public string Email { get; set; } // İletişim E-posta
        public string Phone { get; set; } // İletişim Telefon
        public string CompanyNumber { get; set; } // İletişim Telefon

        public string Industry { get; set; } // Faaliyet Sektörü (Örn: "Tekstil", "Gıda", "Otomotiv")
        public bool IsVerified { get; set; } // Admin tarafından doğrulandı mı?
        public bool IsActive { get; set; } // Şirket Aktif mi?

        public CompanyType Type { get; set; } // Alıcı mı? Satıcı mı?

        public ICollection<CompanyUser> CompanyUsers { get; set; } // Şirket kullanıcıları
        public ICollection<Store> Stores { get; set; } // Şirketin sahip olduğu mağazalar
    }

    public enum CompanyType
    {
        Buyer = 1,  // Sadece alıcı
        Seller = 2, // Sadece satıcı
        Both = 3    // Hem alıcı hem satıcı
    }
}
