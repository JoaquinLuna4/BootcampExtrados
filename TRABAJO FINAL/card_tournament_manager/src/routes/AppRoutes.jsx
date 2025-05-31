import { Routes, Route } from "react-router-dom";
import Layout from "../components/Layout";
import Home from "../views/Home";
import Login from "../views/Login";
import { Usuarios } from "../views/Usuarios";
import ProtectedRoute from "./ProtectedRoute";

function AppRoutes() {
	return (
		<Routes>
			<Route element={<Layout />}>
				<Route path="/" element={<Home />} />
				<Route
					path="/users"
					element={<ProtectedRoute element={<Usuarios />} />}
				/>
			</Route>
			<Route path="/login" element={<Login />} />
		</Routes>
	);
}

export default AppRoutes;
