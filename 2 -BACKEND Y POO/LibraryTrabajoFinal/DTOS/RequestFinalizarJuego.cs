using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTrabajoFinal.DTOS
{
    public class RequestFinalizarJuego
    {
        public required int Id { get; set; }
        public required int GanadorId { get; set; }
        public required DateTime FechaHoraFin { get; set; }
    }

}
