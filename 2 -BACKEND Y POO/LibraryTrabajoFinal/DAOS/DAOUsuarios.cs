using MySqlConnector;
using LibraryTrabajoFinal.Entidades;
using Dapper;
using System.Data.Common;
using LibraryTrabajoFinal.DTOS;


namespace LibraryTrabajoFinal.DAOS
{
    public class DAOUsuarios : IDAOUsuarios
    {
        private readonly string _connectionString;

        public DAOUsuarios(string connectionString)
        {
            _connectionString = connectionString;
        }

        /* ------------------ UTILIDADES  ------------------ */


        public int CountUserByRole(UserRole rol)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT COUNT(*) FROM usuarios WHERE Rol = @Rol";
            return connection.ExecuteScalar<int>(query, new { Rol = rol.ToString() });
        }
        public IEnumerable<Usuario> GetAllUsers()
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM USUARIOS WHERE activo = 1 ";
            return connection.Query<Usuario>(query);
        }

        public Usuario? GetUserByAlias(string alias)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM usuarios WHERE alias= @alias";
            return connection.QueryFirstOrDefault<Usuario>(query, new { Alias= alias});
        }
        public Usuario? GetUserById(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "SELECT * FROM usuarios WHERE Id= @id";
            return connection.QueryFirstOrDefault<Usuario>(query, new { Id = id });
        }

        /* ------------------ CRUD DEL DAO ------------------ */

        public int CreateUser(Usuario user)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = @"
        INSERT INTO usuarios (Nombre, Apellido, Alias, Email, Password, Pais, Rol, FechaRegistro, id_creador)
        VALUES (@Nombre, @Apellido, @Alias, @Email, @Password, @Pais, @Rol, @Fecha_Registro, @IdCreador);
        SELECT LAST_INSERT_ID();";


            var parametros = new
            {
                user.Nombre,
                user.Apellido,
                user.Alias,
                user.Email,
                user.Password,
                user.Pais,
                Rol = Enum.GetName(typeof(UserRole), user.Rol),  //  Conversión a string para MySQL ENUM
                user.Fecha_Registro,
                user.IdCreador
            };

            return connection.ExecuteScalar<int>(query, parametros);
        }
        


        public bool UpdateUserByID(Usuario user )
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
            Rol = @RolString,
            FechaRegistro = @FechaRegistro,
            id_creador = @IdCreador
            WHERE Id = @Id";

            var parameters = new
            {
                user.Id, 
                user.Nombre,
                user.Apellido,
                user.Alias,
                user.Email,
                user.Password,
                user.Pais,
                RolString = Enum.GetName(typeof(UserRole), user.Rol),  //  Conversión a string para MySQL ENUM
                user.Fecha_Registro,
                user.IdCreador
            };


            return connection.Execute(query, parameters) > 0;
        }
        public bool DeleteUserByID(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            const string query = "UPDATE usuarios SET activo = FALSE WHERE id = @id"; // Borrado lógico
            return connection.Execute(query, new { Id = id }) > 0;
        }

    }
}
