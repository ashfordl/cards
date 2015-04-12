// Player.cs
// <copyright file="Player.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using CardsLibrary;

namespace CardsLibrary
{
    /// <summary>
    /// An abstract super-class of every player implementation. Any players should sub-class this.
    /// </summary>
    /// <typeparam name="TInfo"> The implementation of GameInfo to be used. </typeparam>
    public abstract class Player<TInfo, TMove> 
        where TInfo : GameInfo
    {
        /// <summary>
        /// Gets or sets the player's ID number.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the player's hand of cards.
        /// </summary>
        public List<Card> Hand { get; set; }

        /// <summary>
        /// Gets or sets the player's score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Makes the appropriate move using the given arguments.
        /// </summary>
        /// <param name="args"> The information required to make the move. </param>
        /// <returns> The move made. </returns>
        public abstract TMove MakeMove(TInfo args);

        /// <summary>
        /// Orders the hand.
        /// </summary>
        protected abstract void OrderCards();
    }
}
