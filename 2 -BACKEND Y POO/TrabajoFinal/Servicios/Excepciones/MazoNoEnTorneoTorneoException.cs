namespace TrabajoFinal.Servicios.Excepciones
{
    public class MazoNoEnTorneoTorneoException : Exception
    {
        public MazoNoEnTorneoTorneoException(int mazoId, int torneoId)
            : base($"El mazo con ID {mazoId} no pudo ser registrado en el torneo ID {torneoId}.") { }
    }
}
