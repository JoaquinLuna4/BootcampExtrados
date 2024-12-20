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
using System.Xml.Linq;

namespace APIAutorizacionAutenticacion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {


        private AuthService _authService;
        private JWTConfig _jwtConfiguracion;
        private readonly TokenService _tokenService;
        private readonly LibroService _libroService;

        public AuthController(TokenService tokenService, AuthService authService, IOptions<JWTConfig> JwtConfigu, LibroService libroService)
        {
            _authService = authService;
            _jwtConfiguracion = JwtConfigu.Value; ;
            _tokenService = tokenService;
            _libroService = libroService;
        }

         [HttpPost("login")]
        //Este endpoint valida si estan ok las credenciales y genera un token 
        public IActionResult Login([FromBody] CredencialesLogin request)
        {

            // 1. Llamar a ObtenerPerfil para validar las credenciales y obtener la informaci�n del usuario
            var perfilResult = ObtenerPerfil(request) as OkObjectResult;

            if (perfilResult == null)
            {
                // Si ObtenerPerfil devuelve Unauthorized o NotFound, propagamos la respuesta
                return Unauthorized(new { message = "Usuario o contrase�a inv�lidos" });
            }

            // 2. Obtener la informaci�n del usuario p�blico
            var userPublico = perfilResult.Value;

            // 3. Generar el token JWT

            var token = _tokenService.GenerarToken(
                        name: (userPublico as dynamic)?.Nombre ?? "",
                        email: (userPublico as dynamic)?.Email ?? "",
                        rol: (userPublico as dynamic)?.Role ?? "user",
                        id: (userPublico as dynamic)?.id ?? "0"
                         );
            
           
            // 4. Devolver el token junto con la informaci�n del usuario
            return Ok(new
            {
                token,
                usuario = userPublico
            });
        }


        [HttpPost("pedir-libro")]
        [Authorize(Roles = "User")] // Solo accesible por usuarios con rol "usuario"
        public IActionResult PedirLibro([FromBody] SolicitudLibro solicitud)
        {
            try
            {
                // Id del Usuario desde el token de auth
                var userIdFromToken = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value ?? throw new ArgumentNullException());
                
                // Calcular fecha de vencimiento
                var fechaVencimiento = solicitud.FechaPrestamo.AddDays(5);

                // Dato por body de fecha de prestamo

                DateTime fechaPrestamo = solicitud.FechaPrestamo;

                // L�gica para registrar el pr�stamo en la base de datos 
                _libroService.PedirLibro(solicitud.NombreLibro);

                //Obtener idLibro

                int idlibro = _libroService.ObtenerIDlibro(solicitud.NombreLibro);

                //Le asigno el libro al usuario
                _libroService.AsignarLibro(userIdFromToken, fechaVencimiento, fechaPrestamo, idlibro);



                return Ok(new
                {
                    message = "Libro solicitado exitosamente",
                    fechaVencimiento = fechaVencimiento.ToString("yyyy-MM-dd HH:mm:ss"),
                    userIdFromToken

                });
            }
            catch (ArgumentNullException)
            {
                return Unauthorized(new { message = "No se encuentra un ID de usuario v�lido en el token" });
            }
            catch (FormatException)
            {
                return Unauthorized(new { message = "El ID del token no tiene el formato esperado" });
            }

        }
        [HttpPost("devolver-libro")]
        [Authorize(Roles = "User")] // Solo accesible por usuarios con rol "usuario"
        public IActionResult DevolverLibro([FromBody] SolicitudLibro solicitud)
        {
            try
            {
                //var userNameFromToken = int.Parse(User.FindFirst(ClaimTypes.Name)?.Value ?? throw new ArgumentNullException());
                var userName = solicitud.NombreUsuario;
            }
            catch (ArgumentNullException)
            {
                return Unauthorized(new { message = "No se encuentra un ID de usuario v�lido en el token" });
            }
            catch (FormatException)
            {
                return Unauthorized(new { message = "El ID del token no tiene el formato esperado" });
            }

            // Calcular fecha de vencimiento
            var fechaVencimiento = solicitud.FechaPrestamo.AddDays(5);

            // L�gica para registrar el pr�stamo en la base de datos 
            _libroService.DevolverLibro(solicitud.NombreLibro);


            return Ok(new
            {
                message = "Libro devuelto exitosamente"
            });
        }










        [HttpGet("profiles/{nombre}")]
        [Authorize]
        //Este endpoint deja ver a todos los usuarios que querramos siempre y cuando estemos autorizados
        public UsuariosCRUD Profiles(string nombre)
        {
            var usuario = _authService.ObtenerUsuarioPublico(nombre);
            return usuario;
        }

        [HttpPost("myprofile")]
        //Este endpoint unicamente muestra el perfil si coinciden las credenciales enviadas por body
        public IActionResult ObtenerPerfil([FromBody] CredencialesLogin request)
        {

            // Obtener el usuario con el nombre proporcionado
            var usuario = _authService.ObtenerUsuario(request.Nombre);

            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado");
                return NotFound(new { message = "Usuario no encontrado" });
            }
            // Obtener el hash de la contrase�a almacenada
            string storedHash = usuario.Pass;

            //Verifica si la contrase�a ingresada coincide con el hash almacenado
            var passwordService = new PasswordHasher();
            bool isPasswordValid = passwordService.VerifyPassword(request.EnteredPass, storedHash);

            if (isPasswordValid)
            {
                Console.WriteLine("La contrase�a es v�lida");
                var userpublico = _authService.ObtenerUsuarioPublico(request.Nombre);
                return Ok(userpublico);
            }
            else
            {
                Console.WriteLine("La contrase�a es incorrecta");
                return Unauthorized(new { message = "No autorizado" });
            }


        }


        [HttpPost("signup")]
        public UsuariosCRUD CreateNewUser(UsuariosCRUD obtener)
        {

            return _authService.CrearUsuario(obtener.Nombre, obtener.Email, obtener.Pass, obtener.Role);

        }
        [HttpPost("update/{nombre}/{email}")]
        public void UpdateUser(string nombre, string email)
        {
            _authService.UpdateUser(nombre, email);
        }

        [HttpPost("delete/{nombre}")]
        public void DeleteUser(string nombre)
        {
            _authService.BorrarUsuario(nombre);
        }

    }
}
