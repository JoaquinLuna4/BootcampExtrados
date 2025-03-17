namespace LibraryTrabajoFinal.Entidades
{
    public class Mazo
    {
        public int Id { get; set; }
        public required int JugadorId { get; set; }
        public required string Nombre { get; set; }
        public List<int> CartasIds { get; set; } = new(); // Máximo 15 cartas

    }
}
