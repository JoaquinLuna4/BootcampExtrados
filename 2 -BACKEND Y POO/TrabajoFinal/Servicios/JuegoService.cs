using LibraryTrabajoFinal.DAOS;
using TrabajoFinal.Servicios.Excepciones;
using LibraryTrabajoFinal.Entidades;

namespace TrabajoFinal.Servicios
{
    public class JuegoService : IJuegoService
    {
        private readonly IDAOJuego _juegoDAO;
        private readonly ITorneoDAO _torneoDAO;
        private readonly IDAOTorneoJugador _torneoJugadorDAO;

        public JuegoService(IDAOJuego juegoDAO, ITorneoDAO torneoDAO, IDAOTorneoJugador torneoJugadorDAO)
        {
            _juegoDAO = juegoDAO;
            _torneoDAO = torneoDAO;
            _torneoJugadorDAO = torneoJugadorDAO;
        }

        public int CrearJuego(int torneoId, int jugador1Id, int jugador2Id, DateTime fechaHoraInicio)
        {
            Juego nuevoJuego = new()
            {
                 
                TorneoId = torneoId,
                Jugador1Id = jugador1Id,
                Jugador2Id = jugador2Id,
                FechaHoraInicio = fechaHoraInicio,
                Estado = "Pendiente"
            };
            return _juegoDAO.CrearJuego(nuevoJuego);
        }

        public IEnumerable<Juego> ObtenerJuegosPorTorneo(int torneoId)
        {
            return _juegoDAO.ObtenerJuegosPorTorneo(torneoId);
        }

        public bool ModificarEmparejamiento(int organizadorId, int juegoId, int nuevoJugador1Id, int nuevoJugador2Id)
        {
            var juego = _juegoDAO.ObtenerJuegoPorId(juegoId);
            if (juego == null) throw new JuegoNoEncontradoException(juegoId);

            int torneoId = juego.TorneoId;

            var torneo = _torneoDAO.ObtenerTorneoPorId(juego.TorneoId);
            if (torneo == null) throw new TorneoNoEncontradoException(juego.TorneoId);


            // Validar que el organizador sea el correspondiente al torneo
            if (torneo.OrganizadorId != organizadorId)
            {
                throw new AccesoNoAutorizadoException("Solo el organizador del torneo puede modificar emparejamientos.");
            }

            // Validar que el juego no haya iniciado
            if (juego.Estado != "Pendiente")
            {
                throw new InvalidOperationException("Solo se pueden modificar emparejamientos antes de que el juego comience.");
            }
            bool validaInscripto1 = _torneoJugadorDAO.JugadorEstaInscritoEnTorneo(torneo.Id, nuevoJugador1Id);
           

            bool validaInscripto2 = _torneoJugadorDAO.JugadorEstaInscritoEnTorneo(torneo.Id, nuevoJugador2Id);
           


            // Validar que los jugadores estén inscriptos en el torneo, si uno no esta inscripto da error
            if (!validaInscripto1 || !validaInscripto2)
            {
                if (!validaInscripto1) Console.WriteLine($"Jugador {nuevoJugador1Id} no esta inscripto en el torneo: {torneo.Id}");
                if (!validaInscripto2) Console.WriteLine($"Jugador {nuevoJugador2Id} no esta inscripto en el torneo: {torneo.Id}.");

                throw new InvalidOperationException("Ambos jugadores deben estar inscritos en el torneo.");
            }
           
            // Obtener los juegos donde estaban los jugadores antes
            int juegoAnteriorJugador1 = _juegoDAO.ObtenerJuegoIdPorJugador(torneoId, nuevoJugador1Id);
            int juegoAnteriorJugador2 = _juegoDAO.ObtenerJuegoIdPorJugador(torneoId, nuevoJugador2Id);

            if (juegoAnteriorJugador1 > 0 && juegoAnteriorJugador1 != juegoId)
            {
                // Sacamos a nuevoJugador1 de su juego anterior
                _juegoDAO.IntercambiarJugador(juegoAnteriorJugador1, nuevoJugador1Id, juego.Jugador1Id);
            }

            if (juegoAnteriorJugador2 > 0 && juegoAnteriorJugador2 != juegoId)
            {
                // Sacamos a nuevoJugador2 de su juego anterior
                _juegoDAO.IntercambiarJugador(juegoAnteriorJugador2, nuevoJugador2Id, juego.Jugador2Id);
            }

            return _juegoDAO.ModificarEmparejamiento(juegoId, nuevoJugador1Id, nuevoJugador2Id);
        }

        public bool FinalizarJuego(int juegoId, int ganadorId, DateTime fechaHoraFin)
        {
           Juego juego = _juegoDAO.ObtenerJuegoPorId(juegoId);
            if (juego == null) throw new JuegoNoEncontradoException(juegoId);
            if (juego.FechaHoraFin != null) throw new JuegoYaFinalizadoException(juegoId);
            if (juego.FechaHoraFin < juego.FechaHoraInicio)
                throw new HoraFinInvalidaException(juegoId, juego.FechaHoraInicio, fechaHoraFin);
            return _juegoDAO.AsignarGanador(juegoId, ganadorId, fechaHoraFin);
        }
    }

}
