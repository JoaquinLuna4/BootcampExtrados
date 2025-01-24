namespace LibraryTrabajoFinal.Entidades
{
    public class Carta
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Ataque { get; set; }
        public required int Defensa { get; set; }
        public string? Ilustracion { get; set; } 
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow; 
    }
}
