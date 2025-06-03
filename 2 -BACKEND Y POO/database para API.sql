CREATE TABLE Usuarios
(
	ID INT PRIMARY KEY AUTO_INCREMENT,
	Nombre VARCHAR(20) NOT NULL,
	Email VARCHAR(30) NOT NULL,
	Pass VARCHAR(50) NOT NULL
);


INSERT INTO usuarios (Nombre, Email, Pass) VALUE ('Jorge' , 'jorge@jorge.com', 'pass123');
INSERT INTO Usuarios (Nombre, Email, Pass) VALUE ('Lucas' , 'lucas@lucas.com', 'ladriverni');
INSERT INTO Usuarios VALUE (102, 'Mario' , 'mario@mario.com', 17);
INSERT INTO Usuarios VALUE (103, 'Luis' , 'luis@luis.com', 19);
INSERT INTO Usuarios VALUE (104, 'Marcos' , 'marcos@marcos.com', 69);


SELECT * FROM USUARIOS WHERE Email = 'lucia@lucia.com'

SELECT * FROM usuarios WHERE Nombre='JORge'

INSERT INTO Usuarios VALUE (105, 'Eugenia', 'eugenia@eugenia.com')

UPDATE usuarios
SET email = 'lucas@gmail.com'
WHERE nombre = 'Lucas';
SELECT * FROM USUARIOS WHERE Email = 'marcos@marcos.com'
SELECT nombre, email FROM USUARIOS WHERE nombre = 'Jorge'

SELECT * from libros 
SELECT * from prestamos 
SELECT * from usuarios WHERE Nombre = "Esteban"


SELECT id FROM libros WHERE nombre="Historia de dos ciudades"

UPDATE libros SET Disponible = 0
WHERE Nombre="El señor de los Anillos"

UPDATE libros SET Disponible = 1 WHERE Nombre="El Señor de los Anillos"


CREATE TABLE Libros(
	ID INT PRIMARY KEY,
	Nombre VARCHAR(50) NOT NULL,
	Autor VARCHAR(40) NOT NULL,
	Disponible BIT NOT NULL
);
INSERT INTO Libros VALUE (404, 'Don Quijote de la Mancha' , ' Miguel de Cervantes', 1);
INSERT INTO Libros VALUE (505, 'Historia de dos ciudades' , 'Charles Dickens', 1);
INSERT INTO Libros VALUE (606, 'El Señor de los Anillos' , 'J.R.R. Tolkien', 1);


CREATE TABLE Prestamos(
 IDprestamo INT PRIMARY KEY AUTO_INCREMENT,
 UsuarioId INT NOT NULL,
 LibroId INT NOT NULL,
 FechaPrestamo DATETIME NOT NULL,          -- Fecha/hora del préstamo
 FechaVencimiento DATETIME NOT NULL,       -- Fecha de vencimiento del préstamo
 FechaDevolucion DATETIME NULL,            -- Fecha de devolución (NULL si no ha sido devuelto)
 CONSTRAINT FK_Prestamos_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(ID),
 CONSTRAINT FK_Prestamos_Libros FOREIGN KEY (LibroId) REFERENCES Libros(ID)
)

INSERT INTO Prestamos (UsuarioId, LibroId, FechaPrestamo, FechaVencimiento)
VALUES (16, 404, '2024-12-07 15:30:00', '2024-12-07 15:45:00');