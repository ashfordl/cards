// Card.cs
// <copyright file="Card.cs"> This code is protected under the MIT License. </copyright>
using System;
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
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card" /> class. 
        /// </summary>
        /// <param name="val"> The value of the card. </param>
        /// <param name="suit"> The suit of the card. </param>
        public Card(Value val, Suit suit)
        {
            this.Value = val;
            this.Suit = suit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card" /> class. 
        /// </summary>
        /// <param name="val"> The value of the card, as an integer from <see cref="Value"/>. </param>
        /// <param name="suit"> The suit of the card, as an integer from <see cref="Suit"/>. </param>
        public Card(int val, int suit)
        {
            // Converts the integers into the correct value and suit
            CardsLibrary.Value cVal = (CardsLibrary.Value)val;
            CardsLibrary.Suit cSuit = (CardsLibrary.Suit)suit;

            // Creates the card with the correct value and suit
            this.Value = cVal;
            this.Suit = cSuit;
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
        /// Gets a value indicating whether the card is valid or not.
        /// </summary>
        public bool IsValid
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
        public bool IsRed
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
        public bool IsBlack
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
        /// Gets the suit order of this card.
        /// </summary>
        public int SuitVal 
        {
            get
            {
                switch (Suit)
                {
                    case Suit.Clubs:
                        return Settings.ClubsOrder;
                    case Suit.Diamonds:
                        return Settings.DiamondsOrder;
                    case Suit.Spades:
                        return Settings.SpadesOrder;
                    case Suit.Hearts:
                        return Settings.HeartsOrder;
                    case Suit.Null:
                        return Settings.NullOrder;
                    default:
                        return 5;
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
            return c1.Equals(c2);
        }

        /// <summary>
        /// Checks if two cards are not equal.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the two cards are not equal. </returns>
        public static bool operator !=(Card c1, Card c2)
        {
            return !c1.Equals(c2);
        }

        /// <summary>
        /// Checks if one card is less than the other.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the first card is less than the second. </returns>
        public static bool operator <(Card c1, Card c2)
        {
            return c1.LessThan(c2);
        }

        /// <summary>
        /// Checks if one card is greater than the other.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the first card is greater than the second. </returns>
        public static bool operator >(Card c1, Card c2)
        {
            return c1.GreaterThan(c2);
        }

        /// <summary>
        /// Checks if one card is less than or equal to the other.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the first card is less than or equal to the second. </returns>
        public static bool operator <=(Card c1, Card c2)
        {
            return c1.LessThan(c2) || c1 == c2;
        }

        /// <summary>
        /// Checks if one card is greater than or equal to the other.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Whether the first card is greater than or equal to the second. </returns>
        public static bool operator >=(Card c1, Card c2)
        {
            return c1.GreaterThan(c2) || c1 == c2;
        }

        /// <summary>
        /// Gets the hash code of the card.
        /// </summary>
        /// <returns> The hash code of the card. </returns>
        public override int GetHashCode()
        {
            return (int)Math.Pow((int)this.Suit, (int)this.Value);
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
        /// Compares whether the current object is greater than a passed card.
        /// </summary>
        /// <param name="c"> The card to be compared against. </param>
        /// <returns> Whether the current object is greater than the passed card. </returns>
        public bool GreaterThan(Card c) 
        {
            // Return that this card is lower if they are equal
            if (this == c)
            {
                return false;
            }

            // If they have different suits
            if (this.SuitVal != c.SuitVal)
            {
                // If this cards suit is greater, return true
                if (this.SuitVal > c.SuitVal)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // If the suit values are the same and ace is high
            if (Settings.AceHigh)
            {
                // If this card is an ace and card c is not
                if (this.Value == Value.Ace && c.Value != Value.Ace)
                {
                    return true;
                }

                // If card c is an ace and this is not
                if (this.Value != Value.Ace && c.Value == Value.Ace)
                {
                    return false;
                }

                // If they are both aces, return false that its lower
                if (this.Value == Value.Ace && c.Value == Value.Ace)
                {
                    return false;
                }
            }

            // Which has the better number value is returned
            if ((int)this.Value > (int)c.Value)
            {
                return true;
            }
            else if ((int)this.Value < (int)c.Value)
            {
                return false;
            }
            else 
            {
                return false;
            }
        }

        /// <summary>
        /// Compares whether the current object is less than a passed card.
        /// </summary>
        /// <param name="c"> The card to be compared against. </param>
        /// <returns> Whether the current object is less than the passed card. </returns>
        public bool LessThan(Card c)
        {
            return (this.GreaterThan(c) || this.Equals(c)) ? false : true;
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

            // If either suit is null, compare values
            if (this.Suit == Suit.Null || c.Suit == Suit.Null)
            {
                return this.Value == c.Value;
            }

            // If either value is null, compare suits
            if (this.Value == Value.Null || c.Value == Value.Null)
            {
                return this.Suit == c.Suit;
            }

            // Neither suit nor either value is null, so compare both suit and value
            return (this.Value == c.Value) && (this.Suit == c.Suit);
        }
    }
}