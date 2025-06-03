import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Redux from "../components/Redux";
import Home from "../components/Home";
import ProtectedRoute from "./ProtectedRoute";

export default function AppRoutes() {
	return (
		<Router>
			<Routes>
				{/* <Route path="/Redux" element={<Redux />} /> */}

				<Route path="/" element={<Home />} />

				<Route
					path="/protect"
					element={<ProtectedRoute element={<Redux />} />}
				/>
			</Routes>
		</Router>
	);
}
