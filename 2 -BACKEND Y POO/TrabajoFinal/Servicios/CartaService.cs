using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;
using MySqlConnector;
using TrabajoFinal.Servicios.Excepciones;

namespace TrabajoFinal.Servicios
{
    public class CartaService : ICartaService
    {
        private readonly IDAOCarta _cartaDAO;

        public CartaService(IDAOCarta cartaDAO)
        {
            _cartaDAO = cartaDAO;
        }

        public int CrearCarta(string nombre, int ataque, int defensa, string ilustracion, List<int> series)
        {
            var nuevaCarta = new Carta
            {
                Nombre = nombre,
                Ataque = ataque,
                Defensa = defensa,
                Ilustracion = ilustracion,
                SeriesIds = series
            };
            if (_cartaDAO.CartaExiste(nuevaCarta.Nombre, nuevaCarta.Id))
            {
                throw new CartaDuplicadaException(nuevaCarta.Nombre, nuevaCarta.Id);
            }

           
            return _cartaDAO.CrearCarta(nuevaCarta);
        }

        public int CrearMazo(int jugadorId, string nombre, List<int> cartasIds)
        {
            if (cartasIds.Count != 15)
            {
                throw new CantidadCartasMazoInvalidaException(cartasIds.Count);
            }

            var nuevoMazo = new Mazo
            {
                JugadorId = jugadorId,
                Nombre = nombre,
                CartasIds = cartasIds
            };
            try
            {
                return _cartaDAO.CrearMazo(nuevoMazo);
            }
            catch (MySqlException ex) when (ex.Message.Contains("foreign key constraint fails"))
            {
                throw new CartaNoEncontradaException(cartasIds);
            }

        }
    }

}
