namespace TrabajoFinal.Servicios.Excepciones
{
    public class TorneoYaFinalizadoException : Exception
    {
        public int TorneoId { get; }

        public TorneoYaFinalizadoException(int torneoId)
            : base($"El torneo con ID {torneoId} ya está finalizado.")
        {
            TorneoId = torneoId;
        }
    }

}
