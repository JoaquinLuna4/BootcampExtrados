namespace LibraryTrabajoFinal.EntidadesRelaciones
{
    public class JugadorCarta
    {
        public required int Id { get; set; }
        public required int JugadorId { get; set; } // Referencia al jugador (Usuario)
        public required int CartaId { get; set; } // Referencia a la carta
    }
}
