// Kyle Riner
// L5life assignment
// TINFO-200A, Winter 2022

//////////////////////////////////////////////////////////////////////////////////////////////////
// Change History
// Date         Developer   Description of change
// 01-30-2022   kriner      Creation of Glife program file, created new Game
// 02-05-2022   kriner      Added References, description of program, and code documentation
//
// References:
// Code used attributed to Charles Costarella in 01-24-2022 and 01-26-2022 lectures
// Conway's Game of Life Wikipedia Article - https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
//
// The GLife program creates a new Game class and plays the game of life. The user is firstly given
// the details of what the game of life is and then is asked to enter the number of generations they
// want to display. The game's first generation is initialized in the program and the rest of the
// generation are changed based on the rules that a cell is born if it has three neighbors, survives if
// it has two or three living neighbors, and dies in any other case. The game is mostly self-sufficient
// only requiring the user to enter the number of generations they would like to have displayed.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// the namespace for the GLife program
namespace GLife
{
    // declaration of the class that holds the GLife program
    internal class Program
    {
        // the main method that executes the code of the GLife program
        // from Chuck
        static void Main(string[] args)
        {
            // calls the Game constructor to begin playing the game
            Game game = new Game();
            game.PlayTheGame();
        }
    }
}
