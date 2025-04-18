using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitachi_Task
{
    class CountPathsClass
    {
        static int possiblePaths = 0;

        public static int CountPaths(int startRow, int startColumn, int rows, int columns, string[,] matrix, bool[,] visited)
        {
            if (startRow < 0 || startRow >= rows || startColumn < 0 || startColumn >= columns 
                || matrix[startRow, startColumn] == "X" || visited[startRow, startColumn])
            {
                return 0;
            }

            if (matrix[startRow, startColumn] == "F")
            {
                possiblePaths++;
                return 1;
            }

            visited[startRow, startColumn] = true;

            //right = row, col + 1;  left = row, col - 1;  up = row - 1, col;  down = row + 1, col
            int total = CountPaths(startRow - 1, startColumn, rows, columns, matrix, visited) + // up
            CountPaths(startRow + 1, startColumn, rows, columns, matrix, visited) + // down
            CountPaths(startRow, startColumn - 1, rows, columns, matrix, visited) + // left
            CountPaths(startRow, startColumn + 1, rows, columns, matrix, visited); // right

            visited[startRow, startColumn] = false;

            return total;
        }
    }
}
