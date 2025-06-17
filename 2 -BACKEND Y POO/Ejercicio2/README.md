# Ejercicio 2: CRUD con DAO y Acceso a Datos

## Descripción General

Este ejercicio se enfoca en la implementación de un patrón **DAO (Data Access Object)** para interactuar con una base de datos simple. El objetivo es construir una capa de acceso a datos robusta que permita realizar operaciones CRUD (Crear, Leer, Actualizar, Borrar) sobre una tabla de usuarios, prestando especial atención a la seguridad, la gestión de recursos y la lógica de borrado.

## Consigna

1.  **Base de Datos y Tabla:**
    * Crea una base de datos con una única tabla llamada **`Usuarios`**.
    * La tabla `Usuarios` debe contener las siguientes columnas: `Id`, `Nombre`, y `Edad`.

2.  **Funciones de Interacción:**
    * Implementa una función que recupere la lista completa de usuarios de la tabla `Usuarios` y la muestre por pantalla.
    * Implementa una función que busque y recupere la información de un usuario específico a partir de su `Id`.

3.  **Implementación del DAO (Data Access Object):**
    * Crea una **biblioteca de clases separada** que contenga un DAO específico para la tabla `Usuarios`.
    * Este DAO debe permitir realizar las operaciones **CRUD** completas:
        * **C**reate (Crear): Insertar nuevos usuarios.
        * **R**ead (Leer): Recuperar usuarios (ej. por Id o todos).
        * **U**pdate (Actualizar): Modificar la información de usuarios existentes.
        * **D**elete (Borrar): Eliminar usuarios.

## Consideraciones Clave para la Implementación

* **Protección contra SQL Injection**: Asegúrate de que todas las operaciones que involucren parámetros de entrada estén implementadas de forma segura para prevenir ataques de inyección SQL (por ejemplo, usando consultas parametrizadas).
* **Gestión de Conexiones (`using`)**: Utiliza bloques `using` para manejar las conexiones a la base de datos, garantizando que se cierren y liberen correctamente, incluso si ocurren errores.
* **Borrado Lógico**: La operación de borrado (`Delete`) debe ser **lógica**, es decir, no eliminar físicamente el registro de la base de datos, sino marcarlo como inactivo o "borrado" (por ejemplo, añadiendo una columna `activo` o `fecha_baja`).
* **Consumo del DAO**: El DAO implementado en la biblioteca de clases debe ser consumido y utilizado desde el archivo principal del programa (`Program.cs`) para demostrar su funcionalidad.

---