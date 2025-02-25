using MySqlConnector;
using LibraryTrabajoFinal.Entidades;
using LibraryTrabajoFinal.DTOS;
using Dapper;
using Microsoft.Extensions.Logging;


namespace LibraryTrabajoFinal.DAOS
{
    public class TorneoDAO : ITorneoDAO
    {
        private readonly string _connectionString;
      
        public TorneoDAO(string connectionString)
        {
            _connectionString = connectionString;
           
        }

        public int CrearTorneo(Torneo torneo)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
        INSERT INTO torneos (Nombre, OrganizadorId, FechaInicio, FechaFin, Fase, Pais)
        VALUES (@Nombre, @OrganizadorId, @FechaInicio, @FechaFin, @Fase, @Pais);
        SELECT LAST_INSERT_ID();";

            return connection.ExecuteScalar<int>(query, new
            {
                torneo.Nombre,
                torneo.OrganizadorId,
                torneo.FechaInicio,
                torneo.FechaFin,
                torneo.Fase,
                torneo.Pais
                
            });
        }
        public Torneo? ObtenerTorneoPorId(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM torneos WHERE Id = @Id AND Eliminado = FALSE";
            return connection.QueryFirstOrDefault<Torneo>(query, new { Id = id });
        }

        public string ObtenerFaseTorneoPorId(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT Fase FROM torneos WHERE Id = @id AND Eliminado = FALSE";
            
            var fase = connection.QueryFirstOrDefault<string>(query, new { Id = id });
            if (fase == null)
                throw new InvalidOperationException($"No se encontró la fase del torneo con ID {id}.");
            return fase;

        }

        public bool AvanzarFase(int torneoId, string nuevaFase)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
        UPDATE torneos 
        SET Fase = @NuevaFase 
        WHERE Id = @TorneoId AND Fase != 'Finalizacion' AND Eliminado = FALSE";

            var filasAfectadas = connection.Execute(query, new
            {
                NuevaFase = nuevaFase, // Ya es string
                TorneoId = torneoId
            });

            return filasAfectadas > 0;
        }

        public bool TorneoExiste(int torneoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT COUNT(*) FROM Torneos WHERE Id = @TorneoId AND Eliminado = FALSE";
            var count = connection.ExecuteScalar<int>(query, new { TorneoId = torneoId });
            return count > 0;
        }
        public IEnumerable<Torneo> ObtenerTodosLosTorneos()
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM torneos WHERE Eliminado = FALSE";
            return connection.Query<Torneo>(query);
        }
        public bool ActualizarTorneo(Torneo torneo)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
                                    UPDATE torneos
                                    SET Nombre = @Nombre,
                                        FechaInicio = @FechaInicio,
                                        FechaFin = @FechaFin,
                                        Pais = @Pais,
                                        Fase = @Fase
                                    WHERE Id = @Id";

            int filasAfectadas = connection.Execute(query, new
            {
                torneo.Id,
                torneo.Nombre,
                torneo.FechaInicio,
                torneo.FechaFin,
                torneo.Pais,
                Fase = torneo.Fase.ToString()
            });

            return filasAfectadas > 0;
        }
        public bool FinalizarTorneo(int torneoId, int ganadorId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
                UPDATE Torneos 
                SET Fase = 'Finalizacion', GanadorId = @GanadorId
                WHERE Id = @TorneoId";

            int filasAfectadas = connection.Execute(query, new { TorneoId = torneoId, GanadorId = ganadorId });

            return filasAfectadas > 0;
        }
        public bool EliminarTorneo(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "UPDATE torneos SET Eliminado = TRUE WHERE Id = @Id";

            int filasAfectadas = connection.Execute(query, new { Id = id });
            return filasAfectadas > 0;
        }
    }

}
