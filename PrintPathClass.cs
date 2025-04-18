using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitachi_Task
{
    class PrintPathClass
    {
        static int pathLength = 1;

        public static void PrintPath(string[,] matrix, string destinatio, int rows, int cols, string[,] solution, int startRow, int startCol)
        {
            bool[] visited = new bool[rows * cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    solution[i, j] = "0";
                }
            }

            for (int i = 0; i < rows * cols; i++)
            {
                visited[i] = false;
            }

            ShortestPathClass.ShortestPath(startRow, startCol, visited, rows, cols, "F", matrix, solution);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (solution[i, j] != "0" && solution[i, j] != "S")
                    {
                        pathLength++;
                    }
                }
            }

            Console.WriteLine($"Shortest path length: {pathLength - 1}");
            Console.WriteLine("Shortest path map:");

            pathLength = 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (solution[i, j] == "0" || solution[i, j] == "S")
                    {
                        Console.Write(matrix[i, j] + " ");
                    }
                    else
                    {
                        Console.Write(pathLength++ + " ");
                        matrix[i, j] = $"{pathLength - 1}";
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
