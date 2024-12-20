
using datos.DAOS;
using datos.Entidades;

namespace APIAutorizacionAutenticacion.Services
{
    public class LibroService
    {

        
        private readonly IDAOLibros _dao;
        public LibroService(IDAOLibros dao)
        {
            _dao = dao;
        }
        public void PedirLibro(string nombrelibro)
        {
            int rowsAffected = _dao.PedirLibro(nombrelibro);
            

            if (rowsAffected == 0)
            {
                Console.WriteLine("rows affected es 0" );
                throw new Exception("No se logro modificar ninguna fila");
                
            }
        }
        public void DevolverLibro(string nombrelibro)
        {
            int rowsAffected = _dao.DevolverLibro(nombrelibro);

            if (rowsAffected != 0)
            {
                Console.WriteLine("rows affected es 0");
                throw new Exception("No se logro modificar ninguna fila");
            }
        }

        public int ObtenerIDlibro (string nombrelibro)
        {
            return _dao.ObtenerIdLibro(nombrelibro) ;
        }
        public int AsignarLibro(int idSolicitante, DateTime fechaVencimiento, DateTime fechaPrestamo, int idLibro)
        {
            return _dao.AsignarLibro(idSolicitante, fechaVencimiento, fechaPrestamo, idLibro);
        }
    }
}
