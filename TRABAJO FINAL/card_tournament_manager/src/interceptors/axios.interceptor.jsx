import axios from "axios";

export const AxiosInterceptor = () => {
	const updateHeader = (request) => {
		const token = localStorage.getItem("authToken");
		if (token) {
			request.headers["Authorization"] = `Bearer ${token}`;
		}
		return request;
	};

	axios.interceptors.request.use(updateHeader);
};

//     // Opcional: Interceptor de respuesta para manejar errores globales o logout automático
//     axios.interceptors.response.use(
//         (response) => response, // Para respuestas exitosas, solo las pasamos
//         (error) => {
//             // Ejemplo: Si el token es inválido (401), podrías forzar el logout
//             if (error.response && error.response.status === 401) {
//                 // Aquí necesitarías acceso al store de Redux para despachar logout
//                 // Esto es más complejo y lleva a la Opción 2
//                 console.error("Token expirado o inválido. Por favor, inicie sesión de nuevo.");
//                 // alert("Su sesión ha expirado. Por favor, inicie sesión de nuevo.");
//                 // No puedes usar `dispatch` directamente aquí sin pasar el store
//                 // o usar un setup más avanzado.
//             }
//             return Promise.reject(error); // Rechaza la promesa para que el error se propague al catch
//         }
//     );
// };
