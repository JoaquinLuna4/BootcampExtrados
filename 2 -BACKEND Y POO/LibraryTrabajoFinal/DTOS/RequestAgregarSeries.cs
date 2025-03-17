using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTrabajoFinal.DTOS
{
    public class RequestAgregarSeries
    {
        public required List<int> SeriesIds { get; set; }
    }
}
