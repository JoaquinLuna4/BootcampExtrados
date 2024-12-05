
using datos.DAOS;
using datos.Entidades;
using Konscious.Security.Cryptography;
using System.Text;


namespace API_CRUD.Services
{
    public class CRUDService
    {
        private DAOCRUD objeto1 { get; set; }
        private readonly PasswordHasher _passwordHasher = new PasswordHasher();
        public CRUDService() { 
            objeto1 = new DAOCRUD();
        }
        public UsuariosCRUD ObtenerUsuario(string nombre)
        {
            var usuario = objeto1.GetAllUser(nombre);
            if (usuario == null) throw new Exception("Usuario es null en esta instancia");
            return usuario;
        }

        public UsuariosCRUD ObtenerUsuarioPublico(string nombre)
        { 
            var usuario = objeto1.GetUserByNombre(nombre);
            if (usuario == null) throw new Exception("Usuario es null en esta instancia");
            return usuario;
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
            var rowsAfectadas = objeto1.SignUp(nombre, email, hashedPassword);
            if (rowsAfectadas == 0) throw new Exception("Error creando user");
            return usuario;
        }

        public void UpdateUser(string nombre, string email)
        {
            int rowsAffected = objeto1.UpdateUserByName(nombre, email);

            if (rowsAffected == 0)
            {
                throw new Exception("No hay usuarios con el nombre indicado");
            }
        }
        public void BorrarUsuario(string nombre)
        {
            int rowsAffected = objeto1.DeleteUserByName(nombre);

            if (rowsAffected == 0)
            {
                throw new Exception("No hay usuarios con el nombre indicado");
            }
        }

    }
}
