import Snackbar from "@mui/material/Snackbar";
import Grow from "@mui/material/Grow";
import Alert from "@mui/material/Alert";

// Función de transición
function GrowTransition(props) {
	return <Grow {...props} />;
}

export const SnackbarItem = ({
	open, // Booleano: Controla si el Snackbar está visible
	message, // String: El mensaje a mostrar
	severity, // String: 'success', 'info', 'warning', 'error' (valor del alert)
	onClose, // Función: Se llama cuando el Snackbar se cierra (por tiempo o clic)
	autoHideDuration = 3000, // Duración en ms, con un valor por defecto
	anchorOrigin = { vertical: "bottom", horizontal: "center" }, // Posición por defecto
}) => {
	return (
		<Snackbar
			open={open}
			autoHideDuration={autoHideDuration}
			onClose={onClose}
			slots={{ transition: GrowTransition }} // Usamos TransitionComponent en lugar de slots.transition
			anchorOrigin={anchorOrigin}
		>
			<Alert
				onClose={onClose}
				severity={severity} // 'success', 'info', 'warning', 'error'
				variant="filled" // o "standard", "outlined"
				sx={{ width: "100%" }}
			>
				{message}
			</Alert>
		</Snackbar>
	);
};
