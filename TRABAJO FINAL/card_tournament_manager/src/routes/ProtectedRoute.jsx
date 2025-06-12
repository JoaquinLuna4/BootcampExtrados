import { Navigate } from "react-router-dom";
import isAuthenticated from "../utils/hooks/useIsAuth";

const ProtectedRoute = ({ element }) => {
	return isAuthenticated() ? element : <Navigate to="/login" replace />;
};

export default ProtectedRoute;
