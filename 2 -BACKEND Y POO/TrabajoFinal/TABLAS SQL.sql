CREATE TABLE Usuarios (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100),
    Apellido VARCHAR(100),
    Alias VARCHAR(50) UNIQUE NOT NULL,
    Email VARCHAR(150) UNIQUE NOT NULL,
    Pais VARCHAR(50),
    Rol ENUM('Administrador', 'Organizador', 'Juez', 'Jugador') NOT NULL,
    FechaRegistro TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

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


INSERT INTO Usuarios (Nombre, Apellido, Alias, Email, Pais, Rol, FechaRegistro)
VALUES ('Joaquin', 'Luna', 'JoacoLuna4', 'joacoluna@mail.com', 'Argentina', 'Administrador', '1112025');
SELECT LAST_INSERT_ID();

SELECT * FROM usuarios
DESCRIBE usuarios
ALTER TABLE usuarios ADD COLUMN Password VARCHAR(255) NOT NULL;
