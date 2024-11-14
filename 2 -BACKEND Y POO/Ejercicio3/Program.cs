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
            var daoUsuarios = new bdconnection.DAOs.DAOUsuarios();
            
            //Crear Usuario
            
            daoUsuarios.CreateUser(109, "Pablo", 32);

            // Mostrar todos los usuarios activos
          
            var usuarios = daoUsuarios.GetAllUsers();
            Console.WriteLine("Lista de Usuarios:");
            foreach (var usuario in usuarios)
            {
                Console.WriteLine($"Id: {usuario.Id}, Nombre: {usuario.Nombre}, Edad: {usuario.Edad}");
            }
            // Buscar y mostrar un usuario activo por Edad 
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

           // Actualizar un usuario por ID

            daoUsuarios.UpdateUserByID(104, "Marcos", 31, false);

           //Eliminar de manera logica un usuario por ID

            daoUsuarios.DeleteUserByID(104);
        }
    }
}