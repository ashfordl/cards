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
                List<WhistPlayer> losers = this.DetectLosers(hand.Players);

                if (losers.Count == 1)
                {
                    // If there is a clear loser, remove him
                    RemoveLoser(losers[0]);
                }
                else if (losers.Count > 1)
                {
                    // Else pick one at random
                    Random rand = new Random();
                    int index = rand.Next(losers.Count);
                    RemoveLoser(losers[index]);
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
                // Decrement the number of cards
                this.CardsInHand--;
            }
            while (this.Players.Count > 1);

            // Assign the winner
            this.Winner = winner;
        }

        /// <summary>
        /// Detects all players with the lowest score
        /// </summary>
        /// <param name="players"> The players to test between </param>
        /// <returns> A list of all players with the same lowest score</returns>
        protected List<WhistPlayer> DetectLosers(IEnumerable<WhistPlayer> players)
        {
            List<WhistPlayer> losers = new List<WhistPlayer>();
            
            foreach (WhistPlayer compare in players)
            {
                if (compare.Score == 0)
                {
                    losers.Add(compare);
                }
            }

            return losers;
        }

        /// <summary>
        /// Removes the loser from the list of players
        /// </summary>
        /// <param name="loser"> The player to remove </param>
        protected void RemoveLoser(WhistPlayer loser)
        {
            this.Players.Remove(loser);
            Console.WriteLine("Player {0} has been eliminated", loser.Score);
        }
    }
}
