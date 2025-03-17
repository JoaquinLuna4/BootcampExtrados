namespace TrabajoFinal.Servicios.Excepciones
{
    public class CartaNoEncontradaException : Exception
    {
        public List<int> CartasIds { get; }

        public CartaNoEncontradaException(List<int> cartasIds)
            : base($"No se encontraron una o más cartas en la base de datos: {string.Join(", ", cartasIds)}.")
        {
            CartasIds = cartasIds;
        }
    }

}
