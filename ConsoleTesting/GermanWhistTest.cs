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
        public void Run(bool ai = false)
        {
            GermanWhist whist = new GermanWhist();

            whist.AddPlayer(new GermanWhistConsolePlayer());
            if (ai)
            {
                whist.AddPlayer(new GermanWhistConsoleAi());
            }
            else
            {
                whist.AddPlayer(new GermanWhistConsolePlayer());
            }

            whist.Start();

            Console.ReadKey();
        }
    }
}
