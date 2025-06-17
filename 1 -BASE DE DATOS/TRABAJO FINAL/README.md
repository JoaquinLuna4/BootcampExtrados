# Trabajo Final: Sistema de Administración Hotelera

## Descripción General

Este proyecto consiste en el desarrollo de un sistema de administración para un hotel, con un enfoque principal en el diseño y la implementación de su base de datos. La base de datos deberá gestionar la información central del hotel, incluyendo usuarios, clientes, cuartos, estacionamiento y registros operativos clave.

## Tablas Básicas Iniciales

El sistema contará con, al menos, las siguientes tablas básicas:

* **`administrador`**
    * `DNI` (Clave Primaria)
    * `nombre`
    * `nombre_usuario`
    * `contraseña`
    * `fecha_creacion`
    * `fecha_baja` (`NULL` si está activo)
    * **Roles**: Representa a los usuarios con privilegios de administración del sistema.

* **`recepcionista`**
    * `DNI` (Clave Primaria)
    * `nombre`
    * `nombre_usuario`
    * `contraseña`
    * `fecha_alta`
    * `fecha_baja` (`NULL` si está activo)
    * `id_creador` (Referencia al `DNI` del administrador que lo creó)
    * **Roles**: Representa a los usuarios encargados de la operación diaria y atención al cliente.

* **`clientes`**
    * `DNI` (Clave Primaria)
    * `nombre`
    * `fecha_alta`
    * `id_creador` (Referencia al `DNI` del recepcionista que lo registró)

* **`cuartos`**
    * `numero` (Clave Primaria)
    * `disponible` (Indica si el cuarto está libre u ocupado)

* **`estacionamiento`**
    * `numero_lote` (Clave Primaria)

## Requisitos de la Base de Datos

La base de datos deberá ser capaz de registrar y gestionar la siguiente información clave:

1.  **Registro de Entradas y Salidas de Clientes en Cuartos**:
    * Debe registrar las fechas y horas (como `STRING`) de la entrada y salida de cada cliente en un cuarto específico.

2.  **Registro de Acciones de Usuarios (Logs)**:
    * Debe mantener un registro (`logs`) de las acciones realizadas por los administradores y recepcionistas (ej., creación o eliminación de usuarios/clientes, asignación de cuartos, etc.).
    * Esta tabla de logs debería incluir al menos: `id_usuario` (DNI del administrador o recepcionista que realizó la acción) y `mensaje` (descripción de la acción).

3.  **Registro de Tiempos de Limpieza de Cuartos**:
    * Después de cada salida de un cliente de un cuarto, la base de datos debe registrar un tiempo de limpieza.
    * Se debe tener en cuenta el `tiempo de limpieza` que es solicitado por el recepcionista.

4.  **Registro de Estacionamiento de Clientes**:
    * Debe registrar qué lote de estacionamiento utilizó un cliente específico durante su visita a las instalaciones del hotel.

---