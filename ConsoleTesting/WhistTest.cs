// WhistTest.cs
// <copyright file="WhistTest.cs"> This code is protected under the MIT License. </copyright>
using CardGames.Whist;

namespace ConsoleTesting
{
    /// <summary>
    /// The whist test class
    /// </summary>
    public class WhistTest
    {
        /// <summary>
        /// Run the test
        /// </summary>
        public void Run()
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
    }
}
