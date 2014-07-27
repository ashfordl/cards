// GermanWhistTest.cs
// <copyright file="GermanWhistTest.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
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
        public void RunTest(int players = 2)
        {
            GermanWhist whist = new GermanWhist();
            List<GermanWhistPlayer> whistPlayers = new List<GermanWhistPlayer>();

            for (int i = 0; i < players; i++)
            {
                whistPlayers.Add(new GermanWhistConsolePlayer());
                whist.AddPlayer(whistPlayers[i]);
            }

            whist.Start();
        }

        /// <summary>
        /// Runs the test with AI players
        /// </summary>
        public void RunWithAi(int players = 1, int ais = 1)
        {
            GermanWhist whist = new GermanWhist();
            List<GermanWhistPlayer> whistPlayers = new List<GermanWhistPlayer>();

            for (int i = 0; i < players; i++)
            {
                whistPlayers.Add(new GermanWhistConsolePlayer());
                whist.AddPlayer(whistPlayers[i]);
            }
            for (int i = players; i < ais + players; i++)
            {
                whistPlayers.Add(new GermanWhistConsoleAi());
                whist.AddPlayer(whistPlayers[i]);
            }

            whist.Start();
        }
    }
}
