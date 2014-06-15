// Player.cs
// <copyright file="Player.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using CardsLibrary;

namespace CardGames
{
    /// <summary>
    /// An abstract super-class of every player implementation. Any players should sub-class this.
    /// </summary>
    /// <typeparam name="I"> The implementation of GameInfo to be used. </typeparam>
    public abstract class Player<I> where I : GameInfo
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
        /// <returns> The card chosen. </returns>
        public abstract Card MakeMove(I args);

        /// <summary>
        /// Orders the hand from low to high.
        /// </summary>
        protected void OrderCards()
        {
            // A list of a list of cards of one suit
            List<List<Card>> orderedBySuit = new List<List<Card>>(4);

            // Check through each suit
            for (int i = 0; i < 4; i++)
            {
                // Create a new item in the list for the suit
                orderedBySuit.Add(new List<Card>());
                
                // Check through each card in the hand
                foreach (Card c in this.Hand)
                {
                    // If the suit is equal to the one being looked for add it to the list
                    if (c.Suit == (Suit)i + 1)
                    {
                        orderedBySuit[i].Add(c);
                    }
                }
            }

            // Clear the hand
            this.Hand.Clear();

            // Iterate through each list of a suit
            for (int i = 0; i < 4; i++)
            {
                // While there are cards in the list
                while (orderedBySuit[i].Count != 0)
                {
                    // Pick the lowest card and add it to the hand
                    Card lowest = Card.LowestCardFromArray(orderedBySuit[i]);
                    orderedBySuit[i].Remove(lowest);
                    this.Hand.Add(lowest);
                }
            }
        }
    }
}
