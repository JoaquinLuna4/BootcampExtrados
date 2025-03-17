using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.EntidadesRelaciones;
using Microsoft.AspNetCore.Http.HttpResults;
using MySqlConnector;
using TrabajoFinal.Servicios.Excepciones;

namespace TrabajoFinal.Servicios
{
    public class TorneoJugadorService:ITorneoJugadorService
    {
        private readonly IDAOTorneoJugador _torneoJugadorDAO;
        private readonly ITorneoService _torneoService;
        private readonly IDAOTorneoMazo _torneoMazoDAO;


        public TorneoJugadorService(IDAOTorneoJugador torneoJugadorDAO, ITorneoService torneoService, IDAOTorneoMazo torneoMazoDAO)
        {
            _torneoJugadorDAO = torneoJugadorDAO;
            _torneoService = torneoService;
            _torneoMazoDAO = torneoMazoDAO;
        }

        public bool InscribirJugador(int torneoId, int jugadorId, int mazoId)
        {
            // Verificar que el torneo esté en fase de registro
            string fase = _torneoService.ObtenerFaseTorneoId(torneoId);

            if (fase != TorneoFase.Registro) throw new FaseRegistroCaducadaException {
                TorneoId = torneoId,
                FaseActual = fase
            };
            // Validar que el jugador posee el mazo
            if (!_torneoMazoDAO.JugadorPoseeMazo(jugadorId, mazoId))
            {
                throw new JugadorNoPoseeMazoException(jugadorId, mazoId);
            }
            // Validar que el mazo es válido para el torneo
            if (!_torneoMazoDAO.MazoEsValidoParaTorneo(torneoId, mazoId))
            {
                throw new MazoNoValidoParaTorneoException(mazoId, torneoId);
            }

            // Registrar el mazo en el torneo
            try
            {
                if (!_torneoMazoDAO.AsignarMazoATorneo(torneoId, jugadorId, mazoId))
            {
                throw new MazoNoEnTorneoTorneoException(mazoId, torneoId);
                }
            }
            catch (MySqlException ex) when (ex.Message.Contains("Duplicate entry"))
            {
                throw new MazoYaAsignadoATorneoException(torneoId, jugadorId, mazoId);
            }
            try
            {
                // Inscribir jugador con el mazo
                return _torneoJugadorDAO.InscribirJugador(torneoId, jugadorId, mazoId);
            }
            catch (MySqlException ex) when (ex.Message.Contains("Duplicate entry"))
            {
                throw new JugadorYaInscriptoException(torneoId, jugadorId);
            }
        }

        public IEnumerable<TorneoJugador> ObtenerJugadoresPorTorneo(int torneoId)
        {
            return _torneoJugadorDAO.ObtenerJugadoresPorTorneo(torneoId);
        }

       

        public bool DesinscribirJugador(int torneoId, int jugadorId)
        {
            string fase = _torneoService.ObtenerFaseTorneoId(torneoId);

            if (fase != TorneoFase.Registro) throw new FaseRegistroCaducadaException
            {    TorneoId = torneoId,
                 FaseActual =  fase
            };
            return _torneoJugadorDAO.DesinscribirJugador(torneoId, jugadorId);
        }

    }

}
