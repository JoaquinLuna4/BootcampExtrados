/* 3) Vehiculos con los datos del empleado al que pertenece*/
SELECT * FROM empleados_private e 
JOIN vehiculos v ON e.id_empleado = v.id_empleado

/* 4) Lista de personas que no son empleados*/

SELECT * FROM personas 
WHERE dni NOT IN (
		SELECT dni 
		FROM empleados_private
	)
	
/* 5) empleados que hayan venido a trabajar el 10/10/2023 */

SELECT p.nombre, p.apellido, est.fecha_ingreso, est.patente FROM empleados_private e 
	JOIN vehiculos v		    ON e.id_empleado = v.id_empleado 
	JOIN estacionamiento est ON est.patente   = v.patente
	JOIN personas p 			 ON p.DNI         = e.DNI 
WHERE est.fecha_ingreso = '10/10/2023'


/* 6)lista de todos los productos comprados por la persona cuyo dni = 98765432*/

SELECT * FROM ventas v1
	JOIN productos p1 		ON p1.barcode = v1.barcode
	WHERE v1.DNI = 98765432

/* 7) cantidad total de ventas en monto (plata), generada por el vendedor (id_empleado = 2)*/

SELECT SUM(precio) AS Total
	FROM ventas v1
	JOIN productos p1 		ON p1.barcode = v1.barcode
	WHERE v1.id_empleado = 2
	