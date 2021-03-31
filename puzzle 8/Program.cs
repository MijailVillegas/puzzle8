using System;

namespace puzzle_8
{
    class Program
    {
        static int v = 0;
        //variables globales
        static int[] Origen = new int[2];
        static int attepmts = 4;
        static string[,] coord = new string[3, 3];
        static int[,] G = new int[4,10];
        static int[,] coordx = new int[3, 3];
        static int[,] coordy = new int[3, 3];
        static int[,] memory = new int[3, 3];
        static int[,] ECXYGHS = new int[8, 7];
        static int[,] jugada = new int[3, 3];
        static int[,] jugada1 = new int[3, 3];
        static int[,] jugada2 = new int[3, 3];
        static int[,] jugada3 = new int[3, 3];
        static int[,] jugada4 = new int[3, 3];
        static int[,] mclear = new int[,] {
                {10,10,10},
                {10,10,10},
                {10,10,10}
            };

        static int[,] goal = new int[,] {
                {1,2,3},
                {4,5,6},
                {7,8,0}
            };

        static int[,] puzzle8 = new int[,] {
                {0,1,7},
                {4,2,6},
                {5,8,3}
            };


        // MAIN
        static void Main(string[] args)
        {


            FindCero(puzzle8);
            Coordenadas(puzzle8, ECXYGHS);
            LlenaMemCoords();
            CopiaMatriz(puzzle8, jugada);
            //Console.WriteLine("Elementos jugables encontrados en memoria: ");
            //PrintInt(memory);

            Movimientos(jugada);
            CompruebaJugada();
            Console.WriteLine("Primera Jugada: ");
            PrintInt(jugada1);

            Console.WriteLine("Segunda Jugada: ");
            PrintInt(jugada2);

            Console.WriteLine("Tercera Jugada: ");
            PrintInt(jugada3);

            Console.WriteLine("Cuarta Jugada: ");
            PrintInt(jugada4);

            HeuristaG(ECXYGHS);
            SumaG();
            SumaGH();
            Console.WriteLine("Heurística G: ");
            PrintInt(G);
            Console.WriteLine("ECXYGHS: ");
            PrintInt(ECXYGHS);
            //Console.WriteLine("Coordenadas de Cero: " + "y -> " + Origen[0] + " / x -> "+Origen[1]);
            //Console.WriteLine("Coordenadas guaradas en memoria: ");
            //Print(coord);
            //Console.WriteLine("Coordenadas x: ");
            //PrintInt(coordx);
            //Console.WriteLine("Coordenadas y: ");
            //PrintInt(coordy);
            //Console.WriteLine("Elementos jugables encontrados en memoria: ");
            //PrintInt(memory);
            //Console.WriteLine("Puzzle para solucionar: ");
            //PrintInt(puzzle8);
            //Console.WriteLine("Jugadas disponibles: " + attepmts);


        }

        // busca coordenadas de cero
        static void FindCero(int[,] matriz1)
        {
            for (int filas = 0; filas < matriz1.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < matriz1.GetLength(1); columnas++)
                {
                    if (matriz1[filas, columnas] == 0)
                    {
                        Origen[0] = filas;
                        Origen[1] = columnas;
                    }

                }
            }
        }
        // Llena la matriz de ECXYGHS (Elemento, Cálculo, coordenada X, coordenada Y, Heurística G, Heurística H, Suma de G+H)
        static void Coordenadas(int[,] matriz, int[,] matriz2)
        {
            for (int filas = 0; filas < matriz.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < matriz.GetLength(1); columnas++)
                {
                    for (int x = 0; x < matriz2.GetLength(0); x++)
                    {
                        for (int y = 0; y < matriz2.GetLength(1); y++)
                        {
                            ECXYGHS[x, 0] = x+1;

                            if (ECXYGHS[x, 0] == matriz[filas,columnas])
                            {
                                ECXYGHS[x, 2] = filas;
                                ECXYGHS[x, 3] = columnas;
                                int abs = Math.Abs(Origen[0] - filas) + Math.Abs(Origen[1] - columnas);
                                ECXYGHS[x, 1] = abs;
                            }             
                        }
                    }
                    coordy[filas, columnas] = filas;
                    coordx[filas, columnas] = columnas;
                }
            }
        }

        static void CompruebaJugada()
        {
            int n = 0;
            for (int filas = 0; filas < jugada1.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < jugada1.GetLength(1); columnas++)
                {
                    if (jugada1[filas,columnas] == puzzle8[filas,columnas] || jugada1[filas, columnas] == v)
                    {
                        n++;
                    }
                }
            }

            if (n==9)
            {
                CopiaMatriz(mclear,jugada1);
            }
            n = 0;
            for (int filas = 0; filas < jugada2.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < jugada2.GetLength(1); columnas++)
                {
                    if (jugada2[filas, columnas] == puzzle8[filas, columnas] || jugada2[filas, columnas] == v)
                    {
                        n++;
                    }
                }
            }

            if (n == 9)
            {
                CopiaMatriz(mclear, jugada2);
            }

            n = 0;
            for (int filas = 0; filas < jugada3.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < jugada3.GetLength(1); columnas++)
                {
                    if (jugada3[filas, columnas] == memory[filas, columnas])
                    {
                        n++;
                    }
                }
            }

            if (n == 9)
            {
                CopiaMatriz(mclear, jugada3);
            }

            n = 0;
            for (int filas = 0; filas < jugada4.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < jugada4.GetLength(1); columnas++)
                {
                    if (jugada4[filas, columnas] == memory[filas,columnas])
                    {
                        n++;
                    }
                }
            }

            if (n == 9)
            {
                CopiaMatriz(mclear, jugada4);
            }
        }
        static void LlenaMemCoords()
        {
            int y = Origen[0];
            int x = Origen[1];
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

        static void Movimientos(int[,] m)
        {
            int n = 1;
            for (int filas = 0; filas < m.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < m.GetLength(1); columnas++)
                {
                    if (memory[filas, columnas] == m[filas, columnas])
                    {
                        CopiaMatriz(puzzle8, jugada);
                        if (n == 1)
                        {
                            m[Origen[0], Origen[1]] = memory[filas, columnas];
                            m[filas, columnas] = 0;
                            memory[filas, columnas] = 0;
                            CopiaMatriz(jugada, jugada1);
                            n++;
                        }
                        else if (n == 2)
                        {
                            m[Origen[0], Origen[1]] = memory[filas, columnas];
                            m[filas, columnas] = 0;
                            memory[filas, columnas] = 0;
                            CopiaMatriz(jugada, jugada2);
                            n++;
                        }
                        else if (n == 3)
                        {
                            m[Origen[0], Origen[1]] = memory[filas, columnas];
                            m[filas, columnas] = 0;
                            memory[filas, columnas] = 0;
                            CopiaMatriz(jugada, jugada3);

                            n++;
                        }
                        else if (n == 4)
                        {
                            m[Origen[0], Origen[1]] = memory[filas, columnas];
                            m[filas, columnas] = 0;
                            memory[filas, columnas] = 0;
                            CopiaMatriz(jugada, jugada4);
                            n--;
                        }
                    }
                }
            }
        }

        static void HeuristaG(int[,] m)
        {

            for (int x = 0; x < m.GetLength(0); x++)
            {
                int n = 0;
                int aux = 1;
                if (m[x,1] == 1)
                {
                    if (jugada1[m[x,2],m[x,3]] == v)
                    {
                        G[n, 0] = m[x, 0];
                        for (int filas = 0; filas < jugada1.GetLength(0); filas++)
                        {
                            for (int columnas = 0; columnas < jugada1.GetLength(1); columnas++)
                            {
                                if (jugada1[filas,columnas]==goal[filas,columnas])
                                {
                                    G[n,aux] = 1;
                                }
                                aux++;
                            }
                        }
                    }
                    n++;
                    aux = 1;
                    if  (jugada2[m[x, 2], m[x, 3]] == v)
                    {
                        G[n, 0] = m[x, 0];
                        for (int filas = 0; filas < jugada2.GetLength(0); filas++)
                        {
                            for (int columnas = 0; columnas < jugada2.GetLength(1); columnas++)
                            {
                                if (jugada2[filas, columnas] == goal[filas, columnas])
                                {
                                    G[n, aux] = 1;
                                }
                                aux++;
                            }
                        }
                    }
                    n++;
                    aux = 1;
                    if (jugada3[m[x, 2], m[x, 3]] == v)
                    {
                        G[n, 0] = m[x, 0];
                        for (int filas = 0; filas < jugada3.GetLength(0); filas++)
                        {
                            for (int columnas = 0; columnas < jugada3.GetLength(1); columnas++)
                            {
                                if (jugada3[filas, columnas] == goal[filas, columnas])
                                {
                                    G[n, aux] = 1;
                                }
                                aux++;
                            }
                        }
                    }
                    n++;
                    aux = 1;
                    if (jugada4[m[x, 2], m[x, 3]] == v)
                    {
                        G[n, 0] = m[x, 0];
                        for (int filas = 0; filas < jugada4.GetLength(0); filas++)
                        {
                            for (int columnas = 0; columnas < jugada4.GetLength(1); columnas++)
                            {
                                if (jugada4[filas, columnas] == goal[filas, columnas])
                                {
                                    G[n, aux] = 1;
                                }
                                aux++;
                            }
                        }
                    }
                }     
            }
        }

        static void SumaG()
        {
            for (int i = 0; i < G.GetLength(0); i++)
            {
                for (int x = 0; x < ECXYGHS.GetLength(0); x++)
                {
                    if (ECXYGHS[x, 0] == G[i,0])
                    { int aux = 0;
                        for (int j = 1; j < G.GetLength(1); j++)
                        {
                            aux = aux + G[i,j];
                        }
                        ECXYGHS[x, 4] = aux;
                    }
                }
            }

        }

        static void SumaGH()
        {
            for (int x = 0; x < ECXYGHS.GetLength(0); x++)
            {
                int aux = 0;
                aux = ECXYGHS[x,4] + ECXYGHS[x,5];
                ECXYGHS[x,6] = aux;
            }
        }

       


        

        /// Copia matrices del mismo tamaño
/// <summary>
/// ///////////
/// </summary>
/// <param name="m"></param>
/// <param name="n"></param>
/// 

        static void CopiaMatriz(int[,] m, int[,] n)
        {
            for (int filas = 0; filas < m.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < m.GetLength(1); columnas++)
                {
                    n[filas, columnas] = m[filas, columnas];
                }
            }

        }

        /// Borra Fila en matriz G
        /// <summary>
        /// ////////////
        /// </summary>
        /// <param name="matriz"></param>
        /// <param name="fila"></param>
        static void BorraG(int[,] matriz, int fila)
        {
            for (int columnas = 0; columnas < matriz.GetLength(1); columnas++)
            {
                matriz[fila, columnas] = -1;
            }
        }

        // imprime matriz tipado enteros
        static void PrintInt(int[,] p)
        {
            for (int filas = 0; filas < p.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < p.GetLength(1); columnas++)
                {
                    Console.Write(" " + p[filas, columnas]);
                }
                Console.WriteLine();
            }
        }

        // imprime matriz tipado objetos

        static void Print(object[,] p)
        {
            for (int filas = 0; filas < p.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < p.GetLength(1); columnas++)
                {
                    Console.Write(" " + p[filas, columnas]);
                }
                Console.WriteLine();
            }
        }
    }
}
