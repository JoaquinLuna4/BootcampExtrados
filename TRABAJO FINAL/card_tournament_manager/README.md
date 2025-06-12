# Card Tournament Manager

Aplicación web para la gestión de torneos de cartas, desarrollada con React, Vite, Redux Toolkit y Material UI.

## Características

- Login y autenticación de usuarios con persistencia de sesión (JWT).
- Gestión de usuarios autenticados.
- Navegación protegida según autenticación.
- Interfaz moderna y responsiva usando Material UI.
- Tema claro personalizado.
- Arquitectura modular con rutas, servicios, slices y componentes reutilizables.
- Consumo centralizado de API mediante `apiClient`.
- Servicios específicos como `cardsService` y `userService` para la lógica de negocio.
- Interceptores de Axios para manejo automático de tokens.
- Almacenamiento seguro de sesión en localStorage.
- Vistas principales: Home, Login, MyDecks, CreateUser y Usuarios.
- Estructura escalable para agregar nuevas funcionalidades y vistas.

## Estructura del proyecto

```
├── public/
│   ├── favicon.svg
│   └── robots.txt
├── src/
│   ├── App.jsx
│   ├── index.css
│   ├── main.jsx
│   ├── app/
│   ├── assets/
│   ├── components/
│   │   ├── FooterMui.jsx
│   │   ├── Layout.jsx
│   │   ├── Navbar.jsx
│   │   └── ResponsiveAppBar.jsx
│   ├── services/
│   │   ├── cardsService.js
│   │   ├── userService.js
│   │   └── apiClient.js
│   ├── routes/
│   │   ├── AppRoutes.jsx
│   │   └── ProtectedRoute.jsx
│   ├── store/
│   │   ├── store.js
│   │   └── slices/
│   │       └── authSlice.js
│   ├── themes/
│   │   └── lightTheme.js
│   ├── utils/
│   │   └── isValidMail.js
│   │   └── isValidPass.js
│   │   └── hooks/
│   │       ├── handleAuthAction.js
│   │       └── useIsAuth.js
│   └── views/
│       ├── Home.jsx
│       ├── Login.jsx
│       ├── MyDecks.jsx
│       ├── CardAsign.jsx
│       ├── Cards.jsx
│       ├── CreateUser.jsx
│       ├── Profile.jsx
│       ├── Success.jsx
│       └── Usuarios.jsx
├── .env
├── package.json
├── vite.config.js
└── README.md
```

- `public/`: Archivos estáticos como favicon y robots.txt.
- `src/`: Código fuente de la aplicación.
  - `components/`: Componentes reutilizables de la UI (Navbar, Footer, Layout, etc).
  - `routes/`: Definición de rutas y rutas protegidas.
  - `services/`: Servicios para llamadas a la API.
  - `store/`: Configuración de Redux Toolkit y slices.
  - `themes/`: Temas de Material UI.
  - `utils/`: Utilidades como validación de autenticación y validaciones de formularios.
  - `views/`: Vistas principales (Home, Login, Usuarios, MyDecks).

## Scripts disponibles

- `npm run dev` / `pnpm dev`: Inicia la aplicación en modo desarrollo.
- `npm run build` / `pnpm build`: Crea una versión optimizada para producción.
- `npm run preview` / `pnpm preview`: Previsualiza la build de producción localmente.

## Notas técnicas

- Redux Toolkit para la gestión del estado global.
- Autenticación con JWT almacenado en localStorage.
- Interceptores de Axios para añadir el token a las peticiones.
- Rutas protegidas según autenticación.
- Interfaz responsiva y moderna con Material UI.
- Arquitectura escalable y modular.
- Separación clara de responsabilidades por carpetas.

## Tecnologías principales

- React
- Vite
- Redux Toolkit
- Material UI
- Axios

## Autor

Joaquín Luna

© 2025 Joaquín Luna. Todos los derechos reservados.
