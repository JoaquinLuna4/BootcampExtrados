
using datos.DAOS;
using datos.Entidades;



namespace APIAutorizacionAutenticacion.Services
{
    public class AuthService
    {
        private DAOCRUD Objeto1 { get; set; }
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        public AuthService() { 
            Objeto1 = new DAOCRUD();
        }
        public UsuariosCRUD ObtenerUsuario(string nombre)
        {
            var usuario = Objeto1.GetAllUser(nombre);
            if (usuario != null) return usuario;
            throw new Exception("Usuario es null en esta instancia");
        }

        public UsuariosCRUD ObtenerUsuarioPublico(string nombre)
        {
            var usuario = Objeto1.GetUserByNombre(nombre);
            if (usuario != null) return usuario;
            throw new Exception("Usuario es null en esta instancia");
        }

        public UsuariosCRUD CrearUsuario(string nombre, string email, string pass)
        {
            var hashedPassword = _passwordHasher.HashPassword(pass); // Hashear la contraseña
            var usuario = new UsuariosCRUD
            {
                Nombre = nombre,
                Email = email,
                Pass = hashedPassword
            };
            var rowsAfectadas = Objeto1.SignUp(nombre, email, hashedPassword);
            if (rowsAfectadas == 0) throw new Exception("Error creando user");
            return usuario;
        }

        public void UpdateUser(string nombre, string email)
        {
            int rowsAffected = Objeto1.UpdateUserByName(nombre, email);

            if (rowsAffected == 0)
            {
                throw new Exception("No hay usuarios con el nombre indicado");
            }
        }
        public void BorrarUsuario(string nombre)
        {
            int rowsAffected = Objeto1.DeleteUserByName(nombre);

            if (rowsAffected == 0)
            {
                throw new Exception("No hay usuarios con el nombre indicado");
            }
        }

    }
}
