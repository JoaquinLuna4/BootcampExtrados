import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import { store } from "./store/store";
import { Provider } from "react-redux";
import "./index.css";
import App from "./App.jsx";
import { ThemeProvider } from "@mui/material/styles";
import { initializeAuth } from "./store/slices/authSlice";
import { AxiosInterceptor } from "./services/apiClient.js";
import lightTheme from "./themes/lightTheme.js";

const container = document.getElementById("root");

if (container) {
	const root = createRoot(container);
	store.dispatch(initializeAuth());
	AxiosInterceptor();

	root.render(
		<Provider store={store}>
			<ThemeProvider theme={lightTheme}>
				<App />
			</ThemeProvider>
		</Provider>
	);
} else {
	throw new Error(
		"Root element with ID 'root' was not found in the document. Ensure there is a corresponding HTML element with the ID 'root' in your HTML file."
	);
}
