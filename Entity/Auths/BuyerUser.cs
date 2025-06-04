using Entity.Companies;

namespace Entity.Auths
{
    public class BuyerUser 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string UserNumber { get; set; }
        public Guid UserGuidNumber { get; set; }

        public string AwsIamUserArn { get; set; }

        public bool Status { get; set; }

        public UserType UserType { get; set; }

        public int? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
