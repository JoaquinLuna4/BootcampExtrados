using LibraryTrabajoFinal.EntidadesRelaciones;

namespace LibraryTrabajoFinal.DAOS
{
    public interface IDAOTorneoMazo
    {
        bool AsignarMazoATorneo(int torneoId, int jugadorId, int mazoId);
        bool MazoEsValidoParaTorneo(int torneoId, int mazoId);
        IEnumerable<(int CartaId, string Nombre)> ObtenerCartasDeMazo(int mazoId);
        bool JugadorPoseeMazo(int jugadorId, int mazoId);
       
    }
}
