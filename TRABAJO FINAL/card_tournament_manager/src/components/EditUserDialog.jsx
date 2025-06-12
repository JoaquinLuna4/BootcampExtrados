import React, { useState, useEffect } from "react";
import Dialog from "@mui/material/Dialog";
import DialogActions from "@mui/material/DialogActions";
import DialogContent from "@mui/material/DialogContent";
import DialogTitle from "@mui/material/DialogTitle";
import Button from "@mui/material/Button";
import TextField from "@mui/material/TextField";
import CircularProgress from "@mui/material/CircularProgress";
import Alert from "@mui/material/Alert";
import InputLabel from "@mui/material/InputLabel";
import Select from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";

export const EditUserDialog = ({
	open,
	user,
	onClose,
	onSave,
	isSaving,
	saveError,
}) => {
	// Estado interno del formulario para los datos del usuario que se están editando
	const [formData, setFormData] = useState(user || {});

	//Reseteo el form si se abre el dialogo o si cambia el usuario
	useEffect(() => {
		setFormData(user || {});
	}, [user, open]);

	const handleChange = (e) => {
		const { name, value } = e.target;
		setFormData((prev) => ({ ...prev, [name]: value }));
	};

	const handleSubmit = (e) => {
		e.preventDefault();
		onSave(formData); // Llama a la función onSave que viene de usuarios
	};

	// Define los roles posibles para el Select
	const roles = ["Administrador", "Organizador", "Juez"];

	return (
		<Dialog open={open} onClose={onClose} fullWidth maxWidth="sm">
			<DialogTitle>Editar Usuario: {user?.alias || ""}</DialogTitle>
			<DialogContent dividers>
				{saveError && (
					<Alert severity="error" sx={{ mb: 2 }}>
						{saveError}
					</Alert>
				)}
				<form onSubmit={handleSubmit} id="edit-user-form">
					<TextField
						margin="normal"
						fullWidth
						label="Nombre"
						name="nombre"
						value={formData.nombre || ""}
						onChange={handleChange}
					/>
					<TextField
						margin="normal"
						fullWidth
						label="Apellido"
						name="apellido"
						value={formData.apellido || ""}
						onChange={handleChange}
					/>
					<TextField
						margin="normal"
						fullWidth
						label="Alias"
						name="alias"
						value={formData.alias || ""}
						onChange={handleChange}
						required
					/>
					<TextField
						margin="normal"
						fullWidth
						label="Email"
						name="email"
						type="email"
						value={formData.email || ""}
						onChange={handleChange}
						required
					/>

					<TextField
						margin="normal"
						fullWidth
						label="País"
						name="pais"
						value={formData.pais || ""}
						onChange={handleChange}
					/>
					<FormControl fullWidth margin="normal">
						<InputLabel id="rol-label">Rol</InputLabel>
						<Select
							labelId="rol-label"
							id="rol"
							name="rol"
							value={formData.rol || ""}
							label="Rol"
							onChange={handleChange}
							required
						>
							{roles.map((rol) => (
								<MenuItem key={rol} value={rol}>
									{rol}
								</MenuItem>
							))}
						</Select>
					</FormControl>
				</form>
			</DialogContent>
			<DialogActions>
				<Button onClick={onClose} color="primary" disabled={isSaving}>
					Cancelar
				</Button>
				<Button
					type="submit"
					form="edit-user-form" // Vincula el botón al formulario por su id
					variant="contained"
					color="primary"
					disabled={isSaving}
				>
					{isSaving ? <CircularProgress size={24} /> : "Guardar Cambios"}
				</Button>
			</DialogActions>
		</Dialog>
	);
};
