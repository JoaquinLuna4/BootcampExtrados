using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTrabajoFinal.DTOS
{
    public class ResponseJugadorInscrito
    {
        public required int JugadorId { get; set; }
        public required string Alias { get; set; }
        public required int MazoId { get; set; }
    }
}

