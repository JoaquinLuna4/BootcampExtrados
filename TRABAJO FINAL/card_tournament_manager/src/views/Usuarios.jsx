import React from "react";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router";
import { AxiosInterceptor } from "../interceptors/axios.interceptor";
import getUsers from "../services/getUsers.service";
import { useSelector, useDispatch } from "react-redux";
import { logout } from "../store/slices/authSlice";
import { Typography } from "@mui/material";

export const Usuarios = () => {
	const [isLoading, setIsLoading] = useState(true);
	const [error, setError] = useState(null);
	const [user, setUser] = useState([]);

	const navigate = useNavigate();
	const dispatch = useDispatch();

	const token = useSelector((state) => state.auth.token);
	console.log(token, "este es el token");

	const isLoggedIn = !!token;

	useEffect(() => {
		const fetchUsers = async () => {
			if (!token) {
				setIsLoading(false);
				setError("No estás autenticado, por favor inicia sesion.");
				return;
			}
			try {
				setIsLoading(true);
				setError(null);
				const { data } = await getUsers(token);
				setUser(data);
			} catch (error) {
				console.error("Error fetching users:", error);
				if (error.response && error.response.status === 401) {
					setError("No estás autorizado, por favor inicia sesión.");
					dispatch(logout());
					navigate("/login");
				} else {
					setError("Error al obtener usuarios.", error);
				}
			} finally {
				setIsLoading(false);
			}
		};
		fetchUsers();
	}, [token, dispatch, navigate]);

	const handleLogout = () => {
		dispatch(logout());
		navigate("/login");
	};

	return (
		<div>
			{isLoggedIn && <button onClick={handleLogout}>Logout</button>}
			<p className="presentation">This is a list of users:</p>
			{isLoading ? (
				<Typography variant="h6">Loading ... </Typography>
			) : error ? (
				<Typography color="error">{error}</Typography>
			) : (
				<ul>
					{user.map((user) => {
						return (
							<li key={user.id}>
								<p> {user.alias}</p>
							</li>
						);
					})}
				</ul>
			)}
		</div>
	);
};
