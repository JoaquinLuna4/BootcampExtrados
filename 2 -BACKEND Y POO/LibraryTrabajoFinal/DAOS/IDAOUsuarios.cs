using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.DTOS;

namespace LibraryTrabajoFinal.DAOS
{
    public interface IDAOUsuarios
    {
        int CreateUser(Usuario user);
        IEnumerable<Usuario> GetAllUsers();
        Usuario? GetUserByAlias (string alias);
        Usuario? GetUserById(int id);
        bool UpdateUserByID( Usuario user);
        bool DeleteUserByID(int id);
        int CountUserByRole(UserRole rol);
    }
}
