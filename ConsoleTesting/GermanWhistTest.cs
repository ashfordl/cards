// GermanWhistTest.cs
// <copyright file="GermanWhistTest.cs"> This code is protected under the MIT License. </copyright>
using System;
using CardGames.Whist.GermanWhist;

namespace ConsoleTesting
{
    /// <summary>
    /// The german whist test class
    /// </summary>
    public class GermanWhistTest
    {
        /// <summary>
        /// Runs the test
        /// </summary>
        public void Run()
        {
            GermanWhist whist = new GermanWhist();

            whist.AddPlayer(new GermanWhistConsolePlayer());
            whist.AddPlayer(new GermanWhistConsoleAi());

            whist.Start();

            Console.ReadKey();
        }
    }
}
