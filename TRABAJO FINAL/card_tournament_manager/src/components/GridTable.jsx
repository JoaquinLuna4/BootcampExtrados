import Paper from "@mui/material/Paper";
import { DataGrid } from "@mui/x-data-grid/DataGrid";
export const GridTable = ({
	rows,
	columns,
	paginationModel,
	pageSizeOptions,
	sx,
}) => {
	console.log(columns, "this columns en datagrid");

	return (
		<Paper sx={{ height: "80%", width: "100%" }}>
			<DataGrid
				rows={rows}
				columns={columns}
				initialState={{ pagination: { paginationModel } }}
				pageSizeOptions={pageSizeOptions}
				sx={{ border: 0, ...sx }}
				//checkboxSelection //no loimplementé finalmente, lo hice aparte
			/>
		</Paper>
	);
};
