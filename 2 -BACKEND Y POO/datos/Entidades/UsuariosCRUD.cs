
using System.ComponentModel.DataAnnotations;

namespace datos.Entidades
{
    public class UsuariosCRUD
    {
        
        public required string Nombre { get; set; }
        
        public required string Email { get; set; }
        
        public required string Pass { get; set; }

        public required string Role { get; set; }
        public int id { get; set; }

    }
}
