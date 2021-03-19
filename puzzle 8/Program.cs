using System;

namespace puzzle_8
{
    class Program
    {
        //variables globales
        static int[] coords = new int[2];
        static int attepmts = 4;
        static string[,] coord = new string[3, 3];
        static int[,] memory = new int[3, 3];

        static int[,] goal = new int[,] {
                {1,2,3},
                {4,5,6},
                {7,8,0}
            };

        static int[,] puzzle8 = new int[,] {
                {1,0,3},
                {4,2,6},
                {7,8,5}
            };


        // MAIN
        static void Main(string[] args)
        {


            FindCero(puzzle8);
            LlenaMemCoords();
            Console.WriteLine("Coordenadas de Cero: " + "y -> " + coords[0] + " / x -> "+coords[1]);
            Console.WriteLine("Coordenadas guaradas en memoria: ");
            Print(coord);
            Console.WriteLine("Elementos jugables encontrados en memoria: ");
            PrintInt(memory);
            Console.WriteLine("Puzzle para solucionar: ");
            PrintInt(puzzle8);
            Console.WriteLine("Jugadas disponibles: " + attepmts);

        }

        // busca coordenadas de cero
        static void FindCero(int[,] matriz1)
        {
            for (int filas = 0; filas < 3; filas++)
            {
                for (int columnas = 0; columnas < 3; columnas++)
                {
                    if (matriz1[filas, columnas] == 0)
                    {
                        coords[0] = filas;
                        coords[1] = columnas;
                    }
                }
            }
        }


        static void LlenaMemCoords()
        {
            int y = coords[0];
            int x = coords[1];
            int up = y - 1;
            int down = y + 1;
            int left = x - 1;
            int right = x + 1;
            try
            {
                coord[y, x] = Convert.ToString(puzzle8[y, x] + "(" + y + "," + x + ")");
                memory[y, x] = puzzle8[y, x];
            }
            catch
            {
                attepmts--;
            }
            try
            {
                coord[up, x] = Convert.ToString(puzzle8[up, x] + "(" + up + "," + x + ")");
                memory[up, x] = puzzle8[up, x];
            }
            catch
            {
                attepmts--;
            }

            try
            {
                coord[down, x] = Convert.ToString(puzzle8[down, x] + "(" + down + "," + x + ")");
                memory[down, x] = puzzle8[down, x];
            }
            catch
            {
                attepmts--;
            }
            try
            {
                coord[y, left] = Convert.ToString(puzzle8[y, left] + "(" + y + "," + left + ")");
                memory[y, left] = puzzle8[y, left];
            }
            catch
            {
                attepmts--;
            }
            try
            {

                coord[y, right] = Convert.ToString(puzzle8[y, right] + "(" + y + "," + right + ")");
                memory[y, right] = puzzle8[y, right];
            }
            catch
            {
                attepmts--;
            }
        }

        // imprime matriz 
        static void PrintInt(int[,] p)
        {
            for (int filas = 0; filas < 3; filas++)
            {
                for (int columnas = 0; columnas < 3; columnas++)
                {
                    Console.Write(" " + p[filas, columnas]);
                }
                Console.WriteLine();
            }
        }
        
        static void Print(object[,] p)
        {
            for (int filas = 0; filas < 3; filas++)
            {
                for (int columnas = 0; columnas < 3; columnas++)
                {
                    Console.Write(" " + p[filas, columnas]);
                }
                Console.WriteLine();
            }
        }
    }
}
