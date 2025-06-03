
using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;



namespace APIAutorizacionAutenticacion.Services
{
    public class AuthService
    {
        private readonly IDaoCRUD _dao;
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        public AuthService(IDaoCRUD dao) { 
            _dao = dao;
        }
        public UsuariosCRUD ObtenerUsuario(string nombre)
        {
            var usuario = _dao.GetAllUser(nombre);
            if (usuario != null) return usuario;
            throw new Exception("Usuario es null en esta instancia");
        }

        public UsuariosCRUD ObtenerUsuarioPublico(string nombre)
        {
            var usuario = _dao.SearchUser(nombre);
            if (usuario != null) return usuario;
            throw new Exception("Usuario es null en esta instancia");
        }

        public UsuariosCRUD CrearUsuario(string nombre, string email, string pass, string role)
        {
            var hashedPassword = _passwordHasher.HashPassword(pass); // Hashear la contraseña
            var usuario = new UsuariosCRUD
            {
                Nombre = nombre,
                Email = email,
                Pass = hashedPassword,
                Role = role
            };
            var rowsAfectadas = _dao.SignUp(nombre, email, hashedPassword, role);
            if (rowsAfectadas == 0) throw new Exception("Error creando user");
            return usuario;
        }

        public void UpdateUser(string nombre, string email)
        {
            int rowsAffected = _dao.UpdateUserByName(nombre, email);

            if (rowsAffected == 0)
            {
                throw new Exception("No hay usuarios con el nombre indicado");
            }
        }
        public void BorrarUsuario(string nombre)
        {
            int rowsAffected = _dao.DeleteUserByName(nombre);

            if (rowsAffected == 0)
            {
                throw new Exception("No hay usuarios con el nombre indicado");
            }
        }

    }
}
