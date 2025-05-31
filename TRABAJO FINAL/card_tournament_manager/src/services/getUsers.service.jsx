import axios from "axios";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

const urlFetch = `${API_BASE_URL}/Usuarios/listar`;

const getUsers = (token) => {
	return axios.get(urlFetch, {
		headers: {
			Authorization: `Bearer ${token}`,
		},
	});
};

export default getUsers;
