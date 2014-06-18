// GermanWhistTest.cs
// <copyright file="GermanWhistTest.cs"> This code is protected under the MIT License. </copyright>
using System;
using CardGames.Whist.GermanWhist;

namespace ConsoleTesting
{
    /// <summary>
    /// The german whist test class
    /// </summary>
    public class GermanWhistTest : IGameTest
    {
        /// <summary>
        /// Runs the test
        /// </summary>
        public void RunWithAi()
        {
            GermanWhist whist = new GermanWhist();

            whist.AddPlayer(new GermanWhistConsolePlayer());
            whist.AddPlayer(new GermanWhistConsoleAi());
            
            Console.WriteLine("up");

            whist.Start();
        }

        public void RunTest()
        {
            GermanWhist whist = new GermanWhist();

            whist.AddPlayer(new GermanWhistConsolePlayer());
            whist.AddPlayer(new GermanWhistConsolePlayer());

            whist.Start();
        }
    }
}
