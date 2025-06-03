    using LibraryTrabajoFinal.DAOs;
    using LibraryTrabajoFinal.Entidades;


namespace APIclase.Servicios
{
    public class UsuariosServices
    {
        private DAOUsuarios daoUsuarios{get; set;}
        public UsuariosServices(){daoUsuarios = new DAOUsuarios();}

        public IEnumerable<Usuarios> ObtenerUsuarios() {
            return daoUsuarios.GetAllUsers();
        }
        public Usuarios ObtenerUsuarioConEmail(string email){

            var usuario = daoUsuarios.GetUserByEmail(email);
            if (usuario == null) throw new Exception();
            return usuario;
        }

        public Usuarios CrearUsuario(int id, string nombre, string email, int edad)
        {
          var userCreated =  daoUsuarios.CreateUser(id, nombre, email, edad);

            return userCreated;
        }
    }
}
