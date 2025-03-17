using LibraryTrabajoFinal.Entidades;
namespace LibraryTrabajoFinal.DAOS
{
    public interface IDAOCarta
    {
        public int CrearCarta(Carta carta);
        public IEnumerable<Carta> ObtenerCartas();
        bool CartaExiste(string nombre, int serieId);
        public int CrearMazo(Mazo mazo);
    }
}