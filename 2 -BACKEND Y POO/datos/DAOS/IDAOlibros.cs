using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datos.DAOS
{
    public interface IDAOLibros
    {
        int PedirLibro(string nombreLibro);
        int DevolverLibro(string nombreLibro);
        int AsignarLibro(int idSolicitante, DateTime fechaVencimiento, DateTime fechaPrestamo, int idLibro);
        int ObtenerIdLibro(string nombreLibro);
    }
}
