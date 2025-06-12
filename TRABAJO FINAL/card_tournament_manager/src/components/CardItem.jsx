import * as React from "react";
import Card from "@mui/material/Card";
import CardContent from "@mui/material/CardContent";
import CardMedia from "@mui/material/CardMedia";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";
import CardActionArea from "@mui/material/CardActionArea";
import CardActions from "@mui/material/CardActions";

import Checkbox from "@mui/material/Checkbox";
import Chip from "@mui/material/Chip";

import FormControlLabel from "@mui/material/FormControlLabel";

import { LuSwords } from "react-icons/lu";
import { LuShield } from "react-icons/lu";

export default function CardItem({ card, isSelected, onSelect }) {
	const handleToggleSelect = () => {
		onSelect(card.id, !isSelected); // Notifica al padre el ID de la carta y el nuevo estado de selección
	};

	return (
		<>
			<Card key={card.id} sx={{ maxWidth: 345 }}>
				<CardActionArea>
					<CardActions sx={{ justifyContent: "space-between" }}>
						{/* <Button size="small" color="primary">
						Ver Detalles
					</Button> */}
						<FormControlLabel
							control={
								<Checkbox checked={isSelected} onChange={handleToggleSelect} />
							}
							label="Seleccionar"
							labelPlacement="end"
						/>
					</CardActions>
					<CardMedia
						component="img"
						height="440"
						image={card.ilustracion}
						alt={card.nombre}
					/>
					<CardContent>
						<Typography gutterBottom variant="h6" component="div">
							{card.nombre}
						</Typography>
						<Typography variant="body2" sx={{ color: "text.secondary" }}>
							{card.descripcion}
						</Typography>

						<Chip
							sx={{ bgcolor: "primary.main", color: "white" }}
							icon={<LuSwords />}
							label={`Atack ${card.ataque}`}
						/>
						<Chip
							sx={{
								bgcolor: "secondary.main",
								color: "white",
								margin: ".5em",
							}}
							icon={<LuShield />}
							label={`Defense ${card.defensa}`}
						/>
					</CardContent>
				</CardActionArea>
			</Card>
		</>
	);
}
