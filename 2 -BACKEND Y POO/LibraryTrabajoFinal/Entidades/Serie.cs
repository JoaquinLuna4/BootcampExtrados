namespace LibraryTrabajoFinal.Entidades
{
    public class Serie
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required DateTime FechaLanzamiento { get; set; }

        // Relación con cartas
        public List<Carta>? Cartas { get; set; }
    }
}
