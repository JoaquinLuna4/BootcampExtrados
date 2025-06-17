import React from "react";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router";
import { useSelector, useDispatch } from "react-redux";
import { logout } from "../store/slices/authSlice";
import { Typography } from "@mui/material";
import Container from "@mui/material/Container";
import Button from "@mui/material/Button";

import { getUsers } from "../services/userService";

import { deleteUser } from "../services/userService";
import { updateUser } from "../services/userService";

import { ConfirmDialog } from "../components/ConfirmDialog";
import { EditUserDialog } from "../components/EditUserDialog";

import { UserTable } from "../components/UserTable";
import { SnackbarItem } from "../components/SnackbarItem";
import ErrorItem from "../components/ErrorItem";

export const Users = () => {
	const [isLoading, setIsLoading] = useState(true);
	const [error, setError] = useState(null);

	//----------- States para el delete dialog -----------//

	const [openConfirmDialog, setOpenConfirmDialog] = useState(false);
	const [userIdToDelete, setUserIdToDelete] = useState(null);

	//----------- -------------------- -----------//

	// --------------- States para el edit dialog -----------//
	const [openEditDialog, setOpenEditDialog] = useState(false);
	const [userToEdit, setUserToEdit] = useState(null); // Almacena el OBJETO completo del usuario a editar
	const [isSavingEdit, setIsSavingEdit] = useState(false); // Estado para el spinner de guardar
	const [saveEditError, setSaveEditError] = useState(null); // Estado para errores de edicion al guardar
	//---------------- ------------- - --------------------//

	const navigate = useNavigate();
	const dispatch = useDispatch();

	const token = useSelector((state) => state.auth.token);
	const [users, setUsers] = useState([]);
	// const userPAYLOAD = useSelector((state) => state.auth.user);

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
				const response = await getUsers();
				setUsers(response);
				console.log(response, "response del getUsers");
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

	/// Formateo las filas para el componente datagrid
	const rows = users.map((user) => ({
		id: user.id,
		nombre: user.nombre,
		apellido: user.apellido,
		alias: user.alias,
		email: user.email,
		pais: user.pais,
		rol: user.rol,
		fecha_Registro: user.fecha_Registro,
		idCreador: user.idCreador,
	}));

	const handleDeleteUser = async (userId) => {
		console.log(`Eliminar usuario con ID: ${userId}`);
		setUserIdToDelete(userId); // Guarda el id del usuario a eliminar
		setOpenConfirmDialog(true); // Abro el cuadro de dialogo
	};

	const handleConfirmDelete = async () => {
		setOpenConfirmDialog(false); // Cierra el diálogo inmediatamente
		if (userIdToDelete) {
			try {
				await deleteUser(userIdToDelete, token);
				showSnackbar("Usuario eliminado con éxito.", "success"); // Seteo los estados del snackbar
				setUsers((prevUsers) =>
					prevUsers.filter((u) => u.id !== userIdToDelete)
				); //Actualizo la lista de usuarios que estoy mostrando
				console.log(`Eliminando usuario con ID: ${userIdToDelete}`);
				setUserIdToDelete(null); // Limpia el ID
			} catch (error) {
				console.error("Error al eliminar usuario:", error);
				// Manejo de errores de la API de eliminación
				setError(
					"Error al eliminar usuario: " +
						(error.response?.data?.message ||
							error.message ||
							"Error desconocido.")
				);
				showSnackbar("Error al eliminar usuario.", "error");
			}
		}
	};

	const handleCloseConfirmDialog = () => {
		setOpenConfirmDialog(false);
		setUserIdToDelete(null); // limpio el ID del usuario a eliminar
	};

	const handleEditUser = (userId) => {
		// Traigo el usuario completo
		const userFound = users.find((u) => u.id === userId);
		if (userFound) {
			setUserToEdit(userFound); // Guarda el objeto completo del usuario en userToEdit
			setOpenEditDialog(true); // Abre el diálogo de edición
			setSaveEditError(null); // Limpia cualquier error previo
		} else {
			console.warn(`Usuario con ID ${userId} no encontrado para edición.`);
			setError("No se pudo encontrar el usuario para editar.");
		}
	};

	const handleSaveEditedUser = async (updatedUserData) => {
		const dataToSend = {
			...updatedUserData,
			// Si el backend espera 'id' en 0 cuando es una actualización,
			// o lo ignora del body, no lo incluyas aquí si ya está en la URL
			id: updatedUserData.id || 0, // Asegura que el ID vaya en el body si tu DTO lo requiere
			password: updatedUserData.password || "", // Envía vacío si no se tocó, o tu lógica específica
		};
		setIsSavingEdit(true);
		setSaveEditError(null);

		try {
			// Asegúrate de que tu servicio 'updateUser' reciba el ID y los datos
			await updateUser(updatedUserData.id, dataToSend); // Pasa el ID y el DTO completo
			showSnackbar("Usuario modificado con éxito.", "success");
			// Actualiza el estado 'users' para reflejar los cambios en la tabla
			setUsers((prevUsers) =>
				prevUsers.map((u) =>
					u.id === updatedUserData.id ? updatedUserData : u
				)
			);
			setOpenEditDialog(false); // Cierra el diálogo al guardar exitosamente
			setUserToEdit(null); // Limpia el usuario en edición
		} catch (error) {
			console.error("Error al actualizar usuario:", error);
			setSaveEditError(
				"Error al guardar cambios: " +
					(error.response?.data?.message ||
						error.message ||
						"Error desconocido.")
			);
			showSnackbar("Error al modificar usuario.", "error");
		} finally {
			setIsSavingEdit(false);
		}
	};

	const handleCloseEditDialog = () => {
		setOpenEditDialog(false);
		setUserToEdit(null); // Limpia el usuario en edición al cerrar
		setSaveEditError(null); // Limpia errores si el usuario cierra el diálogo
	};

	// --- estados para el Snackbar ---
	const [snackbarOpen, setSnackbarOpen] = useState(false);
	const [snackbarMessage, setSnackbarMessage] = useState("");
	const [snackbarSeverity, setSnackbarSeverity] = useState("success"); // 'success', 'error', 'info', 'warning'

	// --- Funciones para mostrar y cerrar el Snackbar ---
	const showSnackbar = (message, severity = "success") => {
		setSnackbarMessage(message);
		setSnackbarSeverity(severity);
		setSnackbarOpen(true);
	};

	const handleSnackbarClose = (event, reason) => {
		if (reason === "clickaway") {
			return;
		}
		setSnackbarOpen(false);
	};

	return (
		<div>
			{isLoading ? (
				<Typography variant="h3">Loading ... </Typography>
			) : error ? (
				<Container>
					<ErrorItem error={error} />
					<SnackbarItem
						open={snackbarOpen}
						message={snackbarMessage}
						severity={snackbarSeverity}
						onClose={handleSnackbarClose}
					/>
				</Container>
			) : (
				<Container>
					<Typography variant="h4" sx={{ mb: 3, mt: 3 }}>
						Gestión de Usuarios
					</Typography>
					<SnackbarItem />
					<UserTable
						rows={rows}
						isLoading={isLoading}
						error={error}
						onEditUser={handleEditUser}
						onDeleteUser={handleDeleteUser}
					/>
					<ConfirmDialog
						open={openConfirmDialog}
						title="Confirmar"
						message={`Estas por eliminar al usuario con ID ${userIdToDelete}. ¿Deseas confirmar?
						`}
						onConfirm={handleConfirmDelete}
						onClose={handleCloseConfirmDialog}
					/>
					{userToEdit && (
						<EditUserDialog
							open={openEditDialog}
							user={userToEdit} // Pasa el objeto completo del usuario a editar
							onClose={handleCloseEditDialog}
							onSave={handleSaveEditedUser}
							isSaving={isSavingEdit}
							saveError={saveEditError}
						/>
					)}
					<SnackbarItem
						open={snackbarOpen}
						message={snackbarMessage}
						severity={snackbarSeverity}
						onClose={handleSnackbarClose}
					/>
				</Container>
			)}
		</div>
	);
};
