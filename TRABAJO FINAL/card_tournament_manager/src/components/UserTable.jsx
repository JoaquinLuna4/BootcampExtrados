import Paper from "@mui/material/Paper";
import Typography from "@mui/material/Typography";
import IconButton from "@mui/material/IconButton";
import DeleteIcon from "@mui/icons-material/Delete";
import EditIcon from "@mui/icons-material/Edit";
import { GridTable } from "./GridTable";

// Definimos el modelo de paginación aquí, ya que es específico de la tabla
const paginationModel = { page: 0, pageSize: 5 };

export const UserTable = ({
	rows,
	isLoading,
	error,
	onDeleteUser,
	onEditUser,
}) => {
	const rowsInTable = rows.map((row) => ({ id: row.id, ...row }));
	console.log(rowsInTable);

	const columns = [
		{ field: "id", headerName: "ID", flex: 0.2 },
		{ field: "alias", headerName: "Alias", flex: 1 },
		{ field: "nombre", headerName: "Nombre", flex: 1 },
		{ field: "email", headerName: "Email", flex: 1.6 },
		// { field: "rol", headerName: "Rol", flex: 1 },
		{ field: "pais", headerName: "País", flex: 1 },
		{
			field: "actions",
			headerName: "Acciones",
			width: 120,
			sortable: false,
			filterable: false,
			renderCell: (params) => (
				// Aquí usamos 'params.row' para acceder a los datos de la fila actual
				<>
					<IconButton
						aria-label="edit"
						onClick={() => onEditUser && onEditUser(params.row.id)} // Llama a la prop onEditUser si existe
						color="primary"
					>
						<EditIcon />
					</IconButton>
					<IconButton
						aria-label="delete"
						onClick={() => onDeleteUser && onDeleteUser(params.row.id)} // Llama a la prop onDeleteUser si existe
						color="secondary"
					>
						<DeleteIcon />
					</IconButton>
				</>
			),
		},
	];

	if (isLoading) {
		return <Typography variant="h6">Cargando usuarios... </Typography>;
	}

	if (error) {
		return <Typography color="error">Error: {error}</Typography>;
	}

	return (
		<Paper sx={{ height: "80%", width: "100%" }}>
			<GridTable
				rows={rowsInTable}
				columns={columns}
				paginationModel={paginationModel}
				pageSizeOptions={[5, 10]}
				sx={{ border: 0 }}
			/>
		</Paper>
	);
};
