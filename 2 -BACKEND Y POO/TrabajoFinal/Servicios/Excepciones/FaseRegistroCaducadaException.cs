using LibraryTrabajoFinal.Entidades;

namespace TrabajoFinal.Servicios.Excepciones
{
    public class FaseRegistroCaducadaException : Exception
    {
        public FaseRegistroCaducadaException() : base("El torneo ya no está en fase de registro") { }
        public FaseRegistroCaducadaException(string message) : base(message) { }
        public FaseRegistroCaducadaException(string message, Exception innerException) : base(message, innerException) { }

        
         public int TorneoId { get; set; }
         public string FaseActual { get; set; }
    }
}
