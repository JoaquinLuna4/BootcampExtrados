using LibraryTrabajoFinal.EntidadesRelaciones;

namespace LibraryTrabajoFinal.DAOS
{
    public interface IDAOTorneoJuez
    {
        bool AsignarJuezATorneo(int torneoId, int juezId);
        bool EliminarJuezDeTorneo(int torneoId, int juezId);
        IEnumerable<TorneoJuez> ObtenerJuecesPorTorneo(int torneoId);
    }
}
