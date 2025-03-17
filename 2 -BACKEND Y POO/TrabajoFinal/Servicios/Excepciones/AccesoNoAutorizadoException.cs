namespace TrabajoFinal.Servicios.Excepciones
{
    public class AccesoNoAutorizadoException : Exception
    {
        public AccesoNoAutorizadoException(string v)
            : base($"Solo el organizador del torneo puede modificar emparejamientos.") { }
    }
}
