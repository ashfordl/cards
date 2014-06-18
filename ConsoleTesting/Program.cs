// Program.cs
// <copyright file="Program.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
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
            IGameTest gameTest = new GermanWhistTest();

            gameTest.RunWithAi();

            Console.WriteLine("Press enter to continue");
            Console.ReadLine();
        }
    }
}
