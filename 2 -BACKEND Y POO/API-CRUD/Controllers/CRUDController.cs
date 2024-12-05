using API_CRUD.Services;
using datos.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace API_CRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CRUDController : ControllerBase
    {
        private CRUDService _CRUDService{get; set;}
        public CRUDController()
        {
            _CRUDService = new CRUDService();
        }


        [HttpGet("search/")]
        public UsuariosCRUD ObtenerUsuarios(string nombre , string enteredPass)
        {

            // Obtener el usuario con el nombre proporcionado
            var usuario = _CRUDService.ObtenerUsuario(nombre);

            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado");
                throw new Exception("Usuario no encontrado");
            }
            // Obtener el hash de la contraseña almacenada
            string storedHash = usuario.Pass;
           
            //Verifica si la contraseña ingresada coincide con el hash almacenado
            var passwordService = new PasswordHasher();
            bool isPasswordValid = passwordService.VerifyPassword(enteredPass, storedHash);

            if (isPasswordValid)
            {
                Console.WriteLine("La contraseña es válida");
                return _CRUDService.ObtenerUsuarioPublico(nombre);
            }
            else
            {
                Console.WriteLine("La contraseña es incorrecta");
                throw new Exception("No autorizado");
            }


        }
      

        [HttpPost("signup")]
        public UsuariosCRUD CreateNewUser(UsuariosCRUD obtener)
        {

            return _CRUDService.CrearUsuario(obtener.Nombre, obtener.Email, obtener.Pass);

        }
        [HttpPost("update/{nombre}/{email}")]
        public void UpdateUser(string nombre, string email)
        {
            _CRUDService.UpdateUser(nombre, email);
        }

        [HttpPost("delete/{nombre}")]
        public void DeleteUser(string nombre)
        {
            _CRUDService.BorrarUsuario(nombre);
        }

    }
}
