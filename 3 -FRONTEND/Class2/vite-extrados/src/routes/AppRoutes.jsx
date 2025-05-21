import { Routes, Route } from "react-router-dom";
import AboutMe from "../views/AboutMe";
import Home from "../views/Home";
import Proyectos from "../views/Proyectos";
import ProtectedRoute from "./ProtectedRoute";
import Login from "../views/Login";
import Layout from "../components/Layout";

// Podría moverse a un archivo de configuración
const ProyectsTitles = ["Calculator", "To do List", "Weather App"];

function AppRoutes() {
	return (
		<Routes>
			<Route element={<Layout />}>
				<Route path="/" element={<Home />} />
				<Route path="/AboutMe" element={<AboutMe />} />
				<Route
					path="/Proyectos"
					element={
						<ProtectedRoute element={<Proyectos Titles={ProyectsTitles} />} />
					}
				/>
				<Route path="/login" element={<Login />} />
			</Route>
		</Routes>
	);
}

export default AppRoutes;
