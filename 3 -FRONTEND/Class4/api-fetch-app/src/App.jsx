import { useState } from "react";
import { useEffect } from "react";
import axios from "axios";
import "./App.css";

function App() {
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
	}, []);
	console.log(dogImage, "dogImage en axios");
	console.log(error, "error en axios");

	/* Levanta la bandera de isLoading cuando es llamado */
	const getRandomImg = () => {
		setIsLoading(true);
	};
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
				) : (
					<ul>
						{dogImage.map((img) => (
							<li key={img}>
								<img src={img} className="random-image center"></img>
							</li>
						))}
					</ul>
				)}

				<button className="button-random-dog" onClick={getRandomImg}>
					Get more breeds !
				</button>
			</div>
		</>
	);
}

export default App;
