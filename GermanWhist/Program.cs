// Program.cs
// <copyright file="Program.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;

namespace GermanWhist
{
    /// <summary>
    /// An application entry point for a console-based game of German Whist.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the program.
        /// </summary>
        /// <param name="args"> Any arguments the program is run with. </param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Play against an AI? Y/N");

            do
            {
                // The input from the user
                string input = Console.ReadLine();

                if (input.ToUpper() == "Y")
                {
                    Program.RunWithAi();
                    break;
                }
                else if (input.ToUpper() == "N")
                {
                    Program.RunWithoutAi();
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
        /// Runs a game of German Whist with AI players
        /// </summary>
        /// <param name="players"> The amount of human players in the game. </param>
        /// <param name="ais"> The amount of AI players in the game. </param>
        public static void RunWithAi(int players = 1, int ais = 1)
        {
            GermanWhist whist = new GermanWhist();
            List<GermanWhistPlayer> whistPlayers = new List<GermanWhistPlayer>();

            for (int i = 0; i < players; i++)
            {
                whistPlayers.Add(new GermanWhistConsolePlayer());
                whist.AddPlayer(whistPlayers[i]);
            }

            for (int i = players; i < ais + players; i++)
            {
                whistPlayers.Add(new GermanWhistConsoleAi());
                whist.AddPlayer(whistPlayers[i]);
            }

            whist.Start();
        }

        /// <summary>
        /// Runs a game of German Whist
        /// </summary>
        /// <param name="players"> The amount of players in the game. </param>
        public static void RunWithoutAi(int players = 2)
        {
            GermanWhist whist = new GermanWhist();
            List<GermanWhistPlayer> whistPlayers = new List<GermanWhistPlayer>();

            for (int i = 0; i < players; i++)
            {
                whistPlayers.Add(new GermanWhistConsolePlayer());
                whist.AddPlayer(whistPlayers[i]);
            }

            whist.Start();
        }
    }
}
