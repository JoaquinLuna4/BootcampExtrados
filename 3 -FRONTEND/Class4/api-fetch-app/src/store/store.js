import { configureStore } from "@reduxjs/toolkit";
import contadorSlice from "./slices/contadorSlice";

const store = configureStore({
	reducer: {
		contador: contadorSlice,
	},
});

export default store;
