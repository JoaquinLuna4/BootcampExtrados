namespace TrabajoFinal.Servicios.Excepciones
{
    public class FechasTorneoInvalidasException : Exception
    {
        public DateTime FechaInicio { get; }
        public DateTime FechaFin { get; }

        public FechasTorneoInvalidasException(DateTime fechaInicio, DateTime fechaFin)
            : base($"La fecha de inicio '{fechaInicio}' debe ser anterior a la fecha de fin '{fechaFin}'.")
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
