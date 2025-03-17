namespace LibraryTrabajoFinal.Entidades
{
    public class Carta
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Ataque { get; set; }
        public required int Defensa { get; set; }
        public required string Ilustracion { get; set; } // URL de la imagen
        public List<int> SeriesIds { get; set; } = new(); // Relación con series
    }
}
