using Ejercicio3.bdconnection.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ejercicio3
{
    class Program
    {
        static void Main(string[] args)
        {

            // Mostrar todos los usuarios
            var daoUsuarios = new bdconnection.DAOs.DAOUsuarios();

            var usuarios = daoUsuarios.GetAllUsers();
            Console.WriteLine("Lista de Usuarios:");
            foreach (var usuario in usuarios)
            {
                Console.WriteLine($"Id: {usuario.Id}, Nombre: {usuario.Nombre}, Edad: {usuario.Edad}");
            }
            // Buscar y mostrar un usuario por Id
            Console.WriteLine("\n Buscar usuario por Edad:");
            int edad = 35;

            var usuarioPorEdad = daoUsuarios.GetUserByAge(edad);
            if (usuarioPorEdad != null)
            {
                Console.WriteLine($"El usuario con {edad} años es: {usuarioPorEdad.Nombre}");
            }
            else
            {
                Console.WriteLine($"No se encontró usuario con {edad} años");
            }
        }
    }
}