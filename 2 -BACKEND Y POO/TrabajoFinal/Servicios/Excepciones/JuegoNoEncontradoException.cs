namespace TrabajoFinal.Servicios.Excepciones
{
    public class JuegoNoEncontradoException : Exception
    {
        public int JuegoId { get; }

        public JuegoNoEncontradoException(int juegoId)
            : base($"No se encontró el juego con ID {juegoId}.")
        {
            JuegoId = juegoId;
        }

        public JuegoNoEncontradoException(int juegoId, string message)
            : base(message)
        {
            JuegoId = juegoId;
        }

        public JuegoNoEncontradoException(int juegoId, string message, Exception innerException)
            : base(message, innerException)
        {
            JuegoId = juegoId;
        }
    }
}
