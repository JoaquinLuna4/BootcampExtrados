# Ejercicio: Controladores y API REST con ASP.NET Core

## Descripción General

Este ejercicio se enfoca en la creación de una **API Web RESTful** utilizando **ASP.NET Core**. El objetivo principal es construir una capa de servicio que permita gestionar la información de usuarios, interactuando con una base de datos y aplicando lógica de negocio y validaciones específicas. Se hará énfasis en la arquitectura de controladores y la integración con la capa de acceso a datos (DAO).

## Consigna

Generar una **API Web REST de ASP.NET Core** que cumpla con los siguientes requisitos:

1.  **Funcionalidades CRUD:**
    * **Guardar (Crear):** Permitir la creación de nuevos usuarios.
    * **Obtener (Leer):** Permitir la recuperación de información de usuarios. Esto incluye:
        * Obtener todos los usuarios.
        * Buscar y obtener un usuario específico por su **`email`**.
    * **Actualizar:** Permitir la modificación de la información de usuarios existentes.

2.  **Modelo de Usuario:**
    * Cada usuario debe tener las siguientes propiedades: `email`, `nombre`, `edad`.
    * El `email` debe ser la clave única para buscar usuarios.

3.  **Persistencia de Datos:**
    * Los datos de los usuarios deben ser guardados en una **base de datos**.

4.  **Validaciones de Negocio (a nivel de API):**
    * La API debe controlar que la `edad` del usuario sea **mayor a 14 años (`edad > 14`)**.
    * La API debe controlar que el `email` proporcionado sea una dirección de **Gmail** (se puede usar `string.Contains("@gmail.com")` para esta verificación).

5.  **Capa de Acceso a Datos (DAO):**
    * Todos los DAOs (Data Access Objects) que interactúen con la base de datos deben ser implementados como **Singletons**.
    * Estos DAOs deben residir en su **propia biblioteca de clases separada** para mantener una arquitectura modular y limpia.

---