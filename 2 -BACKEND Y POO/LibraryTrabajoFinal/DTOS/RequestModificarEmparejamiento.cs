using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTrabajoFinal.DTOS
{
    public class RequestModificarEmparejamiento
    {
        public required int JuegoId { get; set; }
        public required int NuevoJugador1Id { get; set; }
        public required int NuevoJugador2Id { get; set; }
    }
}
