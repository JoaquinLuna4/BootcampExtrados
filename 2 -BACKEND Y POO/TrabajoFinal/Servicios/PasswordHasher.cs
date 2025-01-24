using LibraryTrabajoFinal.DAOS;
using Konscious.Security.Cryptography;
using System.Text;

namespace TrabajoFinal.Servicios
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            byte[] nohash = Encoding.UTF8.GetBytes(password); //Convert string en Byte para ser tipo valido de Argon2id
            var argon2 = new Argon2id(nohash)
            {
                DegreeOfParallelism = 1, // Degree of parallelism specifies how many of these lanes will be used to generate the hash.
                MemorySize = 19456, // Ajusta el consumo de memoria (in KiB) usados para calc el hash
                Iterations = 2  //numero de iteraciones
            }; // Crea una instancia de Argon2id con parámetros personalizados

            // Hashea la contraseña
            var hash = argon2.GetBytes(128);
            return Convert.ToBase64String(hash);
        }
        public bool VerifyPassword(string enteredPassword, string storedHash)
        {
            byte[] storedHashBytes = Convert.FromBase64String(storedHash); // Convertir el hash almacenado de base64 a bytes
            byte[] passwordBytes = Encoding.UTF8.GetBytes(enteredPassword); // Convertir la contraseña a bytes

            var argon2 = new Argon2id(passwordBytes)
            {
                DegreeOfParallelism = 1,
                MemorySize = 19456,
                Iterations = 2
            };

            // Hashear la contraseña ingresada
            byte[] hashedPassword = argon2.GetBytes(128);

            // Comparar el hash generado con el hash almacenado
            return CompareHashes(storedHashBytes, hashedPassword);

        }
        private static bool CompareHashes(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length != hash2.Length)
                return false;

            int difference = 0;
            for (int i = 0; i < hash1.Length; i++)
            {
                difference |= hash1[i] ^ hash2[i];
            }

            return difference == 0;
        }

    }
}
