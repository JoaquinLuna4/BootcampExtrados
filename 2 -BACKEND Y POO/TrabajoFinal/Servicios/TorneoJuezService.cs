using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.EntidadesRelaciones;
using LibraryTrabajoFinal.Entidades;

namespace TrabajoFinal.Servicios
{
    public class TorneoJuezService : ITorneoJuezService
    {
        private readonly IDAOTorneoJuez _daoTorneoJuez;
        private readonly ITorneoService _torneoService;
        private readonly IUsuarioService _usuarioService;

        public TorneoJuezService(IDAOTorneoJuez daoTorneoJuez, ITorneoService torneoService, IUsuarioService usuarioService)
        {
            _daoTorneoJuez = daoTorneoJuez;
            _torneoService = torneoService;
            _usuarioService = usuarioService;
        }

        public bool AsignarJuezATorneo(int torneoId, int juezId)
        {
            // Validar que el torneo existe
            if (!_torneoService.TorneoExiste(torneoId))
            {
                throw new Exception($"No se encontró el torneo con ID {torneoId}.");
            }

            // Validar que el usuario es un juez
            var juez = _usuarioService.ObtenerUsuarioPorId(juezId);
            if (juez == null || juez.Rol != UserRole.Juez)
            {
                throw new Exception("El usuario especificado no es un juez válido.");
            }

            return _daoTorneoJuez.AsignarJuezATorneo(torneoId, juezId);
        }

        public bool EliminarJuezDeTorneo(int torneoId, int juezId)
        {
            return _daoTorneoJuez.EliminarJuezDeTorneo(torneoId, juezId);
        }

        public IEnumerable<TorneoJuez> ObtenerJuecesPorTorneo(int torneoId)
        {
            return _daoTorneoJuez.ObtenerJuecesPorTorneo(torneoId);
        }
    }
}
