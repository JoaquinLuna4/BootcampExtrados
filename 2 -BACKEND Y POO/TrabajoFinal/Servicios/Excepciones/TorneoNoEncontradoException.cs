namespace TrabajoFinal.Servicios.Excepciones
{
    public class TorneoNoEncontradoException : Exception
    {
        public int TorneoId { get; }

        public TorneoNoEncontradoException(int torneoId)
            : base($"El torneo con ID {torneoId} no fue encontrado.")
        {
            TorneoId = torneoId;
        }
    }
}
