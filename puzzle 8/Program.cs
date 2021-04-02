using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace puzzle_8
{
    class Program
    {
        static int v = 0;
        static int jugadas = 1;
        static int nodos = 0;
        //variables globales
        static int[] Origen = new int[2];
        static List<int> M = new();
        static int attepmts = 4;
        static string[,] coord = new string[3, 3];
        static int[,] G = new int[4,10];
        static int[,] H = new int[4, 10];
        static int[,] coordx = new int[3, 3];
        static int[,] coordy = new int[3, 3];
        static int[,] memory = new int[3, 3];
        static int[,] ECXYGHS = new int[8, 7];
        static int[,] jugada = new int[3, 3];
        static int[,] jugada1 = new int[3, 3];
        static int[,] jugada2 = new int[3, 3];
        static int[,] jugada3 = new int[3, 3];
        static int[,] jugada4 = new int[3, 3];
        static readonly int[,] mclear = new int[,] {
                {10,10,10},
                {10,10,10},
                {10,10,10}
            };

        static int[,] goal = new int[,] {
                {1,2,3},
                {8,0,4},
                {7,6,5}
            };

        static int[,] puzzle8 = new int[,] {
                {0,2,3},
                {1,8,6},
                {7,5,4}
            };


        // MAIN
        static void Main(string[] args)
        {
            Nodo Nodoinicial = new Nodo(puzzle8);
            while (PruebaGoal() == false)
            {
                HeuristicaM();
                Console.WriteLine("Puzzle para solucionar: ");
                Console.WriteLine("--------------------");
                PrintInt(puzzle8);
                Console.WriteLine("--------------------");
                attepmts = 0;
                FindCero(puzzle8);
                Coordenadas(puzzle8, ECXYGHS);
                LlenaMemCoords();
                CopiaMatriz(puzzle8, jugada);
                Movimientos(jugada);
                //PrintList(M);
                CompruebaJugada(jugada1);
                CompruebaJugada(jugada2);
                CompruebaJugada(jugada3);
                CompruebaJugada(jugada4);
                //Console.WriteLine("Primera Jugada: ");
                //PrintInt(jugada1);

                //Console.WriteLine("Segunda Jugada: ");
                //PrintInt(jugada2);

                //Console.WriteLine("Tercera Jugada: ");
                //PrintInt(jugada3);

                //Console.WriteLine("Cuarta Jugada: ");
                //PrintInt(jugada4);
                HeuristaG(ECXYGHS);
                HeuristicaH(ECXYGHS);
                CompruebaBuffer(jugada1); ComprobarNodo(jugada1);
                CompruebaBuffer(jugada2); ComprobarNodo(jugada2);
                CompruebaBuffer(jugada3); ComprobarNodo(jugada3);
                CompruebaBuffer(jugada4); ComprobarNodo(jugada4);
                SumaG();
                SumaH();
                SumaGH();
                //PrintInt(ECXYGHS);
                //PrintInt(G);
                //PrintInt(H);
                SelectorJugada();
                Limpiar(ECXYGHS);
                Limpiar(G);
                Limpiar(H);
                Console.ReadKey();
                jugadas++;
                //PrintInt(puzzle8);
                nodos = 0;
            }
            End();

        }
        static void End()
        {
            jugadas--;
            if (jugadas > 1)
            {
                string i = "s";
                Console.WriteLine("El proceso terminó en {0} Jugada{1}", jugadas, i);
            }
            else
            {
                Console.WriteLine("El proceso terminó en {0} Jugadas", jugadas);
            }
            PrintInt(puzzle8);
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

        static void ComprobarNodo(int[,]p)
        {
            int n = 0;
            for (var i = 0; i < p.GetLength(0); i++)
            {
                for (var j = 0; j < p.GetLength(1); j++)
                {
                    if (p[i,j]==10)
                    {
                        n++;
                        if (n==9)
                        {
                            nodos++;
                            if (nodos == 4)
                            {
                                M.Clear();
                            }
                        }
                    }
                }
            }
        }

        static bool PruebaGoal()
        {
            bool estado = false;
            int count = 0;
            for (int filas = 0; filas < puzzle8.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < puzzle8.GetLength(0); columnas++)
                {
                    if (puzzle8[filas,columnas] == goal[filas,columnas])
                    {
                        count++;
                        if (count == 9)
                        {
                            estado = true;
                            return estado;
                        }
                    }
                }
            }
            return estado;
        }

        static void Limpiar(int[,]p)
        {
            for(var i=0; i < p.GetLength(0);i++)
            { 
                for (var j = 0; j < p.GetLength(1); j++)
                {
                    p[i, j] = 0;
                }
            }

        }

        /// <summary>
        /// El parámetro P recibe la Posible Jugada
        /// </summary>
        /// <param name="p"></param>
        /// M["índice"] es el elemento List<T> del buffer de la memoria
        static void CompruebaBuffer(int[,]p)
        {
            int count = 0; 
            var aux = 0; var a= M.Count/9;
            for (var b = 0; b < a; b++)
            {
                int[] matriz = new int[9];
                int k = 0;
                for (var i = aux; i < aux + 9; i++)
                {
                    matriz[k] = M[i];k++;
                    //Console.Write(matriz[i]);
                }
                int j = 0;
                for (int filas = 0; filas < p.GetLength(0); filas++)
                {
                    for (int columnas = 0; columnas < p.GetLength(0); columnas++)
                    {
                        if (matriz[j].Equals(p[filas, columnas]))
                        {
                            //Console.WriteLine("posición x" + filas + "/y" + columnas + " " + p[filas, columnas] + " es igual a " + matriz[i] + " en posición: " + i);
                            count++;
                            if (count == 9)
                            {
                                CopiaMatriz(mclear, p);
                            }
                        }
                        j++;
                    }
                }
                aux = aux + 9;
                count = 0;
            }
        }

        static void CompruebaJugada(int[,]p)
        {
            int n = 0;
            for (int filas = 0; filas < p.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < p.GetLength(1); columnas++)
                {
                    if (p[filas, columnas] == puzzle8[filas, columnas] || p[filas, columnas] == v)
                    {
                        n++;
                    }
                }
            }

            if (n == 9)
            {
                CopiaMatriz(mclear, p);
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
                            CompruebaBuffer(jugada1);
                        }
                        else if (n == 2)
                        {
                            m[Origen[0], Origen[1]] = memory[filas, columnas];
                            m[filas, columnas] = 0;
                            memory[filas, columnas] = 0;
                            CopiaMatriz(jugada, jugada2);
                            n++;
                            CompruebaBuffer(jugada2);
                        }
                        else if (n == 3)
                        {
                            m[Origen[0], Origen[1]] = memory[filas, columnas];
                            m[filas, columnas] = 0;
                            memory[filas, columnas] = 0;
                            CopiaMatriz(jugada, jugada3);
                            n++;
                            CompruebaBuffer(jugada3);
                        }
                        else if (n == 4)
                        {
                            m[Origen[0], Origen[1]] = memory[filas, columnas];
                            m[filas, columnas] = 0;
                            memory[filas, columnas] = 0;
                            CopiaMatriz(jugada, jugada4);
                            n--;
                            CompruebaBuffer(jugada4);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="matriz"></param>
        /// <param name="matriz2"></param>
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
                            ECXYGHS[x, 0] = x + 1;

                            if (ECXYGHS[x, 0] == matriz[filas, columnas])
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

                                if (jugada1[filas,columnas]!=goal[filas,columnas])
                                { 
                                    G[n,aux] = 1;
                                }
                                else if (jugada1[filas, columnas] == v)
                                {
                                    G[n, aux] = 0;
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
                                if (jugada2[filas, columnas] != goal[filas, columnas])
                                {
                                    G[n, aux] = 1;
                                }
                                else if (jugada1[filas, columnas] == v)
                                {
                                    G[n, aux] = 0;
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
                                if (jugada3[filas, columnas] != goal[filas, columnas])
                                {
                                    G[n, aux] = 1;
                                }
                                else if (jugada1[filas, columnas] == v)
                                {
                                    G[n, aux] = 0;
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
                                if (jugada4[filas, columnas] != goal[filas, columnas])
                                {
                                    G[n, aux] = 1;
                                }
                                else if (jugada1[filas, columnas] == v)
                                {
                                    G[n, aux] = 0;
                                }
                                aux++;
                            }
                        }
                    }
                }     
            }
        }

        static void HeuristicaH(int[,] m)
        {
            for (int i = 0; i < m.GetLength(0); i++)
            {
                int n = 0;
                int aux = 1;
                if (m[i, 1] == 1)
                {
                    if (jugada1[m[i, 2], m[i, 3]] == v)
                    {
                        H[n, 0] = m[i, 0];
                        for (int origenx = 0; origenx < jugada1.GetLength(0); origenx++)
                        {
                            for (int origeny = 0; origeny < jugada1.GetLength(1); origeny++)
                            {
                                for (int x = 0; x < goal.GetLength(0); x++)
                                {
                                    for (int y = 0; y < goal.GetLength(0); y++)
                                    {
                                        if (jugada1[origenx, origeny] == goal[x, y])
                                        {
                                            H[n, aux] = jugadas;
                                        }
                                    }
                                }
                                aux++;

                            }
                        }
                    }
                    n++;
                    aux = 1;

                    if (jugada2[m[i, 2], m[i, 3]] == v)
                    {
                        H[n, 0] = m[i, 0];
                        for (int origenx = 0; origenx < jugada2.GetLength(0); origenx++)
                        {
                            for (int origeny = 0; origeny < jugada2.GetLength(1); origeny++)
                            {
                                for (int x = 0; x < goal.GetLength(0); x++)
                                {
                                    for (int y = 0; y < goal.GetLength(0); y++)
                                    {
                                        if (jugada2[origenx, origeny] == goal[x, y])
                                        {
                                            H[n, aux] = jugadas;
                                        }
                                    }
                                }
                                aux++;

                            }
                        }
                    }
                    n++;
                    aux = 1;

                    if (jugada3[m[i, 2], m[i, 3]] == v)
                    {
                        H[n, 0] = m[i, 0];
                        for (int origenx = 0; origenx < jugada3.GetLength(0); origenx++)
                        {
                            for (int origeny = 0; origeny < jugada3.GetLength(1); origeny++)
                            {
                                for (int x = 0; x < goal.GetLength(0); x++)
                                {
                                    for (int y = 0; y < goal.GetLength(0); y++)
                                    {
                                        if (jugada3[origenx, origeny] == goal[x, y])
                                        {
                                            H[n, aux] = jugadas;
                                            //H[n, aux] = Math.Abs(origenx - x) + Math.Abs(origeny - y);
                                        }
                                    }
                                }
                                aux++;

                            }
                        }
                    }
                    n++;
                    aux = 1;

                    if (jugada4[m[i, 2], m[i, 3]] == v)
                    {
                        H[n, 0] = m[i, 0];
                        for (int origenx = 0; origenx < jugada4.GetLength(0); origenx++)
                        {
                            for (int origeny = 0; origeny < jugada4.GetLength(1); origeny++)
                            {
                                for (int x = 0; x < goal.GetLength(0); x++)
                                {
                                    for (int y = 0; y < goal.GetLength(0); y++)
                                    {
                                        if (jugada4[origenx, origeny] == goal[x, y])
                                        {
                                            H[n, aux] = jugadas;
                                        }
                                    }
                                }
                                aux++;
                            }
                        }
                    }
                    n++;
                    aux = 1;

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

        /// FINAL HEURISTICA G
        /// 

        static void SumaH()
        {
            for (int i = 0; i < H.GetLength(0); i++)
            {
                for (int x = 0; x < ECXYGHS.GetLength(0); x++)
                {
                    if (ECXYGHS[x, 0] == H[i, 0])
                    {
                        int aux = 0;
                        for (int j = 1; j < H.GetLength(1); j++)
                        {
                            aux = aux + H[i, j];
                        }
                        ECXYGHS[x, 5] = jugadas;
                    }
                }
            }
        }

        static void SumaGH()
        {
            for (int x = 0; x < ECXYGHS.GetLength(0); x++)
            {
                int aux = v;
                aux = ECXYGHS[x,4] + ECXYGHS[x,5];
                if (aux==v)
                {
                    aux = 1000;
                }
                ECXYGHS[x,6] = aux;
                
            }
        }

        static void HeuristicaM()
        {
            for (int filas = 0; filas < puzzle8.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < puzzle8.GetLength(0); columnas++)
                {
                    M.Add(puzzle8[filas, columnas]);
                }
            }
        }

        static void TomaJugada(int g, int h , int sgh)
        {
            for (int i = 0; i < ECXYGHS.GetLength(0); i++)
            {
                if (ECXYGHS[i, 6] == sgh || ECXYGHS[i, 5] == h || ECXYGHS[i, 4] == g)
                {
                    for (int filas = 0; filas < 3; filas++)
                    {
                        for (int columnas = 0; columnas < 3; columnas++)
                        {
                            if (jugada1[ECXYGHS[i, 2], ECXYGHS[i, 3]] == v)
                            {
                                //Console.WriteLine("jugada 1 Seleccionada");
                                CopiaMatriz(jugada1, puzzle8);
                                CopiaMatriz(mclear, jugada1);
                                CopiaMatriz(mclear, jugada2);
                                CopiaMatriz(mclear, jugada3);
                                CopiaMatriz(mclear, jugada4);
                                
                            }
                            if (jugada2[ECXYGHS[i, 2], ECXYGHS[i, 3]] == v)
                            {
                                //Console.WriteLine("jugada 2 Seleccionada");
                                CopiaMatriz(jugada2, puzzle8);
                                CopiaMatriz(mclear, jugada1);
                                CopiaMatriz(mclear, jugada2);
                                CopiaMatriz(mclear, jugada3);
                                CopiaMatriz(mclear, jugada4);
                            }
                            if (jugada3[ECXYGHS[i, 2], ECXYGHS[i, 3]] == v)
                            {
                                //Console.WriteLine("jugada 3 Seleccionada");
                                CopiaMatriz(jugada3, puzzle8);
                                CopiaMatriz(mclear, jugada1);
                                CopiaMatriz(mclear, jugada2);
                                CopiaMatriz(mclear, jugada3);
                                CopiaMatriz(mclear, jugada4);
                            }
                            if (jugada4[ECXYGHS[i, 2], ECXYGHS[i, 3]] == v)
                            {
                                //Console.WriteLine("jugada 4 Seleccionada");
                                CopiaMatriz(jugada4, puzzle8);
                                CopiaMatriz(mclear, jugada1);
                                CopiaMatriz(mclear, jugada2);
                                CopiaMatriz(mclear, jugada3);
                                CopiaMatriz(mclear, jugada4);
                            }
                        }
                    }
                }
            }
        }

        static void SelectorJugada()
        {
            int[] array = new int[8];
            for (int i = ECXYGHS.GetLength(0)-1; i >= 0 ; i--)
            {
                array[i] = ECXYGHS[i,6];
               // Console.Write(" [{0}] ", array[i]);
            }
            Array.Sort(array);
            //Console.WriteLine(array[0]);
            TomaJugada(-1, -1, array[0]);
        }

        //static void SelectorJugada()
        //{
        //    int n = 1;
        //    int mayorSGH = ECXYGHS[0, 6];
        //    int mayorH = 0;
        //    int mayorG = 0;
        //    for (int i = 1; i < ECXYGHS.GetLength(0); i++)
        //    {
        //        if (ECXYGHS[i, 6] == v && n > 2)
        //        {
        //            n = 0;
        //        }
        //        else if (ECXYGHS[i, 6] == mayorSGH)
        //        {
        //            mayorSGH = ECXYGHS[i, 6];
        //            n++;
        //        }

        //        else if (ECXYGHS[i, 6] > mayorSGH)
        //        {

        //            mayorSGH = ECXYGHS[i, 6];
        //        }
        //    }
        //    if (n > 1)
        //    {
        //        n = 1;
        //        mayorH = ECXYGHS[0, 5];
        //        for (int i = 1; i < ECXYGHS.GetLength(0); i++)
        //        {
        //            if (ECXYGHS[i, 5] == v && n > 2)
        //            {
        //                n = 0;
        //            }
        //            else if (ECXYGHS[i, 5] == mayorH)
        //            {

        //                mayorH = ECXYGHS[i, 5];
        //                n++;
        //            }

        //            else if (ECXYGHS[i, 5] > mayorH)
        //            {

        //                mayorH = ECXYGHS[i, 5];
        //            }
        //        }
        //    }
        //    if (n > 1)
        //    {
        //        n = 1;
        //        mayorG = ECXYGHS[0, 4];
        //        for (int i = 1; i < ECXYGHS.GetLength(0); i++)
        //        {
        //            if (ECXYGHS[i, 4] == v && n > 2)
        //            {
        //                n = 0;
        //            }
        //            else if (ECXYGHS[i, 4] == mayorG)
        //            {

        //                mayorG = ECXYGHS[i, 4];
        //                n++;
        //            }

        //            else if (ECXYGHS[i, 4] > mayorG)
        //            {

        //                mayorG = ECXYGHS[i, 4];
        //            }
        //        }
        //    }
        //    if (mayorG != v)
        //    {
        //        mayorH = v; mayorSGH = v;
        //        TomaJugada(mayorG, -1, -1);

        //    }
        //    if (mayorH != v)
        //    {
        //        mayorSGH = v;
        //        TomaJugada(-1, mayorH, -1);
        //    }
        //    if (mayorSGH != v)
        //    {
        //        TomaJugada(-1, -1, mayorSGH);
        //    }
        //}

        /// Copia matrices del mismo tamaño
        /// <summary>
        /// ///////////
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>

        /// Copia matrices del mismo tamaño

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

        static void PrintList(List<int> p)
        {
            Console.WriteLine("Buffer de memoria: ");
            foreach (var i in p)
            {
                int j = i;
                Console.Write(i);
                if (j == 9)
                {
                    j = 0;
                    Console.WriteLine();
                }
            }
            //for (var i = 0; i < p.Count; i++)
            //{
            //    int j = i;
            //    Console.Write(p[i]);
            //    if (j == 9)
            //    {
            //        j = 0;
            //        Console.WriteLine();
            //    }
            //}
            Console.WriteLine();
        }
    }
}
