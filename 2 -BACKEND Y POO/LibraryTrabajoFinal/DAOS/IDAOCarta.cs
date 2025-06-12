using LibraryTrabajoFinal.Entidades;
namespace LibraryTrabajoFinal.DAOS
{
    public interface IDAOCarta
    {
        public int CrearCarta(Carta carta);
        public IEnumerable<Carta> getAllCards();
        bool CartaExiste(string nombre, int serieId);
        public int CrearMazo(Mazo mazo);
        public IEnumerable<Mazo> GetDeckbyUser(int JugadorId);
        public IEnumerable<Carta> GetCartasByIds(IEnumerable<int> ids);
        public IEnumerable<Carta> GetCartasByMazoId(int mazoId);
    }
}