# Ejercicio: Subqueries (SQL)

## Descripción General

Este ejercicio se enfoca en la práctica de `subqueries` (subconsultas) en SQL, utilizando un conjunto de tablas predefinidas para simular una base de datos de recursos humanos. El objetivo principal es extraer información específica de la base de datos aplicando diversas técnicas de subconsultas.

**Importante:** Se recomienda leer todo el documento antes de comenzar a implementar las soluciones.

## Tablas del Ejercicio

Las tablas necesarias para este ejercicio se encuentran en la carpeta de este ejercicio. Deberás escribir el código SQL para crear estas tablas y cargar los datos correspondientes en tu base de datos.

## Consignas

Dadas las tablas, se deben realizar las siguientes consultas:

1.  **Obtener todas las locaciones de los Estados Unidos (ID de país “US”).**
2.  **Obtener todos los departamentos de los Estados Unidos.**
3.  **Obtener todos los managers que reporten a un departamento de los Estados Unidos.**
4.  **Obtener todos los usuarios que reporten a un manager que reporte a un departamento de los Estados Unidos.**

## Criterios de Evaluación

Además de la correcta ejecución de las consultas, se evaluará:

* **Creación de `VARCHARS` con tamaño adecuado:** Asegurarse de que los tipos de datos `VARCHAR` definidos para las columnas tengan una longitud apropiada para los datos que almacenarán.

## Notas Importantes

* **Archivos de Datos:** Descarga los archivos de datos (CSV, etc.) desde la ubicación proporcionada (Google Drive o similar) en lugar de intentar utilizarlos directamente desde allí para mayor comodidad y estabilidad.
* **Fechas:** No utilices el tipo de dato `DATETIME`. Trata las fechas como `VARCHAR`.
* **ID de Manager:** El `id` de manager en la tabla de empleados hace referencia al `id` de usuario de la misma tabla (es una relación autorreferencial).
* **Correcciones de Datos:**
    * En la tabla de `departamentos`, los `manager_id` con valor `0` deberían ser `NULL`.
    * En la tabla de `empleados`, el `id` del manager de Steven (ID 100) debería ser `NULL`.
* **Automatización de `INSERT`:** Considera utilizar JavaScript o alguna otra herramienta para automatizar la generación de las sentencias `INSERT` a partir de los datos, facilitando el proceso de carga de datos en la base de datos.

---