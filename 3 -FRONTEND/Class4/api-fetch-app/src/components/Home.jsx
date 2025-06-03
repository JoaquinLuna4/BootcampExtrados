import { useState } from "react";
import { useEffect } from "react";

import axios from "axios";
import "./Home.css";

function Home() {
	const urlFetch = "https://dog.ceo/api/breeds/image/random/3";

	const [isLoading, setIsLoading] = useState(true);
	const [error, setError] = useState(null);
	const [dogImage, setDogImage] = useState([]);

	/* Ejemplo de fetching de datos nativo con Workspace */
	// useEffect(() => {
	// 	fetch(urlFetch)
	// 		.then((response) => response.json())
	// 		.then((data) => {
	// 			setIsLoading(false);
	// 			setDogImage(data.message);
	// 			console.log(data);
	// 		});
	// }, [isLoading]);
	// console.log(dogImage, "dog imageee");

	/* Ejemplo de fetching de datos nativo con Axios */
	useEffect(() => {
		axios
			.get(urlFetch)
			.then((response) => {
				console.log(response, "response axios");

				setDogImage(response.data.message);
				setIsLoading(false);
			})
			.catch((error) => {
				setError(error);
				setIsLoading(false);
			});
	}, [isLoading]);

	/* Levanta la bandera de isLoading cuando es llamado */
	const getRandomImg = () => {
		setIsLoading(true);
	};

	/*  --- Logica de extraccion de datos de raza por url ---  */
	function extraerRazaDeUrlConSplit(url) {
		try {
			const parts = url.split("/");
			// Buscamos el índice donde está 'breeds'
			const breedsIndex = parts.indexOf("breeds");

			// Si 'breeds' existe y no es la penúltima parte...
			if (breedsIndex > -1 && breedsIndex < parts.length - 2) {
				// La raza está en la(s) parte(s) siguiente(s) a 'breeds',
				// hasta la penúltima parte (que es el nombre de archivo).
				// Usamos slice para obtener esas partes y join para unirlas si hay sub-razas.
				const breedParts = parts.slice(breedsIndex + 1, parts.length - 1);
				return breedParts.join("-"); // Une "terrier", "american" con un guión si fuera el caso
			} else {
				return null; // No se encontró el patrón esperado
			}
		} catch (error) {
			console.error("Error al procesar la URL con split:", error);
			return null;
		}
	}

	return (
		<>
			<header>
				<h1>Welcome to Dogs App</h1>
				<h2 className="description">
					Here you can visit to view 3 random images of different dog breeds
				</h2>
			</header>
			<div>
				<p className="presentation">The current randoms breeds are: </p>
				{isLoading ? (
					<h2>Loading ... </h2>
				) : error ? (
					<h2>Error</h2>
				) : (
					<ul>
						{dogImage.map((img) => {
							const raza = extraerRazaDeUrlConSplit(img);
							return (
								<li key={img}>
									<p className="center mb-3">This is a copy of: {raza}</p>
									<img src={img} className="random-image center"></img>
								</li>
							);
						})}
					</ul>
				)}

				<button className="button-random-dog" onClick={getRandomImg}>
					Get more breeds !
				</button>
			</div>
		</>
	);
}

export default Home;
