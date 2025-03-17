namespace TrabajoFinal.Servicios.Excepciones
{
    public class JugadorYaInscriptoException : Exception
    {
        public int TorneoId { get; }
        public int JugadorId { get; }

        public JugadorYaInscriptoException(int torneoId, int jugadorId)
            : base($"El jugador con ID {jugadorId} ya está inscrito en el torneo con ID {torneoId}.")
        {
            TorneoId = torneoId;
            JugadorId = jugadorId;
        }
    }

}
