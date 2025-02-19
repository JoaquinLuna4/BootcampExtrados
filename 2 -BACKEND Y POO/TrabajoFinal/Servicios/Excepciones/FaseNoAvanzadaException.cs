namespace TrabajoFinal.Servicios.Excepciones
{
    public class FaseNoAvanzadaException : Exception
    {
        public int TorneoId { get; }

        public FaseNoAvanzadaException(int torneoId)
            : base($"No se pudo avanzar la fase del torneo con ID {torneoId}.")
        {
            TorneoId = torneoId;
        }
    }
}
