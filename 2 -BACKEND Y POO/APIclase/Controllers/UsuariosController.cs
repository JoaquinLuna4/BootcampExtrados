using Microsoft.AspNetCore.Mvc;
using datos.Entidades;
using datos.DAOs;
using MySqlConnector;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using APIclase.Servicios;



namespace APIclase.Controllers
{
     

    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private UsuariosServices _servicioUsuario
        {
            get; set;
        }
        public UsuarioController()
        {
            _servicioUsuario = new UsuariosServices();
        }


        [HttpGet]
        public IEnumerable<Usuarios> ObtenerUsuarios()
        {
            var usuarios = _servicioUsuario.ObtenerUsuarios();
            return usuarios;
        }
        [HttpGet("{email}")]
        public Usuarios ObtenerUsuarioConEmail(string email)
        { 
            var user = _servicioUsuario.ObtenerUsuarioConEmail(email);
            if (user == null) throw new Exception();
            return user;
        }

        [HttpPost("{id}/{nombre}/{email}/{edad}")]
        public Usuarios CrearUsuario(int id, string nombre, string email, int edad)
        {

           if(email.Contains("@gmail.com", StringComparison.OrdinalIgnoreCase) && edad>=14){

            var user = _servicioUsuario.CrearUsuario(id, nombre, email, edad);
            // obtener el usuario creado desde la base de datos usando el nuevo ID
            var usuarioCreado = _servicioUsuario.ObtenerUsuarioConEmail(email);
                // Verificar si el usuario creado es nulo antes de devolverlo

            if (usuarioCreado == null)
                {
                    // Manejar el caso en que el usuario no se pudo crear
                    throw new Exception("Error al crear el usuario"); // O cualquier otra acción apropiada
                }
                return usuarioCreado;
            }
            else
            {
                throw new Exception("El correo no es dominio gmail o no tiene 14 años");
            }
        }
    }
}
        
    




