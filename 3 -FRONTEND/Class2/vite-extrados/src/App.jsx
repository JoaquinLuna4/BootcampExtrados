import { BrowserRouter, Routes, Route } from "react-router-dom";
import "./App.css";
import BarNav from "./components/BarNav";
import AboutMe from "./components/AboutMe";
import Home from "./components/Home";
import Proyectos from "./components/Proyectos";

function App() {
	const ProyectsTitles = ["Calculator", "To do List", "Weather App"];

	return (
		<>
			<BrowserRouter>
				<BarNav />

				<Routes>
					{<Route path="/" element={<Home />}></Route>}
					{<Route path="/AboutMe" element={<AboutMe />}></Route>}
					{
						<Route
							path="/Proyectos"
							element={<Proyectos Titles={ProyectsTitles} />}
						></Route>
					}
				</Routes>
			</BrowserRouter>
		</>
	);
}

export default App;
