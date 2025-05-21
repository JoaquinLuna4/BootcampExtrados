import React from "react";
import "./Proyectos.css";
export default function Proyectos({ Titles }) {
	return (
		<>
			<div className="proyectos-container">
				<section className="section-container">
					<article className="articulo-container">
						<header className="articulo-header">
							<h2 className="articulo-titulo">{Titles[0]}</h2>
						</header>
						<div className="articulo-body">
							<figure>
								<img
									src="https://calculator-1.com/images/screens/calculator_1.png"
									alt="imagen-calculadora"
									className="articulo-imagen"
								></img>
								<figcaption>Captura de pantalla de una calculadora</figcaption>
							</figure>
							<p className="articulo-descripcion">
								Lorem ipsum dolor sit amet, consectetur adipisicing elit.
								Dolorem perspiciatis minima fugit dolores! Veniam, quaerat
								soluta! Accusamus ullam officiis at, eligendi magnam aperiam sit
								pariatur assumenda fugiat neque iste eos.
							</p>
						</div>
					</article>
					<article className="articulo-container">
						<header className="articulo-header">
							<h2 className="articulo-titulo">{Titles[1]}</h2>
						</header>
						<div className="articulo-body">
							<figure>
								<img
									src="data:image/jpeg;base64,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"
									alt="imagen de app de to do list"
									className="articulo-imagen"
								></img>
								<figcaption>Captura de pantalla de una todo list</figcaption>
							</figure>
							<p className="articulo-descripcion">
								Lorem ipsum dolor sit amet consectetur adipisicing elit.
								Suscipit, distinctio voluptatum numquam tempore natus itaque
								obcaecati aspernatur minima sunt asperiores, accusantium
								consequuntur assumenda nostrum, ab repudiandae voluptatibus
								ducimus dolores placeat.
							</p>
						</div>
					</article>
					<article className="articulo-container">
						<header className="articulo-header">
							<h2 className="articulo-titulo">{Titles[2]}</h2>
						</header>
						<div className="articulo-body">
							<figure>
								<img
									src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQl-Hs1Tym0lmMMUCJEqQlRkEjEoV3Obf2D7A&s"
									alt="imagen de app de clima"
									className="articulo-imagen"
								></img>
								<figcaption>Captura de pantalla de una app de clima</figcaption>
							</figure>
							<p className="articulo-descripcion">
								Lorem ipsum dolor sit amet consectetur adipisicing elit.
								Suscipit, distinctio voluptatum numquam tempore natus itaque
								obcaecati aspernatur minima sunt asperiores, accusantium
								consequuntur assumenda nostrum, ab repudiandae voluptatibus
								ducimus dolores placeat.
							</p>
						</div>
					</article>
				</section>
			</div>
		</>
	);
}
