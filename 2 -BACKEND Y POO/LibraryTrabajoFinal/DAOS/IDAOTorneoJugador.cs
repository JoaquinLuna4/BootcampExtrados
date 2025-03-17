using LibraryTrabajoFinal.EntidadesRelaciones;

namespace LibraryTrabajoFinal.DAOS
{
    public interface IDAOTorneoJugador
    {
        bool InscribirJugador(int torneoId, int jugadorId, int mazoId);
        IEnumerable<TorneoJugador> ObtenerJugadoresPorTorneo(int torneoId);
        bool DesinscribirJugador(int torneoId, int jugadorId);
        bool JugadorEstaInscritoEnTorneo(int id, int nuevoJugador1Id);
    }

}