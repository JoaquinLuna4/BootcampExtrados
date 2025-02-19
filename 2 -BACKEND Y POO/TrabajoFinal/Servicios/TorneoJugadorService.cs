using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.EntidadesRelaciones;
using Microsoft.AspNetCore.Http.HttpResults;
using TrabajoFinal.Servicios.Excepciones;

namespace TrabajoFinal.Servicios
{
    public class TorneoJugadorService:ITorneoJugadorService
    {
        private readonly IDAOTorneoJugador _torneoJugadorDAO;
        private readonly ITorneoService _torneoService;


        public TorneoJugadorService(IDAOTorneoJugador torneoJugadorDAO, ITorneoService torneoService)
        {
            _torneoJugadorDAO = torneoJugadorDAO;
            _torneoService = torneoService;
        }

        public bool InscribirJugador(int torneoId, int jugadorId, int mazoId)
        {
            string fase = _torneoService.ObtenerFaseTorneoId(torneoId);

            if (fase != TorneoFase.Registro) throw new FaseRegistroCaducadaException {
                TorneoId = torneoId,
                FaseActual = fase
            };
            return _torneoJugadorDAO.InscribirJugador(torneoId, jugadorId, mazoId);
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
