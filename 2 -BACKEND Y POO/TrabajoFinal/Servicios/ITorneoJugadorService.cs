using LibraryTrabajoFinal.EntidadesRelaciones;

namespace TrabajoFinal.Servicios
{
    public interface ITorneoJugadorService
    {
        bool InscribirJugador(int torneoId, int jugadorId, int mazoId);
        IEnumerable<TorneoJugador> ObtenerJugadoresPorTorneo(int torneoId);
        bool DesinscribirJugador(int torneoId, int jugadorId);
    }

}