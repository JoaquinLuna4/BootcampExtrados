namespace LibraryTrabajoFinal.Entidades
{
    public class Mazo
    {
        public required int Id { get; set; }
        public required int JugadorId { get; set; } // Referencia a la tabla de usuarios

        // Relación con cartas
        public List<Carta>? Cartas { get; set; }
    }
}
