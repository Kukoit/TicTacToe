# Tic Tac Toe Console Game

A simple console-based Tic Tac Toe game where you play against an AI.

## How to Run

1. Navigate to the TicTacToe directory
2. Run: `dotnet run`

## How to Play

- You are X, the AI is O
- Enter row and column numbers (0-2) to place your symbol
- The board uses coordinates:
  - Row 0 = Top
  - Row 1 = Middle  
  - Row 2 = Bottom
  - Column 0 = Left
  - Column 1 = Center
  - Column 2 = Right

## Project Structure

- **Program.cs** - Main game loop
- **Logic.cs** - All game logic (board management, move validation, win checking, AI)
- **UI.cs** - All user interface (console input/output)

## AI Strategy

The AI uses the following strategy:
1. Try to win if possible
2. Block player from winning
3. Take center if available
4. Take a corner
5. Take any available space
