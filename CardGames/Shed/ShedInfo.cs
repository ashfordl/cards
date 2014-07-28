// ShedInfo.cs
// <copyright file="ShedInfo.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using CardsLibrary;

namespace CardGames.Shed
{
    /// <summary>
    /// An extension of <see cref="GameInfo"/> that provides extra properties for <see cref="Shed"/>.
    /// </summary>
    public class ShedInfo : GameInfo
    {
        /// <summary>
        /// Gets or sets the rules in play.
        /// </summary>
        public Specials RuleInPlay { get; set; }

        /// <summary>
        /// Gets or sets the players left.
        /// </summary>
        public int PlayersLeft { get; set; }

        /// <summary>
        /// Gets or sets the deck left over.
        /// </summary>
        public List<Card> Deck { get; set; }
    }
}
