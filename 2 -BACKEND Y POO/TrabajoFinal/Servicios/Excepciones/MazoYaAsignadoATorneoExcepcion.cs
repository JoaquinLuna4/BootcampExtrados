namespace TrabajoFinal.Servicios.Excepciones
{
    public class MazoYaAsignadoATorneoException : Exception
    {
        public int TorneoId { get; }
        public int JugadorId { get; }
        public int MazoId { get; }

        public MazoYaAsignadoATorneoException(int torneoId, int jugadorId, int mazoId)
            : base($"El mazo con ID {mazoId} ya está asignado al jugador {jugadorId} en el torneo {torneoId}.")
        {
            TorneoId = torneoId;
            JugadorId = jugadorId;
            MazoId = mazoId;
        }
    }

}
