import React from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";
import {
	authLoading,
	loginSuccess,
	authError,
} from "../store/slices/authSlice";
import {
	Avatar,
	Button,
	CssBaseline,
	TextField,
	Box,
	Typography,
	Container,
	Paper,
} from "@mui/material";
import PasswordInput from "../components/PasswordInput";

import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import { useDispatch, useSelector } from "react-redux";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

function LoginForm() {
	const dispatch = useDispatch();
	const navigate = useNavigate();

	const { error, status } = useSelector((state) => state.auth);
	const isLoading = status === "loading";

	const handleSubmit = async (event) => {
		event.preventDefault();
		dispatch(authLoading());

		const data = new FormData(event.currentTarget);
		const values = {
			alias: data.get("alias"),
			enteredPass: data.get("enteredPass"),
		};

		const url = `${API_BASE_URL}/auth/login`;

		try {
			const response = await axios.post(url, values);
			console.log(response, "login exitoso");
			if (response.data) {
				dispatch(loginSuccess(response.data));
				navigate("/users");
			} else {
				dispatch(authError("Error inesperado"));
			}
		} catch (err) {
			console.log("Error en el login:", err);
			let errorMessage = "Ocurrio un error inesperado al iniciar sesion";
			if (axios.isAxiosError(err)) {
				if (err.response) {
					if (err.response.status === 401) {
						errorMessage = "Credenciales inválidas";
					} else if (err.response.data && err.response.data.message) {
						errorMessage = err.response.data.message; //Devolvemos el mensaje del backend
					} else if (err.response.status === 500) {
						errorMessage = "Error en el servidor";
					} else {
						errorMessage = `Error en el servidor con el codigo ${err.response.status}`;
					}
				} else if (err.request) {
					errorMessage =
						"Se realizó la solicitud pero no se recibió respuesta del servidor";
				} else {
					errorMessage = "Algo salio mal al configurar la solicitud";
				}
			} else {
				errorMessage = "Error inesperado ajeno a Axios";
			}
			dispatch(authError(errorMessage));
		}
	};

	return (
		<Container component="main" maxWidth="xs">
			<CssBaseline />
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
				<Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
					<LockOutlinedIcon />
				</Avatar>
				<Typography component="h1" variant="h5">
					Iniciar Sesión
				</Typography>
				<Box component="form" onSubmit={handleSubmit} noValidate sx={{ mt: 1 }}>
					<TextField
						margin="normal"
						required
						fullWidth
						id="alias"
						label="Alias"
						name="alias"
						autoComplete="alias"
						autoFocus
					/>
					<PasswordInput
						margin="normal"
						required
						fullWidth
						name="enteredPass"
						label="Contraseña"
						id="enteredPass"
						autoComplete="current-enteredPass"
					/>
					{error && (
						<Typography color="error" variant="body2">
							{error}
						</Typography>
					)}
					<Button
						type="submit"
						fullWidth
						variant="contained"
						sx={{ mt: 3, mb: 2 }}
						disabled={isLoading}
					>
						{isLoading ? "Iniciando Sesión..." : "Iniciar Sesión"}
					</Button>
				</Box>
			</Paper>
		</Container>
	);
}

export default LoginForm;
