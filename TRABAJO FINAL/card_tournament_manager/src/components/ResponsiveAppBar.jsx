import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import AdbIcon from "@mui/icons-material/Adb";
import { useTheme } from "@mui/material/styles"; // Importa useTheme
import { useNavigate, Link } from "react-router-dom";
import useIsAuth from "../utils/hooks/useIsAuth";
import useAuthActions from "../utils/hooks/handleAuthAction";

import { TbCards } from "react-icons/tb";

const pages = [
	{ title: "Home", path: "/" },
	{ title: "Crear usuario", path: "/create-user" },
	{ title: "Usuarios", path: "/users" },
];
const permanentSettings = [
	{ title: "Perfil", path: "/profile" },
	{ title: "Mis Mazos", path: "/my-decks" },
];
function ResponsiveAppBar() {
	const [anchorElNav, setAnchorElNav] = React.useState(null);
	const [anchorElUser, setAnchorElUser] = React.useState(null);
	const theme = useTheme();

	const navigate = useNavigate();
	const { handleAuthAction } = useAuthActions();

	//Llamo a mi hook personalizado para validar si está autenticado
	const isAuth = useIsAuth();

	const handleOpenNavMenu = (event) => {
		setAnchorElNav(event.currentTarget);
	};
	const handleOpenUserMenu = (event) => {
		setAnchorElUser(event.currentTarget);
	};

	const handleCloseNavMenu = () => {
		setAnchorElNav(null);
	};

	const handleCloseUserMenu = () => {
		setAnchorElUser(null);
	};

	return (
		<AppBar
			position="static"
			sx={{ backgroundColor: theme.palette.primary.main }}
		>
			{" "}
			{/* Usa el color primario del tema */}
			<Container maxWidth="xl">
				<Toolbar disableGutters>
					<TbCards
						sx={{
							display: { xs: "none", md: "flex" },

							color: theme.palette.secondary.main,
						}}
					/>{" "}
					{/* Icono en color secundario */}
					<Typography
						variant="h6"
						noWrap
						component="a"
						href="#app-bar-with-responsive-menu"
						sx={{
							mr: 2,
							display: { xs: "none", md: "flex" },
							fontFamily: "monospace",
							fontWeight: 700,
							letterSpacing: ".3rem",
							color: "inherit", // Hereda el color del AppBar (que ya definimos)
							textDecoration: "none",
						}}
					>
						CARD MANAGER
					</Typography>
					<Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
						<IconButton
							size="large"
							aria-label="account of current user"
							aria-controls="menu-appbar"
							aria-haspopup="true"
							onClick={handleOpenNavMenu}
							color="inherit"
						>
							<MenuIcon />
						</IconButton>
						<Menu
							id="menu-appbar"
							anchorEl={anchorElNav}
							anchorOrigin={{
								vertical: "bottom",
								horizontal: "left",
							}}
							keepMounted
							transformOrigin={{
								vertical: "top",
								horizontal: "left",
							}}
							open={Boolean(anchorElNav)}
							onClose={handleCloseNavMenu}
							sx={{ display: { xs: "block", md: "none" } }}
						>
							{pages.map((page) => (
								<MenuItem
									key={page.title}
									onClick={() => {
										handleCloseNavMenu();
										navigate(page.path);
									}}
								>
									<Typography sx={{ textAlign: "center" }}>
										{page.title}
									</Typography>
								</MenuItem>
							))}
						</Menu>
					</Box>
					<AdbIcon
						sx={{
							display: { xs: "flex", md: "none" },

							color: theme.palette.primary.main,
						}}
					/>
					<Typography
						variant="h5"
						noWrap
						component="a"
						onClick={() => navigate("/")}
						sx={{
							mr: 2,
							display: { xs: "flex", md: "none" },
							flexGrow: 1,
							fontFamily: "monospace",
							fontWeight: 700,
							letterSpacing: ".3rem",
							color: theme.palette.secondary.main,
							textDecoration: "none",
						}}
					>
						CARD MANAGER
					</Typography>
					<Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
						{pages.map((page) => (
							<Button
								key={page.title}
								onClick={() => {
									handleCloseNavMenu();
									navigate(page.path);
								}}
								sx={{ my: 2, color: "white", display: "block" }}
							>
								{page.title}
							</Button>
						))}
					</Box>
					<Box sx={{ flexGrow: 0 }}>
						<Tooltip title="Open settings">
							<IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
								<Avatar alt="Remy Sharp" src="/static/images/avatar/2.jpg" />
							</IconButton>
						</Tooltip>
						<Menu
							sx={{ mt: "45px" }}
							id="menu-appbar"
							anchorEl={anchorElUser}
							anchorOrigin={{
								vertical: "top",
								horizontal: "right",
							}}
							keepMounted
							transformOrigin={{
								vertical: "top",
								horizontal: "right",
							}}
							open={Boolean(anchorElUser)}
							onClose={handleCloseUserMenu}
						>
							{permanentSettings.map((setting) => (
								<MenuItem
									key={setting.title}
									onClick={() => {
										handleCloseUserMenu();
										navigate(setting.path);
									}}
								>
									<Typography sx={{ textAlign: "center" }}>
										{setting.title}
									</Typography>
								</MenuItem>
							))}
							{isAuth ? (
								<MenuItem
									key="Logout"
									onClick={() => handleAuthAction("Logout")}
								>
									<Typography sx={{ textAlign: "center" }}>Logout</Typography>
								</MenuItem>
							) : (
								<MenuItem key="Login" onClick={() => handleAuthAction("Login")}>
									<Typography sx={{ textAlign: "center" }}>Login</Typography>
								</MenuItem>
							)}
						</Menu>
					</Box>
				</Toolbar>
			</Container>
		</AppBar>
	);
}
export default ResponsiveAppBar;
