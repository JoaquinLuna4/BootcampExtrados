using LibraryTrabajoFinal.DTOS;
using LibraryTrabajoFinal.Entidades;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrabajoFinal.Servicios;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly IUsuarioService _usuariosService;

    public AuthController(JwtService jwtService, IUsuarioService usuariosService)
    {
        _jwtService = jwtService;
        _usuariosService = usuariosService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] CredencialesLogin request)
    {
        Usuario? usuario = null;
        // Validar credenciales
        try
        {
         usuario = _usuariosService.ObtenerUsuario(request.Alias);
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error: {ex.Message} , no se encontró el usuario");
            return Unauthorized(new { Message = "Credenciales inválidas" }); //Devuelvo UnAuth al user final, no la respuesta real
        }


        if (usuario == null || !_usuariosService.ValidarPassword(request.EnteredPass, usuario.Password))
        {
            return Unauthorized(new { Message = "Credenciales inválidas" });
        }
        // Generar token
        var token = _jwtService.GenerarToken(
            usuario.Alias,
            usuario.Email, 
            usuario.Rol, 
            usuario.Id);
        // Retornar el token
        return Ok(new { Token = token, Usuario = usuario });
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RequestRegister request)
    {
        try
        {
            var usuario = _usuariosService.CrearUsuario(
                request.Nombre,
                request.Email,
                request.Password,
                request.Rol,
                request.Alias,
                request.Pais,
                request.Apellido);

            return Ok(new { Message = "Usuario registrado exitosamente", Usuario = usuario });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
