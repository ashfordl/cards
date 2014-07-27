// WhistPlayer.cs
// <copyright file="WhistPlayer.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using System.Linq;
using CardsLibrary;

namespace CardGames.Whist
{
    /// <summary>
    /// An abstract super-class of every <see cref="Whist"/> player.
    /// </summary>
    public abstract class WhistPlayer : Player<WhistInfo>
    {
        /// <summary>
        /// Detects the valid cards that could be played.
        /// </summary>
        /// <param name="args"> The current game info. </param>
        /// <returns> A list of valid cards. </returns>
        protected List<Card> DetectValidCards(WhistInfo args)
        {
            // Can the hand follow suit
            bool followSuit = Hand.Any(c => c.Suit == args.FirstSuitLaid);

            if (followSuit && args.FirstSuitLaid != Suit.Null)
            {
                // If the hand can follow suit, return only cards of the correct suit
                return this.Hand.Where(c => c.Suit == args.FirstSuitLaid).ToList();
            }
            else
            {
                // Else return all cards
                return this.Hand;
            }
        }

        /// <summary>
        /// Orders the hand.
        /// </summary>
        protected override void OrderCards()
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
