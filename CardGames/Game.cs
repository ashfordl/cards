// Game.cs
// <copyright file="Game.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;

namespace CardGames
{
    /// <summary>
    /// An abstract super-class of every card game. Any games should sub-class this.
    /// </summary>
    /// <typeparam name="TPlayer"> The player super-class for all player classes of the game. </typeparam>
    /// <typeparam name="TInfo"> The info class for the game. </typeparam>
    public abstract class Game<TPlayer, TInfo> 
        where TInfo : GameInfo 
        where TPlayer : Player<TInfo>
    {
        /// <summary>
        /// Gets or sets the maximum players allowed in the game.
        /// </summary>
        public int MaxPlayers { get; protected set; }

        /// <summary>
        /// Gets or sets the list of players in the game.
        /// </summary>
        protected virtual List<TPlayer> Players { get; set; }

        /// <summary>
        /// Adds a player to the players list.
        /// </summary>
        /// <param name="p"> The player to be added. </param>
        public void AddPlayer(TPlayer p)
        {
            if (this.MaxPlayers <= 0 || this.Players.Count + 1 <= this.MaxPlayers)
            {
                this.Players.Add(p);
            }
            else
            {
                throw new TooManyPlayersException("Max Players " + this.MaxPlayers);
            }
        }

        /// <summary>
        /// Begins the game.
        /// </summary>
        public abstract void Start();
    }
}
