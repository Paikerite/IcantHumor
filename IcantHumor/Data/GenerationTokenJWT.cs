using IcantHumor.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IcantHumor.Data
{
    public static class GenerationTokenJWT
    {
        public static string GenerateToken(SessionUserModel userSession)
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
                Expires = DateTime.UtcNow.AddDays(14),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
