
using System.ComponentModel.DataAnnotations;


namespace datos.Entidades
{
    public class CredencialesLogin
    {
        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required string EnteredPass { get; set; }

    }
}
