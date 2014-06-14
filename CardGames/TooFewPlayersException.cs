// TooFewPlayersException.cs
// <copyright file="TooFewPlayersException.cs"> This code is protected under the MIT License. </copyright>
using System;

namespace CardGames
{
    /// <summary>
    /// An exception raised when TooFewPlayers are added to a game.
    /// </summary>
    [Serializable]
    public class TooFewPlayersException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TooFewPlayersException" /> class.
        /// </summary>
        public TooFewPlayersException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooFewPlayersException" /> class.
        /// </summary>
        /// <param name="message"> The message to explain the exception. </param>
        public TooFewPlayersException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooFewPlayersException" /> class.
        /// </summary>
        /// <param name="message"> The message to explain the exception. </param>
        /// <param name="inner"> The exception that caused the TooFewPlayersException. </param>
        public TooFewPlayersException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
