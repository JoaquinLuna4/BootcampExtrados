CREATE TABLE Usuarios (
    dni INT			 					PRIMARY KEY,
    nombre VARCHAR(30) 				NOT NULL,
    apellido VARCHAR(30) 			NOT NULL,
    username VARCHAR(15) 			NOT NULL,
    pass VARCHAR(10) 				NOT NULL,
    fecha_creacion VARCHAR(10) 	NOT NULL,
    fecha_baja VARCHAR(10) 		NULL    
    rol VARCHAR(5)					NOT NULL,
    id_creador INT						NULL,
	 FOREIGN KEY (id_creador) REFERENCES Usuarios(dni)
)


CREATE TABLE Administrador (
    dni_admin INT 			PRIMARY KEY,
    FOREIGN KEY (dni_admin) REFERENCES usuarios(dni)
);

CREATE TABLE Recepcionista (
	dni_recepcionista INT   PRIMARY KEY,
	id_creador INT 	 		NOT NULL,
	FOREIGN KEY (id_creador) REFERENCES Administrador(dni_admin)
);

CREATE TABLE Clientes(
	dni_cliente INT   		PRIMARY KEY,
	nombre VARCHAR (30)  	NOT NULL,
	apellido VARCHAR (30)   NOT NULL,
	fecha_alta VARCHAR(30) 	NOT NULL,	
	id_creador INT 	 		NOT NULL,
	FOREIGN KEY (id_creador) REFERENCES Recepcionista(dni_recepcionista)
);

CREATE TABLE Cuartos (
	numero INT 					PRIMARY KEY,
	disponible	BIT 			NOT NULL     
)

CREATE TABLE estacionamiento (
	id_estacionamiento INT  PRIMARY KEY AUTO_INCREMENT,
	numero_lote INT 		 	NOT NULL 
)

CREATE TABLE reservas (
	id_reserva INT 				PRIMARY KEY AUTO_INCREMENT,
	id_cliente INT  				NOT NULL,
	id_cuarto  INT 				NOT NULL,
	fecha_entrada VARCHAR(30)  NOT NULL,
	fecha_salida VARCHAR(30)   NULL,
	tiempo_limpieza INT			NULL,
   id_recepcionista INT 		NULL,
	id_estacionamiento INT		NULL,
	FOREIGN KEY (id_cliente) REFERENCES Clientes(dni_cliente),
	FOREIGN KEY (id_cuarto)  REFERENCES cuartos(numero),
	FOREIGN KEY (id_estacionamiento) REFERENCES Estacionamiento(id_estacionamiento)
)

CREATE TABLE logs (
    id INT PRIMARY KEY AUTO_INCREMENT,
    id_usuario INT,
    mensaje TEXT,
    FOREIGN KEY (id_usuario) REFERENCES Usuarios(dni)
);




INSERT INTO reservas (id_cliente, id_cuarto, fecha_entrada, tipo_evento, tiempo_limpieza, id_recepcionista, id_estacionamiento)
VALUES (567890123, 101, '2024-04-05 10:30:00', 'IN', NULL, 987654321, 2);

SELECT * 
FROM reservas r 
JOIN  estacionamiento e ON e.id_estacionamiento = r.id_estacionamiento
WHERE id_cliente = 567890123


INSERT INTO reservas (id_cliente, id_cuarto, fecha_entrada, fecha_salida, tipo_evento, tiempo_limpieza, id_recepcionista, id_estacionamiento)
VALUES (567890123, 101, '2024-04-05 10:30:00', '2024-04-07 21:30:00', 'OUT', 60, 123456780, 2);




/*-----------La introduccion de la reserva deberia ejecutarse de la siguiente manera---------------*/
START TRANSACTION;

INSERT INTO reservas (id_cliente, id_cuarto, fecha_entrada, fecha_salida, tipo_evento, tiempo_limpieza, id_recepcionista, id_estacionamiento)
VALUES (20804036, 103, '2024-04-07 10:30:00', NULL, 'IN', NULL, 987654322, 1);

CALL actualizar_disponibilidad_cuarto(103, 'IN');

COMMIT;

SELECT * from reservas