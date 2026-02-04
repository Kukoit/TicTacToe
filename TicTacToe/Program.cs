namespace TictacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] board = Logic.InitializeBoard();
            bool gameOver = false;
            bool isPlayerTurn = true;

            UI.DisplayWelcome();

            while (!gameOver)
            {
                UI.DisplayBoard(board);

                if (isPlayerTurn)
                {
                    // player turn
                    int row = 0;
                    int col = 0;
                    bool validMove = false;

                    while (!validMove)
                    {
                        UI.GetPlayerMove(out row, out col);
                        validMove = Logic.IsValidMove(board, row, col);

                        if (!validMove)
                        {
                            UI.DisplayInvalidMove();
                        }
                    }

                    board = Logic.PlaceSymbol(board, row, col, 'X');
                }
                else
                {
                    // AI turn
                    UI.DisplayAITurn();
                    int row = 0;
                    int col = 0;
                    Logic.GetAIMove(board, out row, out col);
                    board = Logic.PlaceSymbol(board, row, col, 'O');
                    UI.DisplayAIMove(row, col);
                }

                // check for winner or tie
                if (Logic.CheckWinner(board, 'X'))
                {
                    UI.DisplayBoard(board);
                    UI.DisplayPlayerWin();
                    gameOver = true;
                }
                else if (Logic.CheckWinner(board, 'O'))
                {
                    UI.DisplayBoard(board);
                    UI.DisplayAIWin();
                    gameOver = true;
                }
                else if (Logic.IsBoardFull(board))
                {
                    UI.DisplayBoard(board);
                    UI.DisplayTie();
                    gameOver = true;
                }

                // switch turns
                isPlayerTurn = !isPlayerTurn;
            }

            UI.DisplayGameOver();
        }
    }
}