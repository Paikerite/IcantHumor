using IcantHumor.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IcantHumor.Data
{
    public static class GenerationTokenJWT
    {
        public static string GenerateToken(SessionUserModel userSession, bool isRemember = true)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("31669705-4AEA-4726-9E6F-A452191A25CF"));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {                  
                    new Claim(ClaimTypes.Name, userSession.UserName),
                    new Claim(ClaimTypes.Role, userSession.Role.ToString()),
                    new Claim(ClaimTypes.Sid, userSession.Id.ToString()),
                }, "jwt"),
                Issuer = "http://BIGBOSS.com",
                Audience = "http://SOLIDUS.com",
                Expires = isRemember is true ? DateTime.UtcNow.AddYears(1) : DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public static bool ValidateCurrentToken(string token)
        {
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("31669705-4AEA-4726-9E6F-A452191A25CF"));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = "http://BIGBOSS.com",
                    ValidAudience = "http://SOLIDUS.com",
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during token validation - {ex.Message}");
                return false;
            }
            return true;
        }
    }


}
