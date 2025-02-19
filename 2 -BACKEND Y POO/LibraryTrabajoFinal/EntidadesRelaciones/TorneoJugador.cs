using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTrabajoFinal.EntidadesRelaciones
{
        public class TorneoJugador
        {
            public required int Id { get; set; }
            public required int TorneoId { get; set; }
            public required int JugadorId { get; set; }
            public required int MazoId { get; set; } // Referencia al mazo del jugador
        }
}
