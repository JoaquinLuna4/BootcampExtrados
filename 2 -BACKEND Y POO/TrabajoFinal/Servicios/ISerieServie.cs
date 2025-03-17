using LibraryTrabajoFinal.Entidades;
namespace TrabajoFinal.Servicios
{

    public interface ISerieService
        {
            int CrearSerie(string nombre, DateTime fechaLanzamiento);
            Serie? ObtenerSeriePorId(int id);
            IEnumerable<Serie> ObtenerTodasLasSeries();
            bool EliminarSerie(int id);
        }
  
}
