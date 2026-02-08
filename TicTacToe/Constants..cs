namespace TictacToe
{
    public static class Constants
    {
        // Board dimensions
        public const int BOARD_SIZE = 3;
        public const int MIN_INDEX = 0;
        public const int MAX_INDEX = 2;

        // Symbols
        public const char PLAYER_SYMBOL = 'X';
        public const char AI_SYMBOL = 'O';
        public const char EMPTY_SYMBOL = ' ';

        // Win condition
        public const int SYMBOLS_IN_A_ROW_TO_WIN = 3;

        // AI strategy values
        public const int CENTER_POSITION = 1;
        public const int CORNER_COUNT = 4;

        // Win detection thresholds
        public const int SYMBOLS_NEEDED_FOR_WINNING_MOVE = 2;
        public const int EMPTY_SPACES_FOR_WINNING_MOVE = 1;
    }
}