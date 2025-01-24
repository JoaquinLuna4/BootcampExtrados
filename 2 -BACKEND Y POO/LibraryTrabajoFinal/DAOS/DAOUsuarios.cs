using MySqlConnector;
using LibraryTrabajoFinal.Entidades;
using Dapper;
using System.Data.Common;


namespace LibraryTrabajoFinal.DAOS
{
    public class DAOUsuarios : IDAOUsuarios
    {
        private readonly string _connectionString;

        public DAOUsuarios(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int CreateUser(Usuario user)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
        INSERT INTO usuarios (Nombre, Apellido, Alias, Email, Password, Pais, Rol, FechaRegistro)
        VALUES (@Nombre, @Apellido, @Alias, @Email, @Password, @Pais, @Rol, @Fecha_Registro);
        SELECT LAST_INSERT_ID();";

            return connection.ExecuteScalar<int>(query, user);
        }
        public IEnumerable<Usuario> GetAllUsers()
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM USUARIOS ";
            return connection.Query<Usuario>(query);
        }

        public Usuario? GetUserByAlias(string alias)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM usuarios WHERE alias= @alias";
            return connection.QueryFirstOrDefault<Usuario>(query, new { Alias= alias });
        }

        public bool UpdateUserByID(Usuario user)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
            UPDATE usuarios 
            SET 
            Nombre = @Nombre,
            Apellido = @Apellido,
            Alias = @Alias,
            Email = @Email,
            Pais = @Pais,
            Rol = @Rol,
            FechaRegistro = @FechaRegistro
            WHERE Id = @Id";
            return connection.Execute(query, user) > 0;
        }
        public bool DeleteUserByID(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "DELETE FROM usuarios WHERE id= @id";
            return connection.Execute(query, new { Id = id })>0;
        }

    }
}
