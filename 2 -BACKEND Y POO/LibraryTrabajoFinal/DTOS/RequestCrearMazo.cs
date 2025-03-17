
namespace LibraryTrabajoFinal.DTOS
{
    public class RequestCrearMazo
    {
        public required int JugadorId { get; set; }
        public required string Nombre { get; set; }
        public required List<int> CartasIds { get; set; }
    }
}
