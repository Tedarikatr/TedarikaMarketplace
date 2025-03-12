using Entity.Auth;

namespace Entity.Companys
{
    public class CompanyUser
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public int UserId { get; set; }
        public virtual SellerUser User { get; set; }

        public bool IsAdmin { get; set; } // Şirketin sahibi mi?
    }
}
