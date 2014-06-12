// Program.cs
// <copyright file="Program.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using CardGames.Whist;
using CardsLibrary;

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
            Whist whist = new Whist();
            ConsolePlayer p1 = new ConsolePlayer();
            ConsolePlayer p2 = new ConsolePlayer();
            ConsolePlayer p3 = new ConsolePlayer();

            whist.AddPlayer(p1);
            whist.AddPlayer(p2);
            whist.AddPlayer(p3);

            whist.Start();

            Console.ReadKey();
        }
    }
}
