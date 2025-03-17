using Dapper;
using LibraryTrabajoFinal.Entidades;
using MySqlConnector;

namespace LibraryTrabajoFinal.DAOS
{
    public class DAOSeries : IDAOSeries
    {
        private readonly string _connectionString;

        public DAOSeries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CrearSerie(Serie serie)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "INSERT INTO Series (Nombre, FechaLanzamiento) VALUES (@Nombre, @FechaLanzamiento); SELECT LAST_INSERT_ID();";
            return connection.ExecuteScalar<int>(query, serie);
        }

        public Serie? ObtenerSeriePorId(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM Series WHERE Id = @Id";
            return connection.QueryFirstOrDefault<Serie>(query, new { Id = id });
        }

        public IEnumerable<Serie> ObtenerTodasLasSeries()
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM Series";
            return connection.Query<Serie>(query);
        }

        public bool EliminarSerie(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "DELETE FROM Series WHERE Id = @Id";
            int filasAfectadas = connection.Execute(query, new { Id = id });
            return filasAfectadas > 0;
        }
    }
}
