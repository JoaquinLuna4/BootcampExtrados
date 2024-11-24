using Microsoft.AspNetCore.Mvc;
using APIclase.bdconnection.Entidades;
using APIclase.bdconnection.DAOs;
using MySqlConnector;
using Dapper;
using MySqlConnector;


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
        [HttpPost("{id}/{nombre}/{email}")]
        public Usuarios CrearUsuario(int id, string name, string email)
        {
            var daoUsuarios = new bdconnection.DAOs.DAOUsuarios();
            var usuario = daoUsuarios.CreateUser(id, name, email);
            if (usuario == null) throw new Exception();
            return usuario;
        }

    }


}


