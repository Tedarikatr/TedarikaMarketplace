using Entity.Auths;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services.Auths.Helper
{
    public class JwtService : IJwtService
    {
        private readonly string _secret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiryInMinutes;

        public JwtService(IConfiguration configuration)
        {
            _secret = configuration.GetSection("JwtSettings:SecretKey").Value;
            _issuer = configuration.GetSection("JwtSettings:Issuer").Value;
            _audience = configuration.GetSection("JwtSettings:Audience").Value;

            var expiryInMinutesValue = configuration.GetSection("JwtSettings:ExpiryInMinutes").Value;
            if (string.IsNullOrEmpty(expiryInMinutesValue))
            {
                throw new ArgumentNullException("JwtSettings:ExpiryInMinutes değeri yapılandırma dosyasında bulunamadı.");
            }

            _expiryInMinutes = int.Parse(expiryInMinutesValue);
        }

        public string GenerateBuyerToken(BuyerUser buyerUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, buyerUser.Id.ToString()),
                new Claim(ClaimTypes.Email, buyerUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserType", "Buyer"),
                new Claim("BuyerUserId", buyerUser.Id.ToString()),
                new Claim("Status", buyerUser.Status.ToString().ToLower())
            };

            return GenerateJwtToken(claims);
        }

        public string GenerateSellerToken(SellerUser sellerUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, sellerUser.Id.ToString()),
                new Claim(ClaimTypes.Email, sellerUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserType", "Seller"),
                new Claim("SellerUserId", sellerUser.Id.ToString()),
                new Claim("StoreId", sellerUser.StoreId.ToString()),
                new Claim("Status", sellerUser.Status.ToString().ToLower())
            };

            return GenerateJwtToken(claims);
        }

        public string GenerateAdminToken(AdminUser adminUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, adminUser.Id.ToString()),
                new Claim(ClaimTypes.Email, adminUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserType", "Admin"),
                new Claim("AdminGuidNumber", adminUser.AdminGuidNumber.ToString()),
                new Claim("Status", adminUser.Status.ToString().ToLower())
            };

            return GenerateJwtToken(claims);
        }

        private string GenerateJwtToken(List<Claim> claims)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_secret);
            if (keyBytes.Length < 32)
            {
                throw new ArgumentException("JWT SecretKey en az 256 bit (32 karakter) olmalıdır.");
            }

            var authSigningKey = new SymmetricSecurityKey(keyBytes);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }
}
