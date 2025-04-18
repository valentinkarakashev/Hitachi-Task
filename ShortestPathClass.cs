using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitachi_Task
{
    class ShortestPathClass
    {
        public static bool ShortestPath(int row, int col, bool[] visited, int rows, int cols, string destination, string[,] matrix, string[,] solution)
        {
            if (row < 0 || row >= rows || col < 0 || col >= cols || visited[row * cols + col] || matrix[row, col] == "X")
            {
                return false;
            }

            if (matrix[row, col] == destination)
            {
                solution[row, col] = matrix[row, col];
                return true;
            }

            visited[row * cols + col] = true;
            solution[row, col] = matrix[row, col];

            bool hasdestination = ShortestPath(row + 1, col, visited, rows, cols, destination, matrix, solution) ||
            ShortestPath(row, col + 1, visited, rows, cols, destination, matrix, solution) ||
            ShortestPath(row - 1, col, visited, rows, cols, destination, matrix, solution) ||
            ShortestPath(row, col - 1, visited, rows, cols, destination, matrix, solution);

            if (!hasdestination)
            {
                solution[row, col] = "0";
                return false;
            }

            return true;
        }
    }
}
