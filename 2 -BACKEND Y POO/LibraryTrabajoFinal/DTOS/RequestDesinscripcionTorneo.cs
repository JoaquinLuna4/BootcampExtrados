using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTrabajoFinal.DTOS
{
    public class RequestDesinscripcionTorneo
    {
        public required int TorneoId { get; set; }
        public required int JugadorId { get; set; }
    }
}
