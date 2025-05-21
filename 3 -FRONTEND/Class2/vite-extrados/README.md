# Portfolio de Práctica - Ejercicio

Este proyecto es un pequeño portfolio personal desarrollado como ejercicio práctico de la etapa de Frontend. El objetivo principal fue aplicar y consolidar conceptos fundamentales de React y su ecosistema, utilizando Vite como herramienta de construcción y servidor de desarrollo.

## Descripción

El portfolio consta de tres secciones principales:

1.  **Home:** Página de bienvenida o introducción.
2.  **Proyectos:** Sección para mostrar trabajos o proyectos realizados (no son ejemplos reales).
3.  **About Me:** Una página con información sobre mí, que incluye un ejemplo práctico de renderizado condicional.

## Tecnologías Utilizadas

- **Vite:** Entorno de desarrollo rápido y herramienta de construcción optimizada para front-end moderno.
- **React:** Biblioteca principal para la construcción de la interfaz de usuario.
- **React Router DOM:** Para gestionar el enrutamiento y la navegación entre las diferentes secciones (páginas) de la aplicación.
- **CSS:** Para la estilización, combinando:
  - **Estilos Globales:** Definidos en un archivo central (`index.css` o similar) para aplicar estilos base a toda la aplicación.
  - **Estilos por Componente:** Archivos CSS específicos para cada componente, asegurando encapsulación y modularidad con archivos `.css` importados.
- **HTML Semántico:** Uso de etiquetas HTML adecuadas para mejorar la estructura, accesibilidad y SEO del sitio.

## Conceptos Clave Aplicados

Este ejercicio se centró en la práctica de varios conceptos esenciales de React y desarrollo web:

- **Estructura basada en Componentes:** La aplicación se construyó dividiendo la interfaz en componentes reutilizables y bien definidos.
- **Enrutamiento con React Router DOM:** Se implementaron rutas para navegar fluidamente entre las secciones `Home`, `Proyectos` y `About Me` sin recargar la página.
- **Paso de Props (Props Drilling):** Se practicó el envío de datos e información desde componentes padres hacia componentes hijos.
- **Manejo del Estado con `useState`:** Se utilizó el hook `useState` para gestionar el estado local dentro de los componentes. Un ejemplo claro es el botón "Ver/Ocultar" en la sección "About Me".
- **Renderizado Condicional:** Se implementó lógica para mostrar u ocultar elementos de la interfaz dinámicamente. Específicamente, se usaron **operadores ternarios** para controlar la visibilidad de cierta información en la sección "About Me" mediante un botón.
- **Estilización Modular y Global:** Se aplicó una combinación de estilos globales para la base y estilos específicos para dar apariencia única a cada componente.
- **HTML Semántico:** Se prestó atención al uso correcto de las etiquetas HTML (`<header>`, `<nav>`, `<main>`, `<section>`, `<article>`, etc.) para una estructura web significativa.

## Mejoras implementadas

- **Validación de formularios:**

  - El formulario de contacto ahora valida campos obligatorios, longitud mínima/máxima y formato de email antes de enviar.
  - Los errores se muestran en pantalla y el formulario solo se envía si todos los datos son válidos.
  - Se utiliza un objeto de errores y funciones reutilizables para validar cada campo.

- **Separación de rutas:**

  - Toda la lógica de rutas se encuentra en un componente dedicado (`AppRoutes`), facilitando la organización y el mantenimiento del código.

- **Componente Layout:**

  - Se utiliza un componente `Layout` para englobar la estructura común de la aplicación (barra de navegación y contenedor principal).

- **Estilos y feedback visual:**

  - El formulario de contacto incluye feedback visual para errores y estilos modernos.

- **Buenas prácticas de estructura:**
  - Separación clara entre vistas, componentes y rutas.
  - Código más limpio, escalable y fácil de mantener.

## Instalación y Uso

1.  **Clonar el repositorio:**

    ```bash
    git clone  https://github.com/JoaquinLuna4/BootcampExtrados.git


    cd "3 -FRONTEND/Class2/vite-extrados"
    ```

2.  **Instalar dependencias:**

    ```bash
    npm install
    # o si usas yarn
    # yarn install
    ```

3.  **Ejecutar en modo desarrollo:**

    ```bash
    npm run dev
    # o si usas yarn
    # yarn dev
    ```

    Abre tu navegador en `http://localhost:5173` (o el puerto que indique Vite).

4.  **Construir para producción:**
    ```bash
    npm run build
    # o si usas yarn
    # yarn build
    ```
    Esto generará los archivos estáticos optimizados en la carpeta `dist`.

## Objetivo del Ejercicio

Este proyecto sirvió como una excelente oportunidad para:

- Familiarizarse con el entorno de desarrollo rápido de **Vite**.
- Reforzar la creación y composición de **componentes en React**.
- Implementar **rutas** del lado del cliente con `react-router-dom`.
- Gestionar el **estado** local con `useState`.
- Practicar el paso de **props** entre componentes.
- Aplicar técnicas de **renderizado condicional**.
- Organizar y aplicar **estilos** de manera efectiva.
- Escribir **HTML semántico**.

---

Aclaro nuevamente que es un ejercicio de práctica, no concierne a un uso real.
