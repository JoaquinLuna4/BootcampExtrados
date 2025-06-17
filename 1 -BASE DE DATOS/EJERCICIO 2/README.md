# Ejercicio 2: Procedimientos Almacenados y Lógica de Negocio

## Descripción General

Este ejercicio se centra en la creación de procedimientos almacenados (Stored Procedures) en SQL para automatizar tareas de manipulación de datos y la implementación de lógica de negocio compleja directamente en la base de datos.

## Consignas

### 1. Carga Masiva de Nuevos Empleados

Crea un procedimiento almacenado que realice lo siguiente:

* **Identifique** a todas las personas de la tabla `personas` que **NO** sean actualmente `empleados`.
* **Cargue** a estas personas como nuevos `empleados` en la tabla correspondiente.
* **Asigne** un salario base inicial de `3000` al primer nuevo empleado.
* **Incremente** el salario en `200` por cada empleado adicional que se cargue (es decir, el segundo empleado tendrá `3200`, el tercero `3400`, y así sucesivamente).
* **Asigne** la posición de `"op"` (operador) a todos estos nuevos empleados.

**Ejemplo de salarios para no-empleados:**

* José -> 3000
* Samanta -> 3200
* Erika -> 3400

**Notas importantes para la implementación:**

* Por ser un ejercicio didáctico, se debe ejecutar una sentencia `INSERT INTO...` **por cada una** de las personas que no son empleados.
* Será muy útil entender y aplicar el concepto de `LEFT JOIN` para identificar a las personas que no son empleados.

### 2. Cálculo de Empleados Necesarios para Superar un Salario Objetivo

Crea un procedimiento almacenado que realice lo siguiente:

* Reciba un parámetro de entrada de tipo `INTEGER` llamado `"valor_a_superar"`.
* Seleccione a los empleados, ordenándolos de **mayor a menor** por sueldo.
* Determine cuántos empleados, siguiendo este orden, son necesarios para que la **sumatoria de sus sueldos sea mayor** al `"valor_a_superar"` proporcionado.

**Ejemplo:**

Si `"valor_a_superar"` = `10000` y los sueldos ordenados son:

* emp\_a: 5500
* emp\_b: 4000
* emp\_c: 3500
* emp\_d: 3000
* emp\_e: 3000

El **resultado final** debería ser `3`. Esto se debe a que:
* `5500 + 4000 = 9500` (menor que 10000)
* `5500 + 4000 + 3500 = 13000` (mayor que 10000)

**Notas importantes para la implementación:**

* Este procedimiento implicará una lógica iterativa (similar a un bucle `FOR`) que sumará los sueldos hasta que se alcance o se supere el `valor_a_superar`, contando cuántos empleados fueron necesarios. El resultado final debe ser el contador de empleados.