import React, { useState } from "react";
import "./ContactForm.css";

export const ContactForm = () => {
	const [formData, setFormData] = useState({
		nombre: "",
		email: "",
		message: "",
	});

	const [errors, setErrors] = useState({});

	const manejarCambio = (e) => {
		setFormData({
			...formData,
			[e.target.name]: e.target.value,
		});
	};

	const validarCampoObligatorio = (valor) => {
		return valor.trim() !== "";
	};

	const validarLongitud = (valor, min = 3, max = 8) => {
		return valor.length >= min && valor.length <= max;
	};

	const validarEmail = (email) => {
		const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
		return regex.test(email);
	};

	const manejarEnvio = (e) => {
		e.preventDefault();
		const nuevoError = {};

		if (!validarCampoObligatorio(formData.nombre)) {
			nuevoError.nombre = "Nombre es obligatorio";
		} else if (!validarLongitud(formData.nombre, 3, 10)) {
			nuevoError.nombre = "El nombre debe tener entre 3 y 10 caracteres";
		}

		if (!validarEmail(formData.email)) {
			nuevoError.email = "Correo invalido";
		}

		if (!validarCampoObligatorio(formData.message)) {
			nuevoError.message = "El mensaje es obligatorio";
		} else if (!validarLongitud(formData.message, 10, 200)) {
			nuevoError.message = "El mensaje debe tener entre 10 y 200 caracteres";
		}

		setErrors(nuevoError);

		if (Object.keys(nuevoError).length === 0) {
			console.log("Formulario válido:", formData);
			setFormData({ nombre: "", email: "", message: "" });
		} else {
			console.log("Formulario inválido:", nuevoError);
		}
	};

	return (
		<form onSubmit={manejarEnvio} className="form-container">
			<div className="form-group">
				<input
					type="text"
					name="nombre"
					value={formData.nombre}
					onChange={manejarCambio}
					placeholder="Nombre"
					className="form-input"
				/>
				{errors.nombre && (
					<span className="error-message">{errors.nombre}</span>
				)}
			</div>

			<div className="form-group">
				<input
					type="email"
					name="email"
					value={formData.email}
					onChange={manejarCambio}
					placeholder="Email"
					className="form-input"
				/>
				{errors.email && <span className="error-message">{errors.email}</span>}
			</div>

			<div className="form-group">
				<textarea
					name="message"
					value={formData.message}
					onChange={manejarCambio}
					placeholder="Mensaje"
					className="form-input"
				/>
				{errors.message && (
					<span className="error-message">{errors.message}</span>
				)}
			</div>

			<button type="submit" className="form-button">
				Enviar
			</button>
		</form>
	);
};
