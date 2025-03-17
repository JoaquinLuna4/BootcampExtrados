using LibraryTrabajoFinal.Entidades;

namespace LibraryTrabajoFinal.DAOS
{
    public interface IDAOJuego
    {
        int CrearJuego(Juego juego);
        IEnumerable<Juego> ObtenerJuegosPorTorneo(int torneoId);
        Juego ObtenerJuegoPorId(int juegoID);
        bool ActualizarJuego(Juego juego);
        bool AsignarGanador(int juegoId, int ganadorId, DateTime fechaHoraFin);
        bool ModificarEmparejamiento(int juegoId, int nuevoJugador1Id, int nuevoJugador2Id);

        IEnumerable<Usuario> ObtenerGanadoresPorTorneo(int torneoId);
        IEnumerable<Juego> ObtenerJuegosPendientesPorTorneo(int torneoId);
        int ObtenerJuegoIdPorJugador(int torneoId, int jugadorId);
        bool IntercambiarJugador(int juegoId, int jugadorNuevoId, int jugadorAntiguoId);
    }
}
