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

INSERT INTO Administrador (dni_admin)
VALUES (123456789);
INSERT INTO Administrador (dni_admin)
VALUES (567890124);


INSERT INTO Recepcionista (dni_recepcionista, id_creador)
VALUES (
    987654321,
    123456789
);


/* Ejemplo de USER_admin */

INSERT INTO Usuarios (dni, nombre, apellido, username, pass, fecha_creacion, fecha_baja, rol, id_creador)
VALUES (
    987654321,
    'María',
    'González',
    'mGonzalez',
    'PASS456',
    '2023-11-05',
    NULL,
    'ADMIN',
    NULL
);

INSERT INTO Usuarios (dni, nombre, apellido, username, pass, fecha_creacion, fecha_baja, rol, id_creador)
VALUES (
    987654321,
    'María',
    'González',
    'mGonzalez',
    'PASS456',
    '2023-11-05',
    NULL,
    'RECEPCIONISTA',
    123456789
);

/*Con respecto a reservas*/

/*-----------La introduccion de la reserva deberia ejecutarse de la siguiente manera---------------*/
START TRANSACTION;

INSERT INTO reservas (id_cliente, id_cuarto, fecha_entrada, fecha_salida, tipo_evento, tiempo_limpieza, id_recepcionista, id_estacionamiento)
VALUES (20804036, 103, '2024-04-07 10:30:00', NULL, 'IN', NULL, 987654322, 1);

CALL actualizar_disponibilidad_cuarto(103, 'IN');

COMMIT;

SELECT * from reservas