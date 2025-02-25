using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.DTOS;

namespace LibraryTrabajoFinal.DAOS
{
    public interface ITorneoDAO
    {
        int CrearTorneo(Torneo torneo);
        Torneo? ObtenerTorneoPorId(int id);
        IEnumerable<Torneo> ObtenerTodosLosTorneos();
        string ObtenerFaseTorneoPorId(int id);

        bool AvanzarFase(int torneoId, string nuevaFase);
        bool TorneoExiste(int torneoId);
        bool ActualizarTorneo(Torneo torneo);
        bool FinalizarTorneo(int torneoId, int ganadorId);

        bool EliminarTorneo(int id);
    }

}
