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
    Nombre VARCHAR(255) NOT NULL,
    Ataque INT NOT NULL,
    Defensa INT NOT NULL,
    Ilustracion TEXT NOT NULL
);

CREATE TABLE Series (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    FechaLanzamiento DATE
);
/*Relación muchos a muchos entre Series y Cartas*/
/*ON DELETE CASCADE ayuda a eliminar toda la referencia si se elimina el dato de la tabla primaria 
ya que luego carece de sentido*/
CREATE TABLE CartasSeries (
	 CartaId INT NOT NULL,
    SerieId INT NOT NULL,
    PRIMARY KEY (CartaId, SerieId),
    FOREIGN KEY (CartaId) REFERENCES Cartas(Id) ON DELETE CASCADE,
    FOREIGN KEY (SerieId) REFERENCES Series(Id) ON DELETE CASCADE
);

CREATE TABLE Mazos (
   Id INT AUTO_INCREMENT PRIMARY KEY,
    JugadorId INT NOT NULL,
    Nombre VARCHAR(255) NOT NULL,
    FOREIGN KEY (JugadorId) REFERENCES Usuarios(Id) ON DELETE CASCADE

);

/*Relación muchos a muchos entre Mazos y Cartas*/
CREATE TABLE MazosCartas (
  MazoId INT NOT NULL,
    CartaId INT NOT NULL,
    PRIMARY KEY (MazoId, CartaId),
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
CREATE TABLE TorneoJuez (
    TorneoId INT NOT NULL,
    JuezId INT NOT NULL,
    FOREIGN KEY (TorneoId) REFERENCES Torneos(Id) ON DELETE CASCADE,
    FOREIGN KEY (JuezId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    PRIMARY KEY (TorneoId, JuezId)
);

/*Define cada juego dentro de un torneo.*/
CREATE TABLE Juegos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TorneoId INT NOT NULL,
    FechaHoraInicio DATETIME NOT NULL,
    FechaHoraFin DATETIME NULL,
    Jugador1Id INT NOT NULL,
    Jugador2Id INT NOT NULL,
    GanadorId INT NULL,
    Estado ENUM('Pendiente', 'En Juego', 'Finalizado') NOT NULL DEFAULT 'Pendiente',
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

/* Relacion torneo mazo, para asignar mazos de los jugadores a los torneos */
CREATE TABLE TorneoMazo (
    TorneoId INT NOT NULL,
    JugadorId INT NOT NULL,
    MazoId INT NOT NULL,
    FOREIGN KEY (TorneoId) REFERENCES Torneos(Id) ON DELETE CASCADE,
    FOREIGN KEY (JugadorId) REFERENCES Usuarios(Id) ON DELETE CASCADE,
    FOREIGN KEY (MazoId) REFERENCES Mazos(Id) ON DELETE CASCADE,
    PRIMARY KEY (TorneoId, JugadorId)
);

/*Relacion torneo series para poder indicar series permitidas*/
CREATE TABLE TorneosSeries (
    TorneoId INT NOT NULL,
    SerieId INT NOT NULL,
    PRIMARY KEY (TorneoId, SerieId),
    FOREIGN KEY (TorneoId) REFERENCES Torneos(Id) ON DELETE CASCADE,
    FOREIGN KEY (SerieId) REFERENCES Series(Id) ON DELETE CASCADE
);


INSERT INTO Torneo_Jugadores (torneoID, JugadorId, MazoId) VALUES (3,17,1)

SELECT * from Torneos_Jugadores 




SELECT * FROM usuarios WHERE rol= "Organizador"
SELECT * FROM torneos WHERE ID=11
SELECT * FROM mazos 


SELECT * FROM torneos WHERE Id = 11 AND Eliminado = FALSE


UPDATE torneos 
SET Fase = 'Registro' 
WHERE Id = 7
  AND Fase != 'Finalizado' 
  AND Eliminado = FALSE;


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


SELECT * FROM torneo_jugadores WHERE torneoid = 11
SELECT * FROM torneomazo WHERE torneoid = 11

SELECT * FROM juegos WHERE torneoid = 11

SELECT COUNT(*) FROM Torneo_Jugadores WHERE TorneoId = 11 AND JugadorId = 27

SELECT * FROM series WHERE id=1
SELECT * FROM cartas
SELECT * FROM mazos WHERE id=9

/*Que cartas tiene un mazo*/
SELECT c.Id AS CartaId, c.Nombre 
        FROM MazosCartas mc
        JOIN Cartas c ON mc.CartaId = c.Id
        WHERE mc.MazoId = 9

/*Que series tiene un mazo*/
SELECT c.Id AS CartaId, c.Nombre, cs.SerieId
        FROM MazosCartas mc
        JOIN Cartas c ON mc.CartaId = c.Id
        JOIN CartasSeries cs ON c.Id = cs.CartaId
        WHERE mc.MazoId =9

/*Ver que series estan permitidas en torneo*/
SELECT SerieId 
        FROM TorneosSeries 
        WHERE TorneoId = 11