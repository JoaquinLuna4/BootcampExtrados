using LibraryTrabajoFinal.Entidades;
namespace TrabajoFinal.Servicios
{
    public interface IJuegoService
    {
        int CrearJuego(int torneoId, int jugador1Id, int jugador2Id, DateTime fechaHoraInicio);
        IEnumerable<Juego> ObtenerJuegosPorTorneo(int torneoId);

        bool FinalizarJuego(int juegoId, int ganadorId, DateTime fechaHoraFin);
    }

}
