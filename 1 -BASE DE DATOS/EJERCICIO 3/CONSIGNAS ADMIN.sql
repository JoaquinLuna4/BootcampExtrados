/* ---------- consultas CONSIGNAS ---------- */

/* Creando usuario */
CREATE USER Jorge
IDENTIFIED BY '123456'

GRANT SELECT , INSERT, UPDATE, DELETE
ON  vistas_ejercicio.empleados_private
TO Jorge

GRANT SELECT, INSERT, UPDATE, DELETE
ON estacionamiento , productos, vehiculos, ventas, personas
TO Jorge;

/*1) Lista de empleados sin el sueldo*/
CREATE VIEW empleados_private AS
SELECT id_empleado, DNI, Rol, fecha_ingreso FROM empleados

/* 3) Vehiculos con los datos del empleado al que pertenece*/
SELECT * FROM empleados_private e JOIN vehiculos v ON e.id_empleado = v.id_empleado

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
	

	
/* --------------------CREACION DE DATABASE, TABLAS E INSERTS-------------------- */


CREATE DATABASE VISTAS_EJERCICIO
USE VISTAS_EJERCICIO

/*DROP TABLE personas*/
CREATE TABLE personas (
	DNI INT  PRIMARY KEY ,
	nombre VARCHAR(30) NOT NULL   ,
	apellido VARCHAR (30) NOT NULL,
	email VARCHAR (50) NOT NULL
)
/*DROP TABLE empleados*/
CREATE TABLE empleados (
	id_empleado INT PRIMARY KEY,
	DNI INT NOT NULL,
	SUELDO INT  NOT NULL, 
	ROL VARCHAR(10) NOT NULL,
	fecha_ingreso VARCHAR(10) NOT NULL,
	FOREIGN KEY (dni) REFERENCES personas(dni)
)
/*DROP TABLE vehiculos*/
CREATE TABLE vehiculos (
	patente VARCHAR (15) PRIMARY KEY,
	modelo VARCHAR(30) NOT NULL,
	color VARCHAR(30) NOT NULL,
	id_empleado INT NOT NULL,
	FOREIGN KEY (id_empleado) REFERENCES empleados(id_empleado)
)
/*DROP TABLE estacionamiento*/
CREATE TABLE estacionamiento(
	patente VARCHAR (15) NOT NULL,
	fecha_ingreso VARCHAR (10) NOT NULL,
	lote INT ,
	FOREIGN KEY (patente) REFERENCES vehiculos(patente)
)
/*DROP TABLE productos*/
CREATE TABLE productos(
	barcode INT PRIMARY KEY,
	descripcion VARCHAR(30) NOT NULL,
	precio INT	NOT NULL
)
DROP TABLE ventas
CREATE TABLE ventas(
	DNI INT NOT NULL,
	id_empleado INT NOT NULL,
	barcode INT NOT NULL,
	fecha_hora VARCHAR (10) NOT NULL,
	FOREIGN KEY (DNI) REFERENCES personas(DNI),
	FOREIGN KEY (id_empleado) REFERENCES empleados(id_empleado),
	FOREIGN KEY (barcode) REFERENCES productos(barcode)
)

/* ----------------------------INSERTS -----------------------*/

/*PERSONAS*/
INSERT INTO personas VALUES (41736280, "Joaquin", "Luna", "joaquin@mail.com");
INSERT INTO personas VALUES (32587419, "María", "Sol", "maria.sol@gmail.com");
INSERT INTO personas VALUES (29475612, 'Pedro', 'Rivera', 'pedro_rivera@hotmail.com');
INSERT INTO personas VALUES (18364925, 'Ana', 'López', 'ana.lopez@outlook.com');
INSERT INTO personas VALUES (37129856, 'Lucas', "Martínez", "lucas.martinez@yahoo.com");
INSERT INTO personas VALUES (45013789, 'Sofía', 'González', 'sofia_gonzalez@gmail.com');
INSERT INTO personas VALUES (26958347, 'Diego', "Pérez", "diego.perez@hotmail.com");
INSERT INTO personas VALUES (51234567, 'Laura', 'García', 'laura.garcia@gmail.com');
INSERT INTO personas VALUES (89012345, 'David', 'Romero', 'david_romero@hotmail.com');
INSERT INTO personas VALUES (67890123, 'Andrea', 'Fernández', 'andrea.fernandez@outlook.com');
INSERT INTO personas VALUES (43210987, 'Marcos', 'Torres', 'marcos.torres@yahoo.com');
INSERT INTO personas VALUES (98765432, 'Valentina', 'Jiménez', 'valentina.jimenez@gmail.com');
INSERT INTO personas VALUES (23456789, 'Pablo', 'Ruiz', 'pablo_ruiz@hotmail.com');
INSERT INTO personas VALUES (36789111, 'Juan', 'Vergara', 'juan_vergara@hotmail.com');


/*EMPLEADOS*/
INSERT INTO empleados VALUES (1, 41736280, 30000, 'vendedor', '2023-01-01');
INSERT INTO empleados VALUES (2, 32587419, 25000, 'manager', '2022-05-15');
INSERT INTO empleados VALUES (3, 29475612, 28000, 'vendedor', '2021-11-08');
INSERT INTO empleados VALUES (4, 18364925, 22000, 'vendedor', '2023-03-20');
INSERT INTO empleados VALUES (5, 37129856, 32000, 'manager', '2020-09-01');
INSERT INTO empleados VALUES (6, 45013789, 26000, 'vendedor', '2022-07-10');
INSERT INTO empleados VALUES (7, 26958347, 29000, 'manager', '2021-02-15');

/*VEHICULOS*/
INSERT INTO vehiculos VALUES ('KCB383', 'Corsa', 'Gris', 7);
INSERT INTO vehiculos VALUES ('JKL012', 'Golf', 'Azul', 6);
INSERT INTO vehiculos VALUES ('MNO345', 'Fiesta', 'Rojo', 5);
INSERT INTO vehiculos VALUES ('PQR678', 'Megane', 'Blanco', 4);
INSERT INTO vehiculos VALUES ('STU901', 'Toyota Corolla', 'Plata', 3);
INSERT INTO vehiculos VALUES ('VWX234', 'Ford Focus', 'Verde', 2);
INSERT INTO vehiculos VALUES ('YZX567', 'Audi A4', 'Azul', 1);

/*ESTACIONAMIENTO*/
INSERT INTO estacionamiento VALUES ('STU901','8/10/2023', 1);
INSERT INTO estacionamiento VALUES ('MNO345','10/10/2023', 5);
INSERT INTO estacionamiento VALUES ('VWX234','10/10/2023', 4);
INSERT INTO estacionamiento VALUES ('YZX567','10/10/2023', 2);
INSERT INTO estacionamiento VALUES ('YZX567','18/10/2023', 2);

/*PRODUCTOS*/
INSERT INTO productos VALUES ('424468', 'Computadora', 700000);
INSERT INTO productos VALUES ('789123', 'Smartphone', 450000);
INSERT INTO productos VALUES ('567890', 'Tablet', 300000);
INSERT INTO productos VALUES ('123456', 'Televisión', 600000);

/*VENTAS */
INSERT INTO ventas VALUES (51234567, 2, 424468, '13/02/2024');
INSERT INTO ventas VALUES (51234567, 6, 789123, '1/10/2022');
INSERT INTO ventas VALUES (98765432, 4, 567890, '7/12/2021');
INSERT INTO ventas VALUES (98765432, 2, 789123, '17/11/2023');
INSERT INTO ventas VALUES (98765432, 1, 123456, '31/12/2023');

