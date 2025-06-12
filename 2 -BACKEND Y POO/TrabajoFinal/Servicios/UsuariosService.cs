using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.DTOS;

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


        public bool ActualizarUsuario(int Id, RequestUpdateUser usuario)
        {
            if (usuario == null)
            {

                throw new ArgumentException("Usuario no valido" + usuario);
           
            }
            else
            {
                var usuarioExistente = _dao.GetUserById(Id);
                if (usuarioExistente == null)
                {
                    throw new Exception($"Usuario con ID {Id} no encontrado para actualizar.");
                }

                // Aplicar los cambios del DTO al objeto existente
                if (usuario.Nombre != null) usuarioExistente.Nombre = usuario.Nombre;
                if (usuario.Apellido != null) usuarioExistente.Apellido = usuario.Apellido;
                if (usuario.Alias != null) usuarioExistente.Alias = usuario.Alias;
                if (usuario.Email != null) usuarioExistente.Email = usuario.Email;
                if (usuario.Pais != null) usuarioExistente.Pais = usuario.Pais;

                // --- ¡AQUÍ ESTÁ LA CLAVE DEL ROL! ---
                if (usuario.Rol.HasValue) // Si el DTO recibió un valor de rol (no nulo)
                {
                    usuarioExistente.Rol = usuario.Rol.Value; // Asigna el valor del enum del DTO al objeto de la entidad
                }
                // Si usuarioDto.Rol es null, usuarioExistente.Rol mantiene su valor original, lo cual es correcto para updates parciales.

                // --- AÑADE ESTOS LOGS ---
                string rolDebugString = usuarioExistente.Rol.ToString(); // Obtiene el nombre del enum como string
                Console.WriteLine($"DEBUG: ID de usuario: {Id}");
                Console.WriteLine($"DEBUG: Rol recibido en DTO: '{usuario.Rol?.ToString() ?? "NULO"}'");
                Console.WriteLine($"DEBUG: Rol final en entidad Usuario ANTES de DAO: '{rolDebugString}'");
                Console.WriteLine($"DEBUG: Longitud del string del Rol: {rolDebugString.Length}");
                Console.WriteLine($"DEBUG: Valor entero del Rol (para referencia): {(int)usuarioExistente.Rol}");
                // --- FIN DE LOS LOGS ---

                var actualizado = _dao.UpdateUserByID(usuarioExistente);
            if (!actualizado) throw new Exception("Usuario no encontrado o no se pudo actualizar.");
            return true;
            }
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
