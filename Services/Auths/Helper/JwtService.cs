using Entity.Auths;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Auths.Helper
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAdminToken(AdminUser user)
        {
            return GenerateToken(user.Id, user.Email, UserType.Admin, user.UserNumber, user.UserGuidNumber.ToString(), user.Status);
        }

        public string GenerateBuyerToken(BuyerUser user)
        {
            return GenerateToken(user.Id, user.Email, UserType.Buyer, user.UserNumber, user.UserGuidNumber.ToString(), user.Status, user.CompanyId);
        }

        public string GenerateSellerToken(SellerUser user)
        {
            return GenerateToken(user.Id, user.Email, UserType.Seller, user.UserNumber, user.UserGuidNumber.ToString(), user.Status, user.CompanyId);
        }

        private string GenerateToken(int userId, string email, UserType userType, string userNumber, string userGuid, bool isActive, int? companyId = null)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim("UserGuid", userGuid),
                new Claim("UserNumber", userNumber),
                new Claim("UserType", userType.ToString()),
                new Claim("IsActive", isActive.ToString().ToLower())
            };

            if (companyId.HasValue)
                claims.Add(new Claim("CompanyId", companyId.Value.ToString()));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "DefaultIssuer",
                audience: _configuration["Jwt:Audience"] ?? "DefaultAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToDouble(_configuration["Jwt:ExpireHours"] ?? "3")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
