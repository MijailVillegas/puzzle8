using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace puzzle_8
{
    class Nodo
    {
        public List<Nodo> hijos = new List<Nodo>();
        public Nodo padre;
        public int[,] puzzle = new int[3,3];
        public int index = 0;
        static int[,] goal = new int[,] {
                {1,2,3},
                {8,0,4},
                {7,6,5}
            };

        public Nodo(int[,]p)
        {

        }

        public void AgregarPuzzle(int[,]p)
        {
            for (var i = 0; i < p.GetLength(0); i++)
            {
                for (var j = 0; j < p.GetLength(0); j++)
                {
                    this.puzzle[i, j] = p[i, j];
                }
            }
        }

        public bool PruebaGoal()
        {
            bool estado = false;
            int count = 0;
            for (int filas = 0; filas < puzzle.GetLength(0); filas++)
            {
                for (int columnas = 0; columnas < puzzle.GetLength(0); columnas++)
                {
                    if (puzzle[filas, columnas] == goal[filas, columnas])
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


    }
}
