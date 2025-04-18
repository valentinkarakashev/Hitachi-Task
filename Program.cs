using System.IO;
using System.Net;
using System.Net.Mail;

namespace Hitachi_Task
{
    internal class Program
    {
        static int possiblePaths = 0;
        static int pathLength = 1;
        static void Main(string[] args)
        {
            //EH
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
                        //EH - expes S O X
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

                    CountPaths(startRow, startColumn, rows, columns, matrix, visited);
                    Console.WriteLine($"Number of possible paths: {possiblePaths}");

                    PrintPath(matrix, "F", rows, columns, solution, startRow, startColumn);

                    string filePath = "D:\\Test\\matrix.txt";

                    SaveMatrixToFile(matrix, filePath);

                    SendAttachment();

                    success = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Start over!");
                }
            }        
        }

        static void CountPaths(int startRow, int startColumn, int rows, int columns, string[,] matrix, bool[,] visited)
        {
            // Проверки: извън граници, препятствие, вече посетено
            if (startRow < 0 || startRow >= rows || startColumn < 0 || startColumn >= columns)
            {
                return;
            }

            if (matrix[startRow, startColumn] == "X" || visited[startRow, startColumn])
            {
                return;
            }

            // Ако сме стигнали до 'F' – намерен е път
            if (matrix[startRow, startColumn] == "F")
            {
                possiblePaths++;
                return;
            }

            // Маркираме клетката като посетена
            visited[startRow, startColumn] = true;

            //right = row, col + 1;  left = row, col - 1;  up = row - 1, col;  down = row + 1, col
            CountPaths(startRow - 1, startColumn, rows, columns, matrix, visited); // нагоре
            CountPaths(startRow + 1, startColumn, rows, columns, matrix, visited); // надолу
            CountPaths(startRow, startColumn - 1, rows, columns, matrix, visited); // наляво
            CountPaths(startRow, startColumn + 1, rows, columns, matrix, visited); // надясно

            // Отмаркираме клетката (за следващи пътища)
            visited[startRow, startColumn] = false;
        }

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

            ShortestPath(startRow, startCol, visited, rows, cols, "F", matrix, solution);

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

        static void SaveMatrixToFile(string[,] matrix, string fileName)
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

        static void SendAttachment()
        {
            string fromEmail = "valentin02kr@gmail.com";
            string password = "blhb ysps ktvr cozt";
            Console.Write("Write the email address to which you will send the attachment: ");
            string toEmail = Console.ReadLine();
            string subject = "Shortest Path CSV Report";
            string body = "In this attachment you will see the solution of the task.";
            string attachmentPath = "D:\\Test\\matrix.txt";

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(fromEmail);
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = body;

                if (File.Exists(attachmentPath))
                {
                    Attachment attachment = new Attachment(attachmentPath);
                    mail.Attachments.Add(attachment);
                }
                else
                {
                    Console.WriteLine("Attachment file not found.");
                    return;
                }

                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential(fromEmail, password);
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                Console.WriteLine("Attachment file was sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
            }
        }
    }
}