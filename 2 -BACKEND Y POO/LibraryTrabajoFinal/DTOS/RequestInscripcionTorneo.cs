using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace LibraryTrabajoFinal.DTOS
    {
        public class RequestInscripcionTorneo
        {
            public required int TorneoId { get; set; }
            public required int JugadorId { get; set; }
            public required int MazoId { get; set; } // El mazo con el que participará el jugador
        }
    }


