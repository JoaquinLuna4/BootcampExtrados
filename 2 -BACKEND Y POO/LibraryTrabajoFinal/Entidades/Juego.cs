namespace LibraryTrabajoFinal.Entidades
{
    public class Juego
    {
        public int Id { get; set; }
        public required int TorneoId { get; set; }
        public required DateTime FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFin { get; set; }

        // Jugadores involucrados
        public required int Jugador1Id { get; set; }
        public required int Jugador2Id { get; set; }

        // Estado del juego
        public required string Estado { get; set; } = "Pendiente";

        // Ganador (null si el juego no ha finalizado)
        public int? GanadorId { get; set; }
    }

}
