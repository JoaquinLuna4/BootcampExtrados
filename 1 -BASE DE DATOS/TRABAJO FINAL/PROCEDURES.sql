
/*------PROCEDIMIENTO ALMACENADO PARA ELIMINAR CLIENTES-------*/
DELIMITER $$
CREATE PROCEDURE eliminar_cliente(IN p_dni_cliente INT)
BEGIN
  DELETE FROM Clientes WHERE dni_cliente = p_dni_cliente;
END $$
DELIMITER ;




/*------PROCEDIMIENTO ALMACENADO PARA CAMBIAR ESTADO DE CUARTOS-------*/
DELIMITER $$
CREATE PROCEDURE actualizar_disponibilidad_cuarto(IN p_numero_cuarto INT, IN p_tipo_evento VARCHAR(3))
BEGIN
    IF p_tipo_evento = 'IN' THEN
        UPDATE Cuartos
        SET disponible = 1
        WHERE numero = p_numero_cuarto;
    ELSEIF p_tipo_evento = 'OUT' THEN
        UPDATE Cuartos
        SET disponible = 0
        WHERE numero = p_numero_cuarto;
    END IF;
END $$
DELIMITER ;
