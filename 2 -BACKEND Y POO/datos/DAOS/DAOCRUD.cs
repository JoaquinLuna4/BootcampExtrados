using MySqlConnector;
using LibraryTrabajoFinal.Entidades;
using Dapper;
using System.ComponentModel.Design;
using System.Text.RegularExpressions;
using MySqlConnector.Logging;

namespace LibraryTrabajoFinal.DAOS
{
    public class DAOCRUD : IDaoCRUD
    {
        private readonly string _connectionString;

        public DAOCRUD(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int SignUp(string nombre, string email, string pass, string role)
        {
            using var connection = new MySqlConnection(_connectionString);
            string query = "INSERT INTO Usuarios (Nombre, Email, Pass, Role) VALUE (@nombre , @email, @pass, @role)";
            return connection.Execute(query, new { nombre, email, pass, role });
        }

        public UsuariosCRUD? GetAllUser(string nombre)
        {
            using var connection = new MySqlConnection(_connectionString);
            string query = "SELECT * FROM USUARIOS WHERE nombre = @nombre";
            return connection.QueryFirstOrDefault<UsuariosCRUD>(query, new { nombre });
        }
        public UsuariosCRUD? SearchUser(string nombre)
        {
            using var connection = new MySqlConnection(_connectionString);
            //if (id != null)
            //{
            //    string query = "SELECT nombre, email, role, id FROM USUARIOS WHERE nombre = @id";
            //    return connection.QueryFirstOrDefault<UsuariosCRUD>(query, new { nombre });

            //}
            //else if (nombre != null)
            //{
                string query = "SELECT nombre, email, role, id FROM USUARIOS WHERE nombre = @nombre";
                return connection.QueryFirstOrDefault<UsuariosCRUD>(query, new { nombre });
            //}
            //else {
            //    Console.WriteLine("falta informacion para la busqueda");
            //    return null;
            //        }
            //;
            
        }
        public int UpdateUserByName(string nombre, string email)
        {
            using var connection = new MySqlConnection(_connectionString);
            string query = "UPDATE usuarios SET email = @email WHERE nombre = @nombre";
            return connection.Execute(query, new { nombre, email });
        }
        public int DeleteUserByName(string nombre)
        {
            using var connection = new MySqlConnection(_connectionString);
            string query = "DELETE FROM usuarios WHERE nombre= @nombre";
            return connection.Execute(query, new { nombre });
        }

     
    }
}
