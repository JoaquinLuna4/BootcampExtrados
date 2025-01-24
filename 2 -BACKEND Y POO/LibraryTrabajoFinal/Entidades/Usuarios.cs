namespace LibraryTrabajoFinal.Entidades

{
    public class Usuario
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Apellido { get; set; }
        public required string Alias { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public  string? Pais { get; set; }
        public required UserRole Rol { get; set; }
        public required DateTime Fecha_Registro { get; set; }
    }
    public enum UserRole
    {
        Administrador,
        Organizador,
        Juez,
        Jugador
    }

}
