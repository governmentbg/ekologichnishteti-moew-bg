using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Zopoesht.Infrastructure.AppSettings;
using Zopoesht.Infrastructure.Users.Interfaces;

namespace Zopoesht.Infrastructure.Users
{
    public class JWTService : IJWTService
    {

        public JWTService() { }

        public string CreateToken(int userId, string roleAlias, string username, int? authorityId)
        {
            var claims = new List<Claim> {
                    new Claim("username", username),
                    new Claim(ClaimTypes.Role, roleAlias),
                    new Claim(JwtRegisteredClaimNames.Jti, userId.ToString()),
                    new Claim("authorityId", authorityId.ToString())
                };

            var expires = DateTime.Now.AddHours(AppSettingsConfiguration.AuthConfiguration.ValidHours);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettingsConfiguration.AuthConfiguration.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                AppSettingsConfiguration.AuthConfiguration.Issuer,
                AppSettingsConfiguration.AuthConfiguration.Audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            string tokenString = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return tokenString;
        }
    }
}