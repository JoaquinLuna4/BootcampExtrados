import { Routes, Route } from "react-router-dom";
import Layout from "../components/Layout";
import Home from "../views/Home";
import Login from "../views/Login";
import Cards from "../views/Cards";
import { CardAsign } from "../views/CardAsign";
import { Success } from "../views/Success";
import { CreateUser } from "../views/CreateUser";
import { Users } from "../views/Users";
import { MyDecks } from "../views/MyDecks";
import { Profile } from "../views/Profile";

import ProtectedRoute from "./ProtectedRoute";

function AppRoutes() {
	return (
		<Routes>
			<Route element={<Layout />}>
				<Route path="/" element={<Home />} />
				<Route path="/users" element={<ProtectedRoute element={<Users />} />} />
				<Route path="/create-user" element={<CreateUser />} />
				<Route path="/cards" element={<Cards />} />
				<Route path="/selected-cards" element={<CardAsign />} />
				<Route path="/my-decks" element={<MyDecks />} />
				<Route path="/profile" element={<Profile />} />
			</Route>
			<Route path="/success" element={<Success />} />
			<Route path="/login" element={<Login />} />
		</Routes>
	);
}

export default AppRoutes;
