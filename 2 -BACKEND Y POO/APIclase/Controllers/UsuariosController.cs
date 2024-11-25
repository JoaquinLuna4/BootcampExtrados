using Microsoft.AspNetCore.Mvc;
using APIclase.bdconnection.Entidades;
using APIclase.bdconnection.DAOs;
using MySqlConnector;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;


namespace APIclase.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Usuarios> ObtenerUsuarios()
        {
            var daoUsuarios = new bdconnection.DAOs.DAOUsuarios();
            var usuarios = daoUsuarios.GetAllUsers();

            return usuarios;
        }
        [HttpGet("{email}")]
        public Usuarios ObtenerUsuarioConEmail(string email)
        {
            var daoUsuarios = new bdconnection.DAOs.DAOUsuarios();
            var usuario = daoUsuarios.GetUserByEmail(email);
            if (usuario == null) throw new Exception();
            return usuario;
        }

        [HttpPost("{id}/{nombre}/{email}/{edad}")]
        public Usuarios CrearUsuario(int id, string nombre, string email, int edad)
        {

           if(email.Contains("@gmail.com", StringComparison.OrdinalIgnoreCase) && edad>=14){
            var daoUsuarios = new bdconnection.DAOs.DAOUsuarios();
            int nuevoId = daoUsuarios.CreateUser(id, nombre, email, edad);
            // Aquí puedes obtener el usuario creado desde la base de datos usando el nuevo ID
            var usuarioCreado = daoUsuarios.GetUserByEmail(email);
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
        
    




