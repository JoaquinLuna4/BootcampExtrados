namespace LibraryTrabajoFinal.EntidadesRelaciones
{
    public class MazoCarta
    {
        public required int Id { get; set; }
        public required int MazoId { get; set; } // Referencia al mazo
        public required int CartaId { get; set; } // Referencia a la carta
    }
}
