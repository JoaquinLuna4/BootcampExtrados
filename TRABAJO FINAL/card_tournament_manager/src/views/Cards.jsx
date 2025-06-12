import { useEffect, useState } from "react";
import { getCards } from "../services/cardsService";
import CardItem from "../components/CardItem";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import CardListItem from "../components/CardList";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import ToggleButton from "@mui/material/ToggleButton";
import ToggleButtonGroup from "@mui/material/ToggleButtonGroup";
import ViewListIcon from "@mui/icons-material/ViewList";
import ViewModuleIcon from "@mui/icons-material/ViewModule";
import CircularProgress from "@mui/material/CircularProgress";
import Snackbar from "@mui/material/Snackbar";

import { useNavigate } from "react-router";

const Cards = () => {
	const navigate = useNavigate();

	const [cards, setCards] = useState([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);

	// ---------- Estate para el cambio de vista lista/grid -----------------
	const [viewMode, setViewMode] = useState("grid");

	// ---------- State para almacenar los IDs de las cartas seleccionadas -----------------
	const [selectedCardIds, setSelectedCardIds] = useState([]);

	// Estado para el mensaje de snackbar cuando se repita la carta
	const [snackbarOpen, setSnackbarOpen] = useState(false);
	const [snackbarMessage, setSnackbarMessage] = useState("");

	useEffect(() => {
		const fetchCards = async () => {
			try {
				const response = await getCards();
				setCards(response);
				console.log("Fetching exitoso");
			} catch (error) {
				setError(error);
				console.error("Error fetching cards:", error);
			} finally {
				setLoading(false);
			}
		};

		fetchCards();
	}, []);

	const handleViewModeChange = (event, newViewMode) => {
		if (newViewMode !== null) {
			setViewMode(newViewMode);
		}
	};

	const handleCardSelection = (cardId, isSelected) => {
		setSelectedCardIds((prevSelected) => {
			if (isSelected) {
				return [...prevSelected, cardId];
			} else {
				return prevSelected.filter((id) => id !== cardId);
			}
		});
	};
	const handleContinue = (cardId) => {
		// Agrego las validaciones de 8-15 cartas aca también
		if (selectedCardIds.length < 8 || selectedCardIds.length > 15) {
			alert(
				`Debes seleccionar entre 8 y 15 cartas. Actualmente tienes ${selectedCardIds.length}.`
			);
			return;
		}
		if (selectedCardIds.includes(cardId)) {
			setSnackbarMessage("Esta carta ya está seleccionada.");
			setSnackbarOpen(true);
			return;
		}
		// Redirige a la nueva página, pasando los IDs seleccionados como state
		navigate("/selected-cards", { state: { selectedCardIds } });
	};

	//// --- para cerrar el snackbar --- -- - ////
	const handleCloseSnackbar = (event, reason) => {
		if (reason === "clickaway") {
			return;
		}
		setSnackbarOpen(false);
	};

	///////////------------- Validaciones de errores UX   -------------//////////

	if (loading)
		return (
			<Box
				display="flex"
				justifyContent="center"
				alignItems="center"
				height="80vh"
			>
				<CircularProgress />
				<Typography sx={{ ml: 2 }}>Cargando cartas...</Typography>
			</Box>
		);
	if (error)
		return (
			<Typography color="error">
				Error al cargar cartas: {error.message}
			</Typography>
		);
	if (cards.length === 0 && !loading)
		return <Typography>No se encontraron cartas.</Typography>;

	return (
		<>
			{loading && <p>Loading...</p>}
			{error && <p>Error fetching cards: {error.message}</p>}
			{cards.length === 0 && <p>No cards found.</p>}

			<Box sx={{ padding: 2 }}>
				<Typography variant="h4" gutterBottom>
					Colección de Cartas Disponibles
				</Typography>

				{/* Controles para cambiar la vista */}
				<Box sx={{ display: "flex", justifyContent: "flex-end", mb: 2 }}>
					<Button
						variant="contained"
						color="primary"
						onClick={handleContinue}
						disabled={selectedCardIds.length < 8 || selectedCardIds.length > 15}
					>
						Continuar ({selectedCardIds.length} Cartas)
					</Button>

					<ToggleButtonGroup
						value={viewMode}
						exclusive
						onChange={handleViewModeChange}
						aria-label="text alignment"
					>
						<ToggleButton value="grid" aria-label="grid view">
							<ViewModuleIcon />
						</ToggleButton>
						<ToggleButton value="list" aria-label="list view">
							<ViewListIcon />
						</ToggleButton>
					</ToggleButtonGroup>
				</Box>

				{/* Renderizado condicional de las cartas */}
				{viewMode === "grid" ? (
					<Grid
						container
						spacing={{ xs: 2, md: 3 }}
						columns={{ xs: 4, sm: 8, md: 12 }}
					>
						{cards.map((card) => (
							<Grid sx={{ xs: 4, sm: 4, md: 3 }} key={card.id}>
								<CardItem
									card={card}
									isSelected={selectedCardIds.includes(card.id)}
									onSelect={handleCardSelection}
								/>
							</Grid>
						))}
					</Grid>
				) : (
					// Vista de Lista (usando un nuevo componente para la lista de items)
					<Grid container spacing={2}>
						<CardListItem
							cards={cards}
							isSelected={selectedCardIds}
							onSelect={handleCardSelection}
						/>
						<Snackbar
							open={snackbarOpen}
							autoHideDuration={3000} // Se cierra automáticamente después de 3 segundos
							onClose={handleCloseSnackbar}
							message={snackbarMessage}
							anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
						/>
					</Grid>
				)}
			</Box>
		</>
	);
};

export default Cards;
