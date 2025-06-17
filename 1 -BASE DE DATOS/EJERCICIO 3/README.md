# Ejercicio 3: Vistas (SQL)

## Descripción General

Este ejercicio se enfoca en el diseño y la creación de **Vistas** en SQL. Las vistas permiten presentar datos de una o más tablas de manera simplificada y controlada, lo cual es útil para la seguridad, la abstracción de datos y la reutilización de consultas complejas. Además, el ejercicio incluye la gestión de permisos para un nuevo usuario de base de datos, restringiendo su acceso únicamente a estas vistas.

## Tablas Disponibles

Para este ejercicio, contarás con las siguientes tablas:

* **`personas`**: `dni`, `nombre`, `apellido`, `email`
* **`empleados`**: `id_empleado`, `dni`, `sueldo`, `rol` (`"vendedor"` / `"manager"`), `fecha_ingreso` (`VARCHAR`), `fecha_egreso` (`VARCHAR`)
    * **Nota**: `fecha_egreso = NULL` significa que el empleado sigue activo.
* **`vehiculos`**: `id_vehiculo`, `patente`, `modelo`, `color`, `id_empleado`
* **`estacionamiento`**: `patente`, `fecha_ingreso` (`VARCHAR`), `lote`
    * **Nota**: Esta tabla registra quién estacionó dónde cada día.
* **`productos`**: `codigo_barra`, `descripcion`, `precio`
* **`ventas`**: `dni` (de la persona que compró), `id_empleado` (del vendedor), `codigo_barras`, `fecha_hora`

## Consignas

Deberás realizar las siguientes tareas:

1.  **Crear un nuevo usuario para la base de datos** que solo tenga permisos de `SELECT` sobre las vistas que se generen a continuación.

2.  **Generar las siguientes vistas**:

    * **1. `vw_empleados_sin_sueldo`**
        * Una vista que muestre la lista de todos los empleados, **excluyendo la columna `sueldo`**.

    * **2. `vw_empleados_vigentes`**
        * Una vista con la lista de los empleados que aún están activos (cuya `fecha_egreso` es `NULL`), **sin incluir la columna `sueldo`**.

    * **3. `vw_vehiculos_con_datos_empleado`**
        * Una vista que muestre la lista de vehículos junto con los datos del empleado al que pertenecen (deberás realizar un `JOIN` entre `vehiculos` y `empleados` / `personas`), **sin incluir el sueldo del empleado**.

    * **4. `vw_personas_no_empleados`**
        * Una vista que liste todas las personas que no están registradas como empleados.

    * **5. `vw_empleados_presentes_10oct2023`**
        * Una vista con la lista de todos los empleados que se presentaron a trabajar el **10 de Octubre de 2023**.
        * **Criterio de Presencia**: Se considera que un empleado se presentó a trabajar si su vehículo (`patente`) aparece registrado en la tabla `estacionamiento` para esa fecha específica.

    * **6. `vw_productos_comprados_por_dni`**
        * Una vista con la lista de todos los productos comprados por la persona cuyo `dni` es `36789111`.

    * **7. `vw_ventas_total_vendedor_2`**
        * Una vista que muestre la cantidad total de ventas en monto (plata) generada por el vendedor con `id_empleado = 2`.