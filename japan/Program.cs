using System;
using System.Collections.Generic;
using System.Linq;

namespace JapanCross

{
    class Program
    {
        
        const int rows = 5;
        const int cols = 5;

       
        static int[,] picture = new int[rows, cols]
        {
{1, 1, 0, 1, 1},
{1, 1, 0, 1, 1},
{0, 0, 1, 0, 0},
{1, 0, 0, 0, 1},
{0, 1, 1, 1, 0}
        };

   
        static List<int>[] rowHints = new List<int>[rows];
        static List<int>[] colHints = new List<int>[cols];

        
        static char[,] board = new char[rows, cols];

        
        const char empty = '.';
        const char filled = '#';
        const char crossed = '.';

        static void Main(string[] args)
        {
            GenerateHints();
            ClearBoard();

            bool isSolved = false;
            while (!isSolved)
            {
                PrintBoard();

                Console.WriteLine("Введите координаты клетки (строка и столбец) и действие (закрасить F или очистить D):");
                Console.WriteLine("Пример: 2 4 F");
                Console.WriteLine("Для выхода из игры введите Q");
                string input = Console.ReadLine();
                if (input == "Q" || input == "q")
                {
                    break;
                }

                if (!IsValidInput(input))
                {
                    Console.WriteLine("Неверный формат ввода. ");
                    continue;
                }

                ProcessInput(input);

                isSolved = IsSolved();
                if (isSolved)
                {
                    Console.WriteLine("Поздравляем! Вы отгадали рисунок!");
                    PrintBoard();
                }
            }
        }

        static void GenerateHints()
        {
            for (int i = 0; i < rows; i++)
            {
                rowHints[i] = new List<int>();
                int count = 0;
                for (int j = 0; j < cols; j++)
                {
                    if (picture[i, j] == 1)
                    {
                        count++;
                    }
                    else
                    {
                        if (count > 0)
                        {
                            rowHints[i].Add(count);
                            count = 0;
                        }
                    }
                }
                if (count > 0)
                {
                    rowHints[i].Add(count);
                }
            }

            for (int j = 0; j < cols; j++)
            {
                colHints[j] = new List<int>();
                int count = 0;
                for (int i = 0; i < rows; i++)
                {
                    if (picture[i, j] == 1)
                    {
                        count++;
                    }
                    else
                    {
                        if (count > 0)
                        {
                            colHints[j].Add(count);
                            count = 0;
                        }
                    }
                }
                if (count > 0)
                {
                    colHints[j].Add(count);
                }
            }
        }

        static void ClearBoard()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    board[i, j] = empty;
                }
            }
        }

        static void PrintBoard()
        {
            int maxColHints = colHints.Max(h => h.Count);
            for (int i = 0; i < maxColHints; i++)
            {
                Console.Write(" ");
                for (int j = 0; j < cols; j++)
                {
                    if (i < maxColHints - colHints[j].Count)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.Write(colHints[j][i - (maxColHints - colHints[j].Count)] + " ");
                    }
                }
                Console.WriteLine();
            }

            Console.Write(" ");
            for (int j = 0; j < cols; j++)
            {
                Console.Write("--");
            }
            Console.WriteLine();

            for (int i = 0; i < rows; i++)
            {
                Console.Write("| ");
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.Write("| ");
                foreach (var hint in rowHints[i])
                {
                    Console.Write(hint + " ");
                }
                Console.WriteLine();
            }

            Console.Write(" ");
            for (int j = 0; j < cols; j++)
            {
                Console.Write("--");
            }
            Console.WriteLine();
        }

        static bool IsValidInput(string input)
        {
            string[] parts = input.Split();
            if (parts.Length != 3)
            {
                return false;
            }

            int row, col;
            if (!int.TryParse(parts[0], out row) || !int.TryParse(parts[1], out col))
            {
                return false;
            }
            if (row < 1 || row > rows || col < 1 || col > cols)
            {
                return false;
            }

            char action = parts[2][0];
            if (action != 'D' && action != 'd' && action != 'F' && action != 'f')
            {
                return false;
            }

            return true;
        }

        static void ProcessInput(string input)
        {
            string[] parts = input.Split();

            int row = int.Parse(parts[0]) - 1;
            int col = int.Parse(parts[1]) - 1;
            char action = parts[2][0];

            if (action == 'D' || action == 'd')
            {
                board[row, col] = crossed;
            }
            else if (action == 'F' || action == 'f')
            {
                board[row, col] = filled;
            }
        }

        static bool IsSolved()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (board[i, j] == filled && picture[i, j] != 1)
                    {
                        return false;
                    }

                    if (board[i, j] != filled && picture[i, j] == 1)
                    {
                        return false;
                    }
                }
            }

            
            return true;
        }
    }
}




