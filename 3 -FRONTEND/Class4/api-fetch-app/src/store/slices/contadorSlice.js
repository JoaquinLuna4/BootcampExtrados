import { createSlice } from "@reduxjs/toolkit";

const contadorSlice = createSlice({
	name: "contador",
	initialState: { valor: 0 },
	reducers: {
		incrementar: (state) => {
			state.valor += 1;
		},
		decrementar: (state) => {
			state.valor -= 1;
		},
	},
});

export const { incrementar, decrementar } = contadorSlice.actions;
export default contadorSlice.reducer;
