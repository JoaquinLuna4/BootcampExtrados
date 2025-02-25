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
        
    }

}
