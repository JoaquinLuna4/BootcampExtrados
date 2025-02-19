using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrabajoFinal.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IDAOUsuarios _dao;
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        
        public UsuarioService(IDAOUsuarios dao)
        {
            _dao = dao;
            
        }
        public int ContarUsuariosPorRol(UserRole rol)
        {
            return _dao.CountUserByRole(rol);
        }
        public Usuario ObtenerUsuario(string alias)
        {
            var usuario = _dao.GetUserByAlias(alias);
            if (usuario != null) return usuario;
            throw new Exception("Usuario no encontrado.");
        }
        public Usuario ObtenerUsuarioPorId(int id)
        {
            var usuario = _dao.GetUserById(id);
            if (usuario != null) return usuario;
            throw new Exception("Usuario no encontrado.");
        }

        public IEnumerable<Usuario> ObtenerTodosLosUsuarios()
        {
            var usuarios = _dao.GetAllUsers();
            if (usuarios != null && usuarios.Any()) return usuarios;
            throw new Exception("No hay usuarios registrados.");
        }

        public Usuario CrearUsuario(string nombre, string email, string password, UserRole rol, string alias, string? pais = null, string? apellido = null, int? idCreador = null)
        {
            var hashedPassword = _passwordHasher.HashPassword(password);

            

            Usuario usuario = new()
            {
                Id = 0, // Valor temporal para cumplir con 'required'
                Nombre = nombre,
                Email = email,
                Password = hashedPassword,
                Alias = alias,
                Pais = pais, // Opcional
                Apellido = apellido, // Opcional
                Rol = rol,
                Fecha_Registro = DateTime.UtcNow, // Fecha actual
                IdCreador = idCreador
            };

            var id = _dao.CreateUser(usuario);
            if (id <= 0) throw new Exception("Error al crear el usuario.");

            usuario.Id = id; // Asigna el ID real después de la creación
            return usuario;
        }


        public bool ActualizarUsuario(int id, Usuario usuario)
        {
            if (usuario == null || usuario.Id != id)
                throw new ArgumentException("El ID del usuario no coincide o los datos son inválidos.");

            var actualizado = _dao.UpdateUserByID(usuario);
            if (!actualizado) throw new Exception("Usuario no encontrado o no se pudo actualizar.");
            return true;
        }

        public bool EliminarUsuario(int id)
        {
            var eliminado = _dao.DeleteUserByID(id);
            if (!eliminado) throw new Exception("Usuario no encontrado o no se pudo eliminar.");
            return true;
        }
        public bool ValidarPassword(string EnteredPass, string storedPassword)
        {
            return _passwordHasher.VerifyPassword(EnteredPass, storedPassword);
        }

    }
}
