using MySqlConnector;
using LibraryTrabajoFinal.Entidades;
using Dapper;

namespace LibraryTrabajoFinal.DAOS
{
    public class DAOJuego : IDAOJuego
    {
        private readonly string _connectionString;

        public DAOJuego(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CrearJuego(Juego juego)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
            INSERT INTO Juegos (TorneoId, FechaHoraInicio, Jugador1Id, Jugador2Id, Estado)
            VALUES (@TorneoId, @FechaHoraInicio, @Jugador1Id, @Jugador2Id, 'Pendiente');
            SELECT LAST_INSERT_ID();";

            return connection.ExecuteScalar<int>(query, juego);
        }

        public IEnumerable<Juego> ObtenerJuegosPorTorneo(int torneoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM Juegos WHERE TorneoId = @TorneoId";
            return connection.Query<Juego>(query, new { TorneoId = torneoId });
        }
        public Juego ObtenerJuegoPorId(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM Juegos WHERE id = @id";
            var juego = connection.QueryFirstOrDefault<Juego>(query, new { id = id});
            if (juego == null)
                throw new InvalidOperationException($"No se encontró el juego con ID {id}.");

            return juego;
        }
        public bool ActualizarJuego(Juego juego)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
            UPDATE Juegos 
            SET FechaHoraInicio = @FechaHoraInicio, 
                FechaHoraFin = @FechaHoraFin, 
                Estado = @Estado 
            WHERE Id = @Id";

            return connection.Execute(query, juego) > 0;
        }
        public bool AsignarGanador(int juegoId, int ganadorId, DateTime fechaHoraFin)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
            UPDATE Juegos 
            SET GanadorId = @GanadorId, Estado = 'Finalizado', FechaHoraFin = @FechaHoraFin 
            WHERE Id = @JuegoId";


            int filasAfectadas = connection.Execute(query, new { JuegoId = juegoId, GanadorId = ganadorId, FechaHoraFin = fechaHoraFin });

            return filasAfectadas > 0;
        }
        public bool ModificarEmparejamiento(int juegoId, int nuevoJugador1Id, int nuevoJugador2Id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
                UPDATE Juegos
                SET Jugador1Id = @NuevoJugador1Id, Jugador2Id = @NuevoJugador2Id
                WHERE Id = @JuegoId AND Estado = 'Pendiente'";

            int filasAfectadas = connection.Execute(query, new { JuegoId = juegoId, NuevoJugador1Id = nuevoJugador1Id, NuevoJugador2Id = nuevoJugador2Id });

            return filasAfectadas > 0;
        }

        public IEnumerable<Usuario> ObtenerGanadoresPorTorneo(int torneoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
        SELECT u.* FROM Usuarios u
        JOIN Juegos j ON u.Id = j.GanadorId
        WHERE j.TorneoId = @TorneoId AND j.GanadorId IS NOT NULL";

            return connection.Query<Usuario>(query, new { TorneoId = torneoId });
        }
        public IEnumerable<Juego> ObtenerJuegosPendientesPorTorneo(int torneoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
        SELECT * FROM Juegos
        WHERE TorneoId = @TorneoId AND Estado = 'Pendiente'";

            return connection.Query<Juego>(query, new { TorneoId = torneoId });
        }
        public int ObtenerJuegoIdPorJugador(int torneoId, int jugadorId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
                SELECT Id FROM Juegos 
                WHERE TorneoId = @TorneoId 
                AND (Jugador1Id = @JugadorId OR Jugador2Id = @JugadorId)
                AND Estado = 'Pendiente'
                LIMIT 1";

            return connection.ExecuteScalar<int>(query, new { TorneoId = torneoId, JugadorId = jugadorId });
        }
        public bool IntercambiarJugador(int juegoId, int nuevoJugadorId, int jugadorAnteriorId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
                UPDATE Juegos 
                SET Jugador1Id = CASE WHEN Jugador1Id = @JugadorAnteriorId THEN @NuevoJugadorId ELSE Jugador1Id END,
                    Jugador2Id = CASE WHEN Jugador2Id = @JugadorAnteriorId THEN @NuevoJugadorId ELSE Jugador2Id END
                WHERE Id = @JuegoId";

            int filasAfectadas = connection.Execute(query, new { JuegoId = juegoId, NuevoJugadorId = nuevoJugadorId, JugadorAnteriorId = jugadorAnteriorId });

            return filasAfectadas > 0;
        }

    }

}
