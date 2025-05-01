# Visor de Imágenes de Perros - Ejercicio API Bootcamp React

Este proyecto es un ejercicio práctico desarrollado durante la etapa de frontend, centrado en el consumo de APIs externas desde una aplicación React. El objetivo principal fue practicar cómo realizar peticiones HTTP utilizando tanto el API `Workspace` nativo como la biblioteca `axios`, y manejar los datos resultantes dentro de un componente React utilizando hooks.

## Descripción

La aplicación consume la API pública de [Dog CEO](https://dog.ceo/dog-api/) para obtener y mostrar 3 imágenes aleatorias de perros. Este ejercicio sirvió para poner en práctica:

1.  La realización de llamadas a una API REST.
2.  El uso del hook `useEffect` para ejecutar efectos secundarios (como el fetching de datos).
3.  El manejo del estado con `useState` para almacenar y mostrar los datos obtenidos.
4.  La comparación práctica entre `Workspace` y `axios` para realizar solicitudes HTTP.

## Tecnologías Utilizadas

- **Vite:** Entorno de desarrollo y herramienta de construcción rápida para front-end.
- **React:** Biblioteca principal para construir la interfaz de usuario.
- **`Workspace` API:** Interfaz nativa del navegador para realizar peticiones de red asíncronas.
- **`axios`:** Biblioteca popular de cliente HTTP basada en promesas (se utilizó en el ejercicio para comparar su sintaxis y características con `Workspace`).
- **CSS:** Para estilos básicos de visualización de las imágenes y la estructura.
- **HTML:** Para la maquetación semántica básica de la aplicación.

## Conceptos Clave Aplicados

Este ejercicio se enfocó en los siguientes conceptos fundamentales:

- **Consumo de API Externa:** Conexión y obtención de datos desde la API pública de Dog CEO (`https://dog.ceo/dog-api/`).
- **Peticiones HTTP Asíncronas:** Implementación de solicitudes `GET` para traer las URLs de las imágenes utilizando:
  - **`Workspace`:** Se utilizó para demostrar el método estándar incorporado en los navegadores modernos.
  - **`axios`:** Se incluyó como parte de la práctica para familiarizarse con esta librería común en el ecosistema React, notando diferencias en la configuración y el manejo de respuestas/errores.
- **Hook `useEffect`:** Se utilizó para realizar la llamada a la API una vez que el componente se monta en el DOM, gestionando así los efectos secundarios de manera controlada.
- **Hook `useState`:** Esencial para almacenar las URLs de las imágenes recibidas de la API y potencialmente para manejar estados de carga (`isLoading` y `setIsLoading`).
- **Renderizado de Datos Dinámicos:** Mostrar las imágenes en la interfaz de usuario una vez que las URLs han sido obtenidas y almacenadas en el estado del componente.
- **Entorno de Desarrollo Moderno:** Aprovechamiento de las ventajas de Vite para un ciclo de desarrollo ágil (HMR - Hot Module Replacement, build optimizado).

## Instalación y Uso

1.  **Clonar el repositorio:**

    ```bash
    git clone  https://github.com/JoaquinLuna4/BootcampExtrados.git
    cd "3 -FRONTEND/Class4/api-fetch-app"
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

    Abre tu navegador en la dirección local que indique Vite (usualmente `http://localhost:5173`).

4.  **Construir para producción:**
    ```bash
    npm run build
    # o si usas yarn
    # yarn build
    ```
    Los archivos optimizados para producción se generarán en la carpeta `dist`.

## API de Referencia

- **Dog CEO API:** [https://dog.ceo/dog-api/](https://dog.ceo/dog-api/)
  - Endpoint utilizado (ejemplo para imágenes aleatorias): `https://dog.ceo/api/breeds/image/random/3`

## Objetivo del Ejercicio

El propósito central de este proyecto fue ganar experiencia práctica en:

- La **interacción con APIs externas** desde React.
- El uso efectivo de **`useEffect`** para manejar efectos secundarios como el fetching de datos.
- La gestión del **estado asíncrono** con `useState`.
- La comparación y aplicación de dos métodos populares para hacer peticiones HTTP: **`Workspace` y `axios`**.
- El renderizado de **datos obtenidos de forma asíncrona**.

---

Aclaro nuevamente que es un ejercicio de práctica, no concierne a un uso real.
