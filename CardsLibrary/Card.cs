// Card.cs
// <copyright file="Card.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using System.Linq;

namespace CardsLibrary
{
    /// <summary>
    /// A class to model a single playing card.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Card" /> class. 
        /// </summary>
        public Card()
        {
            this.Value = CardsLibrary.Value.Null;
            this.Suit = CardsLibrary.Suit.Null;
            this.Comparer = new CardComparer(new Dictionary<Card, int>());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card" /> class. 
        /// </summary>
        /// <param name="val"> The value of the card. </param>
        /// <param name="suit"> The suit of the card. </param>
        /// <param name="comparer"> The card comparer. </param>
        public Card(Value val, Suit suit, CardComparer comparer)
        {
            this.Value = val;
            this.Suit = suit;
            this.Comparer = comparer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card" /> class. 
        /// </summary>
        /// <param name="val"> The value of the card, as an integer from <see cref="Value"/>. </param>
        /// <param name="suit"> The suit of the card, as an integer from <see cref="Suit"/>. </param>
        /// <param name="comparer"> The card comparer. </param>
        public Card(int val, int suit, CardComparer comparer)
        {
            // Converts the integers into the correct value and suit
            CardsLibrary.Value cVal = (CardsLibrary.Value)val;
            CardsLibrary.Suit cSuit = (CardsLibrary.Suit)suit;

            // Creates the card with the correct value and suit
            this.Value = cVal;
            this.Suit = cSuit;
            this.Comparer = comparer;
        }

        /// <summary>
        /// Gets or sets the value of the card. 
        /// </summary>
        public Value Value { get; set; } 

        /// <summary>
        /// Gets or sets the suit of the card.
        /// </summary>
        public Suit Suit { get; set; }

        /// <summary>
        /// Gets or sets the card comparer.
        /// </summary>
        public CardComparer Comparer { get; set; }

        /// <summary>
        /// Gets a value indicating whether the card is valid or not.
        /// </summary>
        public bool Valid
        {
            get
            {
                if (Value != Value.Null && Suit != Suit.Null)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the card is red.
        /// </summary>
        public bool Red
        {
            get
            {
                if (Suit == Suit.Hearts || Suit == Suit.Diamonds)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the card is black.
        /// </summary>
        public bool Black
        {
            get
            {
                if (Suit == Suit.Clubs || Suit == Suit.Spades)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Returns the lowest card from a collection of cards.
        /// </summary>
        /// <param name="cards"> The collection of cards to be compared. </param>
        /// <returns> The lowest card from the collection. </returns>
        public static Card LowestCardFromArray(IEnumerable<Card> cards)
        {
            Card lowest = cards.First();

            // Compares current lowest against all cards in the collection
            foreach (Card c in cards)
            {
                if (c <= lowest)
                {
                    lowest = c;
                }
            }

            return lowest;
        }

        /// <summary>
        /// Returns the highest card from a collection of cards.
        /// </summary>
        /// <param name="cards"> The collection of cards to be compared. </param>
        /// <returns> The highest card. </returns>
        public static Card HighestCardFromArray(IEnumerable<Card> cards)
        {
            Card highest = cards.First();

            // Compares current highest against all cards in the collection
            foreach (Card c in cards)
            {
                if (c > highest)
                {
                    highest = c;
                }
            }

            return highest;
        }

        /// <summary>
        /// Checks if two cards are equal.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the two cards are equal. </returns>
        public static bool operator ==(Card c1, Card c2)
        {
            return c1.Comparer.Equal(c1, c2);
        }

        /// <summary>
        /// Checks if two cards are not equal.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the two cards are not equal. </returns>
        public static bool operator !=(Card c1, Card c2)
        {
            return !c1.Comparer.Equal(c1, c2);
        }

        /// <summary>
        /// Checks if one card is less than the other.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the first card is less than the second. </returns>
        public static bool operator <(Card c1, Card c2)
        {
            return c1.Comparer.LessThan(c1, c2);
        }

        /// <summary>
        /// Checks if one card is greater than the other.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the first card is greater than the second. </returns>
        public static bool operator >(Card c1, Card c2)
        {
            return c1.Comparer.GreaterThan(c1, c2);
        }

        /// <summary>
        /// Checks if one card is less than or equal to the other.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the first card is less than or equal to the second. </returns>
        public static bool operator <=(Card c1, Card c2)
        {
            return c1.Comparer.LessThan(c1, c2) || c1 == c2;
        }

        /// <summary>
        /// Checks if one card is greater than or equal to the other.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the first card is greater than or equal to the second. </returns>
        public static bool operator >=(Card c1, Card c2)
        {
            return c1.Comparer.GreaterThan(c1, c2) || c1 == c2;
        }

        /// <summary>
        /// Gets the hash code of the card.
        /// </summary>
        /// <returns> The hash code of the card. </returns>
        public override int GetHashCode()
        {
            int r = 1;
            r = (r * 31) + (int)this.Value;
            r = (r * 31) + (int)this.Suit;
            return r;
        }

        /// <summary>
        /// Represents the card as a short string.
        /// </summary>
        /// <returns> A concise string to represent the card. </returns>
        public string ToShortString()
        {
            string str = null;

            switch (Value)
            {
                case Value.Ace:
                    str += 'A';
                    break;
                case Value.Two:
                    str += '2';
                    break;
                case Value.Three:
                    str += '3';
                    break;
                case Value.Four:
                    str += '4';
                    break;
                case Value.Five:
                    str += '5';
                    break;
                case Value.Six:
                    str += '6';
                    break;
                case Value.Seven:
                    str += '7';
                    break;
                case Value.Eight:
                    str += '8';
                    break;
                case Value.Nine:
                    str += '9';
                    break;
                case Value.Ten:
                    str += '0';
                    break;
                case Value.Jack:
                    str += 'J';
                    break;
                case Value.Queen:
                    str += 'Q';
                    break;
                case Value.King:
                    str += 'K';
                    break;
                case Value.Null:
                    str += '-';
                    break;
            }

            switch (Suit)
            {
                case Suit.Clubs:
                    str += 'C';
                    break;
                case Suit.Diamonds:
                    str += 'D';
                    break;
                case Suit.Spades:
                    str += 'S';
                    break;
                case Suit.Hearts:
                    str += 'H';
                    break;
                case Suit.Null:
                    str += '-';
                    break;
            }

            return str;
        }

        /// <summary>
        /// Represents the card as a unicode string.
        /// </summary>
        /// <returns> A concise string to represent the card. </returns>
        public string ToUnicodeString()
        {
            string str = null;

            switch (Value)
            {
                case Value.Ace:
                    str += 'A';
                    break;
                case Value.Two:
                    str += '2';
                    break;
                case Value.Three:
                    str += '3';
                    break;
                case Value.Four:
                    str += '4';
                    break;
                case Value.Five:
                    str += '5';
                    break;
                case Value.Six:
                    str += '6';
                    break;
                case Value.Seven:
                    str += '7';
                    break;
                case Value.Eight:
                    str += '8';
                    break;
                case Value.Nine:
                    str += '9';
                    break;
                case Value.Ten:
                    str += '0';
                    break;
                case Value.Jack:
                    str += 'J';
                    break;
                case Value.Queen:
                    str += 'Q';
                    break;
                case Value.King:
                    str += 'K';
                    break;
                case Value.Null:
                    str += '-';
                    break;
            }

            switch (Suit)
            {
                case Suit.Clubs:
                    str += '♣';
                    break;
                case Suit.Diamonds:
                    str += '♦';
                    break;
                case Suit.Spades:
                    str += '♠';
                    break;
                case Suit.Hearts:
                    str += '♥';
                    break;
                case Suit.Null:
                    str += '-';
                    break;
            }

            return str;
        }

        /// <summary>
        /// Represents the card as a string.
        /// </summary>
        /// <returns> A string to represent the card. </returns>
        public override string ToString()
        {
            return string.Format("{0} of {1}", this.Value.ToString(), this.Suit.ToString());
        }

        /// <summary>
        /// Compares an object against the current object.
        /// </summary>
        /// <param name="obj"> The object to be compared against. </param>
        /// <returns> Whether the current object is equal to the passed object. </returns>
        public override bool Equals(object obj)
        {
            // If the obj is not a card return false
            if (!(obj is Card))
            {
                return false;
            }

            // obj must be a Card
            Card c = obj as Card;

            // Return if the values are the same
            return this.Suit == c.Suit && this.Value == c.Value;
        }
    }
}