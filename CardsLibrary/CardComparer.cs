// CardComparer.cs
// <copyright file="CardComparer.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using System.Linq;

namespace CardsLibrary
{
    /// <summary>
    /// A class used for comparing cards.
    /// </summary>
    public class CardComparer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardComparer" /> class.
        /// </summary>
        /// <param name="cardScores"> The dictionary of card scores to use for comparisons. </param>
        public CardComparer(Dictionary<Card, int> cardScores)
        {
            this.CardScores = cardScores;
        }

        /// <summary>
        /// Gets or sets the dictionary of cards and their scores.
        /// </summary>
        public Dictionary<Card, int> CardScores { get; set; }

        /// <summary>
        /// Gets the score of a card.
        /// </summary>
        /// <param name="c"> The card to get a score from. </param>
        /// <returns> The score value of the card. </returns>
        public int GetCardScore(Card c)
        {
            // Check for the same card
            foreach (Card key in this.CardScores.Keys)
            {
                if (key.Suit == c.Suit && key.Value == c.Value)
                {
                    return this.CardScores[key];
                }
            }

            // Check for the same value if no score found
            foreach (Card key in this.CardScores.Keys.Where(card => card.Suit == Suit.Null))
            {
                if (key.Value == c.Value)
                {
                    return this.CardScores[key];
                }
            }

            // Check for the same suit if no score found
            foreach (Card key in this.CardScores.Keys.Where(card => card.Value == Value.Null))
            {
                if (key.Suit == c.Suit)
                {
                    return this.CardScores[key];
                }
            }
            
            // Return a defualt value of 0 if nothing was found
            return 0;
        }

        /// <summary>
        /// Checks if a card is equal in score to another.
        /// </summary>
        /// <param name="c1"> The first card to check. </param>
        /// <param name="c2"> The second card to check. </param>
        /// <returns> Whether the card's scores are equal. </returns>
        public bool Equal(Card c1, Card c2)
        {
            return this.GetCardScore(c1) == this.GetCardScore(c2);
        }

        /// <summary>
        /// Checks if a card is greater than another card.
        /// </summary>
        /// <param name="c1"> The first card to check. </param>
        /// <param name="c2"> The second card to check. </param>
        /// <returns> Whether c1 is greater than c2. </returns>
        public bool GreaterThan(Card c1, Card c2)
        {
            return this.GetCardScore(c1) > this.GetCardScore(c2);
        }

        /// <summary>
        /// Checks if a card is lesser than another card.
        /// </summary>
        /// <param name="c1"> The first card to check. </param>
        /// <param name="c2"> The second card to check. </param>
        /// <returns> Whether c1 is less than c2. </returns>
        public bool LessThan(Card c1, Card c2)
        {
            return this.GetCardScore(c1) < this.GetCardScore(c2);
        }
    }
}
