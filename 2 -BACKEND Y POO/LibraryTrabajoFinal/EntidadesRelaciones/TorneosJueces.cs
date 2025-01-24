namespace LibraryTrabajoFinal.EntidadesRelaciones
{
    public class TorneoJuez
    {
        public required int Id { get; set; }
        public required int TorneoId { get; set; } // Referencia al torneo
        public required int JuezId { get; set; } // Referencia al juez (Usuario)
    }
}
