using LibraryTrabajoFinal.EntidadesRelaciones;

namespace TrabajoFinal.Servicios
{
    public interface ITorneoJuezService
    {
        bool AsignarJuezATorneo(int torneoId, int juezId);
        bool EliminarJuezDeTorneo(int torneoId, int juezId);
        IEnumerable<TorneoJuez> ObtenerJuecesPorTorneo(int torneoId);
    }
}
