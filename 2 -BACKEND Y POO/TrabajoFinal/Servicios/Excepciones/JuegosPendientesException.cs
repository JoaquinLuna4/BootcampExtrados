namespace TrabajoFinal.Servicios.Excepciones
{
    public class JuegosPendientesException : Exception
    {
        public int TorneoId { get; }

        public JuegosPendientesException(int torneoId)
            : base($"No se puede avanzar la fase del torneo ID {torneoId} hasta que todos los juegos hayan finalizado.")
        {
            TorneoId = torneoId;
        }
    }

}
