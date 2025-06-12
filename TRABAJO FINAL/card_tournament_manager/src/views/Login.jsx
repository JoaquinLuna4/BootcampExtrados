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
import { loginUser } from "../services/userService.js";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import { useDispatch, useSelector } from "react-redux";

function LoginForm() {
	const dispatch = useDispatch();
	const navigate = useNavigate();

	const { error, status } = useSelector((state) => state.auth);
	const isLoading = status === "loading";

	// Credenciales hardcodeadas para los usuarios demo
	const demoUsers = {
		admin: { alias: "elAdmin", enteredPass: "Admin" },
		organizer: { alias: "pedroOrg", enteredPass: "12345" },
		juez: { alias: "juez_demo", enteredPass: "12345" },
	};

	const handleSubmit = async (event) => {
		event.preventDefault();
		dispatch(authLoading());

		const data = new FormData(event.currentTarget);
		const values = {
			alias: data.get("alias"),
			enteredPass: data.get("enteredPass"),
		};

		try {
			const response = await loginUser(values);
			console.log(response, "login exitoso");
			if (response) {
				dispatch(loginSuccess(response));
				navigate("/");
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
	const handleDemoLogin = async (userType) => {
		const { alias: demoAlias, enteredPass: demoPass } = demoUsers[userType];

		try {
			const values = { alias: demoAlias, enteredPass: demoPass };
			// Realizar el login con las credenciales demo
			const response = await loginUser(values);
			console.log(response, "login exitoso");
			if (response) {
				dispatch(loginSuccess(response));
				navigate("/");
			} else {
				dispatch(authError("Error inesperado"));
			}
		} catch (error) {
			console.error("Error en el login de demostración:", error);
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
				<Typography
					variant="subtitle2"
					align="center"
					sx={{ mt: 2, mb: 1, fontWeight: "bold" }}
				>
					Users DEMO:
				</Typography>
				<Box
					sx={{
						display: "flex",
						flexDirection: "column",
						gap: 1, // Espacio entre los botones
					}}
				>
					<Button
						fullWidth
						variant="outlined"
						onClick={() => handleDemoLogin("admin")}
						disabled={isLoading}
					>
						Login como Admin Demo
					</Button>
					<Button
						fullWidth
						variant="outlined"
						onClick={() => handleDemoLogin("organizer")}
						disabled={isLoading}
					>
						Login como Organizador Demo
					</Button>
					<Button
						fullWidth
						variant="outlined"
						onClick={() => handleDemoLogin("juez")}
						disabled={isLoading}
					>
						Login como Juez Demo
					</Button>
				</Box>
			</Paper>
		</Container>
	);
}

export default LoginForm;
