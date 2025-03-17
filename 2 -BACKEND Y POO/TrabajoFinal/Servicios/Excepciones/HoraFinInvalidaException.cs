namespace TrabajoFinal.Servicios.Excepciones
{
    public class HoraFinInvalidaException : Exception
    {
        public int JuegoId { get; }
        public DateTime FechaHoraInicio { get; }
        public DateTime FechaHoraFin { get; }

        public HoraFinInvalidaException(int juegoId, DateTime inicio, DateTime fin)
            : base($"La hora de finalización {fin} no puede ser menor a la de inicio {inicio} en el juego con ID {juegoId}.")
        {
            JuegoId = juegoId;
            FechaHoraInicio = inicio;
            FechaHoraFin = fin;
        }
    }

}
