import { useState } from "react";
import { useSelector } from "react-redux";

import {
	Button,
	TextField,
	Typography,
	Box,
	Paper,
	Container,
	FormControl,
	InputLabel,
	Select,
	MenuItem,
} from "@mui/material";
import { isValidEmail } from "../utils/isValidMail";
import { isValidPassword } from "../utils/isValidPass";
import axios from "axios";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

// Define los roles que el usuario logueado puede crear, para poder mostrar en el selec unicamente las opciones que tiene disponible
const CREATABLE_ROLES = {
	Administrador: ["Administrador", "Organizador", "Juez"],
	Organizador: ["Juez", "Organizador"],
	Juez: [],
	Jugador: [],
};

export const CreateUser = () => {
	const [isLoading, setIsLoading] = useState(false);

	//---------------- Estados del formulario ----------------
	const [nombre, setNombre] = useState("");
	const [apellido, setApellido] = useState("");
	const [alias, setAlias] = useState("");
	const [email, setEmail] = useState("");
	const [password, setPassword] = useState("");
	const [pais, setPais] = useState("");
	const [selectedRoleToCreate, setSelectedRoleToCreate] = useState("");

	// --------------Estados de error y exito -------
	const [errorMessage, setErrorMessage] = useState("");
	const [successMessage, setSuccessMessage] = useState("");

	// ------------- Usuario logueado --------------
	const currentUser = useSelector((state) => state.auth.user);

	const rolesCanCreate = currentUser
		? CREATABLE_ROLES[currentUser.rol] || []
		: [];

	const getEndpoint = (roleToCreate) => {
		switch (roleToCreate) {
			case "Administrador":
				return `${API_BASE_URL}/usuarios/crear-administrador`;
			case "Organizador":
				return `${API_BASE_URL}/usuarios/crear-organizador`;
			case "Juez":
				return `${API_BASE_URL}/usuarios/crear-juez`;
			default:
				return "";
		}
	};

	const handleSubmit = async (event) => {
		event.preventDefault();
		setErrorMessage("");
		setSuccessMessage("");
		setIsLoading(true);

		if (!selectedRoleToCreate) {
			setErrorMessage("Por favor, selecciona un rol para el nuevo usuario.");
			setIsLoading(false);
			return;
		}

		const url = getEndpoint(selectedRoleToCreate); //Saco la URL del switch-case al que le pasé el value de roleToCreate
		if (!url) {
			setErrorMessage("Rol seleccionado no válido para creación de usuario.");
			setIsLoading(false);
			return;
		}

		// Validación de campos

		if (!alias.trim() || !email.trim() || !password.trim()) {
			setErrorMessage("Los campos alias, email y contraseña son obligatorios.");
			setIsLoading(false);
			return;
		} else if (!isValidEmail(email.trim())) {
			setErrorMessage("El formato del email es inválido.");
			setIsLoading(false);
			return;
		} else if (!isValidPassword(password.trim())) {
			setErrorMessage("La contraseña debe tener al menos 5 caracteres.");
			setIsLoading(false);
			return;
		}

		// Prepara el objeto de datos según la estructura que espera el backend
		const userData = {
			Nombre: nombre,
			Apellido: apellido,
			Alias: alias,
			Email: email,
			Password: password,
			Pais: pais,
			Rol: selectedRoleToCreate, // El rol que se está creando
		};

		try {
			// Envía el objeto userData completo al backend
			const response = await axios.post(url, userData);
			console.log(response.data, response.status, "este es el response");

			setSuccessMessage(
				`Usuario "${alias}" con rol "${selectedRoleToCreate}" creado exitosamente.`
			);

			// Limpiar los campos del formulario después del éxito
			setNombre("");
			setApellido("");
			setAlias("");
			setEmail("");
			setPassword("");
			setPais("");
			setSelectedRoleToCreate("");
			setIsLoading(false);
		} catch (err) {
			console.error("Error al crear usuario:", err);
			let message = errorMessage || "Error al crear usuario. Intenta de nuevo.";
			if (axios.isAxiosError(err) && err.response) {
				if (err.response.status === 401 || err.response.status === 403) {
					message =
						"No tienes permiso para crear este tipo de usuario o tu sesión ha expirado.";
				} else if (err.response.data && err.response.data.message) {
					message = err.response.data.message;
					return (
						<Typography color="error" sx={{ mt: 1, textAlign: "center" }}>
							{message} {errorMessage}
						</Typography>
					);
				} else if (err.response.status === 400) {
					message =
						"Datos inválidos. Por favor, verifica la información proporcionada.";
				}
			} else {
				message = `Error inesperado: ${err.message || String(err)}`;
			}
			setErrorMessage(message);
			setIsLoading(false);
		}
	};
	// Si no hay usuario o no tiene roles para crear
	if (!currentUser || rolesCanCreate.length === 0) {
		return (
			<Typography color="error" sx={{ mt: 4, textAlign: "center" }}>
				No tienes permisos para crear usuarios.
			</Typography>
		);
	} else {
		return (
			<Container component="main" maxWidth="sm">
				<Paper
					elevation={6}
					sx={{
						my: 8,
						p: 4,
						display: "flex",
						flexDirection: "column",
						alignItems: "center",
					}}
				>
					<Typography component="h1" variant="h5">
						Crear Nuevo Usuario
					</Typography>
					<Box
						component="form"
						onSubmit={handleSubmit}
						noValidate
						sx={{ mt: 3, width: "100%" }}
					>
						{/* Selector de Rol */}
						<FormControl fullWidth margin="normal" required>
							<InputLabel id="select-role-label">
								Rol del Nuevo Usuario
							</InputLabel>
							<Select
								labelId="select-role-label"
								id="selectedRoleToCreate"
								value={selectedRoleToCreate}
								label="Rol del Nuevo Usuario"
								onChange={(e) => setSelectedRoleToCreate(e.target.value)}
							>
								<MenuItem value="">
									<em>Seleccionar Rol</em>
								</MenuItem>
								{rolesCanCreate.map((role) => (
									<MenuItem key={role} value={role}>
										{role}
									</MenuItem>
								))}
							</Select>
						</FormControl>

						{/* Campos de Datos del Usuario */}
						<TextField
							margin="normal"
							required
							fullWidth
							id="nombre"
							label="Nombre"
							name="nombre"
							autoComplete="given-name"
							value={nombre}
							onChange={(e) => setNombre(e.target.value)}
						/>
						<TextField
							margin="normal"
							required
							fullWidth
							id="apellido"
							label="Apellido"
							name="apellido"
							autoComplete="family-name"
							value={apellido}
							onChange={(e) => setApellido(e.target.value)}
						/>
						<TextField
							margin="normal"
							required
							fullWidth
							id="alias"
							label="Alias"
							name="alias"
							autoComplete="off"
							value={alias}
							onChange={(e) => setAlias(e.target.value)}
						/>
						<TextField
							margin="normal"
							required
							fullWidth
							id="email"
							label="Email"
							name="email"
							autoComplete="email"
							type="email"
							value={email}
							onChange={(e) => setEmail(e.target.value)}
						/>
						<TextField
							margin="normal"
							required
							fullWidth
							name="password"
							label="Contraseña"
							type="password"
							id="password"
							autoComplete="new-password"
							value={password}
							onChange={(e) => setPassword(e.target.value)}
						/>
						<TextField
							margin="normal"
							required
							fullWidth
							id="pais"
							label="País"
							name="pais"
							autoComplete="country"
							value={pais}
							onChange={(e) => setPais(e.target.value)}
						/>

						{/* Mensajes de Error y Éxito */}
						{errorMessage && (
							<Typography
								color="error"
								variant="body2"
								sx={{ mt: 1, textAlign: "center" }}
							>
								{errorMessage}
							</Typography>
						)}
						{successMessage && (
							<Typography
								color="success.main"
								variant="body2"
								sx={{ mt: 1, textAlign: "center" }}
							>
								{successMessage}
							</Typography>
						)}

						{/* Botón de Submit */}
						<Button
							type="submit"
							fullWidth
							variant="contained"
							sx={{ mt: 3, mb: 2 }}
							onSubmit={handleSubmit}
							disabled={isLoading || !selectedRoleToCreate} // Deshabilitar si no hay rol seleccionado o si está cargando
						>
							{isLoading ? `Creando usuario...` : `Crear Usuario`}
						</Button>
					</Box>
				</Paper>
			</Container>
		);
	}
};
