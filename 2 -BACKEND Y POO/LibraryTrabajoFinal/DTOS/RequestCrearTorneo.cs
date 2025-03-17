using LibraryTrabajoFinal.Entidades;
using System.Text.Json.Serialization;

namespace LibraryTrabajoFinal.DTOS
{
    public class RequestCrearTorneo
    {
        public required string Nombre { get; set; }
        public required int OrganizadorId { get; set; }
        public required DateTime FechaInicio { get; set; }
        public required DateTime FechaFin { get; set; }
        public required string Pais { get; set; }
        public List<int>? SeriesIds { get; set; }
    }
}
