import React from "react";
import "./Login.css";

const Login = () => {
	const handleLogin = () => {
		localStorage.setItem("token", "true");
		// esto podria estar en redux
		window.location.href = "/Proyectos";
	};

	return (
		<div className="login-container">
			<h2>Login</h2>
			<button onClick={handleLogin}>Iniciar sesión</button>
		</div>
	);
};

export default Login;
