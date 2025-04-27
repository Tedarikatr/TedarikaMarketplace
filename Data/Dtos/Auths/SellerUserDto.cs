namespace Data.Dtos.Auths
{
    public class SellerUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserNumber { get; set; }
    }

    public class SellerRegisterDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public class SellerLoginDto
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }

    public class SellerProfileDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool HasCompany { get; set; }
        public bool HasStore { get; set; }
    }
}
