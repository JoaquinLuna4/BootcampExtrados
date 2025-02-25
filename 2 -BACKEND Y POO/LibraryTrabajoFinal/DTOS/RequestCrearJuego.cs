using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTrabajoFinal.DTOS
{
    public class RequestCrearJuego
    {
        public required int TorneoId { get; set; }
        public required int Jugador1Id { get; set; }
        public required int Jugador2Id { get; set; }
        public required DateTime FechaHoraInicio { get; set; }
    }

}
