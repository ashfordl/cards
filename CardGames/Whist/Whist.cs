// Whist.cs
// <copyright file="Whist.cs"> This code is protected under the MIT License. </copyright>
using System;
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

            this.CardsInHand = 7;
        }

        /// <summary>
        /// Gets or sets all players in the game.
        /// </summary>
        public override List<WhistPlayer> Players { get; protected set; }

        /// <summary>
        /// Gets or sets how many cards should be in each player's hand.
        /// </summary>
        public int CardsInHand { get; set; }

        /// <summary>
        /// Begins the game.
        /// </summary>
        public override void Start()
        {
            this.Deal(cards: this.CardsInHand);

            Suit trumps = (Suit)new Random().Next(3) + 1; // Add one so it can't be Suit.Null
            bool aceHigh = true;

            for (int i = 0; i < this.CardsInHand; i++)
            {
                List<Card> laid = new List<Card>();
                Suit first = Suit.Null;

                foreach (WhistPlayer play in this.Players)
                {
                    Console.WriteLine("Player " + this.Players[this.Players.IndexOf(play)].ID);

                    // Create a new instance of WhistInfo
                    WhistInfo info = new WhistInfo();
                    info.CardsInPlay = laid;
                    info.FirstSuitLaid = first;
                    info.RoundNumber = 0;
                    info.Trumps = trumps;
                    info.AceHigh = aceHigh;

                    // Make the move, and remove the card from their hand
                    Card c = play.MakeMove(info);
                    play.Hand.Remove(c);
                    laid.Add(c);
                    
                    // Update the first laid card, if necessary
                    if (first == Suit.Null)
                    {
                        first = c.Suit;
                    }
                }

                // Detect winner
                WhistPlayer winner = this.Players[laid.IndexOf(Card.HighestCardFromArray(laid))];

                Console.WriteLine("Player " + winner.ID + " won the hand!\n");

                this.OrderPlayers(this.Players.IndexOf(winner));
            }
        }

        /// <summary>
        /// Re-orders the Players for the next trick.
        /// </summary>
        /// <param name="winnerIndex"> The winner of the last trick. </param>
        protected void OrderPlayers(int winnerIndex)
        {
            List<WhistPlayer> prevPlayers = this.Players.GetRange(0, winnerIndex);
            this.Players.RemoveRange(0, winnerIndex);
            this.Players.AddRange(prevPlayers);
        }
    }
}
