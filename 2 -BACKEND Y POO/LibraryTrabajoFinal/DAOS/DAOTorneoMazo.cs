using Dapper;
using LibraryTrabajoFinal.DAOS;
using LibraryTrabajoFinal.EntidadesRelaciones;
using Microsoft.Extensions.Logging;
using MySqlConnector;

namespace LibraryTrabajoFinal.DAOS
{
    public class DAOTorneoMazo : IDAOTorneoMazo
    {
        private readonly string _connectionString;

private readonly ILogger<DAOTorneoMazo> _logger;
        public DAOTorneoMazo(string connectionString, ILogger<DAOTorneoMazo> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public bool AsignarMazoATorneo(int torneoId, int jugadorId, int mazoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
                INSERT INTO TorneoMazo (TorneoId, JugadorId, MazoId)
                VALUES (@TorneoId, @JugadorId, @MazoId)";

            int filasAfectadas = connection.Execute(query, new { TorneoId = torneoId, JugadorId = jugadorId, MazoId = mazoId });
            return filasAfectadas > 0;
        }

        public bool MazoEsValidoParaTorneo(int torneoId, int mazoId)
        {
            using var connection = new MySqlConnection(_connectionString);

            _logger.LogInformation("Verificando si el mazo {MazoId} es válido para el torneo {TorneoId}", mazoId, torneoId);
            const string query = @"
                SELECT COUNT(DISTINCT mc.CartaId)  
                FROM MazosCartas mc
                JOIN CartasSeries cs ON mc.CartaId = cs.CartaId
                JOIN TorneosSeries ts ON cs.SerieId = ts.SerieId
                WHERE mc.MazoId = @MazoId AND ts.TorneoId = @TorneoId";

           

            int cantidadCartasValidas = connection.ExecuteScalar<int>(query, new { MazoId = mazoId, TorneoId = torneoId });
            
            _logger.LogInformation("Cartas válidas encontradas en el mazo: {CantidadCartasValidas}", cantidadCartasValidas);



            bool esValido = cantidadCartasValidas == 15; // Todas las cartas del mazo deben pertenecer a las series permitidas en el torneo

            _logger.LogInformation("El mazo {MazoId} {EsValido} es válido para el torneo {TorneoId}", mazoId, esValido ? "SÍ" : "NO", torneoId);

            return esValido;
        }


        /*Permite saber que cartas tiene un mazo*/
        public IEnumerable<(int CartaId, string Nombre)> ObtenerCartasDeMazo(int mazoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
                SELECT c.Id AS CartaId, c.Nombre 
                FROM MazosCartas mc
                JOIN Cartas c ON mc.CartaId = c.Id
                WHERE mc.MazoId = @MazoId";

            return connection.Query<(int, string)>(query, new { MazoId = mazoId });
        }


        public bool JugadorPoseeMazo(int jugadorId, int mazoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT COUNT(*) FROM Mazos WHERE Id = @MazoId AND JugadorId = @JugadorId";
            int count = connection.ExecuteScalar<int>(query, new { MazoId = mazoId, JugadorId = jugadorId });
            return count > 0;
        }

        
    }
}
