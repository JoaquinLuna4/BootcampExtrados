import React, { useEffect, useState } from "react";
import {
	Box,
	Typography,
	Grid,
	CircularProgress,
	Alert,
	Card,
	CardContent,
	CardActions,
	Button,
	Accordion,
	AccordionSummary,
	AccordionDetails,
} from "@mui/material";
import ExpandIcon from "@mui/icons-material/Expand";
import { useNavigate } from "react-router-dom";
import { getDecksByUserId } from "../services/cardsService";
import { getCardsOfDeck } from "../services/cardsService";

export const MyDecks = () => {
	const [myDecks, setMyDecks] = useState([]);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);
	const navigate = useNavigate();

	// Estado para el mazo actualmente expandido para mostrar sus cartas
	const [expandedMazoId, setExpandedMazoId] = useState(null);

	// Estado para las cartas del mazo actualmente expandido
	const [currentDeckCards, setCurrentDeckCards] = useState([]);
	const [loadingDeckCards, setLoadingDeckCards] = useState(false); // Carga de cartas de un mazo específico
	const [errorDeckCards, setErrorDeckCards] = useState(null);

	////-- ---- ---   Recupero el usuario --------------- - -- /
	const authUser = JSON.parse(localStorage.getItem("authUser"));
	const userId = authUser ? authUser.id : null;

	const handleExpandMazo = async (mazoId) => {
		// Si ya está expandido, colapsarlo
		if (expandedMazoId === mazoId) {
			setExpandedMazoId(null);
			setCurrentDeckCards([]); // Limpiar las cartas
			setErrorDeckCards(null); // Limpiar errores
			return;
		}

		// Si no está expandido, expandirlo y cargar sus cartas
		setExpandedMazoId(mazoId);
		setCurrentDeckCards([]); // Limpiar cartas anteriores
		setLoadingDeckCards(true); // Activar carga de cartas específicas
		setErrorDeckCards(null); // Limpiar errores de carga de cartas

		try {
			const cards = await getCardsOfDeck(mazoId);
			setCurrentDeckCards(cards);
		} catch (err) {
			setErrorDeckCards(
				err.message || "Error al cargar las cartas de este mazo."
			);
			console.error(`Error al cargar cartas para el mazo ${mazoId}:`, err);
		} finally {
			setLoadingDeckCards(false); // Desactivar carga
		}
	};

	useEffect(() => {
		const fetchMyDecks = async () => {
			if (!userId) {
				setError("Usuario no autenticado. Por favor, inicia sesión.");

				setLoading(false);
				return;
			}

			try {
				setLoading(true);
				const data = await getDecksByUserId(userId);
				setMyDecks(data);
				console.log("Mis mazos cargados:", data);
			} catch (err) {
				setError(err.message || "Error al cargar tus mazos.");
				console.error("Error al cargar mazos:", err);
			} finally {
				setLoading(false);
			}
		};

		fetchMyDecks();
	}, [userId]);

	if (loading) {
		return <CircularProgress />;
	}

	if (error) {
		return <Alert severity="error">{error}</Alert>;
	}

	return (
		<Box sx={{ padding: 3 }}>
			<Typography variant="h4" gutterBottom>
				Mis Mazos
			</Typography>

			{myDecks.length === 0 ? (
				<Typography variant="h6" color="text.secondary">
					Aún no has creado ningún mazo. ¡Anímate a crear uno!
				</Typography>
			) : (
				<Grid container spacing={3}>
					{myDecks.map((mazo) => (
						<Grid item xs={12} key={mazo.id}>
							{" "}
							<Accordion
								expanded={expandedMazoId === mazo.id}
								onChange={() => handleExpandMazo(mazo.id)}
							>
								<AccordionSummary
									expandIcon={<ExpandIcon />}
									aria-controls={`panel${mazo.id}-content`}
									id={`panel${mazo.id}-header`}
								>
									<Typography variant="h6">{mazo.nombre}</Typography>
									<Typography sx={{ ml: 2, color: "text.secondary" }}>
										(ID: {mazo.id})
									</Typography>
								</AccordionSummary>
								<AccordionDetails>
									{loadingDeckCards && expandedMazoId === mazo.id ? (
										<Box
											display="flex"
											justifyContent="center"
											alignItems="center"
											sx={{ py: 2 }}
										>
											<CircularProgress size={24} />
											<Typography sx={{ ml: 2 }}>Cargando cartas...</Typography>
										</Box>
									) : errorDeckCards && expandedMazoId === mazo.id ? (
										<Alert severity="error">{errorDeckCards}</Alert>
									) : currentDeckCards.length === 0 &&
									  expandedMazoId === mazo.id ? (
										<Typography color="text.secondary">
											Este mazo no tiene cartas.
										</Typography>
									) : (
										expandedMazoId === mazo.id && (
											<Grid container spacing={2}>
												{currentDeckCards.map((card) => (
													<Grid item xs={12} sm={6} md={4} lg={3} key={card.id}>
														<CardItem
															card={card}
															isSelected={false} // Las cartas en el mazo no están "seleccionadas"
															onSelect={() => {}} // No hay acción de selección aquí
														/>
													</Grid>
												))}
											</Grid>
										)
									)}
								</AccordionDetails>
							</Accordion>
						</Grid>
					))}
				</Grid>
			)}

			<Button
				variant="contained"
				color="primary"
				onClick={() => navigate("/cards")}
				sx={{ mt: 4 }}
			>
				Crear Nuevo Mazo
			</Button>
		</Box>
	);
};
