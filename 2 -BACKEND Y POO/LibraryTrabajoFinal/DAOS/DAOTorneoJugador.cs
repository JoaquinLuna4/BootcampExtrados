using Dapper;
using LibraryTrabajoFinal.EntidadesRelaciones;
using MySqlConnector;

namespace LibraryTrabajoFinal.DAOS
{
    public class DAOTorneoJugador:IDAOTorneoJugador
    {
        private readonly string _connectionString;

        public DAOTorneoJugador(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Inscribir a un jugador en un torneo
        public bool InscribirJugador(int torneoId, int jugadorId, int mazoId)
        {
            using var connection = new MySqlConnection(_connectionString);

            // Verificamos que el torneo esté en fase de Registro antes de inscribir
            const string verificarTorneo = "SELECT Fase FROM Torneos WHERE Id = @TorneoId AND Eliminado = FALSE";
            var fase = connection.ExecuteScalar<string>(verificarTorneo, new { TorneoId = torneoId });

            if (fase != "Registro")
            {
                return false; // No se puede inscribir si el torneo ya comenzó
            }

            // Insertar al jugador en el torneo con su mazo
            const string query = @"
        INSERT INTO Torneo_Jugadores (TorneoId, JugadorId, MazoId)
        VALUES (@TorneoId, @JugadorId, @MazoId);";

            int filasAfectadas = connection.Execute(query, new { TorneoId = torneoId, JugadorId = jugadorId, MazoId = mazoId });

            return filasAfectadas > 0;
        }

        // Lista de jugadores inscritos en un torneo
        public IEnumerable<TorneoJugador> ObtenerJugadoresPorTorneo(int torneoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM Torneo_Jugadores WHERE TorneoId = @TorneoId";

            return connection.Query<TorneoJugador>(query, new { TorneoId = torneoId });
        }

        // Desinscribir a un jugador antes del inicio del torneo
        public bool DesinscribirJugador(int torneoId, int jugadorId)
        {
            using var connection = new MySqlConnection(_connectionString);

            
            const string verificarTorneo = "SELECT Fase FROM Torneos WHERE Id = @TorneoId";
            var fase = connection.ExecuteScalar<string>(verificarTorneo, new { TorneoId = torneoId });

            if (fase != "Registro")
            {
                return false; // Check de que sigue en fase de registro
            }

            // Eliminar al jugador del torneo
            const string query = "DELETE FROM Torneo_Jugadores WHERE TorneoId = @TorneoId AND JugadorId = @JugadorId";
            int filasAfectadas = connection.Execute(query, new { TorneoId = torneoId, JugadorId = jugadorId });

            return filasAfectadas > 0;
        }
    }

}
