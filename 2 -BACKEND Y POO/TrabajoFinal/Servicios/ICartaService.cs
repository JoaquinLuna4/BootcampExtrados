namespace TrabajoFinal.Servicios
{
    public interface ICartaService
    {
        public int CrearCarta(string nombre, int ataque, int defensa, string ilustracion, List<int> series);
        public int CrearMazo(int jugadorId, string nombre, List<int> cartasIds);
    }
}