import { createSlice } from "@reduxjs/toolkit";

const authSlice = createSlice({
	name: "auth",
	initialState: {
		token: localStorage.getItem("authToken") || null, //pone el valor del localstorage primero y si no es null
		user: localStorage.getItem("authUser")
			? JSON.parse(localStorage.getItem("authUser"))
			: null,
		status: "empty",
		error: null,
	},
	reducers: {
		// Login correcto
		loginSuccess: (state, action) => {
			//El payload es la información relevante que viene como un objeto del Login
			//Es lo que estoy enviando como {token, user}

			state.token = action.payload.token; // Acceder a .data.token
			state.user = action.payload.usuario; // Acceder a .data.usuario
			state.status = "succeeded";
			state.error = null;
			localStorage.setItem("authToken", action.payload.token); // lo hace persistir en localStorage
			localStorage.setItem("authUser", JSON.stringify(action.payload.usuario)); // lo hace persistir a usuario en localStorage
		},
		// Metodo para el logout
		logout: (state) => {
			state.token = null;
			state.user = null;
			state.status = "empty";
			state.error = null;
			localStorage.removeItem("authToken"); // Eliminar del localStorage
		},
		// Metodo para manejar el inicio de carga para un estado global de carga
		authLoading: (state) => {
			state.status = "loading";
			state.error = null;
		},
		// Manejo de errores de auth
		authError: (state, action) => {
			state.status = "failed";
			state.error = action.payload;
			state.token = null; // Si hay error pongo el token en null
			localStorage.removeItem("authToken"); // Limpiamos el token si hay un error grave
		},
		// Metodo para rehidratar el estado desde localStorage
		initializeAuth: (state) => {
			const storedToken = localStorage.getItem("authToken");
			if (storedToken) {
				state.token = storedToken;
			}
		},
	},
});

export const { loginSuccess, logout, authLoading, authError, initializeAuth } =
	authSlice.actions;

export default authSlice.reducer;
