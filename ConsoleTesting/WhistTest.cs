// WhistTest.cs
// <copyright file="WhistTest.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
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
        public void RunTest(int players)
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

        /// <summary>
        /// Runs the test with AI players
        /// </summary>
        public void RunWithAi(int players, int ais)
        {
            Console.WriteLine("There is no AI!");
        }
    }
}
