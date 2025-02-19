using LibraryTrabajoFinal.Entidades;

namespace TrabajoFinal.Servicios.Excepciones
{
    public class FaseFinalizadaException : Exception
    {
        public int TorneoId { get; set; }
        public string FaseActual { get; set; }

        public FaseFinalizadaException(int torneoId,    string fase)
            : base($"El torneo con ID {torneoId} ya está en fase '{fase}' y no puede avanzar más.")
        {
            TorneoId = torneoId;
            FaseActual = fase;
        }
    }
}
