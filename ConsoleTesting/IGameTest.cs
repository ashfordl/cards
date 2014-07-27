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
        void RunTest(int players);

        /// <summary>
        /// Runs the test with AI players
        /// </summary>
        void RunWithAi(int players, int ais);
    }
}
