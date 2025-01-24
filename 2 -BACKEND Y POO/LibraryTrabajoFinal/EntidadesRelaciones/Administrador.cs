namespace LibraryTrabajoFinal.EntidadesRelaciones
{
    public class Administrador
    {
        public required int Id { get; set; }
        public required int UsuarioId { get; set; } // Referencia al usuario base
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }
}
