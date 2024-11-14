using System;

namespace OchoReinas
{
    class Program
    {
        static int N = 8; // Tamaño del tablero

        static bool SePuede(int[,] board, int row, int col)
        {
            // Verifica si una reina se puede colocar en (row, col) 
            int i, j;

            /* Valida todas las filas en la columna*/
            for (i = 0; i < row; i++)
                if (board[i, col] == 1)
                    return false;

            /* La diagonal izquierda */
            for (i = row, j = col; i >= 0 && j >= 0; i--, j--)
                if (board[i, j] == 1)
                    return false;

            /* La diagonal derecha*/
            for (i = row, j = col; i >= 0 && j < N; i--, j++)
                if (board[i, j] == 1)
                    return false;

            return true;
        }

        static bool ColocarReinas(int[,] board, int row)
        {
           
            if (row >= N)
                return true;

           
            for (int col = 0; col < N; col++)
            {
              
                if (SePuede(board, row, col))
                {   
                    board[row, col] = 1;

                   
                    if (ColocarReinas(board, row + 1) == true)
                        return true;

                   
                    board[row, col] = 0; // BACKTRACKING
                }
            }

           
            return false;
        }

        static void Solucion(int[,] board)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                    Console.Write(" " + board[i, j]);
                Console.WriteLine();
            }
        }

     
        static void Main(string[] args)
        {
            int[,] board = new int[N, N];

            if (ColocarReinas(board, 0) == false)
            {
                Console.WriteLine("Solution does not exist");
            }

            Solucion(board);
        }
    }
}