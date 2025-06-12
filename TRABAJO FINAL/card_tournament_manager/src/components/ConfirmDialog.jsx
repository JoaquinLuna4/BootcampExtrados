import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogContentText from "@mui/material/DialogContentText";
import DialogTitle from "@mui/material/DialogTitle";
import Button from "@mui/material/Button";

export const ConfirmDialog = ({ open, title, message, onConfirm, onClose }) => {
	return (
		<Dialog
			open={open}
			onClose={onClose} // Permite cerrar el diálogo haciendo clic fuera o con Escape
			aria-labelledby="confirm-dialog-title"
			aria-describedby="confirm-dialog-description"
		>
			<DialogTitle id="confirm-dialog-title">{title}</DialogTitle>
			<DialogContent>
				<DialogContentText id="confirm-dialog-description">
					{message}
				</DialogContentText>
			</DialogContent>
			<DialogActions>
				<Button onClick={onClose} color="primary">
					Cancelar
				</Button>
				<Button onClick={onConfirm} color="secondary" autoFocus>
					Confirmar
				</Button>
			</DialogActions>
		</Dialog>
	);
};
