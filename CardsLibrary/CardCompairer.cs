// CardCompairer.cs
// <copyright file="CardCompairer.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsLibrary
{
    /// <summary>
    /// A class used for compairing cards.
    /// </summary>
    class CardCompairer
    {
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
