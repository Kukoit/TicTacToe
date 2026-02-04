namespace TictacToe
{
    class Logic
    {
        // initialize a 3x3 board with empty spaces
        public static char[,] InitializeBoard()
        {
            char[,] board = new char[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ' ';
                }
            }
            return board;
        }

        // check if a move is valid 
        public static bool IsValidMove(char[,] board, int row, int col)
        {
            if (row < 0 || row > 2 || col < 0 || col > 2)
            {
                return false;
            }

            if (board[row, col] != ' ')
            {
                return false;
            }

            return true;
        }

        // place a symbol on the board
        public static char[,] PlaceSymbol(char[,] board, int row, int col, char symbol)
        {
            board[row, col] = symbol;
            return board;
        }

        // check if a player has won
        public static bool CheckWinner(char[,] board, char symbol)
        {
            // check horizontal lines
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == symbol && board[i, 1] == symbol && board[i, 2] == symbol)
                {
                    return true;
                }
            }

            // check vertical lines
            for (int j = 0; j < 3; j++)
            {
                if (board[0, j] == symbol && board[1, j] == symbol && board[2, j] == symbol)
                {
                    return true;
                }
            }

            // check diagonal (top-left to bottom-right)
            if (board[0, 0] == symbol && board[1, 1] == symbol && board[2, 2] == symbol)
            {
                return true;
            }

            // check diagonal (top-right to bottom-left)
            if (board[0, 2] == symbol && board[1, 1] == symbol && board[2, 0] == symbol)
            {
                return true;
            }

            return false;
        }

        // check if the board is full (tie condition)
        public static bool IsBoardFull(char[,] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // AI makes a move (simple strategy)
        public static void GetAIMove(char[,] board, out int row, out int col)
        {
            // Initialize variables
            row = 0;
            col = 0;

            // first, try to win
            if (TryToWin(board, 'O', out row, out col))
            {
                return;
            }

            // second, try to block player from winning
            if (TryToWin(board, 'X', out row, out col))
            {
                return;
            }

            // third, take center if available
            if (board[1, 1] == ' ')
            {
                row = 1;
                col = 1;
                return;
            }

            // fourth, take a corner
            int[,] corners = { { 0, 0 }, { 0, 2 }, { 2, 0 }, { 2, 2 } };
            for (int i = 0; i < 4; i++)
            {
                int r = corners[i, 0];
                int c = corners[i, 1];
                if (board[r, c] == ' ')
                {
                    row = r;
                    col = c;
                    return;
                }
            }

            // finally, take any available space
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == ' ')
                    {
                        row = i;
                        col = j;
                        return;
                    }
                }
            }

            // should never reach here if board is not full
            row = -1;
            col = -1;
        }

        // helper method to try to complete a winning line or block opponent
        private static bool TryToWin(char[,] board, char symbol, out int row, out int col)
        {
            // Check each row
            for (int i = 0; i < 3; i++)
            {
                if (CheckAndReturnEmptySpot(board, symbol, i, 0, i, 1, i, 2, out row, out col))
                {
                    return true;
                }
            }

            // check each column
            for (int j = 0; j < 3; j++)
            {
                if (CheckAndReturnEmptySpot(board, symbol, 0, j, 1, j, 2, j, out row, out col))
                {
                    return true;
                }
            }

            // check diagonal (top-left to bottom-right)
            if (CheckAndReturnEmptySpot(board, symbol, 0, 0, 1, 1, 2, 2, out row, out col))
            {
                return true;
            }

            // check diagonal (top-right to bottom-left)
            if (CheckAndReturnEmptySpot(board, symbol, 0, 2, 1, 1, 2, 0, out row, out col))
            {
                return true;
            }

            row = -1;
            col = -1;
            return false;
        }

        // check if a line has two of the same symbol and one empty space
        private static bool CheckAndReturnEmptySpot(char[,] board, char symbol,
            int r1, int c1, int r2, int c2, int r3, int c3, out int row, out int col)
        {
            char pos1 = board[r1, c1];
            char pos2 = board[r2, c2];
            char pos3 = board[r3, c3];

            // count how many positions have the symbol and how many are empty
            int symbolCount = 0;
            int emptyCount = 0;
            int emptyRow = -1;
            int emptyCol = -1;

            if (pos1 == symbol) symbolCount++;
            else if (pos1 == ' ') { emptyCount++; emptyRow = r1; emptyCol = c1; }

            if (pos2 == symbol) symbolCount++;
            else if (pos2 == ' ') { emptyCount++; emptyRow = r2; emptyCol = c2; }

            if (pos3 == symbol) symbolCount++;
            else if (pos3 == ' ') { emptyCount++; emptyRow = r3; emptyCol = c3; }

            // if we have 2 of our symbol and 1 empty space, we can win/block here
            if (symbolCount == 2 && emptyCount == 1)
            {
                row = emptyRow;
                col = emptyCol;
                return true;
            }

            row = -1;
            col = -1;
            return false;
        }
    }
}
