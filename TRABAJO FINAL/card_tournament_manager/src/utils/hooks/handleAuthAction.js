// src/hooks/useAuthActions.js
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { logout } from "../../store/slices/authSlice";

/**
 * Hook personalizado para manejar acciones de autenticación (Login/Logout).
 * Proporciona una función `handleAuthAction` que redirige o despacha el logout.
 */
const useAuthActions = () => {
	const navigate = useNavigate();
	const dispatch = useDispatch();

	const handleAuthAction = (action) => {
		if (action === "Login") {
			navigate("/login");
		} else if (action === "Logout") {
			dispatch(logout());
			navigate("/login");
		}
	};

	return { handleAuthAction };
};

export default useAuthActions;
