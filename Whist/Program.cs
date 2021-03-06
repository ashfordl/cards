﻿// Program.cs
// <copyright file="Program.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;

namespace Whist
{
    /// <summary>
    /// An application entry point for a console-based hand of Whist.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the program.
        /// </summary>
        /// <param name="args"> Any arguments the program is run with. </param>
        public static void Main(string[] args)
        {
            Console.WriteLine("How many people are playing Whist?");

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
                    Program.RunWhist(input);
                    break;
                }
            }
            while (true);
        }

        /// <summary>
        /// Runs a game of Whist
        /// </summary>
        /// <param name="players"> The amount of players in the game. </param> 
        public static void RunWhist(int players)
        {
            Whist whist = new Whist();
            List<WhistPlayer> whistPlayers = new List<WhistPlayer>();

            for (int i = 0; i < players; i++)
            {
                whistPlayers.Add(new ConsolePlayer());
                whist.AddPlayer(whistPlayers[i]);
            }

            whist.Start();
        }
    }
}
