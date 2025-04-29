import React, { useState } from "react";
import "./AboutMe.css";

export default function AboutMe() {
	const [Opciones, setOpciones] = useState(0);
	console.log(Opciones, "esto es opciones");

	const toggleOpciones = () => {
		if (Opciones === 0) {
			setOpciones(1);
		} else {
			setOpciones(0);
		}
	};

	return (
		<>
			<h1 className="title">AboutMe</h1>
			<section>
				<h2 className="center p-40">
					Me encanta el desarrollo, la tecnologia, y crear cosas nuevas
				</h2>
				<p className="max-width-65 about-p">
					Lorem ipsum dolor sit amet consectetur adipisicing elit. Quos nihil
					voluptate dolorum voluptatum sunt aliquam ab inventore atque commodi
					quisquam ipsa praesentium rerum iste, ducimus culpa eius eveniet
					assumenda consequatur? Lorem ipsum dolor sit amet consectetur
					adipisicing elit. Recusandae impedit inventore cupiditate, nisi
					repellat doloremque dolores vero mollitia similique provident
					consequuntur blanditiis debitis tempora aliquam veritatis iure
					eligendi doloribus vitae. Lorem ipsum dolor sit amet consectetur
					adipisicing elit. Et beatae inventore nobis laboriosam reprehenderit
					enim odio placeat corrupti aperiam dignissimos, tempore iusto
					incidunt, dolor natus quo ab fugiat veritatis fuga?
				</p>
			</section>
			<section>
				<h2 className="center p-40">
					Podes contactarme por los siguientes links:
				</h2>
				<button className="center about-button " onClick={toggleOpciones}>
					Ver/Ocultar Opciones
				</button>

				{Opciones ? (
					<div className="center">
						<div className="social-links-grid">
							<a
								href="https://linkedin.com"
								target="_blank"
								rel="noopener noreferrer"
								className="social-link"
							>
								<img src="/src/assets/linkedin.png" alt="LinkedIn" />
								<p>LinkedIn</p>
							</a>
							<a
								href="https://facebook.com"
								target="_blank"
								rel="noopener noreferrer"
								className="social-link"
							>
								<img src="/src/assets/facebook.png" alt="Facebook" />
								<p>Facebook</p>
							</a>
							<a
								href="https://twitter.com"
								target="_blank"
								rel="noopener noreferrer"
								className="social-link"
							>
								<img src="/src/assets/twitter.png" alt="Twitter" />
								<p>Twitter</p>
							</a>
						</div>
					</div>
				) : (
					<></>
				)}
			</section>
		</>
	);
}
