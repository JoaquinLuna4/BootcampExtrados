import { Outlet } from "react-router-dom";
import BarNav from "./BarNav";

function Layout() {
	return (
		<div className="app-container">
			<BarNav />
			<Outlet />
		</div>
	);
}

export default Layout;
