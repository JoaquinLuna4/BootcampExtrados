CREATE TABLE Usuarios
(
	ID INT PRIMARY KEY,
	Nombre VARCHAR(20) NOT NULL,
	Email VARCHAR(30) NOT NULL,
	Edad INT NOT NULL
);

INSERT INTO Usuarios VALUE (100, 'Jorge' , 'jorge@jorge.com', 56);
INSERT INTO Usuarios VALUE (101, 'Lucas' , 'lucas@lucas.com', 15);
INSERT INTO Usuarios VALUE (102, 'Mario' , 'mario@mario.com', 17);
INSERT INTO Usuarios VALUE (103, 'Luis' , 'luis@luis.com', 19);
INSERT INTO Usuarios VALUE (104, 'Marcos' , 'marcos@marcos.com', 69);


SELECT * FROM USUARIOS WHERE Email = 'mario@mario.com'

INSERT INTO Usuarios VALUE (105, 'Eugenia', 'eugenia@eugenia.com')

SELECT * FROM USUARIOS WHERE Email = 'marcos@marcos.com'