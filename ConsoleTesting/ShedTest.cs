// ShedTest.cs
// <copyright file="ShedTest.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using CardGames.Shed;

namespace ConsoleTesting
{
    /// <summary>
    /// The shed test class
    /// </summary>
    public class ShedTest : IGameTest
    {
        /// <summary>
        /// Runs the test
        /// </summary>
        /// <param name="players"> The amount of players in the game. </param> 
        public void RunTest(int players)
        {
            Shed shed = new Shed();
            List<ShedPlayer> shedPlayers = new List<ShedPlayer>();

            for (int i = 0; i < players; i++)
            {
                shedPlayers.Add(new ShedConsolePlayer());
                shed.AddPlayer(shedPlayers[i]);
            }

            shed.Start();
        }

        /// <summary>
        /// Runs the test with AI players
        /// </summary>
        /// <param name="players"> The amount of players in the game. </param>
        /// <param name="ais"> The amount of ais in the game. </param>
        public void RunWithAi(int players, int ais)
        {
            Console.WriteLine("There is no AI!");
        }
    }
}
