namespace TrabajoFinal.Servicios.Excepciones
{
    public class NombreTorneoInvalidoException : Exception
    {
        public string Nombre { get; }

        public NombreTorneoInvalidoException(string nombre)
            : base($"El nombre del torneo '{nombre}' no es válido. Debe contener al menos un carácter.")
        {
            Nombre = nombre;
        }
    }
}
