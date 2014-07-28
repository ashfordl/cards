// CardHelper.cs
// <copyright file="CardHelper.cs"> This code is protected under the MIT License. </copyright>
using CardsLibrary;

namespace CardGames.Shed
{
    /// <summary>
    /// An extension class for the Card
    /// </summary>
    public static class CardHelper
    {
        /// <summary>
        /// Works out of a card is one up, one down, or equal in value to another card.
        /// </summary>
        /// <param name="c1"> The card checking. </param>
        /// <param name="c2"> The card being checked. </param>
        /// <returns> If the card being checked is one up, one down, or equal in value to the card checking. </returns>
        public static bool OneUpDownOrEqual(this Card c1, Card c2)
        {
            // Convert the values to integers
            int c1Val = (int)c1.Value;
            int c2Val = (int)c2.Value;

            if (c1Val == c2Val)
            {
                // If the values are the same
                return true;
            }
            else if (c1Val - 1 == c2Val)
            {
                // If c1's value is one greater than c2's
                return true;
            }
            else if (c1Val + 1 == c2Val)
            {
                // If c1's value is one less than c2's
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
