using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;

namespace TrabajoFinal.Servicios
{
    public class SerieService : ISerieService
    {
        private readonly IDAOSeries _serieDAO;

        public SerieService(IDAOSeries serieDAO)
        {
            _serieDAO = serieDAO;
        }

        public int CrearSerie(string nombre, DateTime fechaLanzamiento)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la serie no puede estar vacío.");

            return _serieDAO.CrearSerie(new Serie { Nombre = nombre, FechaLanzamiento = fechaLanzamiento });
        }

        public Serie? ObtenerSeriePorId(int id)
        {
            return _serieDAO.ObtenerSeriePorId(id);
        }

        public IEnumerable<Serie> ObtenerTodasLasSeries()
        {
            return _serieDAO.ObtenerTodasLasSeries();
        }

        public bool EliminarSerie(int id)
        {
            return _serieDAO.EliminarSerie(id);
        }
    }
}
