using LibraryTrabajoFinal.Entidades;

namespace LibraryTrabajoFinal.DAOS
{
    public interface IDAOUsuarios
    {
        int CreateUser(Usuario user);
        IEnumerable<Usuario> GetAllUsers();
        Usuario? GetUserByAlias(string alias);
        bool UpdateUserByID(Usuario user);
        bool DeleteUserByID(int id);
      
        
    }
}
