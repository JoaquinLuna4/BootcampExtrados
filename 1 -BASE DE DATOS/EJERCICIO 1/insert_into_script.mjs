import fs from "fs";

console.log("hola");

// Nombre del archivo de entrada
const inputFile = "empleados.txt";

// Nombre del archivo de salida
const outputFile = "sentencias_insert.sql";

// Función para crear las sentencias INSERT
function createInsertStatements(data) {
	const lines = data.split("\n");
	let insertStatements = "";

	// Saltar las 3 primeras líneas (encabezados)
	for (let i = 3; i < lines.length; i++) {
		const line = lines[i];
		if (line.trim() !== "" && columns.length > 0) {
			// Verificar si todos los elementos de columns son válidos antes de usar trim()
			if (columns.every((column) => column !== undefined)) {
				const insertStatement = `INSERT INTO empleados VALUES (
        ${columns[0].trim()},
        "${columns[1].trim()}",
        "${columns[2].trim()}",
        "${columns[3].trim()}",
        "${columns[4].trim()}",
        "${columns[5].trim()}",
        "${columns[6].trim()}",
        ${columns[7].trim()},
        ${columns[8].trim()},
        ${columns[9].trim()},
        ${columns[10].trim()}
      );\n`;
				insertStatements += insertStatement;
			} else {
				console.error("Error en la línea:", line);
				console.error("Columnas:", columns);
			}
		}

		return insertStatements;
	}

	// Leer el archivo y procesar los datos
	fs.readFile(inputFile, "utf8", (err, data) => {
		if (err) {
			console.error(err);
		} else {
			const insertStatements = createInsertStatements(data);
			console.log(insertStatements); // Imprime las sentencias en la consola
		}
	});
}
