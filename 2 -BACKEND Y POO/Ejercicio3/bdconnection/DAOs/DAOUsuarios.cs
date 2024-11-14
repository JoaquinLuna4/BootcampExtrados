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
            string query = "SELECT * FROM USUARIOS";
            return connection.Query<Usuarios>(query);
        }


        public Usuarios? GetUserByAge(int edad)
        {
            string query = "SELECT * FROM USUARIOS WHERE EDAD = @edad";
            using (var connection = new MySqlConnection(conn))
            {
                var usuario = connection.QueryFirstOrDefault<Usuarios>(query, new { edad = edad });
                return usuario;
            }
        }
    }
}
