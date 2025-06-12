using LibraryTrabajoFinal.Entidades;

namespace TrabajoFinal.Servicios
{
    public interface ICartaService
    {
        public int CrearCarta(string nombre, int ataque, int defensa, string ilustracion, List<int> series);
        public int CrearMazo(int jugadorId, string nombre, List<int> cartasIds);
        public IEnumerable<Carta> ListarCartas();
        public IEnumerable<Mazo> ObtenerMazoPorId(int id);
        public IEnumerable<Carta> ObtenerCartaPorId(IEnumerable<int> ids);
        public IEnumerable<Carta> ObtenerCartasDeMazo(int mazoId);
    }
}