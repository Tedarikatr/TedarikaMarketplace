namespace Data.Dtos.Auths
{
    public class AdminUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsSuperAdmin { get; set; }
    }

    public class AdminLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AdminRegisterDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string SuperAdminEmail { get; set; }
    }
}
