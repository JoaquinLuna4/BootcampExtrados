using LibraryTrabajoFinal.Entidades;

namespace LibraryTrabajoFinal.DTOS
{
    public class RequestUpdateUser
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; } 
        public string? Alias { get; set; }
        public string? Email { get; set; }
        public string? Pais { get; set; }
        public UserRole? Rol { get; set; }
    }
}

