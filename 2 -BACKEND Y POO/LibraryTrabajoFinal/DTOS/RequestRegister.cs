using LibraryTrabajoFinal.Entidades;

namespace LibraryTrabajoFinal.DTOS
{
    public class RequestRegister
    {
        public required string Nombre { get; set; }
        public string? Apellido { get; set; } // Opcional
        public required string Alias { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Pais { get; set; } // Opcional
        public required UserRole Rol { get; set; }
    }
}
