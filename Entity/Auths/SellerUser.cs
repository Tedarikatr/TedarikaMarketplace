using Entity.Companies;
using Entity.Stores;

namespace Entity.Auths
{
    public class SellerUser 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserNumber { get; set; }
        public Guid UserGuidNumber { get; set; }

        public bool Status { get; set; }

        public UserType UserType { get; set; }

        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        public virtual ICollection<Store> Store { get; set; }

    }
}
