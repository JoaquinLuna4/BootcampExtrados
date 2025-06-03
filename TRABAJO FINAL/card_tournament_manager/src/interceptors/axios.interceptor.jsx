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
