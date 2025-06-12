using LibraryTrabajoFinal.DTOS;
using LibraryTrabajoFinal.Entidades;

namespace TrabajoFinal.Servicios
{
    public interface IUsuarioService
    {
        Usuario ObtenerUsuario(string alias);
        Usuario ObtenerUsuarioPorId(int id);
        IEnumerable<Usuario> ObtenerTodosLosUsuarios();
        Usuario CrearUsuario(string nombre, string email, string password, UserRole rol, string alias, string? pais = null, string? apellido = null, int? idCreador = null);
        bool ActualizarUsuario(int id, RequestUpdateUser usuario);
        bool EliminarUsuario(int id);
        bool ValidarPassword(string enteredPass, string password);
        int ContarUsuariosPorRol(UserRole role);
    }
}