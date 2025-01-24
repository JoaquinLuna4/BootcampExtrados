using Dapper;
using LibraryTrabajoFinal.Entidades;
using MySqlConnector;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data;


namespace LibraryTrabajoFinal.DAOS
{
    public class DAOlibros : IDAOLibros
    {
        private readonly string _connectionString;

        public DAOlibros(string connectionString)
        {
            _connectionString = connectionString;
        }


        public int PedirLibro(string nombrelibro)
        {
            using var connection = new MySqlConnection(_connectionString);
            string query = "UPDATE libros SET Disponible = 0 WHERE Nombre=@nombrelibro";
            int rowsAffected = connection.Execute(query, new { nombrelibro });
            return rowsAffected;  
        }

        public int ObtenerIdLibro(string nombreLibro)
        {
            using var connection = new MySqlConnection(_connectionString);
            string query = "SELECT id FROM libros WHERE Nombre=@nombrelibro";
            return connection.QueryFirstOrDefault<int>(query, new { nombreLibro });
        }

        public int DevolverLibro(string nombrelibro)
        {
            using var connection = new MySqlConnection(_connectionString);
            string query = "UPDATE libros SET Disponible = 1 WHERE Nombre=@nombrelibro";
            var result = connection.QueryFirstOrDefault<int>(query, new { nombrelibro });
            return result > 0 ? result : 0;  // Si no se afecta ninguna fila, retorna 0
        }
        public int AsignarLibro(int idSolicitante, DateTime fechaVencimiento, DateTime fechaPrestamo, int idLibro)
        {
            using var connection = new MySqlConnection(_connectionString);
            string query = "INSERT INTO Prestamos (UsuarioId, LibroId, FechaPrestamo, FechaVencimiento) " +
                            "VALUES (@idSolicitante, @idLibro, @fechaPrestamo ,@fechaVencimiento)";
            return connection.Execute(query, new
            {
                idSolicitante,
                fechaVencimiento,
                fechaPrestamo,
                idLibro
            });
        }
    }
}
