namespace Data.Dtos.Auths
{
    public class BuyerUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserNumber { get; set; }
    }

    public class BuyerUserCreateDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }

    public class BuyerUserInfoDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserNumber { get; set; }
        public bool Status { get; set; }
    }

    public class BuyerLoginDto
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }
}
