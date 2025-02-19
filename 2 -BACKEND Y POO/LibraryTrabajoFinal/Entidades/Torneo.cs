namespace LibraryTrabajoFinal.Entidades
{
    public class Torneo
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int OrganizadorId { get; set; } // Referencia a un organizador (Usuario)

        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; }
        public required string Pais { get; set; }

        // Fase actual del torneo
        public required string Fase { get; set; }

        // Relación con jueces y jugadores
        public List<Usuario>? Jueces { get; set; }
        public List<Usuario>? Jugadores { get; set; }
        public int? GanadorId { get; set; }
    }

    public static class TorneoFase
    {
        public const string Registro = "Registro";
        public const string Torneo = "Torneo";
        public const string Finalizacion = "Finalizacion";
    }
}
