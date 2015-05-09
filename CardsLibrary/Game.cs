// Game.cs
// <copyright file="Game.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using CardsLibrary;

namespace CardsLibrary
{
    /// <summary>
    /// An abstract super-class of every card game. Any games should sub-class this.
    /// </summary>
    /// <typeparam name="TPlayer"> The player super-class for all player classes of the game. </typeparam>
    /// <typeparam name="TInfo"> The info class for the game. </typeparam>
    /// <typeparam name="TMove"> The class that players will return after making a move. </typeparam>
    public abstract class Game<TPlayer, TInfo, TMove>
        where TPlayer : Player<TInfo, TMove>
        where TInfo : GameInfo
    {
        /// <summary>
        /// Gets or sets the maximum number of players allowed in the game.
        /// </summary>
        public int MaxPlayers { get; protected set; }

        /// <summary>
        /// Gets or sets the list of players in the game.
        /// </summary>
        public List<TPlayer> Players { get; protected set; }

        /// <summary>
        /// Gets or sets the winner of the game.
        /// </summary>
        public TPlayer Winner { get; protected set; }

        /// <summary>
        /// Gets or sets the deck.
        /// </summary>
        public List<Card> Deck { get; protected set; }

        /// <summary>
        /// Adds a player to the players list.
        /// </summary>
        /// <param name="p"> The player to be added. </param>
        public void AddPlayer(TPlayer p)
        {
            if ((this.MaxPlayers <= 0 || this.Players.Count + 1 <= this.MaxPlayers) && this.Players.Count <= 52)
            {
                this.Players.Add(p);
                this.Players[this.Players.Count - 1].ID = this.Players.Count - 1;
            }
            else
            {
                throw new TooManyPlayersException(string.Format("Max Players {0}, limit 52.", this.MaxPlayers));
            }
        }

        /// <summary>
        /// Adds a collection of players to the players list.
        /// </summary>
        /// <param name="p"> The collection of players to be added. </param>
        /// <remarks> If too many players are added, as many as possible will be added before an exception is raised. </remarks>
        public void AddPlayer(IEnumerable<TPlayer> p)
        {
            foreach (TPlayer player in p)
            {
                this.AddPlayer(player);
            }
        }

        /// <summary>
        /// Begins the game.
        /// </summary>
        public abstract void Start();

        /// <summary>
        /// Deals the pack to the players.
        /// </summary>
        /// <param name="comparer"> The card comparer to give to all cards. </param> 
        /// <param name="shuffle"> Whether to shuffle the Deck or not. </param>
        /// <param name="cards"> The number of cards to deal. </param>
        /// <param name="allCard"> Whether all the cards are getting dealt. This will ignore the cards parameter. </param>
        public void Deal(CardComparer comparer, bool shuffle = true, int cards = 0, bool allCard = false)
        {
            // Create a Deck
            this.Deck = CardFactory.PopulateDeck(comparer, shuffle);
            
            // Calculate how many cards to deal per player
            int cardsToDeal = (int)Math.Floor((double)(52 / this.Players.Count));

            // If the method has been called with a specified number of cards to deal
            if (cards != 0 && !allCard)
            {
                // Validate parameter
                if (cards < 1 || cards > cardsToDeal)
                {
                    // Throw an exception if the argument is invalid
                    throw new ArgumentException(string.Format("Invalid number of cards to deal. Min: 1, Max: {0}, Given: {1}", cardsToDeal, cards));
                }
                else
                {
                    // Assign the parameter to the variable used by the loop
                    cardsToDeal = cards;
                }
            }
            
            // If some cards should be left over, do so, else deal them all
            if (!allCard) 
            { 
                // For each player, deal their hand
                foreach (TPlayer player in this.Players)
                {
                    player.Hand = Deck.GetRange(0, cardsToDeal);
                    Deck.RemoveRange(0, cardsToDeal);
                }
            }
            else
            {
                // Deal all cards
                for (int i = 0; i < Deck.Count; i++)
                {
                    this.Players[i % this.Players.Count].Hand.Add(Deck[i]);
                }

                // Clear the Deck 
                Deck.RemoveRange(0, Deck.Count);
            }
        }

        /// <summary>
        /// Gives a new card comparer to all cards in the game.
        /// </summary>
        /// <param name="comparer"> The new comparer. </param>
        public void GiveNewComparerToAllCard(CardComparer comparer)
        {
            // Assign comparer in the deck
            foreach (Card c in this.Deck)
            {
                c.Comparer = comparer;
            }

            // Assign comparer in each player's hand
            foreach (TPlayer p in this.Players)
            {
                foreach (Card c in p.Hand)
                {
                    c.Comparer = comparer;
                }
            }
        }
    }
}
