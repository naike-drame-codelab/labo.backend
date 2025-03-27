using Labo.Application.Interfaces.Security;
using Labo.Domain.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Labo.API.Security
{
    public class TokenManager(TokenManager.Config config) : ITokenManager
    {
        public class Config
        {
            public string Secret { get; set; } = null!;
            public int Duration { get; set; } // en secondes
            public string Issuer { get; set; } = null!;
            public string Audience { get; set; } = null!;
        }

        public string CreateToken(int id, string username, string email, Role role)
        {
            JwtSecurityTokenHandler handler = new();
            JwtSecurityToken token = new(
                // issuer
                config.Issuer,
                // audience
                config.Audience,
                // claims
                CreateClaims(id, username, email, role),
                // start
                DateTime.Now,
                // expires
                DateTime.Now.AddSeconds(config.Duration),
                //  signingKey
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret)),
                    SecurityAlgorithms.HmacSha256
                ));
            return handler.WriteToken(token);
        }

        private IEnumerable<Claim> CreateClaims(int id, string username, string email, Role role)
        {
            yield return new Claim(ClaimTypes.Name, username);
            yield return new Claim(ClaimTypes.Email, email);
            yield return new Claim(ClaimTypes.Role, role.ToString());
            yield return new Claim(ClaimTypes.NameIdentifier, id.ToString(), ClaimValueTypes.Integer32);
        }

        public int ValidateTokenWithoutLifeTime(string token)
        {
            JwtSecurityTokenHandler handler = new();
            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuer = true,
                ValidIssuer = config.Issuer,
                ValidateAudience = true,
                ValidAudience = config.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret)),
                ValidateLifetime = false
            };

            var claims = handler.ValidateToken(token, validationParameters, out SecurityToken s);
            return int.Parse(claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "-1");
        }
    }
}



