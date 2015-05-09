// GameInfo.cs
// <copyright file="GameInfo.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using CardsLibrary;

namespace CardsLibrary
{
    /// <summary>
    /// An object to represent game data to pass to player classes. 
    /// </summary>
    public class GameInfo
    {
        /// <summary>
        /// Gets or sets the current round number.
        /// </summary>
        public int RoundNumber { get; set; }
        
        /// <summary>
        /// Gets or sets the current cards in play.
        /// </summary>
        public List<Card> CardsInPlay { get; set; }
    }
}
