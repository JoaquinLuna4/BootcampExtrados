namespace TrabajoFinal.Servicios.Excepciones
{
    public class MazoNoValidoParaTorneoException : Exception
    {
        public MazoNoValidoParaTorneoException(int mazoId, int torneoId)
            : base($"El mazo con ID {mazoId} no cumple con las series permitidas en el torneo ID {torneoId}.") { }
    }
}
