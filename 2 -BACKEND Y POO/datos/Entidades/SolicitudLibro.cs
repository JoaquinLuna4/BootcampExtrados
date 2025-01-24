
namespace LibraryTrabajoFinal.Entidades
{
    public class SolicitudLibro
    {
        public DateTime FechaPrestamo { get; set; }
        public required string NombreLibro { get; set; }
        public required string NombreUsuario { get; set; }
    }

}
