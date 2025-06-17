# Ejercicio: Autenticación y Autorización (JWT) y Gestión de Préstamos

## Descripción General

Este ejercicio amplía la API Web RESTful de autenticación y autorización basada en JWT, incorporando funcionalidades de gestión de préstamos de libros. Se enfoca en la protección de endpoints, la validación de roles de usuario, la configuración externa de parámetros sensibles y la aplicación de principios de inyección de dependencias para una arquitectura más robusta y mantenible.

## Consigna

Crea una API Web que incluya los siguientes endpoints y funcionalidades:

1.  **Endpoint de Registro de Usuario (`/api/auth/register`)**
    * Permite a un nuevo usuario registrarse en el sistema.
    * Almacena la contraseña del usuario de forma segura, utilizando **hashing** (ej., BCrypt, Argon2, SHA256 con salt) antes de guardarla en la base de datos.

2.  **Endpoint de Login de Usuario (`/api/auth/login`)**
    * Recibe las credenciales (nombre de usuario/email y contraseña) del usuario.
    * **Valida** las credenciales proporcionadas contra las almacenadas en la base de datos (comparando la contraseña hasheada).
    * Si las credenciales son válidas, genera y devuelve un **JSON Web Token (JWT)** al cliente.
    * Además del token, la respuesta debe incluir un **objeto con la información básica del usuario** (ej., ID, nombre de usuario, rol, etc.).

3.  **Endpoint Protegido para Obtener Información de Usuario (`/api/users/{atributo}`)**
    * Este endpoint debe permitir obtener la información de un usuario (por ejemplo, buscándolo por un atributo como su `ID`, `username` o `email`).
    * **CRÍTICO**: Este endpoint **solo debe poder ser llamado por un usuario que esté previamente logueado** (autenticado). Esto se logrará validando el JWT que el cliente envíe en cada solicitud a este endpoint.

4.  **Nuevo Endpoint: Pedir un Libro (`/api/books/borrow`)**
    * Permite a un usuario solicitar el préstamo de un libro.
    * **Entrada**: Este endpoint debe recibir:
        * Una `fecha/hora` en la que se le entrega el libro al usuario.
        * El `nombre del libro` que se está prestando.
        * El `id del usuario` al que se le está prestando el libro.
    * **Fecha de Vencimiento**: El endpoint debe calcular y devolver una fecha de vencimiento del alquiler del libro, que será **5 días después de la fecha de préstamo**.
    * **Validación de Usuario**: El endpoint debe recibir un token de usuario con un `id` y debe **revisar que el `id` del usuario que está haciendo la solicitud (del token) y el `id` del usuario al que se le está dando el préstamo sean el mismo**.
    * **Autorización por Rol**: Este endpoint **solo debe poder ser accedido por usuarios con el rol `"usuario"`**.

## Consideraciones Clave para la Implementación

* **Gestión de Contraseñas Seguras**: La importancia del hashing y salting para el almacenamiento de contraseñas.
* **Generación y Validación de JWT**: Entender cómo se crea un JWT (secret key, claims, expiration) y cómo se valida en el servidor.
* **Manejo de Roles**: Implementar un sistema de roles (`"usuario"`, etc.) que se incluya en los claims del JWT para controlar el acceso a endpoints específicos.
* **Manejo de Errores**: La API debe manejar adecuadamente los errores (ej., credenciales inválidas, token inválido/expirado, acceso no autorizado, validaciones de negocio fallidas).
* **Separación de Responsabilidades**: Mantener la lógica de autenticación y autorización separada de la lógica de negocio principal.
* **Configuración de CORS (Cross-Origin Resource Sharing)**: Implementar la configuración adecuada de CORS para permitir que clientes de diferentes orígenes puedan consumir la API de forma segura.
* **Gestión Centralizada de Configuración**:
    * Mover la **clave secreta del JWT** al archivo `appsettings.json` y consumirla desde allí, tanto en la configuración de la aplicación (`Program.cs` o `Startup.cs`) como en el controlador donde se genere o valide el token.
    * Mover el **connection string de la base de datos** al archivo `appsettings.json` y consumirlo desde allí.
* **Inyección de Dependencias (DI)**: Aplicar el patrón de Inyección de Dependencias para obtener y utilizar los DAOs en los controladores, promoviendo un código más modular, testeable y mantenible.

---