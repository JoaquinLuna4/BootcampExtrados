using APIAutorizacionAutenticacion.Services;
using Configuracion;
using datos.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIAutorizacionAutenticacion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private AuthService _CRUDService { get; set; }
        private JWTConfig _jwtConfiguracion;
        public AuthController(IOptions<JWTConfig> JwtConfigu)
        {
            _CRUDService = new AuthService();
            _jwtConfiguracion = JwtConfigu.Value;
        }

        [HttpPost ("JWT")]
        public string Login() {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguracion.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, "5"),
                new Claim(ClaimTypes.Name, "JOACO")
            };
            var Sectoken = new JwtSecurityToken(
                _jwtConfiguracion.Issuer,
                _jwtConfiguracion.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
                var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            return token;

            }

        [HttpPost("login")]
        //Este endpoint valida si estan ok las credenciales y genera un token 
        public IActionResult Login([FromBody] CredencialesLogin request)
        {
            // 1. Llamar a ObtenerPerfil para validar las credenciales y obtener la información del usuario
            var perfilResult = ObtenerPerfil(request) as OkObjectResult;

            if (perfilResult == null)
            {
                // Si ObtenerPerfil devuelve Unauthorized o NotFound, propagamos la respuesta
                return Unauthorized(new { message = "Usuario o contraseña inválidos" });
            }

            // 2. Obtener la información del usuario público
            var userPublico = perfilResult.Value;

            // 3. Generar el token JWT
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secret_secret_secret_secret_secret"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, (userPublico as dynamic)?.Nombre ?? ""),
                new Claim(ClaimTypes.Email, (userPublico as dynamic)?.Email ?? ""),
                //new Claim("Role", (userPublico as dynamic)?.Role ?? "user")
            };

            var Sectoken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            // 4. Devolver el token junto con la información del usuario
            return Ok(new
            {
                token,
                usuario = userPublico
            });
        }

      

        [HttpGet("profiles/{nombre}")]
        [Authorize]
        //Este endpoint deja ver a todos los usuarios que sepamos siempre y cuando estemos autorizados
        public UsuariosCRUD Profiles(string nombre)
        {
            var usuario = _CRUDService.ObtenerUsuarioPublico(nombre);
            return usuario;
        }

        [HttpPost("myprofile")]
        //Este endpoint unicamente muestra el perfil si coinciden las credenciales enviadas por 
        public IActionResult ObtenerPerfil([FromBody] CredencialesLogin request)
        {

            // Obtener el usuario con el nombre proporcionado
            var usuario = _CRUDService.ObtenerUsuario(request.Nombre);

            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado");
                return NotFound(new { message = "Usuario no encontrado" });
            }
            // Obtener el hash de la contraseña almacenada
            string storedHash = usuario.Pass;

            //Verifica si la contraseña ingresada coincide con el hash almacenado
            var passwordService = new PasswordHasher();
            bool isPasswordValid = passwordService.VerifyPassword(request.EnteredPass, storedHash);

            if (isPasswordValid)
            {
                Console.WriteLine("La contraseña es válida");
                var userpublico = _CRUDService.ObtenerUsuarioPublico(request.Nombre);
                return Ok(userpublico);
            }
            else
            {
                Console.WriteLine("La contraseña es incorrecta");
                return Unauthorized(new { message = "No autorizado" });
            }


        }


        [HttpPost("signup")]
        public UsuariosCRUD CreateNewUser(UsuariosCRUD obtener)
        {

            return _CRUDService.CrearUsuario(obtener.Nombre, obtener.Email, obtener.Pass);

        }
        [HttpPost("update/{nombre}/{email}")]
        public void UpdateUser(string nombre, string email)
        {
            _CRUDService.UpdateUser(nombre, email);
        }

        [HttpPost("delete/{nombre}")]
        public void DeleteUser(string nombre)
        {
            _CRUDService.BorrarUsuario(nombre);
        }

    }
}
