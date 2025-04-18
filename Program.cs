using System.IO;
using System.Net;
using System.Net.Mail;

namespace Hitachi_Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool success = false;

            while (!success)
            {
                try
                {
                    int rows = int.Parse(Console.ReadLine());
                    int columns = int.Parse(Console.ReadLine());

                    string[,] solution = new string[rows, columns];
                    string[,] matrix = new string[rows, columns];
                    bool[,] visited = new bool[rows, columns];

                    int startRow = 0;
                    int startColumn = 0;

                    int countS = 0;
                    int countF = 0;

                    for (int row = 0; row < rows; row++)
                    {
                        string[] symbols = Console.ReadLine().Split(" ").ToArray();

                        while (symbols.Length != columns)
                        {
                            if (symbols.Length != columns)
                            {
                                Console.WriteLine($"The columns must be {columns}! Try again!");
                            }
                            symbols = Console.ReadLine().Split(" ").ToArray();
                        }

                        for (int col = 0; col < columns; col++)
                        {
                            if (symbols[col] == "S")
                            {
                                startRow = row;
                                startColumn = col;
                                countS++;
                            }

                            if (symbols[col] == "F")
                            {
                                countF++;
                            }

                            if (countS > 1 || countF > 1)
                            {
                                throw new ArgumentException("You can only use 'S' or 'F' once!");
                            }

                            if (symbols[col] != "S" && symbols[col] != "X" && symbols[col] != "O" && symbols[col] != "F")
                            {
                                throw new ArgumentException("You can use only 'S', 'X' or 'O' in the matrix!");
                            }

                            matrix[row, col] = symbols[col];
                        }
                    }

                    int possiblePaths = CountPathsClass.CountPaths(startRow, startColumn, rows, columns, matrix, visited);
                    Console.WriteLine($"Number of possible paths: {possiblePaths}");

                    PrintPathClass.PrintPath(matrix, "F", rows, columns, solution, startRow, startColumn);

                    string filePath = "D:\\Test\\matrix.txt";

                    SaveMatrixToFileClass.SaveMatrixToFile(matrix, filePath);

                    SendAttachmentClass.SendAttachment();

                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Start over!");
                }
            }        
        }
    }
}