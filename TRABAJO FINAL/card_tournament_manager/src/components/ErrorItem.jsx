import React from "react";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";

const ErrorItem = ({ error }) => {
	return (
		<Container
			sx={{
				mt: 4,
				flexDirection: "column",
				justifyContent: "center",
				alignItems: "center",
				height: "100vh",
				alignContent: "center",
				justifyItems: "center",
			}}
		>
			<Typography variant="h4" color="error">
				{error}
			</Typography>

			<Typography variant="h4" sx={{ mt: 2 }}>
				Por favor, actualiza la página.
			</Typography>
			<Button onClick={() => window.location.reload()}>Recargar</Button>
		</Container>
	);
};

export default ErrorItem;
