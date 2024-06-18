//////////////////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date         Developer   Description of change
// 01-30-2022   kriner      Creation of Game.cs file, initialized variables
// 01-30-2022   kriner      Creation of all relevant methods of the program
// 01-31-2022   kriner      Wrote ProcessGameBoard, GetNeighborCount, DetermineDeadOrAlive methods, initial testing
// 02-04-2022   kriner      Added documentation to methods and variables, added User Interface
// 02-05-2022   kriner      Added references, program description, and final testing before canvas submission
//
// References:
// Code used attributed to Charles Costarella in 01-24-2022 and 01-26-2022 lectures
// Conway's Game of Life Wikipedia Article - https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
//
// The Game class intializes two 50 by 80 cell gameboards, one with a pre-set pattern of dead and alive cells and 
// the other with only dead cells to operate as a buffer between each generation in the game. The user is 
// given an explanation of the game through a user interface and enters the generation they want to display. The
// program iterates through each cell of the gameboard using nested for loops to display the current gameboard and
// begin processing the status of each cell for the next gameboard. The active board is then swapped with the buffer
// board in order to display the next generation in the game.

using System;

// the namespace for the GLife program
namespace GLife
{
    // declaration of the class that holds the Glife program
    internal class Game
    {
        // constants for the dimensions of the basic gameboard
        public readonly int ROW_SIZE = 50;
        public readonly int COL_SIZE = 80;

        // constants for the output of alive, dead, and blank cells
        public const char LIVE = '@';
        public const char DEAD = '-';
        public const char SPACE = ' ';

        // the two gameboards used in the program. the buffBoard is used
        // to buffer the results as they are processed from the active
        // gameBoard
        private char[,] gameBoard;
        private char[,] buffBoard;

        // construstor used in the GLife program to initialize the gameboard
        // sizes according to the row size and column size constants
        // from Chuck
        public Game()
        {
            gameBoard = new char[ROW_SIZE, COL_SIZE];
            buffBoard = new char[ROW_SIZE, COL_SIZE];

            // initialize the gameboard(s)
            UserInterface();
            InitializeGameBoards();
            InsertStartupPatterns(25, 20);
        }

        // user interface for the program that is displayed to the user upon execution
        private void UserInterface()
        {
            Console.WriteLine(@"
Welcome to the Game of Life Program! The Game of Life, also known simply as Life, is a 
cellular automaton devised by the British mathematician John Horton Conway in 1970. It is a 
zero-player game, meaning that its evolution is determined by its initial state, requiring no 
further input. One interacts with the Game of Life by creating an initial configuration and 
observing how it evolves. (from Wikipedia https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)

You will be prompted for the number of generations you would like the game to display and 
patterns will be outputted on a 50 by 80 cell game board based on several rules:  

Any live cell with fewer than two live neighbours dies, as if by underpopulation.
Any live cell with two or three live neighbours lives on to the next generation.
Any live cell with more than three live neighbours dies, as if by overpopulation.
Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.

Thank you for using this program and enjoy!
");
        }

        // initializes the gameboards by iterating through the rows and columns
        // of the gameboards and wiping them by replacing all cells with a dead
        // character
        // from Chuck
        private void InitializeGameBoards()
        {
            // increments row one at a time
            for (int r = 0; r < ROW_SIZE; r++)
            {
                // increments column one at a time
                for (int c = 0; c < COL_SIZE; c++)
                {
                    // wipes gameboards
                    gameBoard[r, c] = DEAD;
                    buffBoard[r, c] = DEAD;
                }
            }
        }

        // prompts the user for the number of generations they would like to display
        // and plays the game until the number of generations displayed is equal to
        // that number
        // from Chuck
        public void PlayTheGame()
        {
            // prompts the user for the number of generations they would
            // like to display
            Console.Write("Please enter the number of generations to display: ");
            int numGenerations = int.Parse(Console.ReadLine());

            // plays the game for however many generations the user entered
            for (int generation = 1; generation <= numGenerations; generation++)
            {
                // display the gameboard for the current generation
                DisplayCurrentGameBoard(generation);

                // process the game board for the next generation
                ProcessGameBoard();

                // before exiting the loop, the two boards are swapped
                // in preparation for the next generation
                SwapTheBoards();
            }
        }

        // swaps the active board with the buffer board to wipe it for
        // the next generation
        // from Chuck
        private void SwapTheBoards()
        {
            char[,] temp = gameBoard;
            gameBoard = buffBoard;
            buffBoard = temp;
        }

        // iterates through the gameboard and for each cell calls the DetermineDeadOrAlive
        // method to determine the status of the cell in the next generation of the game
        // from Chuck
        private void ProcessGameBoard()
        {
            // increments row one at a time
            for (int r = 0; r < ROW_SIZE; r++)
            {
                // increments column one at a time
                for (int c = 0; c < COL_SIZE; c++)
                {
                    buffBoard[r, c] = DetermineDeadOrAlive(r, c);
                }
            }
        }

        // takes in int r and int c as parameters that designate what row and column the 
        // cell that is being evaluated is located at. calls the GetNeighborCount method
        // to see how many neighbor the given cell has and assigns it a DEAD or LIVE
        // status based on the rules of the game
        // from Chuck
        private char DetermineDeadOrAlive(int r, int c)
        {
            // count the neighbor cells that are LIVE
            int count = GetNeighborCount(r, c);

            // Any live cell with fewer than two live neighbours dies, as if by underpopulation.
            // Any live cell with two or three live neighbours lives on to the next generation.
            // Any live cell with more than three live neighbours dies, as if by overpopulation.
            // Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
            // from Wikipedia https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
            if (count == 2) return gameBoard[r, c];
            else if (count == 3) return LIVE;
            else return DEAD;
        }

        // takes in int r and int c as parameters that designate what row and column the 
        // cell that is being evaluated is located at. begins by initializing the neighborCount
        // at 0 and checks all cells around the cell in question for LIVE neighbors. if a cell
        // surrounding them is alive, the neighborCount variable is incremented by 1.
        // from Chuck
        private int GetNeighborCount(int r, int c)
        {
            int neighborCount = 0;

            // 8 exceptional cases, 4 corners and 4 edges
            if (r == 0 && c == 0)
            {
                // top left corner
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighborCount++;
            }
            else if (r == 0 && c == COL_SIZE - 1)
            {
                // top right corner
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
            }
            else if (r == ROW_SIZE - 1 && c == COL_SIZE - 1)
            {
                // bottom right corner
                if (gameBoard[r - 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
            }
            else if (r == ROW_SIZE - 1 && c == 0)
            {
                // bottom left corner
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
            }
            else if (r == 0)
            {
                // top row edge
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighborCount++;
            }
            else if (c == COL_SIZE - 1)
            {
                // right edge
                if (gameBoard[r - 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
            }
            else if (r == ROW_SIZE - 1)
            {
                // bottom edge
                if (gameBoard[r - 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
            }
            else if (c == 0)
            {
                // left edge
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighborCount++;
            }
            else
            {
                if (gameBoard[r - 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c] == LIVE) neighborCount++;
                if (gameBoard[r - 1, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r, c + 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c - 1] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c] == LIVE) neighborCount++;
                if (gameBoard[r + 1, c + 1] == LIVE) neighborCount++;
            }
            return neighborCount;
        }

        // iterates through both dimensions of the array and displays the active gameboard
        // from Chuck
        private void DisplayCurrentGameBoard(int gen)
        {
            Console.WriteLine($"\nDisplaying generation #{gen}");
            // increments row one at a time
            for (int r = 0; r < ROW_SIZE; r++)
            {
                // increments column one at a time
                for (int c = 0; c < COL_SIZE; c++)
                {
                    Console.Write($"{SPACE}{gameBoard[r, c]}");
                }
                Console.WriteLine();
            }
        }

        // initializes a startup pattern for the user to base their game off of.
        // places the pattern in the middle of the gameboard by taking in row and 
        // column parameters
        // from Chuck
        private void InsertStartupPatterns(int r, int c)
        {
            // insert 8 LIVE cells for the first segment
            gameBoard[r, c + 1] = LIVE;
            gameBoard[r, c + 2] = LIVE;
            gameBoard[r, c + 3] = LIVE;
            gameBoard[r, c + 4] = LIVE;
            gameBoard[r, c + 5] = LIVE;
            gameBoard[r, c + 6] = LIVE;
            gameBoard[r, c + 7] = LIVE;
            gameBoard[r, c + 8] = LIVE;
            // 1 DEAD cell
            // insert 5 LIVE cells
            gameBoard[r, c + 10] = LIVE;
            gameBoard[r, c + 11] = LIVE;
            gameBoard[r, c + 12] = LIVE;
            gameBoard[r, c + 13] = LIVE;
            gameBoard[r, c + 14] = LIVE;
            // 3 DEAD cells
            // insert 3 LIVE cells
            gameBoard[r, c + 18] = LIVE;
            gameBoard[r, c + 19] = LIVE;
            gameBoard[r, c + 20] = LIVE;
            // 6 DEAD cells
            // insert 7 LIVE cells
            gameBoard[r, c + 27] = LIVE;
            gameBoard[r, c + 28] = LIVE;
            gameBoard[r, c + 29] = LIVE;
            gameBoard[r, c + 30] = LIVE;
            gameBoard[r, c + 31] = LIVE;
            gameBoard[r, c + 32] = LIVE;
            gameBoard[r, c + 33] = LIVE;
            // 1 DEAD cell
            // insert 5 LIVE cells
            gameBoard[r, c + 35] = LIVE;
            gameBoard[r, c + 36] = LIVE;
            gameBoard[r, c + 37] = LIVE;
            gameBoard[r, c + 38] = LIVE;
            gameBoard[r, c + 39] = LIVE;
        }
    }
}