namespace TrabajoFinal.Servicios.Excepciones
{
    public class JugadorNoPoseeMazoException : Exception
    {
        public JugadorNoPoseeMazoException(int jugadorId, int mazoId)
            : base($"El jugador con ID {jugadorId} no posee el mazo con ID {mazoId}.") { }
    }
}
