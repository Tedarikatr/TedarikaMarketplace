using Entity.Companys;

namespace Entity.Auth
{
    public class BuyerUser
    {
        public int Id { get; set; }

        public ICollection<Company> Company { get; set; } // Kullanıcının şirketleri


    }
}
