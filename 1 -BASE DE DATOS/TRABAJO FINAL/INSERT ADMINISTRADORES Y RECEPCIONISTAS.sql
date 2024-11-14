/*---------------------------admin y recepcionistas-------------------*/

INSERT INTO Administrador (dni_admin)
VALUES (123456789);
INSERT INTO Administrador (dni_admin)
VALUES (567890124);


INSERT INTO Recepcionista (dni_recepcionista, id_creador)
VALUES (
    987654321,
    123456789
);

INSERT INTO Recepcionista (dni_recepcionista, id_creador)
VALUES (
    987654322,
    567890124
);

INSERT INTO Recepcionista (dni_recepcionista, id_creador)
VALUES (
    123456780,
    567890124
);

INSERT INTO Recepcionista (dni_recepcionista, id_creador)
VALUES (
    40587713,
    123456789
);

SELECT * FROM administrador 
WHERE dni_admin = 567890124

987654322 recepc