# Backend para Torneo de Cartas Fullstack

Este repositorio contiene el código fuente del backend desarrollado como proyecto final del bootcamp de programación fullstack. El objetivo principal es gestionar la lógica y la persistencia de datos para un torneo de cartas con diferentes roles de usuario.

## Consigna del Proyecto

El proyecto consiste en desarrollar el backend de un torneo de cartas con los siguientes roles de usuario y funcionalidades de creación:

* **Admin:** Puede crear otros administradores, organizadores y jueces.
* **Organizador:** Creado por un administrador, puede crear jueces.
* **Juez:** Creado por un administrador o un organizador.
* **Jugador:** Puede registrarse por sí mismo.

El backend debe gestionar la información de los usuarios, los torneos, las partidas y cualquier otra entidad relevante para el funcionamiento del torneo. Se requiere el uso de las siguientes tecnologías:

* **Backend:** C++ con .NET
* **Base de Datos:** MySQL

## Tecnologías Utilizadas

* **Lenguaje de Programación:** C++ (para la lógica principal, aunque la interacción con .NET se realiza a través de interoperabilidad)
* **.NET Framework/Core:** Para la creación de la API y la gestión de la lógica de la aplicación.
* **MySQL:** Como sistema de gestión de bases de datos para el almacenamiento persistente de la información.
* **Dapper:** Una micro-ORM para simplificar la interacción con la base de datos MySQL.
* **ASP.NET Core Web API:** Para construir la interfaz de la aplicación a través de servicios web.
* **JWT (JSON Web Tokens):** Para la autenticación y autorización de usuarios.
* **Hashing de Contraseñas:** Para almacenar las contraseñas de forma segura.
* **Postman:** Para la prueba y verificación de los endpoints de la API.

## Estructura del Proyecto (Implementación)

El backend está estructurado siguiendo un patrón típico de aplicaciones .NET, con las siguientes capas principales:

* **Controladores (Controllers):** Manejan las peticiones HTTP entrantes, validan los datos y delegan la lógica a los servicios.
* **Servicios (Services):** Contienen la lógica de negocio de la aplicación. Realizan operaciones como la creación, actualización y eliminación de usuarios, la gestión de torneos, etc.
* **Acceso a Datos (DAO - Data Access Objects):** Se encargan de la interacción con la base de datos MySQL. Contienen métodos para realizar operaciones CRUD (Crear, Leer, Actualizar, Eliminar) en las diferentes tablas.
* **Modelos (Models):** Representan las entidades del dominio (por ejemplo, `Usuario`, `Juego`, `Torneo`).
* **DTOs (Data Transfer Objects):** Se utilizan para transferir datos entre el cliente y el servidor, como el `RequestRegister` para la creación de usuarios.
* **Configuración:** Archivos de configuración para la conexión a la base de datos, la configuración de JWT, etc.
* **Autenticación:** Implementación de la autenticación basada en JWT para proteger las rutas de la API y verificar los roles de los usuarios.
* **Testing:** Se utilizaron colecciones de Postman para probar exhaustivamente los diferentes endpoints de la API.

### Implementaciones Destacadas

* **Gestión de Usuarios:**
    * Creación de usuarios con diferentes roles (Admin, Organizador, Juez, Jugador) con las restricciones de creación especificadas en la consigna.
    * Implementación de la columna `id_creador` en la tabla `Usuarios` para rastrear quién creó cada usuario (excepto jugadores).
    * Borrado lógico de usuarios mediante una columna `activo` para evitar la eliminación permanente de datos.
    * Autenticación de usuarios mediante JWT, generando tokens que contienen información sobre el rol y el ID del usuario.
    * Protección de endpoints de la API mediante el atributo `[Authorize]` y la verificación de roles.
* **Acceso a Datos:**
    * Utilización de Dapper para interactuar con la base de datos MySQL de forma eficiente.
    * Implementación de métodos CRUD en los DAOs para las diferentes entidades.
* **Lógica de Negocio:**
    * Implementación de servicios para encapsular la lógica de creación, actualización y eliminación de usuarios, aplicando las reglas de negocio definidas en la consigna.
    * Validación de los datos de entrada en los controladores y servicios.
* **Gestión de Juegos (Ejemplo):**
    * Implementación de una consulta SQL para actualizar los jugadores en un juego (`Jugador1Id`, `Jugador2Id`).
* **Testing de Endpoints:**
    * Se realizó un testeo exhaustivo de los endpoints de la API utilizando Postman.
    * Se crearon y guardaron colecciones de pruebas en Postman para verificar la funcionalidad, los códigos de respuesta y la correcta manipulación de los datos de la API. Estas colecciones pueden ser encontradas en la carpeta `Postman Tests` (o similar) del repositorio.

## Aprendizajes Clave

Durante el desarrollo de este proyecto, se adquirieron y aplicaron los siguientes conocimientos:

* Diseño y arquitectura de un backend para una aplicación web.
* Desarrollo de APIs RESTful con ASP.NET Core Web API.
* Interoperabilidad entre C++ y .NET (aunque el código principal mostrado se centra en .NET).
* Interacción con bases de datos MySQL utilizando Dapper.
* Implementación de autenticación y autorización seguras con JWT.
* Manejo de roles de usuario y permisos.
* Implementación de borrado lógico para la gestión de datos.
* Uso de DTOs para la transferencia de datos.
* Registro y depuración de la aplicación (a través de `Console.WriteLine` como se mostró en el desarrollo).
* **Uso de Postman para el testeo de APIs:**
    * Creación y gestión de colecciones de pruebas.
    * Envío de diferentes tipos de peticiones HTTP (GET, POST, PUT, DELETE).
    * Verificación de códigos de respuesta HTTP.
    * Validación del cuerpo de las respuestas JSON.
    * Uso de variables de entorno en Postman.
    * Guardado y exportación de las colecciones de pruebas para futuras ejecuciones.

## Cómo Ejecutar el Proyecto (Instrucciones Básicas)

1.  **Requisitos Previos:**
    * .NET SDK instalado en tu máquina.
    * MySQL Server instalado y en ejecución.
    * Algún cliente de API (como Postman o Insomnia) para probar los endpoints.
    * **Postman** instalado para ejecutar las pruebas de la API.

2.  **Configuración de la Base de Datos:**
    * Crea una base de datos llamada `trabajofinalextrados` (o el nombre que hayas configurado) en tu servidor MySQL.
    * Ejecuta los scripts SQL (que deberían estar en una carpeta `Database` o similar en el repositorio) para crear las tablas (`Usuarios`, `Juegos`, `Series`, `CartaSeries`, etc.) y sus relaciones.
    * Configura la cadena de conexión a tu base de datos MySQL en el archivo de configuración de la aplicación (`appsettings.json` o similar).

3.  **Configuración de la Autenticación JWT:**
    * Revisa y configura la sección de JWT en tu archivo de configuración (`appsettings.json`), incluyendo la clave secreta, el emisor y la audiencia.

4.  **Ejecución del Backend:**
    * Navega hasta la carpeta del proyecto backend en tu terminal o línea de comandos.
    * Ejecuta el comando: `dotnet run`

5.  **Prueba de la API con Postman:**
    * Importa la colección de pruebas de Postman (ubicada en la carpeta `Postman Tests` o similar) a tu aplicación Postman.
    * Configura las variables de entorno necesarias (por ejemplo, la URL base de la API).
    * Ejecuta las colecciones de pruebas para verificar la funcionalidad de los endpoints.

## Autor

Joaquin Luna

---
