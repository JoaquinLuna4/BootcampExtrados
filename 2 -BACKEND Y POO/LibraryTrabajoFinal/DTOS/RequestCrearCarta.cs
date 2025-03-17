namespace LibraryTrabajoFinal.DTOS
{
    public class RequestCrearCarta
    {
        public required string Nombre { get; set; }
        public required int Ataque { get; set; }
        public required int Defensa { get; set; }
        public required string Ilustracion { get; set; }
        public required List<int> SeriesIds { get; set; }
    }
}