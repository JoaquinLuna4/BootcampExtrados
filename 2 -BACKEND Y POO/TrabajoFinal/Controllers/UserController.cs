using Microsoft.AspNetCore.Mvc;
using LibraryTrabajoFinal.Entidades;
using TrabajoFinal.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity; 
using System.Security.Claims; 
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using LibraryTrabajoFinal.DTOS;


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







        /* ---------------- CRUD DE USUARIOS SEGUN AUTH --------------- */

        // Crear administradores (solo admin)
        [Authorize(Roles = "Administrador")]
        [HttpPost("crear-administrador")]
        public IActionResult CrearAdministrador([FromBody] RequestRegister usuario)
        {
            // Validacion: Solo permite crear Administradores
            if (usuario.Rol != UserRole.Administrador
                )
            {
                return BadRequest(new { Message = "El rol a crear debe ser 'Administrador'." });
            }

            var idCreadorString = User.FindFirstValue(ClaimTypes.Sid);
            if (int.TryParse(idCreadorString, out int idCreador))
                { 

                var nuevoAdministrador = _usuarioService.CrearUsuario(
                    usuario.Nombre, usuario.Email, usuario.Password, usuario.Rol, usuario.Alias, usuario.Pais, usuario.Apellido);

                return Ok(new { Message = "Administrador creado exitosamente", Usuario = nuevoAdministrador });
            } else
            {
                return BadRequest(new { Message = "ID Creador no se pudo parsear a int" });
            }
        }
       
        [Authorize]
        [HttpPost("crear-organizador")]
        public IActionResult CrearOrganizador([FromBody] RequestRegister usuario)
        {
            if (usuario.Rol != UserRole.Organizador)
            {
                return BadRequest(new { Message = "El rol a crear debe ser 'Organizador'." });
            }

            
                var idCreadorString = User.FindFirstValue(ClaimTypes.Sid);

                if (int.TryParse(idCreadorString, out int idCreador))
                {
                    var nuevoOrganizador = _usuarioService.CrearUsuario(
                        usuario.Nombre, usuario.Email, usuario.Password, usuario.Rol, usuario.Alias, usuario.Pais, usuario.Apellido, idCreador); // Pasar idCreador

                    return Ok(new { Message = "Organizador creado exitosamente", Usuario = nuevoOrganizador });
                }
                else
                {
                    return BadRequest(new { Message = "ID de usuario inválido en el token." });
                }
            
            
        }

        // Crear Juez (Solo admin y juez)
        [Authorize(Roles = "Administrador,Organizador")]
        [HttpPost("crear-juez")]
        public IActionResult CrearJuez([FromBody] RequestRegister usuario)
        {
            if (usuario.Rol != UserRole.Juez)
                return BadRequest(new { Message = "El rol a crear debe ser 'Juez'." });
            var idCreadorString = User.FindFirstValue(ClaimTypes.Sid);
            if (int.TryParse(idCreadorString, out int idCreador))
            {
                var nuevoJuez = _usuarioService.CrearUsuario(
                usuario.Nombre, usuario.Email, usuario.Password, usuario.Rol, usuario.Alias, usuario.Pais, usuario.Apellido);

            return Ok(new { Message = "Juez creado exitosamente", Usuario = nuevoJuez });
            }
            else
            {
                return BadRequest(new { Message = "ID Creador no se pudo parsear a int" });
            }
        }

        // Crear Jugador (Cualquiera puede)
        [AllowAnonymous]
        [HttpPost("registrar-jugador")]
        public IActionResult RegistrarJugador([FromBody] RequestRegister usuario)
        {
            if (usuario.Rol != UserRole.Jugador)
                return BadRequest(new { Message = "El rol a crear debe ser 'Jugador'." });

            var nuevoJugador = _usuarioService.CrearUsuario(
                usuario.Nombre, usuario.Email, usuario.Password, usuario.Rol, usuario.Alias, usuario.Pais, usuario.Apellido);

            return Ok(new { Message = "Jugador registrado exitosamente", Usuario = nuevoJugador });
        }

        /* ------------ ENDPOINT UTILES ---------- */

        /* Ver lista de usuarios segun rol*/
        [Authorize]
        [HttpGet("listar")]
        public IActionResult ListarUsuarios()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var usuarios = _usuarioService.ObtenerTodosLosUsuarios();

            if (role == "Administrador")
            {
                // Si es administrador, devuelve la lista completa
                return Ok(usuarios);
            }
            else
            {
                // Si es otro rol, oculta nombres y devuelve solo alias
                var usuariosAnonimizados = usuarios.Select(u => new
                {
                    u.Id,
                    u.Alias,
                    u.Rol
                });

                return Ok(usuariosAnonimizados);
            }
        }

        /* Ver mi perfil */
        [Authorize]
        [HttpGet("mi-perfil")]
        public IActionResult ObtenerMiPerfil()
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.Sid)?.Value ?? "0");
            Usuario usuario = _usuarioService.ObtenerUsuarioPorId(userId);

            if (usuario == null)
            {
                return NotFound(new { Message = "Usuario no encontrado" });
            }

            return Ok(usuario);
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
        [Authorize(Roles = "Administrador,Organizador")]

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
