namespace TictacToe
{
    class Logic
    {
        // Initialize a board with empty spaces
        public static char[,] InitializeBoard()
        {
            char[,] board = new char[Constants.BOARD_SIZE, Constants.BOARD_SIZE];

            for (int i = 0; i < Constants.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Constants.BOARD_SIZE; j++)
                {
                    board[i, j] = Constants.EMPTY_SYMBOL;
                }
            }

            return board;
        }

        // Check if a move is valid (within bounds and space is empty)
        public static bool IsValidMove(char[,] board, int row, int col)
        {
            if (!IsWithinBounds(row, col))
            {
                return false;
            }

            if (!IsSpaceEmpty(board, row, col))
            {
                return false;
            }

            return true;
        }

        // Check if coordinates are within board bounds
        private static bool IsWithinBounds(int row, int col)
        {
            return row >= Constants.MIN_INDEX && row <= Constants.MAX_INDEX &&
                   col >= Constants.MIN_INDEX && col <= Constants.MAX_INDEX;
        }

        // Check if a space is empty
        private static bool IsSpaceEmpty(char[,] board, int row, int col)
        {
            return board[row, col] == Constants.EMPTY_SYMBOL;
        }

        // Place a symbol on the board
        public static char[,] PlaceSymbol(char[,] board, int row, int col, char symbol)
        {
            board[row, col] = symbol;
            return board;
        }

        // Check if a player has won (dynamic approach)
        public static bool CheckWinner(char[,] board, char symbol)
        {
            // Check all horizontal lines
            if (CheckAllHorizontalLines(board, symbol))
            {
                return true;
            }

            // Check all vertical lines
            if (CheckAllVerticalLines(board, symbol))
            {
                return true;
            }

            // Check both diagonals
            if (CheckDiagonals(board, symbol))
            {
                return true;
            }

            return false;
        }

        // Check all horizontal lines for a win
        private static bool CheckAllHorizontalLines(char[,] board, char symbol)
        {
            for (int row = 0; row < Constants.BOARD_SIZE; row++)
            {
                if (CheckHorizontalLine(board, row, symbol))
                {
                    return true;
                }
            }
            return false;
        }

        // Check a single horizontal line
        private static bool CheckHorizontalLine(char[,] board, int row, char symbol)
        {
            int matchCount = 0;

            for (int col = 0; col < Constants.BOARD_SIZE; col++)
            {
                if (board[row, col] == symbol)
                {
                    matchCount++;
                }
            }

            return matchCount == Constants.BOARD_SIZE;
        }

        // Check all vertical lines for a win
        private static bool CheckAllVerticalLines(char[,] board, char symbol)
        {
            for (int col = 0; col < Constants.BOARD_SIZE; col++)
            {
                if (CheckVerticalLine(board, col, symbol))
                {
                    return true;
                }
            }
            return false;
        }

        // Check a single vertical line
        private static bool CheckVerticalLine(char[,] board, int col, char symbol)
        {
            int matchCount = 0;

            for (int row = 0; row < Constants.BOARD_SIZE; row++)
            {
                if (board[row, col] == symbol)
                {
                    matchCount++;
                }
            }

            return matchCount == Constants.BOARD_SIZE;
        }

        // Check both diagonals for a win
        private static bool CheckDiagonals(char[,] board, char symbol)
        {
            return CheckTopLeftToBottomRightDiagonal(board, symbol) ||
                   CheckTopRightToBottomLeftDiagonal(board, symbol);
        }

        // Check diagonal from top-left to bottom-right
        private static bool CheckTopLeftToBottomRightDiagonal(char[,] board, char symbol)
        {
            int matchCount = 0;

            for (int i = 0; i < Constants.BOARD_SIZE; i++)
            {
                if (board[i, i] == symbol)
                {
                    matchCount++;
                }
            }

            return matchCount == Constants.BOARD_SIZE;
        }

        // Check diagonal from top-right to bottom-left
        private static bool CheckTopRightToBottomLeftDiagonal(char[,] board, char symbol)
        {
            int matchCount = 0;

            for (int i = 0; i < Constants.BOARD_SIZE; i++)
            {
                if (board[i, Constants.BOARD_SIZE - 1 - i] == symbol)
                {
                    matchCount++;
                }
            }

            return matchCount == Constants.BOARD_SIZE;
        }

        // Check if the board is full (tie condition)
        public static bool IsBoardFull(char[,] board)
        {
            for (int i = 0; i < Constants.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Constants.BOARD_SIZE; j++)
                {
                    if (board[i, j] == Constants.EMPTY_SYMBOL)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // AI makes a move (orchestrates strategy)
        public static void GetAIMove(char[,] board, out int row, out int col)
        {
            row = 0;
            col = 0;

            // Try each strategy in priority order
            if (TryWinningMove(board, out row, out col))
            {
                return;
            }

            if (TryBlockingMove(board, out row, out col))
            {
                return;
            }

            if (TryCenterMove(board, out row, out col))
            {
                return;
            }

            if (TryCornerMove(board, out row, out col))
            {
                return;
            }

            TryAnyAvailableMove(board, out row, out col);
        }

        // Try to make a winning move
        private static bool TryWinningMove(char[,] board, out int row, out int col)
        {
            return TryToCompleteWinningLine(board, Constants.AI_SYMBOL, out row, out col);
        }

        // Try to block player from winning
        private static bool TryBlockingMove(char[,] board, out int row, out int col)
        {
            return TryToCompleteWinningLine(board, Constants.PLAYER_SYMBOL, out row, out col);
        }

        // Try to take the center position
        private static bool TryCenterMove(char[,] board, out int row, out int col)
        {
            row = Constants.CENTER_POSITION;
            col = Constants.CENTER_POSITION;

            if (board[row, col] == Constants.EMPTY_SYMBOL)
            {
                return true;
            }

            row = 0;
            col = 0;
            return false;
        }

        // Try to take a corner position
        private static bool TryCornerMove(char[,] board, out int row, out int col)
        {
            int[,] corners = GetCornerPositions();

            for (int i = 0; i < Constants.CORNER_COUNT; i++)
            {
                int r = corners[i, 0];
                int c = corners[i, 1];

                if (board[r, c] == Constants.EMPTY_SYMBOL)
                {
                    row = r;
                    col = c;
                    return true;
                }
            }

            row = 0;
            col = 0;
            return false;
        }

        // Get corner positions for the board
        private static int[,] GetCornerPositions()
        {
            return new int[,]
            {
                { Constants.MIN_INDEX, Constants.MIN_INDEX },
                { Constants.MIN_INDEX, Constants.MAX_INDEX },
                { Constants.MAX_INDEX, Constants.MIN_INDEX },
                { Constants.MAX_INDEX, Constants.MAX_INDEX }
            };
        }

        // Try to take any available move
        private static void TryAnyAvailableMove(char[,] board, out int row, out int col)
        {
            for (int i = 0; i < Constants.BOARD_SIZE; i++)
            {
                for (int j = 0; j < Constants.BOARD_SIZE; j++)
                {
                    if (board[i, j] == Constants.EMPTY_SYMBOL)
                    {
                        row = i;
                        col = j;
                        return;
                    }
                }
            }

            // Should never reach here if board isn't full
            row = -1;
            col = -1;
        }

        // Try to complete a winning line for a given symbol
        private static bool TryToCompleteWinningLine(char[,] board, char symbol, out int row, out int col)
        {
            // Check all horizontal lines
            if (CheckHorizontalLinesForWinningMove(board, symbol, out row, out col))
            {
                return true;
            }

            // Check all vertical lines
            if (CheckVerticalLinesForWinningMove(board, symbol, out row, out col))
            {
                return true;
            }

            // Check both diagonals
            if (CheckDiagonalsForWinningMove(board, symbol, out row, out col))
            {
                return true;
            }

            row = -1;
            col = -1;
            return false;
        }

        // Check all horizontal lines for a winning move opportunity
        private static bool CheckHorizontalLinesForWinningMove(char[,] board, char symbol, out int row, out int col)
        {
            for (int r = 0; r < Constants.BOARD_SIZE; r++)
            {
                int[] coords = new int[Constants.BOARD_SIZE * 2]; // Store row,col pairs
                for (int c = 0; c < Constants.BOARD_SIZE; c++)
                {
                    coords[c * 2] = r;
                    coords[c * 2 + 1] = c;
                }

                if (CheckLineForWinningMove(board, symbol, coords, out row, out col))
                {
                    return true;
                }
            }

            row = -1;
            col = -1;
            return false;
        }

        // Check all vertical lines for a winning move opportunity
        private static bool CheckVerticalLinesForWinningMove(char[,] board, char symbol, out int row, out int col)
        {
            for (int c = 0; c < Constants.BOARD_SIZE; c++)
            {
                int[] coords = new int[Constants.BOARD_SIZE * 2]; // Store row,col pairs
                for (int r = 0; r < Constants.BOARD_SIZE; r++)
                {
                    coords[r * 2] = r;
                    coords[r * 2 + 1] = c;
                }

                if (CheckLineForWinningMove(board, symbol, coords, out row, out col))
                {
                    return true;
                }
            }

            row = -1;
            col = -1;
            return false;
        }

        // Check both diagonals for a winning move opportunity
        private static bool CheckDiagonalsForWinningMove(char[,] board, char symbol, out int row, out int col)
        {
            // Check top-left to bottom-right diagonal
            int[] diagonal1 = new int[Constants.BOARD_SIZE * 2];
            for (int i = 0; i < Constants.BOARD_SIZE; i++)
            {
                diagonal1[i * 2] = i;
                diagonal1[i * 2 + 1] = i;
            }

            if (CheckLineForWinningMove(board, symbol, diagonal1, out row, out col))
            {
                return true;
            }

            // Check top-right to bottom-left diagonal
            int[] diagonal2 = new int[Constants.BOARD_SIZE * 2];
            for (int i = 0; i < Constants.BOARD_SIZE; i++)
            {
                diagonal2[i * 2] = i;
                diagonal2[i * 2 + 1] = Constants.BOARD_SIZE - 1 - i;
            }

            if (CheckLineForWinningMove(board, symbol, diagonal2, out row, out col))
            {
                return true;
            }

            row = -1;
            col = -1;
            return false;
        }

        // Check if a specific line has a winning move opportunity (2 symbols + 1 empty)
        private static bool CheckLineForWinningMove(char[,] board, char symbol, int[] coords, out int row, out int col)
        {
            int symbolCount = 0;
            int emptyCount = 0;
            int emptyRow = -1;
            int emptyCol = -1;

            // Loop through all positions in the line
            for (int i = 0; i < Constants.BOARD_SIZE; i++)
            {
                int r = coords[i * 2];
                int c = coords[i * 2 + 1];
                char position = board[r, c];

                if (position == symbol)
                {
                    symbolCount++;
                }
                else if (position == Constants.EMPTY_SYMBOL)
                {
                    emptyCount++;
                    emptyRow = r;
                    emptyCol = c;
                }
            }

            if (IsWinningMoveAvailable(symbolCount, emptyCount))
            {
                row = emptyRow;
                col = emptyCol;
                return true;
            }

            row = -1;
            col = -1;
            return false;
        }

        // Count how many positions in a line contain the given symbol
        private static int CountSymbolsInLine(char pos1, char pos2, char pos3, char symbol)
        {
            int count = 0;
            if (pos1 == symbol) count++;
            if (pos2 == symbol) count++;
            if (pos3 == symbol) count++;
            return count;
        }

        // Count how many positions in a line are empty
        private static int CountEmptySpacesInLine(char pos1, char pos2, char pos3)
        {
            int count = 0;
            if (pos1 == Constants.EMPTY_SYMBOL) count++;
            if (pos2 == Constants.EMPTY_SYMBOL) count++;
            if (pos3 == Constants.EMPTY_SYMBOL) count++;
            return count;
        }

        // Check if a winning move is available (2 symbols and 1 empty space)
        private static bool IsWinningMoveAvailable(int symbolCount, int emptyCount)
        {
            return symbolCount == Constants.SYMBOLS_NEEDED_FOR_WINNING_MOVE &&
                   emptyCount == Constants.EMPTY_SPACES_FOR_WINNING_MOVE;
        }
    }
}