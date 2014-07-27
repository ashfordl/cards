// IGameTest.cs
// <copyright file="IGameTest.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTesting
{
    /// <summary>
    /// A manual test for a game
    /// </summary>
    public interface IGameTest
    {
        /// <summary>
        /// Runs the test
        /// </summary>
        /// <param name="players"> The amount of players in the game. </param>
        void RunTest(int players);

        /// <summary>
        /// Runs the test with AI players
        /// </summary>
        /// <param name="players"> The amount of players in the game. </param>
        /// <param name="ais"> The amount of ai's in the game. </param>
        void RunWithAi(int players, int ais);
    }
}
