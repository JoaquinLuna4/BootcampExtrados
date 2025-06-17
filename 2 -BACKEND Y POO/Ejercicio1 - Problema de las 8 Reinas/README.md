# Ejercicio 1: Problema de las 8 Reinas

## Descripción General

Este ejercicio aborda el clásico **"Problema de las N Reinas"**, en este caso específico para un tablero de ajedrez de 8x8. El objetivo es desarrollar un programa que encuentre todas las posibles configuraciones (o al menos una) para colocar 8 reinas en el tablero de tal manera que ninguna reina pueda atacar o "comer" a otra.

## Consigna

Crea un programa que determine y muestre cómo colocar **8 reinas** en un tablero de ajedrez de 8x8 de forma que ninguna reina esté en posición de atacar a otra.

## Consideraciones para la Implementación

* **Representación del Tablero**: Decide cómo representar el tablero de ajedrez en tu programa (por ejemplo, una matriz 2D, un array, etc.).
* **Lógica de Ataque**: Implementa la lógica para verificar si una posición es segura para colocar una reina (es decir, que no esté en la misma fila, columna o diagonal que otra reina ya colocada).
* **Algoritmo**: Este problema suele resolverse eficientemente utilizando algoritmos de **backtracking** (vuelta atrás), que exploran soluciones de forma recursiva.
* **Salida**: El programa debe ser capaz de mostrar al menos una configuración válida (o idealmente, todas las configuraciones posibles si el rendimiento lo permite). La representación de la salida debe ser clara, indicando la posición de cada reina en el tablero.

## Posibles Enfoques (Sugerencia)

* Utilizar un enfoque recursivo de backtracking para probar las posiciones.
* Mantener el estado de las filas, columnas y diagonales ocupadas para optimizar las comprobaciones.

---