import { useLocation, useNavigate } from "react-router-dom";
import { useEffect } from "react";
import Box from "@mui/material/Box";
import { Typography } from "@mui/material";
import Grid from "@mui/material/Grid";
import Card from "@mui/material/Card";
import CardMedia from "@mui/material/CardMedia";
import CardContent from "@mui/material/CardContent";
import CardActions from "@mui/material/CardActions";
import Button from "@mui/material/Button";
import { createDeck } from "../services/cardsService";
import { useState } from "react";
import TextField from "@mui/material/TextField";
import { getCardDetailsByIds } from "../services/cardsService";
import CircularProgress from "@mui/material/CircularProgress";
import Alert from "@mui/material/Alert";

export const CardAsign = () => {
	const location = useLocation();
	const navigate = useNavigate();

	const [error, setError] = useState(null);

	const selectedCardIds = location.state?.selectedCardIds || [];

	const [deckName, setDeckName] = useState(""); // Estado para el nombre del mazo
	const [detailedSelectedCards, setDetailedSelectedCards] = useState([]); //Estado para recuperar la info de las cartas seleccionadas
	const [loadingDetails, setLoadingDetails] = useState(true);
	const [detailsError, setDetailsError] = useState(null);

	useEffect(() => {
		const fetchDetailedCards = async () => {
			if (selectedCardIds.length > 0) {
				setLoadingDetails(true);
				setDetailsError(null); // Limpiar errores previos
				try {
					// Llama a tu servicio para obtener los detalles completos de las cartas
					const details = await getCardDetailsByIds(selectedCardIds);
					// Los IDs seleccionados pueden estar en un orden diferente al de la API.
					// Opcional: Reordenar las cartas detalladas para que coincidan con el orden de `selectedCardIds`
					const orderedDetails = selectedCardIds
						.map((id) => details.find((card) => card.id === id))
						.filter((card) => card !== undefined); // Filtra cualquier ID que no se haya encontrado

					setDetailedSelectedCards(orderedDetails);
				} catch (error) {
					console.error("Error al obtener detalles de las cartas:", error);
					setDetailsError("No se pudieron cargar los detalles de las cartas.");
				} finally {
					setLoadingDetails(false);
				}
			} else {
				setLoadingDetails(false);
				setDetailedSelectedCards([]); // Asegurarse de que esté vacío si no hay IDs
			}
		};

		fetchDetailedCards();
	}, [selectedCardIds]);

	// Recupero el user id del local storage
	const authUser = JSON.parse(localStorage.getItem("authUser"));
	const userId = authUser ? authUser.id : null;

	const handleConfirm = async () => {
		if (!userId) {
			setError(
				"No se pudo obtener el ID del usuario. Por favor, inicia sesión y añade las cartas nuevamente."
			);
			return;
		}
		if (deckName.trim() === "") {
			setError("Por favor, ingresa un nombre para el mazo.");
			return;
		}

		if (selectedCardIds.length < 8 || selectedCardIds.length > 15) {
			setError("Debes seleccionar entre 8 y 15 cartas para crear un mazo.");
			return;
		}

		const newDeck = {
			JugadorId: userId,
			Nombre: deckName,
			CartasIds: selectedCardIds,
		};
		console.log("Datos del mazo a enviar:", newDeck);

		try {
			const response = await createDeck(newDeck); // Llama a tu servicio de backend
			console.log("Mazo creado con éxito:", response);
			navigate("/success"); // Redirige a la pagina de éxito
		} catch (error) {
			console.error("Error al crear el mazo:", error);
			setError(
				`Error al crear el mazo: ${error.message || "Error desconocido"}`
			);
		}
	};
	if (loadingDetails) {
		return (
			<Box
				display="flex"
				justifyContent="center"
				alignItems="center"
				height="80vh"
			>
				<CircularProgress />
				<Typography sx={{ ml: 2 }}>
					Cargando detalles de las cartas seleccionadas...
				</Typography>
			</Box>
		);
	}

	if (detailsError) {
		return (
			<Box
				display="flex"
				flexDirection="column"
				justifyContent="center"
				alignItems="center"
				height="80vh"
			>
				<Alert severity="error" sx={{ mb: 2 }}>
					{detailsError}
				</Alert>
				<Button variant="contained" onClick={() => navigate("/cards")}>
					Volver a seleccionar cartas
				</Button>
			</Box>
		);
	}

	return (
		<Box sx={{ padding: 2 }}>
			<Typography variant="h4" gutterBottom>
				Cartas Seleccionadas
			</Typography>
			{error && (
				<Typography color="error" sx={{ mb: 2 }}>
					{error}
				</Typography>
			)}
			<TextField
				label="Nombre del Mazo"
				variant="outlined"
				fullWidth
				value={deckName}
				onChange={(e) => setDeckName(e.target.value)}
				sx={{ mb: 3 }}
				required
			/>
			<Button
				variant="contained"
				color="primary"
				onClick={handleConfirm}
				sx={{ mt: 2, mb: 4 }}
			>
				Confirmar Asignación de Cartas
			</Button>

			{detailedSelectedCards.length === 0 ? (
				<Typography>No has seleccionado ninguna carta.</Typography>
			) : (
				<Grid container spacing={2}>
					{detailedSelectedCards.map((card) => (
						<Grid item xs={12} sm={6} md={4} key={card.id}>
							<Card>
								<CardMedia
									component="img"
									height="140"
									image={card.ilustracion}
									alt={card.nombre}
								/>
								<CardContent>
									<Typography gutterBottom variant="h5" component="div">
										{card.nombre}
									</Typography>
									<Typography variant="body2" color="text.secondary">
										{card.descripcion}
									</Typography>
								</CardContent>
								<CardActions>
									{/* Puedes añadir botones de detalles o quitar cartas aquí */}
								</CardActions>
							</Card>
						</Grid>
					))}
				</Grid>
			)}
		</Box>
	);
};
