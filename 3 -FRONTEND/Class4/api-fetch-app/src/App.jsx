import store from "./store/store";
import { Provider } from "react-redux";
import AppRoutes from "./routes/AppRoutes";

import Redux from "./components/Redux";
import Home from "./components/Home";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

function App() {
	return (
		<Provider store={store}>
			<Router>
				<Routes>
					<Route path="/Redux/" element={<Redux />} />
					<Route path="/Home/" element={<Home />} />
				</Routes>
			</Router>
		</Provider>
	);
}

export default App;
