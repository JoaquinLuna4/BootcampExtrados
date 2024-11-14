
CREATE TABLE personas
(
	id INTEGER PRIMARY KEY,
	nombre VARCHAR(20) NOT NULL,
	email VARCHAR(50) NOT NULL
);

CREATE TABLE empleados
(
	id INTEGER PRIMARY KEY AUTO_INCREMENT,
	posicion VARCHAR(30),
	id_persona INTEGER NOT NULL,
	salario FLOAT NOT NULL,
	fecha_baja DATETIME NULL,
	CHECK (posicion IN ('adm','op','tr')),
	FOREIGN KEY (id_persona) REFERENCES personas(id)
);

INSERT INTO personas VALUE (100, 'Jorge' , "jorge@mail.com");
INSERT INTO personas VALUE (101, 'Lucas' , "lucas@mail.com");
INSERT INTO personas VALUE (102, 'Mario' , "mario@mail.com");
INSERT INTO personas VALUE (103, 'Maria' , "maria@mail.com");
INSERT INTO personas VALUE (104, 'Eugenia' , "eugenia@mail.com");
INSERT INTO personas VALUE (105, 'Santiago' , "santiago@mail.com");
INSERT INTO personas VALUE (106, 'Sandra' , "sandra@mail.com");
INSERT INTO personas VALUE (107, 'Laura' , "laura@mail.com");

INSERT INTO empleados (posicion, id_persona, salario, fecha_baja) VALUE ( 'adm', 100, 1500, NULL);
INSERT INTO empleados (posicion, id_persona, salario, fecha_baja)VALUE ('op', 104, 5500, '20251202');
INSERT INTO empleados (posicion, id_persona, salario, fecha_baja)VALUE ( 'op', 103, 2500, '20240103');
INSERT INTO empleados (posicion, id_persona, salario, fecha_baja)VALUE ('adm', 102, 6000, '20250314');
INSERT INTO empleados (posicion, id_persona, salario, fecha_baja)VALUE ('tr', 101, 4500, '20270707');



/* - PROCEDIMIENTO CONSIGNA °1 - */

DROP PROCEDURE contratar_empleados
CALL  contratar_empleados()
DELIMITER $$

CREATE PROCEDURE contratar_empleados()
BEGIN
  DECLARE v_salario INT DEFAULT 3000;
  DECLARE v_id_persona INT;
  DECLARE flag INT DEFAULT FALSE;
  DECLARE cur CURSOR FOR
    SELECT p.id 															/*Selecciono todas las personas por id*/
    FROM personas p
    LEFT JOIN empleados e ON p.id = e.id_persona 				/*Join con tabla empleados*/
    WHERE e.id IS NULL;													/*que no tienen id empleado*/
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET flag = TRUE;

  OPEN cur;

  loopy: LOOP
    FETCH cur INTO v_id_persona;
    IF flag THEN
      LEAVE loopy;
    END IF;

    INSERT INTO empleados (id_persona, salario, posicion)
    VALUES (v_id_persona, v_salario, 'OP'); 					/*todos con la posicion de "op" y el valor actual de v_salario*/

    SET v_salario = v_salario + 200; 							/*Sumamos $200 a cada nuevo empleado*/
  END LOOP;

  CLOSE cur; 
END $$

DELIMITER ;




/* - PROCEDIMIENTO CONSIGNA °2 - */

DROP PROCEDURE contar_empleados
DELIMITER $$

CREATE PROCEDURE contar_empleados(IN valor_a_superar INT)
BEGIN
  DECLARE suma INT DEFAULT 0;
  DECLARE contador INT DEFAULT 0;
  DECLARE valor INT;
  DECLARE flag BOOLEAN DEFAULT FALSE;  -- Inicializa flag como FALSE

  DECLARE cur CURSOR FOR 
  							SELECT e.salario
  							FROM empleados e
  							ORDER BY e.salario DESC;

  DECLARE CONTINUE HANDLER FOR NOT FOUND SET flag = TRUE;

  OPEN cur;

  read_loop: LOOP
    FETCH cur INTO valor;
    IF flag THEN
      LEAVE read_loop;
    END IF;
    SET suma = suma + valor;
    SET contador = contador + 1;
    IF suma >= valor_a_superar THEN
      LEAVE read_loop;
    END IF;
  END LOOP;

  CLOSE cur;
  SELECT contador AS "Nro de empleados necesarios" ;
END $$

DELIMITER ;

CALL contar_empleados(18000)


