import axios from "axios";
import store from "../store/store";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

// Crea una instancia de Axios
const apiClient = axios.create({
	baseURL: API_BASE_URL,
	timeout: 5000, // Establece un tiempo de espera de 5 segundos
	headers: {
		"Content-Type": "application/json",
	},
});

//Traigo el interceptor acá en vez de en un archivo separado como lo tenia
export const AxiosInterceptor = () => {
	const updateHeader = (request) => {
		//Saco el token de Redux
		const token = store.getState().auth.token;
		if (token) {
			request.headers["Authorization"] = `Bearer ${token}`;
		}
		return request;
	};

	apiClient.interceptors.request.use(updateHeader);
};
export default apiClient;
