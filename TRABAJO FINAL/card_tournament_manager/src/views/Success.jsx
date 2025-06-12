import {
	Box,
	Container,
	Typography,
	Button,
	CircularProgress,
} from "@mui/material"; // Importa Box y CircularProgress si lo necesitas para un fallback
import CheckCircleOutlineIcon from "@mui/icons-material/CheckCircleOutline"; // Icono de éxito
import { useNavigate, useLocation } from "react-router-dom";

export const Success = () => {
	const navigate = useNavigate();
	const location = useLocation(); // Para acceder al estado si se pasa información

	const deckName = location.state?.deckName || "";

	return (
		<Container
			sx={{
				display: "flex",
				flexDirection: "column",
				alignItems: "center",
				justifyContent: "center",
				minHeight: "80vh",
				textAlign: "center",
				padding: 3,
			}}
			maxWidth="sm" // Limita el ancho del contenedor para una mejor lectura
		>
			<CheckCircleOutlineIcon
				sx={{ fontSize: 80, color: "success.main", mb: 2 }}
			/>{" "}
			{/* Icono grande de éxito */}
			<Typography variant="h3" component="h1" gutterBottom color="success.dark">
				¡Éxito!
			</Typography>
			<Typography variant="h5" gutterBottom sx={{ mb: 3 }}>
				El mazo {deckName} ha sido creado exitosamente.
			</Typography>
			<Box
				sx={{
					display: "flex",
					flexDirection: { xs: "column", sm: "row" },
					gap: 2,
					width: "100%",
					justifyContent: "center",
				}}
			>
				<Button
					variant="contained"
					color="primary"
					onClick={() => navigate("/my-decks")} // Podrías tener una ruta para "mis mazos"
					size="large"
				>
					Ver Mis Mazos
				</Button>

				<Button
					variant="outlined"
					color="secondary"
					onClick={() => navigate("/cards")} // Para crear otro mazo, vuelve a la selección de cartas
					size="large"
				>
					Crear Otro Mazo
				</Button>

				{/* Opcional: un botón para volver al inicio general del sitio */}
				<Button
					variant="text"
					color="inherit"
					onClick={() => navigate("/")}
					size="large"
				>
					Ir al Inicio
				</Button>
			</Box>
		</Container>
	);
};
