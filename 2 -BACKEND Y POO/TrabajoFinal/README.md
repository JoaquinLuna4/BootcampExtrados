# Trabajo Final Backend - Administración de Torneos de Cartas Coleccionables Online

Este proyecto consiste en el desarrollo de un backend para un sistema de administración de torneos de eliminaciones online de juegos de cartas coleccionables. Se implementó utilizando **.NET 8** y **MySQL** como base de datos.

## Funcionalidades Principales

El sistema gestiona cartas, mazos, juegos/partidas y torneos, con las siguientes características clave:

*   **Cartas:** Pertenecen a una o múltiples series y poseen atributos como ilustración, ataque y defensa.
*   **Mazos:** Compuestos por 15 cartas únicas.
*   **Juegos/Partidas:** Enfrentamientos 1 vs 1 con un máximo de 30 minutos de duración y un ganador definido.
*   **Torneos:** Organizados por un "Organizador" con la asignación de "Jueces" y la planificación de juegos en base a la cantidad de jugadores y el tiempo disponible. Los torneos se dividen en tres fases:
    *   **Registro:** Los jugadores se registran con su mazo (compuesto por cartas de su colección y pertenecientes a las series habilitadas para el torneo). El registro se realiza manualmente por el organizador para simplificar el alcance del proyecto.
    *   **Torneo:** El sistema planifica los juegos por rondas, eliminando perdedores y avanzando a la siguiente ronda automáticamente.
    *   **Finalización:** Se declara al ganador del torneo.

## Roles de Usuario

El sistema define los siguientes roles con sus respectivos permisos:

*   **Administrador:**
    *   CRUD de otros usuarios (administradores, organizadores, jueces y jugadores).
    *   Visualización y cancelación de torneos.
*   **Organizador:**
    *   CRUD de torneos.
    *   Creación de jueces.
    *   Avance de fases del torneo.
    *   Modificación de juegos del torneo.
*   **Juez:**
    *   Oficialización de resultados de juegos dentro de un torneo autorizado.
    *   Descalificación de jugadores.
*   **Jugador:**
    *   Registro de su colección de cartas.
    *   Registro en torneos y creación de mazos.

## Información Gestionada

El sistema almacena y gestiona la siguiente información relevante:

*   **Jugador:** Nombre, Alias (único y visible para otros jugadores), País, Email, Juegos Ganados/Perdidos, Torneos Ganados, Descalificaciones, Foto/Avatar, Lista de cartas.
*   **Juez:** Nombre, Alias (único y visible para otros jugadores), Email, País, Torneos Oficializados, Foto/Avatar.
*   **Organizador:** Nombre, Email, País, Torneos Organizados.
*   **Administrador:** Usuario.
*   **Torneo:** Fecha y hora de inicio/fin, País, Lista de jueces, Series de cartas habilitadas, Fase, Jugadores inscriptos, Mazos de cada jugador, Resultados de juegos, Ganador.
*   **Juegos:** Fecha-Hora inicio/fin, Jugadores, Torneo, Ganador.
*   **Cartas:** Ilustración, Ataque, Defensa, Serie.
*   **Series:** Nombre, Lista de cartas, Fecha de salida.

## Notas Adicionales

*   Se registra quién creó a cada organizador/juez.
*   Los jueces solo pueden oficializar juegos de torneos en los que están autorizados y una vez que el juego ha comenzado.
*   Los jugadores y jueces solo ven los alias de otros jugadores y jueces. Los nombres reales solo son visibles para organizadores y administradores.
*   Los alias de jugadores y jueces son únicos.

## Tecnologías

*   .NET 8
*   MySQL
