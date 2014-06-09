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
            this.MaxPlayers = 7;
            this.Players = new List<WhistPlayer>();
            this.ActivePlayers = new List<WhistPlayer>(this.Players);
        }

        /// <summary>
        /// Gets or sets the current round.
        /// </summary>
        protected int Round { get; set; }

        /// <summary>
        /// Gets or sets the current active players.
        /// </summary>
        protected List<WhistPlayer> ActivePlayers { get; set; }

        /// <summary>
        /// Gets or sets all players in the game.
        /// </summary>
        protected override List<WhistPlayer> Players { get; set; }

        /// <summary>
        /// Begins the game.
        /// </summary>
        public override void Start()
        {
            // Each iteration is a round
            while (this.ActivePlayers.Count > 1)
            {
                List<Card> cards = new List<Card>();
                
                Suit trumps = Suit.Null;
                Suit first = Suit.Null;

                List<WhistPlayer> plays = this.OrderPlayers();
                foreach (WhistPlayer player in plays)
                {
                    WhistInfo roundInfo = new WhistInfo();
                    roundInfo.FirstSuitLaid = first;
                    roundInfo.Trumps = trumps;
                    roundInfo.RoundNumber = this.Round;
                    //// Init other whistinfo stuff

                    Card card = player.MakeMove(roundInfo);
                    cards.Add(card);
                }

                this.Round++;
            }
        }

        /// <summary>
        /// Orders the players for dealing and play order.
        /// </summary>
        /// <returns> An ordered list of players. </returns>
        protected List<WhistPlayer> OrderPlayers()
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
        }
    }
}
