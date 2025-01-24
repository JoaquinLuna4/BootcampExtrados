namespace LibraryTrabajoFinal.Entidades
{
    public class Juego
    {
        public required int Id { get; set; }
        public required int TorneoId { get; set; } // Referencia al torneo

        public required DateTime FechaHoraInicio { get; set; }
        public DateTime? FechaHoraFin { get; set; } // Permite null si no ha terminado

        // Jugadores involucrados
        public required int Jugador1Id { get; set; }
        public required int Jugador2Id { get; set; }

        // Ganador permite null no ha terminado
        public int? GanadorId { get; set; }
    }
}
