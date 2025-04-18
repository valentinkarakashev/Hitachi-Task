using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hitachi_Task
{
    class SaveMatrixToFileClass
    {
        public static void SaveMatrixToFile(string[,] matrix, string fileName)
        {
            string[] lines = new string[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string row = string.Empty;

                for (int k = 0; k < matrix.GetLength(1); k++)
                {
                    row = row + " " + matrix[i, k];
                }

                lines[i] = row;
            }

            File.WriteAllLines(fileName, lines);
        }
    }
}
