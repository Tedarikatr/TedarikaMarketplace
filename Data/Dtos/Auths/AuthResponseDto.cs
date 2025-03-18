using Entity.Auths;

namespace Data.Dtos.Auths
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public UserType Role { get; set; }
        public string Email { get; set; }
        public string UserNumber { get; set; }
    }
}
