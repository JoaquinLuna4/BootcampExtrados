# Trabajo Final Backend - Sistema de Administración de Torneos Online de TCG

## Curso: Extrados 2024

## Introducción

Este repositorio contiene la implementación del backend para un sistema de administración de torneos de eliminación directa de juegos de cartas coleccionables (TCG) online. El proyecto aborda la complejidad de gestionar desde la definición de cartas y mazos, hasta la planificación y ejecución de torneos con múltiples fases y roles de usuario definidos.

## Conceptos Clave del Sistema

### Cartas y Series

- Las cartas se organizan en **"series"**.
- Una carta puede pertenecer a una o **múltiples series**.

### Mazos

- Un mazo de cartas se compone de **15 cartas únicas** (sin repeticiones).

### Juegos / Partidas

- Cada juego dura un **máximo de 30 minutos**.
- Es un enfrentamiento **1 vs 1**.
- Siempre hay un **ganador** (no se permiten empates).

### Torneos

- Un torneo es **organizado por un "Organizador"**.
- Los "Jueces" son asignados para **oficializar los resultados**.
- El sistema debe **planificar los días y horarios** de los juegos basándose en la cantidad de jugadores y el tiempo disponible.
- Cada torneo puede **limitar las series de cartas** con las que se puede jugar.

### Fases del Torneo

El ciclo de vida de un torneo se divide en tres fases principales:

1.  **Fase 1: Registro**

    - Los jugadores se registran con el mazo de cartas con el que participarán.
    - El mazo debe estar armado únicamente con cartas del jugador y de las series permitidas en el torneo.
    - El registro de jugadores en esta fase es **finalizado manualmente por el Organizador**.

2.  **Fase 2: Torneo (Rondas de Juego)**

    - El sistema **impide nuevas inscripciones**.
    - Se **planifican los días y horarios** de los juegos necesarios, así como los emparejamientos de jugadores para la primera ronda.
    - Esta fase consta de **N mini-fases o rondas de juegos**, donde los perdedores de la ronda anterior son eliminados.
    - El sistema **re-planifica automáticamente** los juegos con los ganadores para la siguiente ronda una vez que todos los juegos de la ronda actual han finalizado.

3.  **Fase 3: Finalización**
    - Una vez que el juego final se juega y su resultado es oficializado, el torneo pasa al estado **"finalizado"**.

## Roles de Usuario

El sistema define los siguientes roles, con diferentes niveles de acceso y responsabilidades:

- **Administrador:**

  - Puede **crear, editar y eliminar** otros administradores, organizadores, jueces y jugadores.
  - Puede **ver y cancelar** torneos.
  - Debe existir **al menos un administrador** predefinido en la base de datos; no es posible el auto-registro de administradores.
  - Puede ver quién creó a qué Organizador/Juez.

- **Organizador:**

  - Puede **crear, editar y cancelar torneos**.
  - Puede **crear Jueces**.
  - Puede **avanzar un torneo a la siguiente fase**.
  - Puede **modificar los juegos** de un torneo.
  - Debe ser **creado por un Administrador**.

- **Juez:**

  - Puede **oficializar el resultado** de un juego dentro de un evento.
  - Puede **descalificar** a un jugador de un evento si es necesario.
  - Debe ser **creado por un Administrador u Organizador**.
  - Solo puede oficializar juegos de **torneos en los que esté autorizado** y solo después de que el juego haya comenzado.
  - Su `alias` debe ser **único**.

- **Jugador:**
  - Puede **registrar las cartas** que posee en su colección.
  - Puede **registrarse en un torneo** y armar un mazo con sus cartas para participar.
  - Puede **registrarse en el sistema por su cuenta**.
  - Su `alias` debe ser **único**.

## Información Básica a Guardar / Calcular

La siguiente información se espera que el sistema pueda devolver al frontend. Esto implica que la base de datos debe almacenar los datos necesarios para calcular o proveer esta información, aunque no cada ítem sea necesariamente un campo directo en una tabla.

### Jugador

- Nombre y apellido
- Alias
- País
- Email
- Juegos ganados
- Juegos perdidos
- Torneos ganados
- Descalificaciones y su razón
- Foto/avatar
- Lista de cartas que posee.
- **Visibilidad**: Solo el `alias` es visible para otros jugadores y jueces. Organizadores y Administradores pueden ver el nombre y apellido.

### Juez

- Nombre y apellido
- Alias
- Email
- País
- Torneos que oficializó
- Foto/avatar
- **Visibilidad**: Solo el `alias` es visible para jugadores. Organizadores y Administradores pueden ver el nombre y apellido.

### Organizador

- Nombre y apellido
- Email
- País
- Torneos que organizó

### Administrador

- Usuario (nombre de usuario)

### Torneo

- Fecha y hora de inicio
- Fecha y hora de fin
- País
- Lista de jueces asignados
- Lista de series de cartas habilitadas
- Fase actual (Registro, Torneo, Finalización)
- Jugadores inscriptos
- Mazo de cartas con el que entró cada jugador
- Resultados de cada juego
- Ganador

### Juegos / Partidas

- Fecha-Hora inicio
- Fecha-Hora fin
- Jugadores participantes
- Torneo al que pertenece
- Ganador

### Cartas

- Ilustración (URL o referencia)
- Ataque
- Defensa
- Serie a la que pertenece

### Series

- Nombre
- Lista de cartas que pertenecen a esta serie
- Fecha de salida de la serie

## Arquitectura y Tecnologías

La solución de backend se desarrolló utilizando **ASP.NET Core** como framework principal. Para mantener una arquitectura modular y facilitar la gestión de las entidades y lógicas de negocio, se hizo uso de una biblioteca de clases dedicada:

- **`LibraryTrabajoFinal`**: Esta biblioteca encapsula las definiciones de modelos, interfaces, y posiblemente la lógica de acceso a datos (DAOs) y servicios que interactúan directamente con la base de datos y/o aplican reglas de negocio complejas. Su objetivo es desacoplar estas capas del proyecto principal de la API.

---
