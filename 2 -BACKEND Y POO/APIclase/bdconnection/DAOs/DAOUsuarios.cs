using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIclase.bdconnection.Entidades;
using Dapper;
using MySqlConnector;
using System.Reflection.Metadata.Ecma335;


namespace APIclase.bdconnection.DAOs
{
    public class DAOUsuarios
    {
        public string conn = "Server = 127.0.0.1 ; Database = ejerciciobackend; UId = root; Password = 030419Fl; Allow User Variables=true;";
        /*Permite el uso de conn en ambos metodos*/
        public IEnumerable<Usuarios> GetAllUsers()
        {
            using var connection = new MySqlConnection(conn);
            string query = "SELECT * FROM USUARIOS";
            return connection.Query<Usuarios>(query);
        }
        public Usuarios? GetUserByEmail(string email)
        {
            using var connection = new MySqlConnection(conn);
            string query = "SELECT * FROM USUARIOS WHERE Email = @email";
            return connection.QueryFirstOrDefault<Usuarios>(query, new { email });
        }
        /*METODOS CREATE / UPDATE */
        public void CreateUser(int id, string nombre, string email)
        {
            using var connection = new MySqlConnection(conn);
            string query = "INSERT INTO Usuarios VALUE (@id, @nombre, @email)";
            connection.Execute(query, new { id, nombre, email });
        }
        public void UpdateUserByID(int id, string nombre, int email)
        {
            using var connection = new MySqlConnection(conn);
            string query = "UPDATE Usuarios SET Nombre = @nombre, Email=@email WHERE id=@id ";
            connection.Execute(query, new { id, nombre, email });

        }
    }
}

