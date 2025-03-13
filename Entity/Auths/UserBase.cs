using Entity.Companies;

namespace Entity.Auths
{
    public abstract class UserBase
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

        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
