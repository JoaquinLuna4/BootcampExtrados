import React from "react";

/// Import de Redux ///
import { useDispatch, useSelector } from "react-redux";
import { decrementar, incrementar } from "../store/slices/contadorSlice";

// Import Css
import "./Redux.css";

export default function Redux() {
	//////// Redux toolkit  ///////////

	const valor2 = useSelector((state) => state.contador.valor);
	const dispatch = useDispatch();
	console.log(valor2);

	return (
		<>
			<h1>Redux</h1>
			<div className="Redux">
				<p>{valor2}</p>
				<div className="button-random-dog">
					<button
						onClick={() => dispatch(incrementar())}
						className="button-primary"
					>
						{" "}
						Incrementar
					</button>
					<button
						onClick={() => dispatch(decrementar())}
						className="button-primary"
					>
						{" "}
						Decrementar
					</button>
				</div>
			</div>
		</>
	);
}
