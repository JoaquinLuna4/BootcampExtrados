namespace TrabajoFinal.Servicios.Excepciones
{
    public class CartaDuplicadaException : Exception
    {
        public string Nombre { get; }
        public int SerieId { get; }

        public CartaDuplicadaException(string nombre, int serieId)
            : base($"La carta '{nombre}' ya existe en la serie con ID {serieId}.")
        {
            Nombre = nombre;
            SerieId = serieId;
        }
    }

}
