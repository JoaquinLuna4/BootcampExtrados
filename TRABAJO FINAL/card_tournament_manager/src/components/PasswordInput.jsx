import React, { useState } from "react";
import {
	FormControl,
	InputLabel,
	OutlinedInput,
	InputAdornment,
	IconButton,
} from "@mui/material";
import Visibility from "@mui/icons-material/Visibility";
import VisibilityOff from "@mui/icons-material/VisibilityOff";

const PasswordInput = ({ label, value, onChange, id, name, ...props }) => {
	const [showPassword, setShowPassword] = useState(false);

	const handleClickShowPassword = () => {
		setShowPassword((prev) => !prev); // Alterna el estado de showPassword
	};

	const handleMouseDownPassword = (event) => {
		event.preventDefault(); // Evita que el foco se pierda al hacer clic y arrastrar
	};

	return (
		<FormControl variant="outlined" fullWidth sx={{ mt: 2, mb: 1 }}>
			<InputLabel htmlFor={id}>{label}</InputLabel>
			<OutlinedInput
				id={id}
				name={name}
				type={showPassword ? "text" : "password"} // Cambia el tipo de input
				value={value}
				onChange={onChange}
				endAdornment={
					// Adorno al final del input
					<InputAdornment position="end">
						<IconButton
							aria-label="toggle password visibility"
							onClick={handleClickShowPassword}
							onMouseDown={handleMouseDownPassword}
							edge="end"
						>
							{showPassword ? <VisibilityOff /> : <Visibility />}{" "}
							{/* Cambia el icono */}
						</IconButton>
					</InputAdornment>
				}
				label={label}
				{...props} // Pasa cualquier otra prop adicional
			/>
		</FormControl>
	);
};

export default PasswordInput;
