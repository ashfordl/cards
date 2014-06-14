// Program.cs
// <copyright file="Program.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using CardGames.Whist;
using CardGames.Whist.GermanWhist;
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
            GermanWhist whist = new GermanWhist();

            whist.AddPlayer(new GermanWhistConsolePlayer());
            whist.AddPlayer(new GermanWhistConsolePlayer());

            whist.Start();

            Console.ReadKey();
        }
    }
}
