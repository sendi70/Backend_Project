using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationServer.Services
{
    public class TokenGenerator
    {
        public string GenerateToken(string secretKey, string issuer, string audience, double expirationMinutes, IEnumerable<Claim> claims = null)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new JwtSecurityToken(issuer, 
                audience, 
                claims, 
                System.DateTime.UtcNow, 
                System.DateTime.UtcNow.AddMinutes(expirationMinutes), 
                credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
