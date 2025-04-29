import { Link } from "react-router-dom";
import "./BarNav.css";
export default function BarNav() {
	return (
		<nav>
			<ul>
				<li>
					<Link to="/" className="navbar-button">
						Inicio
					</Link>
				</li>
				<li>
					<Link to="/Proyectos" className="navbar-button">
						Proyectos
					</Link>
				</li>
				<li>
					<Link to="/AboutMe" className="navbar-button">
						AboutMe
					</Link>
				</li>
			</ul>
		</nav>
	);
}
