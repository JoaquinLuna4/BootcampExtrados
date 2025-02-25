using LibraryTrabajoFinal.DAOS;
using TrabajoFinal.Servicios.Excepciones;
using LibraryTrabajoFinal.Entidades;

namespace TrabajoFinal.Servicios
{
    public class JuegoService : IJuegoService
    {
        private readonly IDAOJuego _juegoDAO;

        public JuegoService(IDAOJuego juegoDAO)
        {
            _juegoDAO = juegoDAO;
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
        public bool FinalizarJuego(int juegoId, int ganadorId, DateTime fechaHoraFin)
        {
           Juego juego = _juegoDAO.ObtenerJuegoPorId(juegoId);
            if (juego == null) throw new JuegoNoEncontradoException(juegoId);

            if (juego.FechaHoraFin != null)
            {
                throw new InvalidOperationException("El juego ya fue finalizado.");
            }

            if (juego.FechaHoraFin < juego.FechaHoraInicio)
            {
                throw new InvalidOperationException("La hora de finalización no puede ser menor a la de inicio.");
            }

            return _juegoDAO.AsignarGanador(juegoId, ganadorId, fechaHoraFin);
        }
    }

}
