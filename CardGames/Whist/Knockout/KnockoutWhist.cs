// KnockoutWhist.cs
// <copyright file="KnockoutWhist.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGames.Whist.Knockout
{
    /// <summary>
    /// An implementation of knockout whist.
    /// </summary>
    public class KnockoutWhist : Game<WhistPlayer, WhistInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KnockoutWhist" /> class.
        /// </summary>
        public KnockoutWhist()
        {
            this.MaxPlayers = 7;
            this.Players = new List<WhistPlayer>();

            this.CardsInHand = 7;
        }

        /// <summary>
        /// Gets or sets how many cards should be in each player's hand.
        /// </summary>
        public int CardsInHand { get; set; }

        /// <summary>
        /// Begins the game.
        /// </summary>
        public override void Start()
        {
            // The eventual winner
            WhistPlayer winner = this.Players[0];

            do
            {
                // Play a single hand of Whist
                Whist hand = new Whist();
                hand.AddPlayer(this.Players);
                hand.CardsInHand = this.CardsInHand;
                hand.Start();

                // Detect all losers
                List<WhistPlayer> losers = this.DetectLoser(hand.Players);
                WhistPlayer loser;

                if (losers.Count == 1)
                {
                    // If there is a clear loser, remove him
                    loser = losers[0];
                }
                else 
                {
                    // Else pick one at random
                    Random rand = new Random();
                    int index = rand.Next(losers.Count);
                    loser = losers[index];
                }
                
                // Check for winners
                if (this.CardsInHand == 1 && this.Players.Count > 1)
                {
                    // If this was the last round, pick the winner
                    winner = hand.Winner;
                    break;
                }
                else if (this.Players.Count == 1)
                {
                    // If all other players have been eliminated, break with the winner
                    winner = this.Players[0];
                    break;
                }

                // Else continue with the next round
                // Remove the loser and decrement the number of cards
                this.Players.Remove(loser);
                this.CardsInHand--;

                Console.WriteLine("Player {0} has been eliminated", loser.Score);
            }
            while (this.Players.Count > 1);

            this.Winner = winner;
        }

        /// <summary>
        /// Detects all players with the lowest score
        /// </summary>
        /// <param name="players"> The players to test between </param>
        /// <returns> A list of all players with the same lowest score</returns>
        protected List<WhistPlayer> DetectLoser(IEnumerable<WhistPlayer> players)
        {
            int worstScore = 0;
            List<WhistPlayer> losers = new List<WhistPlayer>();
            
            foreach (WhistPlayer compare in players)
            {
                if (losers.Count == 0)
                {
                    losers.Add(compare);
                    worstScore = compare.Score;
                }
                else if (compare.Score == worstScore)
                {
                    losers.Add(compare);
                }
                else if (compare.Score < worstScore)
                {
                    losers.Clear();

                    worstScore = compare.Score;
                    losers.Add(compare);
                }
            }

            return losers;
        }
    }
}
