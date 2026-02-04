using System;

namespace TictacToe
{
    class UI
    {
        // display welcome message
        public static void DisplayWelcome()
        {
            Console.WriteLine("Welcome to Tic Tac Toe!");
            Console.WriteLine("You are X, AI is O");
            Console.WriteLine();
        }

        // display the current board state
        public static void DisplayBoard(char[,] board)
        {
            Console.WriteLine();
            Console.WriteLine("   0   1   2");
            Console.WriteLine("  -----------");

            for (int i = 0; i < 3; i++)
            {
                Console.Write(i + " ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("| " + board[i, j] + " ");
                }
                Console.WriteLine("|");
                Console.WriteLine("  -----------");
            }
            Console.WriteLine();
        }

        // get player move input
        public static void GetPlayerMove(out int row, out int col)
        {
            Console.WriteLine("Your turn!");
            Console.Write("Enter row (0-2): ");
            string rowInput = Console.ReadLine();
            Console.Write("Enter column (0-2): ");
            string colInput = Console.ReadLine();

            // try to parse input, default to -1 if invalid
            if (!int.TryParse(rowInput, out row))
            {
                row = -1;
            }

            if (!int.TryParse(colInput, out col))
            {
                col = -1;
            }
        }

        // display invalid move message
        public static void DisplayInvalidMove()
        {
            Console.WriteLine("Invalid move! That space is taken or out of bounds. Try again.");
        }

        // display AI turn message
        public static void DisplayAITurn()
        {
            Console.WriteLine("AI's turn...");
        }

        // display AI's move
        public static void DisplayAIMove(int row, int col)
        {
            Console.WriteLine("AI placed O at row " + row + ", column " + col);
        }

        // display player win message
        public static void DisplayPlayerWin()
        {
            Console.WriteLine("Congratulations! You won!");
        }

        // display AI win message
        public static void DisplayAIWin()
        {
            Console.WriteLine("AI won! Better luck next time!");
        }

        // display tie message
        public static void DisplayTie()
        {
            Console.WriteLine("It's a tie! Good game!");
        }

        // display game over message
        public static void DisplayGameOver()
        {
            Console.WriteLine("Game Over!");
        }
    }
}
