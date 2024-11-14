CREATE USER Jorge
IDENTIFIED BY '123456'

GRANT SELECT 
ON ejercicio.personas
TO Jorge

REVOKE SELECT
ON ejercicio.personas
FROM Jorge


/*Si le doy permiso a toda la tabla puede visualizar valores que no deberia ver*/
CREATE VIEW empleados_1 AS 
SELECT e.id, e.posicion, e.salario FROM empleados e



DROP VIEW empleados_1
CREATE VIEW empleados_1
WITH CHECK OPTION; /*Esto va a hacer que para que haga alguna modificacion, lo tenga que visualizar*/


CREATE ALGORITHM = MERGE VIEW empleados_sin_admin(ID_EMPLEADOS, NOMBRE_POSICION)  /* Esto devuelve una vista como si 
																												estuvieramos haciendo un AS para poner un alias */
SELECT id, posicion FROM personas

/*Hago una vista que se le permita a Jorge, no le doy acceso a toda la tabla*/

GRANT UPDATE
ON curso2024.empleados_1
TO Jorge

/*Le permito a jorge hacer actualizaciones de la vista, y esas actualizaciones le van a permitir 
modificar tambien la tabla empleados*/


GRANT SELECT, INSERT, UPDATE, DELETE
ON curso2024.usuarios_sin_adm
