using MySqlConnector;
using datos.Entidades;
using Dapper;

namespace datos.DAOS
{
    public class DAOCRUD
    {
        public string conn = "Server = 127.0.0.1 ; Database = ejerciciobackend; UId = root; Password = 030419Fl; Allow User Variables=true;";
        public int SignUp(string nombre, string email, string pass)
        {
            using var connection = new MySqlConnection(conn);
            string query = "INSERT INTO Usuarios (Nombre, Email, Pass) VALUE (@nombre , @email, @pass)";
            return connection.Execute(query, new { nombre, email, pass });
        }

        public UsuariosCRUD? GetAllUser(string nombre)
        {
            using var connection = new MySqlConnection(conn);
            string query = "SELECT * FROM USUARIOS WHERE nombre = @nombre";
            return connection.QueryFirstOrDefault<UsuariosCRUD>(query, new { nombre });
        }
        public UsuariosCRUD? GetUserByNombre(string nombre)
        {
            using var connection = new MySqlConnection(conn);
            string query = "SELECT nombre, email FROM USUARIOS WHERE nombre = @nombre";
            return connection.QueryFirstOrDefault<UsuariosCRUD>(query, new { nombre });
        }
        public int UpdateUserByName(string nombre, string email)
        {
            using var connection = new MySqlConnection(conn);
            string query = "UPDATE usuarios SET email = @email WHERE nombre = @nombre";
            return connection.Execute(query, new { nombre, email });
        }
        public int DeleteUserByName(string nombre)
        {
            using var connection = new MySqlConnection(conn);
            string query = "DELETE FROM usuarios WHERE nombre= @nombre";
            return connection.Execute(query, new { nombre });
        }
    }
}
