import React from "react";
import { Typography } from "@mui/material";
import Container from "@mui/material/Container";
import Button from "@mui/material/Button";
import { useNavigate } from "react-router";
import Grid from "@mui/material/Grid";
import useAuthActions from "../utils/hooks/handleAuthAction";

function Home() {
	const userProfile = JSON.parse(localStorage.getItem("authUser"));
	const navigate = useNavigate();
	const { handleAuthAction } = useAuthActions();

	return (
		<>
			<Container>
				<Typography variant="h1" align="center" sx={{ mb: 4, mt: 4 }}>
					Bienvenido {userProfile?.alias}
				</Typography>
				<Typography variant="body1" align="center">
					Esta es tu página de inicio.
				</Typography>
				<Typography variant="body2" align="center" sx={{ mt: 2 }}>
					Aquí puedes encontrar información relevante y acceder a tus funciones.
				</Typography>

				<Grid
					container
					justifyContent="center"
					spacing={2}
					sx={{ textAlign: "center", mt: 4 }}
				>
					<Grid item>
						<Button
							variant="contained"
							color="primary"
							onClick={() => {
								navigate("/profile");
							}}
						>
							Ir a mi perfil
						</Button>
					</Grid>
					<Grid item>
						<Button
							variant="contained"
							color="primary"
							onClick={() => {
								navigate("/create-user");
							}}
						>
							Crear Usuario
						</Button>
					</Grid>
					<Grid item>
						<Button
							variant="contained"
							color="secondary"
							onClick={() => handleAuthAction("Logout")}
						>
							Cerrar sesión
						</Button>
					</Grid>
				</Grid>
			</Container>
		</>
	);
}

export default Home;
