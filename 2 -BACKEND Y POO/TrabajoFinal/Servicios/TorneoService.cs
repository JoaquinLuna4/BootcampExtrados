using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.DTOS;
using TrabajoFinal.Servicios.Excepciones;


namespace TrabajoFinal.Servicios
{
    public class TorneoService : ITorneoService
    {
        private readonly ITorneoDAO _torneoDAO;
        private readonly IDAOTorneoJugador _torneoJugadorDAO;
        private readonly IDAOJuego _juegoDAO;

        public TorneoService(ITorneoDAO torneoDAO, IDAOTorneoJugador DAOTorneoJugador, IDAOJuego juegoDAO)
        {
            _torneoDAO = torneoDAO;
            _torneoJugadorDAO = DAOTorneoJugador;
            _juegoDAO = juegoDAO;
        }
        public int CrearTorneo(RequestCrearTorneo torneo)
        {
            // Valida que exista nombre
            if (string.IsNullOrWhiteSpace(torneo.Nombre))
                throw new NombreTorneoInvalidoException(torneo.Nombre);

            // Valida fechas 
            if (torneo.FechaInicio >= torneo.FechaFin)
                throw new FechasTorneoInvalidasException(torneo.FechaInicio, torneo.FechaFin);

            // Crea el torneo con fase por defecto en fase registro 
            var nuevoTorneo = new Torneo
            {
                
                Nombre = torneo.Nombre,
                OrganizadorId = torneo.OrganizadorId,
                FechaInicio = torneo.FechaInicio,
                FechaFin = torneo.FechaFin,
                Pais = torneo.Pais,
                Fase = TorneoFase.Registro // Fase por defecto
            };

            // Recupero la id del torneo creado
            int torneoId = _torneoDAO.CrearTorneo(nuevoTorneo);

            // Si el torneo tiene series permitidas, las insertamos
            if (torneo.SeriesIds != null && torneo.SeriesIds.Any())
            {
                _torneoDAO.AsignarSeriesATorneo(torneoId, torneo.SeriesIds);
            }

            return _torneoDAO.CrearTorneo(nuevoTorneo);
        }
       
        public void AgregarSeriesATorneoExistente(int torneoId, List<int> seriesIds)
        {
            // Validar que el torneo existe
            if (!_torneoDAO.TorneoExiste(torneoId))
            {
                throw new TorneoNoEncontradoException(torneoId);
            }

            // Obtener la fase actual y validar que esté en fase de "Registro"
            string faseActual = _torneoDAO.ObtenerFaseTorneoPorId(torneoId);
            if (faseActual != TorneoFase.Registro)
            {
                throw new FaseRegistroCaducadaException { TorneoId = torneoId, FaseActual = faseActual };
            }

            // Obtener las series ya asignadas al torneo
            List<int> seriesActuales = _torneoDAO.ObtenerSeriesPorTorneo(torneoId);

            // Filtrar las series que ya están permitidas para evitar duplicados
            var seriesNuevas = seriesIds.Except(seriesActuales).ToList();

            if (!seriesNuevas.Any())
            {
                throw new InvalidOperationException("Las series proporcionadas ya están permitidas en este torneo.");
            }

            // Agregar solo las series nuevas
            _torneoDAO.AsignarSeriesATorneo(torneoId, seriesNuevas);
        }
        public Torneo? ObtenerTorneoPorId(int id)
        {
            return _torneoDAO.ObtenerTorneoPorId(id);
        }
        public bool TorneoExiste(int torneoId)
        {
            return _torneoDAO.TorneoExiste(torneoId);
        }
        public IEnumerable<Torneo> ObtenerTodosLosTorneos()
        {
            return _torneoDAO.ObtenerTodosLosTorneos();
        }
        public string ObtenerFaseTorneoId(int id)
        {
            return _torneoDAO.ObtenerFaseTorneoPorId(id);
        }
        public bool ActualizarTorneo(Torneo torneo)
        {
            return _torneoDAO.ActualizarTorneo(torneo);
        }
        public bool AvanzarFase(int torneoId)
        {
            // Validar existencia del torneo
            if (!_torneoDAO.TorneoExiste(torneoId))
                throw new TorneoNoEncontradoException(torneoId);

            // Obtener fase actual
            var faseActual = _torneoDAO.ObtenerFaseTorneoPorId(torneoId);
            if (faseActual == TorneoFase.Finalizacion)
                throw new TorneoYaFinalizadoException(torneoId);

            // Validar que no haya juegos pendientes
            var juegosPendientes = _juegoDAO.ObtenerJuegosPendientesPorTorneo(torneoId);
            if (juegosPendientes.Any())
                throw new JuegosPendientesException(torneoId);


            // Determinar nueva fase
            string nuevaFase = faseActual switch
            {
                TorneoFase.Registro => TorneoFase.Torneo,
                TorneoFase.Torneo => TorneoFase.Finalizacion,
                _ => throw new InvalidOperationException("Fase inválida.")
            };

            // Avanzar la fase directamente como string
            bool faseAvanzada = _torneoDAO.AvanzarFase(torneoId, nuevaFase);

            if (!faseAvanzada)
            {
                throw new InvalidOperationException($"No se pudo avanzar la fase del torneo ID {torneoId}.");
            }
            // Cuando la nueva fase a enviar sea torneo: que ya genere los primeros juegos
            if (nuevaFase == TorneoFase.Torneo)
            {
                GenerarPrimeraRonda(torneoId);
            }

            return true;
        }
        public void GenerarPrimeraRonda(int torneoId)
        {
            var jugadores = _torneoJugadorDAO.ObtenerJugadoresPorTorneo(torneoId).ToList();
            if (jugadores.Count < 2)
            {
                throw new InvalidOperationException("No hay suficientes jugadores para iniciar el torneo.");
            }

            // Ordenar aleatoriamente los jugadores
            var random = new Random();
            jugadores = jugadores.OrderBy(x => random.Next()).ToList();
            Console.WriteLine("jugadores aleatorios" , jugadores );

            // Calcular la hora de inicio de la primera partida
            DateTime horaInicio = DateTime.Now;

            for (int i = 0; i < jugadores.Count; i += 2) //Recorremos la lista aleatoria de 2 en 2
            {
                if (i + 1 < jugadores.Count) //si hay al menos dos jugadores crea el cruce entre ellos
                {
                    _juegoDAO.CrearJuego(new Juego
                    {
                        TorneoId = torneoId,
                        Jugador1Id = jugadores[i].JugadorId,  //jugador de la lista del for
                        Jugador2Id = jugadores[i + 1].JugadorId, //jugador siguiente al anterior (for +1)
                        FechaHoraInicio = horaInicio,
                        Estado = "Pendiente"
                    });

                    // Sumar 30 minutos para el siguiente juego
                    horaInicio = horaInicio.AddMinutes(30);
                }
            }
        }
        public bool AvanzarRonda(int torneoId)
        {
            var juegosPendientes = _juegoDAO.ObtenerJuegosPendientesPorTorneo(torneoId);

            if (juegosPendientes.Any()) //Valida que todos los juegos de la ronda actual terminaron
            {
                throw new InvalidOperationException("No se puede avanzar la ronda hasta que todos los juegos hayan finalizado.");
            }

            var ganadores = _juegoDAO.ObtenerGanadoresPorTorneo(torneoId).ToList();

            if (ganadores.Count == 1) //Si solo queda un jugador finaliza el torneo
            {
                _torneoDAO.FinalizarTorneo(torneoId, ganadores.First().Id);
                return true;
            }
            
            //Si quedan mas jugadores genera nueva ronda
            else { 
            GenerarNuevaRonda(torneoId, ganadores);
            return true;
            }
        }
        private void GenerarNuevaRonda(int torneoId, List<Usuario> ganadores)
        {
            var random = new Random();
            ganadores = ganadores.OrderBy(x => random.Next()).ToList();
            Console.WriteLine("jugadores aleatorios", ganadores);


            DateTime horaInicio = DateTime.Now;

            for (int i = 0; i < ganadores.Count; i += 2)
            {
                if (i + 1 < ganadores.Count)
                {
                    _juegoDAO.CrearJuego(new Juego
                    {
                        TorneoId = torneoId,
                        Jugador1Id = ganadores[i].Id,
                        Jugador2Id = ganadores[i + 1].Id,
                        FechaHoraInicio = horaInicio,
                        Estado = "Pendiente"
                    });

                    horaInicio = horaInicio.AddMinutes(30);
                }
            }
        }
        public bool EliminarTorneo(int id)
        {
            return _torneoDAO.EliminarTorneo(id);
        }
    }

}
