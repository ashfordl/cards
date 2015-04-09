// TooManyPlayersException.cs
// <copyright file="TooManyPlayersException.cs"> This code is protected under the MIT License. </copyright>
using System;

namespace CardsLibrary
{
    /// <summary>
    /// An exception raised when TooManyPlayers are added to a game.
    /// </summary>
    [Serializable]
    public class TooManyPlayersException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyPlayersException" /> class.
        /// </summary>
        public TooManyPlayersException() 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyPlayersException" /> class.
        /// </summary>
        /// <param name="message"> The message to explain the exception. </param>
        public TooManyPlayersException(string message) : base(message) 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TooManyPlayersException" /> class.
        /// </summary>
        /// <param name="message"> The message to explain the exception. </param>
        /// <param name="inner"> The exception that caused the TooManyPlayersException. </param>
        public TooManyPlayersException(string message, Exception inner) : base(message, inner) 
        { 
        }
    }
}