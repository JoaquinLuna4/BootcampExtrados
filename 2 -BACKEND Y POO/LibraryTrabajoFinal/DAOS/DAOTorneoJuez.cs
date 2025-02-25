using Dapper;
using LibraryTrabajoFinal.EntidadesRelaciones;
using MySqlConnector;

namespace LibraryTrabajoFinal.DAOS
{
    public class DAOTorneoJuez : IDAOTorneoJuez
    {
        private readonly string _connectionString;

        public DAOTorneoJuez(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AsignarJuezATorneo(int torneoId, int juezId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
                INSERT INTO TorneoJuez (TorneoId, JuezId) 
                VALUES (@TorneoId, @JuezId);";

            int filasAfectadas = connection.Execute(query, new { TorneoId = torneoId, JuezId = juezId });
            return filasAfectadas > 0;
        }

        public bool EliminarJuezDeTorneo(int torneoId, int juezId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "DELETE FROM TorneoJuez WHERE TorneoId = @TorneoId AND JuezId = @JuezId;";

            int filasAfectadas = connection.Execute(query, new { TorneoId = torneoId, JuezId = juezId });
            return filasAfectadas > 0;
        }

        public IEnumerable<TorneoJuez> ObtenerJuecesPorTorneo(int torneoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM TorneoJuez WHERE TorneoId = @TorneoId;";
            return connection.Query<TorneoJuez>(query, new { TorneoId = torneoId });
        }
    }
}
