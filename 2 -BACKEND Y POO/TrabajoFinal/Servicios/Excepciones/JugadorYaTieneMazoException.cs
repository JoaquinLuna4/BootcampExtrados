namespace TrabajoFinal.Servicios.Excepciones
{
    public class JugadorYaTieneMazoException : Exception
    {
        public JugadorYaTieneMazoException(int jugadorId, int mazoId)
            : base($"El jugador con ID {jugadorId} ya posee el mazo con ID {mazoId}.") { }
    }
}
