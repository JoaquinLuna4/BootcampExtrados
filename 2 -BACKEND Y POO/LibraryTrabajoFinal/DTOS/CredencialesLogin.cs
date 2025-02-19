using System.ComponentModel.DataAnnotations;


namespace LibraryTrabajoFinal.DTOS
{
    public class CredencialesLogin
    {
        [Required]
        public required string Alias { get; set; }

        [Required]
        public required string EnteredPass { get; set; }

    }
}
