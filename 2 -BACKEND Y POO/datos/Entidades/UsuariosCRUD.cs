
using System.ComponentModel.DataAnnotations;

namespace datos.Entidades
{
    public class UsuariosCRUD
    {
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Pass { get; set; }

    }
}
