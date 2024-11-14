using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ejercicio3.bdconnection.Entidades;
using Dapper;
using MySqlConnector;
using System.Reflection.Metadata.Ecma335;


namespace Ejercicio3.bdconnection.DAOs
{
    public class DAOUsuarios
    {
        public string conn = "Server = 127.0.0.1 ; Database = ejerciciobackend; UId = root; Password = 030419Fl;";
        /*Permite el uso de conn en ambos metodos*/
        public IEnumerable<Usuarios> GetAllUsers()
        {
            using var connection = new MySqlConnection(conn);
            string query = "SELECT * FROM USUARIOS WHERE IsDelete=False";
            return connection.Query<Usuarios>(query);
        }


        public Usuarios? GetUserByAge(int edad)
        {
            string query = "SELECT * FROM USUARIOS WHERE IsDelete=False AND EDAD = @edad";
            using (var connection = new MySqlConnection(conn))
            {
                var usuario = connection.QueryFirstOrDefault<Usuarios>(query, new { edad = edad });
                return usuario;
            }
        }
        /*METODOS CREATE / UPDATE / DELETE */
        public void CreateUser(int id, string nombre, int edad)
        {
            using var connection = new MySqlConnection(conn);
            string query = "INSERT INTO Usuarios VALUE (@id, @nombre, @edad, isDelete=True)";
            connection.Execute(query, new { id, nombre, edad});
        }
        public void UpdateUserByID(int id, string nombre, int edad, bool isDelete)
        {
            using var connection = new MySqlConnection(conn);
            string query = "UPDATE Usuarios SET Nombre = @nombre, Edad=@edad , isDelete=@isDelete WHERE id=@id ";
            connection.Execute(query, new { id, nombre, edad, isDelete });
        }
        public void DeleteUserByID(int id)
        {
            using var connection = new MySqlConnection(conn);
            string query = "UPDATE Usuarios SET IsDelete = 1 WHERE Id = @id";
            connection.Execute(query, new { id});
        }
    }
}
