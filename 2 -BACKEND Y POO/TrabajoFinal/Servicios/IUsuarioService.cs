using LibraryTrabajoFinal.Entidades;

namespace TrabajoFinal.Servicios
{
    public interface IUsuarioService
    {
        Usuario ObtenerUsuario(string alias);
        IEnumerable<Usuario> ObtenerTodosLosUsuarios();
        Usuario CrearUsuario(string nombre, string email, string password, UserRole rol, string alias, string? pais = null, string? apellido = null);
        bool ActualizarUsuario(int id, Usuario usuario);
        bool EliminarUsuario(int id);
        bool ValidarPassword(string enteredPass, string password);
    }
}