// Program.cs
// <copyright file="Program.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using CardsLibrary;
using CardGames.Shed;
using CardGames.Whist;
using CardGames.Whist.Knockout;

namespace ConsoleTesting
{
    /// <summary>
    /// The main program (for testing at the moment).
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the program.
        /// </summary>
        /// <param name="args"> Any arguments/commands that the program is run/compiled with. </param>
        public static void Main(string[] args)
        {
            IGameTest game;

            Program.SelectGame(out game);

            Program.RunGame(game);

            Console.WriteLine("\n\n\n\nPress enter to continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Lets the user select the game they wish to play.
        /// </summary>
        /// <param name="gameTest"> The game. </param>
        public static void SelectGame(out IGameTest gameTest)
        {
            int gameType;

            Console.WriteLine("Which game do you want to play?\n");
            Console.WriteLine("A single Round of Whist (1)");
            Console.WriteLine("Knockout Whist (2)");
            Console.WriteLine("German Whist (3)");
            Console.WriteLine("Shed (4)");

            do
            {
                // Get the input and catch the error if it is not a number
                try
                {
                    gameType = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nThat is not a valid input!");
                    continue;
                }

                // If the number is not one of the games
                if (gameType > 4 || gameType < 1)
                {
                    Console.WriteLine("\nThat is not a valid input!");
                    continue;
                }

                // If no errors were thrown break the loop
                break;
            } 
            while (true);

            // Initialize the game according to what was inputted
            switch (gameType)
            {
                case 1: 
                    gameTest = new WhistTest(); 
                    break;
                case 2: 
                    gameTest = new KnockoutWhistTest(); 
                    break;
                case 3:
                    gameTest = new GermanWhistTest();
                    break;
                case 4:
                    gameTest = new ShedTest();
                    break;
                default:
                    gameTest = new WhistTest(); // Default game is Whist
                    break;
            }
        }

        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="game"> The game. </param>
        public static void RunGame(IGameTest game)
        {
            if (game is WhistTest)
            {
                Program.RunWhist((WhistTest)game);
            }
            else if (game is KnockoutWhistTest)
            {
                Program.RunKnockoutWhist((KnockoutWhistTest)game);
            }
            else if (game is GermanWhistTest)
            {
                Program.RunGermanWhist((GermanWhistTest)game);
            }
            else if (game is ShedTest)
            {
                Program.RunShed((ShedTest)game);
            }
        }

        /// <summary>
        /// Run the whist game and select how many players.
        /// </summary>
        /// <param name="game"> The game. </param>
        public static void RunWhist(WhistTest game)
        {
            Console.WriteLine("How many people are playing?");

            do
            {
                // The input from the user
                int input;
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nThat is not a valid input!");
                    continue;
                }

                if (input < 2)
                {
                    Console.WriteLine("\nYou must have at least 2 players to play!");
                }
                else if (input > new Whist().MaxPlayers)
                {
                    Console.WriteLine("\nYou must have {0} or less players!", new Whist().MaxPlayers);
                }
                else
                {
                    // If the amount of players was accepted, run the game
                    Console.Clear();
                    game.RunTest(input);
                    break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Run the knockout whist game and select how many players.
        /// </summary>
        /// <param name="game"> The game. </param>
        public static void RunKnockoutWhist(KnockoutWhistTest game)
        {
            Console.WriteLine("How many people are playing?");

            do
            {
                // The input from the user
                int input;
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nThat is not a valid input!");
                    continue;
                }

                if (input < 2)
                {
                    Console.WriteLine("\nYou must have at least 2 players to play!");
                }
                else if (input > new KnockoutWhist().MaxPlayers)
                {
                    Console.WriteLine("\nYou must have {0} or less players!", new KnockoutWhist().MaxPlayers);
                }
                else
                {
                    // If the amount of players was accepted, run the game
                    Console.Clear();
                    game.RunTest(input);
                    break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Run the german whist game and select how many players.
        /// </summary>
        /// <param name="game"> The game. </param>
        public static void RunGermanWhist(GermanWhistTest game)
        {
            Console.WriteLine("Play against an AI? Y/N");

            do
            {
                // The input from the user
                string input = Console.ReadLine();

                if (input.ToUpper() == "Y")
                {
                    game.RunWithAi();
                    break;
                }
                else if (input.ToUpper() == "N")
                {
                    game.RunTest();
                    break;
                }
                else
                {
                    Console.WriteLine("That is not a valid input!");
                }
            }
            while (true);
        }

        /// <summary>
        /// Run the shed game and select how many players.
        /// </summary>
        /// <param name="game"> The game. </param>
        public static void RunShed(ShedTest game)
        {
            Console.WriteLine("How many people are playing?");

            do
            {
                // The input from the user
                int input;
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("\nThat is not a valid input!");
                    continue;
                }

                if (input < 2)
                {
                    Console.WriteLine("\nYou must have at least 2 players to play!");
                }
                else if (input > new Shed().MaxPlayers)
                {
                    Console.WriteLine("\nYou must have {0} or less players!", new Shed().MaxPlayers);
                }
                else
                {
                    // If the amount of players was accepted, run the game
                    Console.Clear();
                    game.RunTest(input);
                    break;
                }
            }
            while (true);
        }
    }
}
