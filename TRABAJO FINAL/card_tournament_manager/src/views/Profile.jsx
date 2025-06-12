// src/pages/ProfilePage.jsx
import React, { useState, useEffect } from "react";
import {
	Box,
	Typography,
	CircularProgress,
	Alert,
	Paper,
	Grid,
	List,
	ListItem,
	ListItemText,
	ListItemIcon,
	Button,
} from "@mui/material";
import PersonIcon from "@mui/icons-material/Person";
import EmailIcon from "@mui/icons-material/Email";
import PublicIcon from "@mui/icons-material/Public";
import BadgeIcon from "@mui/icons-material/Badge";
import AccountCircleIcon from "@mui/icons-material/AccountCircle";
import EventIcon from "@mui/icons-material/Event";
import VpnKeyIcon from "@mui/icons-material/VpnKey";

import { useNavigate } from "react-router-dom";
import { getProfile } from "../services/userService";

export const Profile = () => {
	const [userProfile, setUserProfile] = useState(null);
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState(null);
	const navigate = useNavigate();
	useEffect(() => {
		const fetchProfile = async () => {
			try {
				setLoading(true);
				setError(null);
				const profileData = await getProfile();
				setUserProfile(profileData);
			} catch (err) {
				console.error("Error al cargar el perfil:", err);
				setError(err.message || "No se pudo cargar el perfil del usuario.");

				// Si el error es de autenticación redirigir a login
				if (
					err.message.includes("token") ||
					err.response?.status === 401 ||
					err.response?.status === 403
				) {
					navigate("/login");
				}
			} finally {
				setLoading(false);
			}
		};

		fetchProfile();
	}, [navigate]);

	// --- Renderizado de estados de carga y error ---
	if (loading) {
		return (
			<Box
				display="flex"
				justifyContent="center"
				alignItems="center"
				height="80vh"
			>
				<CircularProgress />
				<Typography sx={{ ml: 2 }}>Cargando perfil...</Typography>
			</Box>
		);
	}

	if (error) {
		return (
			<Box
				display="flex"
				flexDirection="column"
				justifyContent="center"
				alignItems="center"
				height="80vh"
			>
				<Alert severity="error" sx={{ mb: 2 }}>
					{error}
				</Alert>
				<Button variant="contained" onClick={() => navigate("/login")}>
					Ir a Iniciar Sesión
				</Button>
			</Box>
		);
	}

	// Si userProfile es null después de cargar y sin error, algo salió mal
	if (!userProfile) {
		return (
			<Box
				display="flex"
				justifyContent="center"
				alignItems="center"
				height="80vh"
			>
				<Typography>No hay datos de perfil disponibles.</Typography>
			</Box>
		);
	}

	// --- Función auxiliar para formatear la fecha de registro ---
	const formatFechaRegistro = (dateString) => {
		if (!dateString || dateString === "0001-01-01T00:00:00") {
			return "N/A"; // Valor por defecto si la fecha es nula o el valor predeterminado de .NET
		}
		const date = new Date(dateString);
		// Ejemplo de formato: "12 de junio de 2025"
		return date.toLocaleDateString("es-AR", {
			year: "numeric",
			month: "long",
			day: "numeric",
		});
	};

	// --- Renderizado del perfil ---
	return (
		<Box sx={{ padding: 3, maxWidth: 800, margin: "auto" }}>
			{" "}
			{/* Centra el contenido */}
			<Typography variant="h4" gutterBottom align="center" sx={{ mb: 4 }}>
				Mi Perfil
			</Typography>
			<Paper elevation={4} sx={{ padding: 4, borderRadius: "12px" }}>
				{" "}
				{/* Usa Paper para un contenedor con sombra */}
				<Grid container spacing={3} alignItems="center">
					{/* Sección de Avatar/Alias */}
					<Grid item xs={12} sm={4} sx={{ textAlign: "center" }}>
						<AccountCircleIcon
							sx={{ fontSize: 120, color: "primary.main", mb: 1 }}
						/>
						<Typography variant="h5" sx={{ fontWeight: "bold" }}>
							{userProfile.alias}
						</Typography>
						<Typography variant="body2" color="text.secondary">
							ID: {userProfile.id}
						</Typography>
					</Grid>

					{/* Sección de Detalles del Perfil */}
					<Grid item xs={12} sm={8}>
						<List dense>
							{" "}
							{/* Lista densa para ocupar menos espacio */}
							<ListItem>
								<ListItemIcon>
									<PersonIcon color="action" />
								</ListItemIcon>
								<ListItemText
									primary={
										<Typography variant="body1" sx={{ fontWeight: "bold" }}>
											Nombre Completo
										</Typography>
									}
									secondary={
										<Typography variant="body2" sx={{ color: "text.primary" }}>
											{`${userProfile.nombre} ${userProfile.apellido}`}
										</Typography>
									}
								/>
							</ListItem>
							<ListItem>
								<ListItemIcon>
									<EmailIcon color="action" />
								</ListItemIcon>
								<ListItemText
									primary={
										<Typography variant="body1" sx={{ fontWeight: "bold" }}>
											Email
										</Typography>
									}
									secondary={
										<Typography variant="body2" sx={{ color: "text.primary" }}>
											{userProfile.email}
										</Typography>
									}
								/>
							</ListItem>
							<ListItem>
								<ListItemIcon>
									<PublicIcon color="action" />
								</ListItemIcon>
								<ListItemText
									primary={
										<Typography variant="body1" sx={{ fontWeight: "bold" }}>
											País
										</Typography>
									}
									secondary={
										<Typography variant="body2" sx={{ color: "text.primary" }}>
											{userProfile.pais}
										</Typography>
									}
								/>
							</ListItem>
							<ListItem>
								<ListItemIcon>
									<BadgeIcon color="action" />
								</ListItemIcon>
								<ListItemText
									primary={
										<Typography variant="body1" sx={{ fontWeight: "bold" }}>
											Rol
										</Typography>
									}
									secondary={
										<Typography variant="body2" sx={{ color: "text.primary" }}>
											{userProfile.rol}
										</Typography>
									}
								/>
							</ListItem>
							<ListItem>
								<ListItemIcon>
									<EventIcon color="action" />
								</ListItemIcon>
								<ListItemText
									primary={
										<Typography variant="body1" sx={{ fontWeight: "bold" }}>
											Fecha de Registro
										</Typography>
									}
									secondary={
										<Typography variant="body2" sx={{ color: "text.primary" }}>
											{formatFechaRegistro(userProfile.fecha_Registro)}
										</Typography>
									}
								/>
							</ListItem>
							{/* Mostrar idCreador solo si no es null */}
							{userProfile.idCreador && (
								<ListItem>
									<ListItemIcon>
										<VpnKeyIcon color="action" />
									</ListItemIcon>
									<ListItemText
										primary={
											<Typography variant="body1" sx={{ fontWeight: "bold" }}>
												ID de Creador
											</Typography>
										}
										secondary={
											<Typography
												variant="body2"
												sx={{ color: "text.primary" }}
											>
												{userProfile.idCreador}
											</Typography>
										}
									/>
								</ListItem>
							)}
						</List>
					</Grid>
				</Grid>
			</Paper>
		</Box>
	);
};
