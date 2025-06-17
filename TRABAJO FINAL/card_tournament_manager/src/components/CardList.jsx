import { GridTable } from "./GridTable";
import Checkbox from "@mui/material/Checkbox";

export default function CardListItem({ cards, isSelected, onSelect }) {
	const columns = [
		{
			field: "seleccionar",
			headerName: "Seleccionar",
			width: 150,
			sortable: false,
			filterable: false,
			renderCell: (params) => (
				<Checkbox
					// params.row es el objeto de la fila actual (una carta)
					checked={params.row.isSelected} // Usar el estado de selección de la fila
					onChange={() => onSelect(params.row.id, !params.row.isSelected)} // Notificar al padre
				/>
			),
		},
		{ field: "nombre", headerName: "Nombre de la carta", flex: 1 },
		{ field: "ataque", headerName: "Ataque", flex: 1 },
		{ field: "defensa", headerName: "Defensa", flex: 1 },
	];
	//Mapeo las cartas para añadir isSelected necesario para renderCell
	const rowsWithSelectionState = cards.map((card) => ({
		...card, // Mantener todas las propiedades originales de la carta
		id: card.id,
		isSelected: isSelected && isSelected.includes(card.id), // Determinar si esta carta específica está seleccionada
	}));

	return (
		<GridTable
			rows={rowsWithSelectionState}
			columns={columns}
			paginationModel={{ page: 0, pageSize: 5 }}
			pageSizeOptions={[5, 10]}
			sx={{ height: 400, width: "100%" }}
		/>
	);
}
