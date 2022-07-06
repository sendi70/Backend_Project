using AuthenticationServer.Models;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationServer.Services
{
    public class AccessTokenGenerator
    {
        static SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("91aoTyQ9DLzGqflzWKvnzBsiv4XhGW7GidcLAbnIP0u67zSzihA5YpKymsmsU"));
        SigningCredentials credentials=new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        public string GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Username)
            };

            JwtSecurityToken token = new JwtSecurityToken("https://localhost:5001", "https://localhost:5001",claims,System.DateTime.UtcNow,System.DateTime.UtcNow.AddHours(1),credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
