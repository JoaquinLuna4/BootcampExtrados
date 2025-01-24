using Microsoft.AspNetCore.Mvc;
using LibraryTrabajoFinal.Entidades;
using TrabajoFinal.Servicios;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.HttpResults;


namespace TrabajoFinal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // GET: api/usuarios
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var usuarios = _usuarioService.ObtenerTodosLosUsuarios();
            return Ok(usuarios);
        }

        // GET: api/usuarios/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(string alias)
        {
            {
                try
                {
                    var usuario = _usuarioService.ObtenerUsuario(alias);
                    return Ok(usuario);
                }
                catch (Exception ex)
                {
                    return StatusCode(404, new { ex.Message });
                }
            }
        }

        [Authorize(Roles = "Juez")]
        [HttpPost("solo-para-administradores")]
        public IActionResult AdministradorEndpoint()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (role != "Juez")
            {
                return Unauthorized(new { message = "Acceso denegado. Solo usuarios con el rol 'Juez' pueden acceder a este recurso." });
            }

            return Ok(new { Message = "Bienvenido, administrador" });
        }



        // POST: api/usuarios
        [HttpPost("register")]
        public IActionResult Register([FromBody] RequestRegister request)
            {
            try
            {
                var usuario = _usuarioService.CrearUsuario(
                    nombre: request.Nombre,
                    email: request.Email,
                    password: request.Password,
                    rol: request.Rol,
                    alias: request.Alias,
                    pais: request.Pais,
                    apellido: request.Apellido
                );

                return Ok(new { Message = "Usuario registrado exitosamente", Usuario = usuario });
            }
            catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }

        // POST: api/usuarios/{id}
        [HttpPost("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Usuario usuario)
        {
            try
            {
                _usuarioService.ActualizarUsuario(id, usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        // DELETE: api/usuarios/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _usuarioService.EliminarUsuario(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { ex.Message });
            }
        }
    }
}
