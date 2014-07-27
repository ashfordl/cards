// KnockoutWhistTest.cs
// <copyright file="KnockoutWhistTest.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
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
        /// <param name="players"> The amount of players in the game. </param>
        public void RunTest(int players)
        {
            KnockoutWhist whist = new KnockoutWhist();
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
        /// <param name="players"> The amount of players in the game. </param>
        /// <param name="ais"> The amount of ai's in the game. </param>
        public void RunWithAi(int players, int ais)
        {
            Console.WriteLine("There is no AI");
        }
    }
}
