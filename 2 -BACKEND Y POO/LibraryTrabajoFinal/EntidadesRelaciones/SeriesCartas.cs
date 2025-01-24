namespace LibraryTrabajoFinal.EntidadesRelaciones
{
    public class SerieCarta
    {
        public required int Id { get; set; }
        public required int SerieId { get; set; } // Referencia a la serie
        public required int CartaId { get; set; } // Referencia a la carta
    }
}
