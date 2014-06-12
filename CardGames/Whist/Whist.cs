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
                    Console.WriteLine("Player " + this.Players.IndexOf(play));

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
                // TODO: Re-order players in order to allow them to play first
                Console.WriteLine(this.Players.IndexOf(winner) + "\n");
            }
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
