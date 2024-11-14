CREATE TABLE personas
(
		id INTEGER PRIMARY KEY AUTO_INCREMENT,
		nombre VARCHAR(20) NOT NULL,
		email VARCHAR(50) NOT NULL
)

CREATE TABLE empleados
(
	id INTEGER PRIMARY KEY,
	posicion VARCHAR(30),
	id_personas INTEGER NOT NULL,
	FOREIGN KEY (id_personas) REFERENCES personas(id)
)

INSERT INTO personas (nombre, email) VALUES ("jose","jose@email");
INSERT INTO personas (nombre, email) VALUES ("maria","maria@email");
INSERT INTO personas (nombre, email) VALUES ("jorge","jorge@email");
INSERT INTO personas (nombre, email) VALUES ("eugenia","eugenia@email");


INSERT INTO empleados (id, posicion, id_personas) VALUES (50,"manager",2);

DROP TABLE empleados
CREATE TABLE empleados
(
	id_empleado INTEGER PRIMARY KEY,
	nombre VARCHAR(30) NOT NULL,
	apellido VARCHAR(30)NOT NULL,
	email VARCHAR(15)NOT NULL,
	telefono VARCHAR(20) NOT NULL,
	fecha_en VARCHAR(10)NOT NULL,
	id_trabajo VARCHAR(12) NOT NULL,
	salario INTEGER NOT NULL,
	comision INTEGER NOT NULL,
	ID_MANAGER INTEGER NOT NULL,
	ID_DEPARTAMENTO INTEGER NOT NULL
	
)
DROP TABLE departamentos
CREATE TABLE departamentos(
	ID_DEPARTAMENTO INTEGER PRIMARY KEY,
	NOMBRE VARCHAR(30) NOT NULL,
	ID_MANAGER INTEGER NOT NULL,
   ID_LOCACION INTEGER NOT NULL	
)

DROP TABLE locaciones
CREATE TABLE locaciones(
   ID_LOCACION  INTEGER PRIMARY KEY,
	DIRECCION 	 VARCHAR(45) NULL,
	CODIGO_POSTAL VARCHAR(30) NULL,
	CIUDAD 	    VARCHAR(30) NULL ,
	ESTADO_PROVINCIA VARCHAR(30)NULL,
	ID_PAIS      VARCHAR(4)NULL
) 


INSERT INTO empleados VALUES (
100, 
"Steven" ,
"King" , 
"SKING",
"515.123.4567",
"2003-06-17",
"AD_PRES", 
24000.00 ,
0.00, 
0 , 
90
) 

SET GLOBAL local_infile = 1;


LOAD DATA INFILE 'D:\Empleadocsv.csv'
INTO TABLE empleados
FIELDS TERMINATED BY ',' 
ENCLOSED BY '"' 
LINES TERMINATED BY '\n';

SHOW VARIABLES LIKE "secure_file_priv"


LOAD DATA LOCAL INFILE 'D:\\MySQL\\MySQL Server 9.0\\Uploads\\locaciones.csv' 
INTO TABLE `ejercicio1`.`locaciones` 
FIELDS TERMINATED BY ',' 
OPTIONALLY ENCLOSED BY ',' 
ESCAPED BY ',' 
IGNORE 1 ROWS

(@ID_LOCACION, DIRECCION, CODIGO_POSTAL, CIUDAD, ESTADO_PROVINCIA, ID_PAIS)
SET ID_LOCACION = @ID_LOCACION,
    DIRECCION = DIRECCION,
    CODIGO_POSTAL = CODIGO_POSTAL,
    CIUDAD = CIUDAD,
    ESTADO_PROVINCIA = ESTADO_PROVINCIA,
    ID_PAIS = ID_PAIS;
SHOW WARNINGS;




#-----------PROCEDURES -----------#


PROCEDIMIENTO DE ALMACENADO

CREATE PROCEDURE ObtenerTodo() - > una función
BEGIN

					-> todo lo que esta aca adentro es lo que va a a ejecturar
SELECT * 
FROM personas;
select *
from empleados

END;



para los procedimientos de almacenado vamos a tener que cambiar los delimitadores para que no sean " ; "

DELIMITER **
CREATE PROCEDURE ObtenerTodo() - > una función
	BEGIN
					
		SELECT * 
		FROM personas;				-> todo lo que esta aca adentro es lo que va a a ejecturar
		select *
		from empleados;

	END**



SHOW PROCEDURE  STATUS - > VEO TODOS LOS PROCEDURES CREADOS con su data correspondiente

CALL ObtenerTodo(); -> es para llamar a la funcion o procedimiento


CREATE PROCEDURE obtenerUsuario(
	DIRECCION  - el parametro entra o sale? - id_seleccion - TIPO DE DATO - integer
	IN id_seleccion integer  ----> ingresa informacion
	OUT nombre_seleccion VARCHAR(30) ---> Sale info
)
BEGIN 
 SELECT *
 FROM personas
 WHERE id = id_seleccion;
END


BEGIN
SELECT *
FROM personas
INTO nombre

CALL ObtenerUsuario(1);

DELIMITER $$
CREATE PROCEDURE IF NOT EXISTS ObtenerUsuario(   # ---> LO VA A EJECUTAR, NO VA A TIRAR ERROR
		IN ID_Seleccion INTEGER
		OUT nombre_seleccion VARCHAR(30)
		)
		
BEGIN 
 IF seleccion = 'p' THEN
		SELECT *
		FROM personas;
 ELSE 
 		SELECT *
		FROM empleados
	END IF
END$$


CALL ObtenerUsuario( 1. @nom)
SELECT @nom 


#Declaro variables#

DELIMITER $$
CREATE PROCEDURE ej_loop_simple()
BEGIN 
	DECLARE suma INT DEFAULT 0;
	SET suma = 0 ;
	DECLARE cuenta INT DEFAULT 0;
	
	#nombre: loop para declararlo
	loopy: LOOP
		
		SET suma = suma + cuenta 
		SET cuenta = cuenta -1;
		
		IF cuenta = 0 THEN 
			LEAVE loopy;  #---> esto indica que termina el loop
		
		END IF 
	END LOOP loopy;

END $$















