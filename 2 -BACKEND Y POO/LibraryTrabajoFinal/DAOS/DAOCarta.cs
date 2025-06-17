using MySqlConnector;
using LibraryTrabajoFinal.Entidades;
using Dapper;

namespace LibraryTrabajoFinal.DAOS
{
    public class DAOCarta : IDAOCarta
    {
        private readonly string _connectionString;

        public DAOCarta(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CrearCarta(Carta carta)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
            INSERT INTO Cartas (Nombre, Ataque, Defensa, Ilustracion)
            VALUES (@Nombre, @Ataque, @Defensa, @Ilustracion);
            SELECT LAST_INSERT_ID();";

            int cartaId = connection.ExecuteScalar<int>(query, carta);

            foreach (var serieId in carta.SeriesIds)
            {
                connection.Execute("INSERT INTO CartasSeries (CartaId, SerieId) VALUES (@CartaId, @SerieId);",
                    new { CartaId = cartaId, SerieId = serieId });
            }

            return cartaId;
        }
        public bool CartaExiste(string nombre, int serieId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT COUNT(*) FROM Cartas WHERE Nombre = @Nombre AND SerieId = @SerieId";
            int count = connection.ExecuteScalar<int>(query, new { Nombre = nombre, SerieId = serieId });
            return count > 0;
        }

        public IEnumerable<Carta> getAllCards()
        {
            using var connection = new MySqlConnection(_connectionString);
            return connection.Query<Carta>("SELECT * FROM Cartas");
        }

        public int CrearMazo(Mazo mazo)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
            INSERT INTO Mazos (JugadorId, Nombre)
            VALUES (@JugadorId, @Nombre);
            SELECT LAST_INSERT_ID();";

            int mazoId = connection.ExecuteScalar<int>(query, mazo);

            foreach (var cartaId in mazo.CartasIds)
            {
                connection.Execute("INSERT INTO MazosCartas (MazoId, CartaId) VALUES (@MazoId, @CartaId);",
                    new { MazoId = mazoId, CartaId = cartaId });
            }

            return mazoId;
        }
        public IEnumerable<Mazo> GetDeckbyUser(int JugadorId)
        {
            using var connection = new MySqlConnection(_connectionString);
           
            const string query = "SELECT * FROM Mazos WHERE JugadorId = @JugadorId;";

            return connection.Query<Mazo>(query, new { JugadorId = JugadorId });
        }

        public IEnumerable<Carta> getCardById(int cardId)
        {
            using var connection = new MySqlConnection(_connectionString);

            const string query = "SELECT * FROM Cartas WHERE Id= @cardId;";

            return connection.Query<Carta>(query, new { Id = cardId});
        }
        public IEnumerable<Carta> GetCartasByIds(IEnumerable<int> ids)
        { using var connection = new MySqlConnection(_connectionString);
        const string query = @"
                SELECT * FROM Cartas WHERE Id IN @Ids;"; 
        return connection.Query<Carta>(query, new { Ids = ids });
        }
        public IEnumerable<Carta> GetCartasByMazoId(int mazoId)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
               SELECT
                c.Id,
                c.Nombre,  
                c.Ilustracion,
                c.Ataque,
                c.Defensa  
            FROM Cartas c
            INNER JOIN MazosCartas mc ON c.Id = mc.CartaId
            WHERE mc.MazoId =@mazoId";

            return connection.Query<Carta>(query, new { MazoId = mazoId });
        }
    }

}
