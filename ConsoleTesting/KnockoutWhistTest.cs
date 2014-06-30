// KnockoutWhistTest.cs
// <copyright file="KnockoutWhistTest.cs"> This code is protected under the MIT License. </copyright>
using System;
using CardGames.Whist;
using CardGames.Whist.Knockout;

namespace ConsoleTesting
{
    /// <summary>
    /// The whist test class
    /// </summary>
    public class KnockoutWhistTest : IGameTest
    {
        /// <summary>
        /// Runs the test
        /// </summary>
        public void RunTest()
        {
            KnockoutWhist whist = new KnockoutWhist();

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
