import apiClient from "./apiClient";

export const createUserAdmin = async (userData) => {
	const response = await apiClient.post(
		"/usuarios/crear-administrador",
		userData
	);
	return response.data;
};

export const createUserOrganizador = async (userData) => {
	const response = await apiClient.post(
		"/usuarios/crear-organizador",
		userData
	);
	return response.data;
};
export const createUserJuez = async (userData) => {
	const response = await apiClient.post("/usuarios/crear-juez", userData);
	return response.data;
};

export const deleteUser = async (userId) => {
	const response = await apiClient.delete(`/usuarios/${userId}`);
	return response.data;
};

export const updateUser = async (userId, userData) => {
	const response = await apiClient.put(`/usuarios/${userId}`, userData);
	return response.data;
};

export const getUsers = async () => {
	const response = await apiClient.get(`/Usuarios/listar`);
	return response.data;
};
