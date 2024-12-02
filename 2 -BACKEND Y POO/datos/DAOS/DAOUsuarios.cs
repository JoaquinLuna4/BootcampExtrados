using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using datos.Entidades;
using Dapper;
using MySqlConnector;
using System.Reflection.Metadata.Ecma335;


namespace datos.DAOs
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
        //public void CreateUser(int id, string nombre, string email)
        //{
        //    using var connection = new MySqlConnection(conn);
        //    string query = "INSERT INTO Usuarios VALUE (@id, @nombre, @email)";
        //    connection.Execute(query, new { id, nombre, email });
        //}

        public Usuarios CreateUser(int id, string nombre, string email, int edad)
        {
            using var connection = new MySqlConnection(conn);
            string query = "INSERT INTO Usuarios (Id, Nombre, Email, edad) VALUES (@id, @nombre, @email, @edad); SELECT LAST_INSERT_ID();";
           
                int filasAfectadas = connection.Execute(query, new { id, nombre, email, edad });

               return new Usuarios { Id = id, Nombre = nombre, Email = email, Edad = edad };
            
        }   
        public void UpdateUserByID(int id, string nombre, int email,int edad)
        {
            using var connection = new MySqlConnection(conn);
            string query = "UPDATE Usuarios SET Nombre = @nombre, Email=@email, Edad=@edad WHERE id=@id ";
            connection.Execute(query, new { id, nombre, email });

        }
    }
}

