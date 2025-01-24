using Configuracion;
using LibraryTrabajoFinal.Entidades;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TrabajoFinal.Servicios
{
        public class JwtService
        {
            private readonly JWTConfig _jwtConfig;

            public JwtService(IOptions<JWTConfig> jwtConfig)
            {
                _jwtConfig = jwtConfig.Value;
            }

            public string GenerarToken(string alias, string email, UserRole rol, int id)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Sid, id.ToString()),
            new Claim(ClaimTypes.Role, rol.ToString()),
            new Claim(ClaimTypes.Name, alias),
            new Claim(ClaimTypes.Email, email ),
        };

                var token = new JwtSecurityToken(
                    issuer: _jwtConfig.Issuer,
                    audience: _jwtConfig.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(_jwtConfig.ExpireMinutes),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }

 
