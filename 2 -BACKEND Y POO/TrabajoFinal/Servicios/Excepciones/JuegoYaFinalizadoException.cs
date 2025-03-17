namespace TrabajoFinal.Servicios.Excepciones
{
    public class JuegoYaFinalizadoException : Exception
    {
        public int JuegoId { get; }

        public JuegoYaFinalizadoException(int juegoId)
            : base($"El juego con ID {juegoId} ya fue finalizado.")
        {
            JuegoId = juegoId;
        }
    }

}
