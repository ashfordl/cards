// Whist.cs
// <copyright file="Whist.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using CardsLibrary;

namespace CardGames.Whist
{
    /// <summary>
    /// An implementation of a single round of Whist.
    /// </summary>
    public class Whist : Game<WhistPlayer, WhistInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Whist" /> class.
        /// </summary>
        public Whist()
        {
            this.MaxPlayers = 52;
            this.Players = new List<WhistPlayer>();
        }

        /// <summary>
        /// Gets or sets all players in the game.
        /// </summary>
        protected override List<WhistPlayer> Players { get; set; }

        /// <summary>
        /// Begins the game.
        /// </summary>
        public override void Start()
        {
            
        }

        /// <summary>
        /// Orders the players for dealing and play order.
        /// </summary>
        /// <returns> An ordered list of players. </returns>
        /*protected List<WhistPlayer> OrderPlayers()
        {
            if (this.Round == 1)
            {
                return this.ActivePlayers;
            }

            int playerOffset = this.Round % this.Players.Count;
            do
            {
                if (this.ActivePlayers.Contains(this.Players[playerOffset]))
                {
                    int firstInd = this.ActivePlayers.IndexOf(this.Players[playerOffset]);
                    List<WhistPlayer> ordered = this.ActivePlayers.GetRange(firstInd, this.ActivePlayers.Count - firstInd);
                    ordered.AddRange(this.ActivePlayers.GetRange(0, firstInd));
                    return ordered;
                }
                else
                {
                    playerOffset++;
                    continue;
                }
            } 
            while (true);
        }*/
    }
}
