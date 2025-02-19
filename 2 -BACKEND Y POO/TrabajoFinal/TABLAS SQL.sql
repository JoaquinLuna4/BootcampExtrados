CREATE TABLE Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100),
    Alias VARCHAR(50) UNIQUE NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Pais VARCHAR(50),
    Rol ENUM('Administrador', 'Organizador', 'Juez', 'Jugador') NOT NULL,
    FechaRegistro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
/*Sumarle id creador a la tabla usuarios (puede ser null con los jugadores) */

/*Agregamos id creador para no dropear toda la tabla*/
ADD FOREIGN KEY (id_creador) REFERENCES Usuarios(Id); 
ALTER TABLE Usuarios
ADD COLUMN id_creador INT NULL,
/*Implementamos borrado logico para no dropear toda la tabla*/
ALTER TABLE Usuarios
ADD COLUMN activo BOOLEAN DEFAULT TRUE;



CREATE TABLE Cartas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Ataque INT,
    Defensa INT,
    Ilustracion VARCHAR(255),
    FechaCreacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Series (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    FechaLanzamiento DATE
);
/*Relación muchos a muchos entre Series y Cartas*/
/*ON DELETE CASCADE ayuda a eliminar toda la referencia si se elimina el dato de la tabla primaria 
ya que luego carece de sentido*/
CREATE TABLE Series_Cartas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    SerieId INT,
    CartaId INT,
    FOREIGN KEY (SerieId) REFERENCES Series(Id) ON DELETE CASCADE,
    FOREIGN KEY (CartaId) REFERENCES Cartas(Id) ON DELETE CASCADE
);

CREATE TABLE Mazos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    JugadorId INT,
    TorneoId INT,
    FOREIGN KEY (JugadorId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (TorneoId) REFERENCES Torneos(Id) ON DELETE CASCADE
);

/*Relación muchos a muchos entre Mazos y Cartas*/
CREATE TABLE Mazos_Cartas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    MazoId INT,
    CartaId INT,
    FOREIGN KEY (MazoId) REFERENCES Mazos(Id) ON DELETE CASCADE,
    FOREIGN KEY (CartaId) REFERENCES Cartas(Id) ON DELETE CASCADE
);


CREATE TABLE Torneos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    OrganizadorId INT,
    FechaInicio DATETIME,
    FechaFin DATETIME,
    Fase ENUM('Registro', 'Torneo', 'Finalización') DEFAULT 'Registro',
    Pais VARCHAR(50),
    Eliminado BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (OrganizadorId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);


/*Relación entre torneos y jueces*/
CREATE TABLE Torneos_Jueces (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TorneoId INT,
    JuezId INT,
    FOREIGN KEY (TorneoId) REFERENCES Torneos(Id) ON DELETE CASCADE,
    FOREIGN KEY (JuezId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);

/*Define cada juego dentro de un torneo.*/
CREATE TABLE Juegos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TorneoId INT,
    FechaHoraInicio DATETIME,
    FechaHoraFin DATETIME,
    Jugador1Id INT,
    Jugador2Id INT,
    GanadorId INT,
    FOREIGN KEY (TorneoId) REFERENCES Torneos(Id) ON DELETE CASCADE,
    FOREIGN KEY (Jugador1Id) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (Jugador2Id) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (GanadorId) REFERENCES Usuarios(Id) ON DELETE SET NULL
);

/*(Relación entre jugadores y cartas que poseen)*/
CREATE TABLE Jugadores_Cartas (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    JugadorId INT,
    CartaId INT,
    FOREIGN KEY (JugadorId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (CartaId) REFERENCES Cartas(Id) ON DELETE CASCADE
);

/*Relacion entre torneo y jugadores, jugadores pueden estar en muchos torneos 
con diferentes mazos*/
CREATE TABLE Torneo_Jugadores (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TorneoId INT NOT NULL,
    JugadorId INT NOT NULL,
    MazoId INT NOT NULL, -- Hace referencia al mazo con el que el jugador participa
    FOREIGN KEY (TorneoId) REFERENCES Torneos(Id) ON DELETE CASCADE,
    FOREIGN KEY (JugadorId) REFERENCES Usuarios(Id) ON DELETE CASCADE
);


INSERT INTO Torneo_Jugadores (torneoID, JugadorId, MazoId) VALUES (3,17,1)

SELECT * from Torneos_Jugadores 


INSERT INTO Usuarios (Nombre, Apellido, Alias, Email, Pais, Rol, FechaRegistro)
VALUES ('Joaquin', 'Luna', 'JoacoLuna4', 'joacoluna@mail.com', 'Argentina', 'Administrador', '1112025');
SELECT LAST_INSERT_ID();

SELECT * FROM usuarios WHERE rol= "Jugador"
SELECT * FROM torneos WHERE ID= 2
SELECT * FROM mazos 


SELECT * FROM torneos WHERE Id = 3 AND Eliminado = FALSE
SHOW CREATE TABLE torneos;
ALTER TABLE torneos MODIFY COLUMN Fase ENUM('Registro', 'Torneo', 'Finalizacion') NOT NULL;
SHOW CREATE TABLE torneos;

UPDATE torneos 
SET Fase = 'Registro' 
WHERE Id = 4 
  AND Fase != 'Finalizacion' 
  AND Eliminado = FALSE;

ALTER TABLE torneos 
MODIFY COLUMN Fase VARCHAR(20) NOT NULL;



SELECT Id, Nombre, Fase, Eliminado FROM torneos WHERE Id = 4;
ALTER TABLE torneos MODIFY COLUMN Eliminado BOOLEAN DEFAULT FALSE;
UPDATE torneos 
SET Fase = 'Torneo' 
WHERE Id = 4 AND Fase != 'Finalizacion' AND Eliminado = FALSE;
SELECT COUNT(*) FROM Torneos WHERE Id = 4 AND Eliminado = FALSE
SHOW COLUMNS FROM torneos LIKE 'Fase';


INSERT INTO mazos (1, 3
DESCRIBE usuarios
ALTER TABLE usuarios ADD COLUMN Password VARCHAR(255) NOT NULL;


INSERT INTO torneos (Nombre, OrganizadorId, FechaInicio, FechaFin, Fase, Pais)
VALUES ("Torneo de Primavera", 15, "2024-09-10T10:00:00", "2024-09-15T18:00:00", "Registro","España");
SELECT LAST_INSERT_ID();
