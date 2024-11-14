-- Trigger para la tabla Recepcionista
DELIMITER $$
CREATE TRIGGER log_creacion_recepcionista
AFTER INSERT ON Recepcionista
FOR EACH ROW
BEGIN
  INSERT INTO logs (id_usuario, mensaje)
  VALUES (NEW.id_creador, CONCAT('Se creó un nuevo recepcionista: ', NEW.dni_recepcionista),' por el admin ', NEW.id_creador);
END $$
DELIMITER ;

-- Trigger para la tabla Clientes
DELIMITER $$
CREATE TRIGGER log_creacion_cliente
AFTER INSERT ON Clientes
FOR EACH ROW
BEGIN
  INSERT INTO logs (id_usuario, mensaje)
  VALUES (NEW.id_creador, CONCAT('Se creó un nuevo cliente: ', NEW.dni_cliente, ' por el recepcionista ', NEW.id_creador));
END $$
DELIMITER ;



