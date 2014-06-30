// WhistTest.cs
// <copyright file="WhistTest.cs"> This code is protected under the MIT License. </copyright>
using System;
using CardGames.Whist;

namespace ConsoleTesting
{
    /// <summary>
    /// The whist test class
    /// </summary>
    public class WhistTest : IGameTest
    {
        /// <summary>
        /// Runs the test
        /// </summary>
        public void RunTest()
        {
            Whist whist = new Whist();

            ConsolePlayer p1 = new ConsolePlayer();
            ConsolePlayer p2 = new ConsolePlayer();
            ConsolePlayer p3 = new ConsolePlayer();

            whist.AddPlayer(p1);
            whist.AddPlayer(p2);
            whist.AddPlayer(p3);

            whist.Start();
        }

        /// <summary>
        /// Runs the test with AI players
        /// </summary>
        public void RunWithAi()
        {
            Console.WriteLine("Es gibt kein AI");
            Console.WriteLine("There is no AI");
        }
    }
}
