using LibraryTrabajoFinal.Entidades;

namespace LibraryTrabajoFinal.DAOS
{
    public interface IDAOSeries
    {
        int CrearSerie(Serie serie);
        Serie? ObtenerSeriePorId(int id);
        IEnumerable<Serie> ObtenerTodasLasSeries();
        bool EliminarSerie(int id);
    }
}
