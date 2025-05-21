import { Link, useNavigate } from "react-router-dom";
import "./BarNav.css";

const BarNav = () => {
	const navigate = useNavigate();
	const isAuthenticated = !!localStorage.getItem("token");

	const handleLogout = () => {
		localStorage.removeItem("token");
		navigate("/");
	};

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
				<li>
					{isAuthenticated && (
						<button className="navbar-button logout-btn" onClick={handleLogout}>
							Logout
						</button>
					)}
				</li>
			</ul>
		</nav>
	);
};

export default BarNav;
