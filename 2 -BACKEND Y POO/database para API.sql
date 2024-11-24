CREATE TABLE Usuarios
(
	ID INT PRIMARY KEY,
	Nombre VARCHAR(20) NOT NULL,
	Email VARCHAR(30) NOT NULL
);

INSERT INTO Usuarios VALUE (100, 'Jorge' , 'jorge@jorge.com');
INSERT INTO Usuarios VALUE (101, 'Lucas' , 'lucas@lucas.com');
INSERT INTO Usuarios VALUE (102, 'Mario' , 'mario@mario.com');
INSERT INTO Usuarios VALUE (103, 'Luis' , 'luis@luis.com');
INSERT INTO Usuarios VALUE (104, 'Marcos' , 'marcos@marcos.com');


SELECT * FROM USUARIOS WHERE Email = 'mario@mario.com'

INSERT INTO Usuarios VALUE (105, 'Eugenia', 'eugenia@eugenia.com')

SELECT * FROM USUARIOS WHERE Email = 'marcos@marcos.com'