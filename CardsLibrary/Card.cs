// Card.cs
// <copyright file="Card.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardsLibrary
{
    /// <summary>
    /// This is the class that contains all the data
    /// and methods associated with a card within the game.
    /// </summary>
    /// <remarks> Fully functioning with operator overloads. </remarks>
    public class Card
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Card" /> class. 
        /// </summary>
        /// <remarks> Initializes the card as a null card. </remarks>
        public Card()
        {
            this.Value = CardsLibrary.Value.Null;
            this.Suit = CardsLibrary.Suit.Null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card" /> class. 
        /// </summary>
        /// <param name="val"> The is the value of the card. This is of type Value in Value.cs. </param>
        /// <param name="suit"> The is the suit of the card. This is of type Suit in Suit.cs. </param>
        /// <remarks> The card will be created with the specified suit and value. </remarks>
        public Card(Value val, Suit suit)
        {
            this.Value = val;
            this.Suit = suit;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Card" /> class. 
        /// </summary>
        /// <param name="val"> This is the value of the card. It is a integer and get explicitly turned into a Value from Value.cs. </param>
        /// <param name="suit"> This is the suit of the card. It is a integer and get explicitly turned into a Suit from Suit.cs.</param>
        /// <remarks> The card will be created with the specified suit and value. </remarks>
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
        /// Gets or sets the Value property. </summary>
        /// <value> The face value of the card i.e. Ace, Two, Three etc. </value>
        public Value Value { get; set; } 

        /// <summary>
        /// Gets or sets the Suit property. </summary>
        /// <value> The suit value of the card i.e. Clubs, Spades etc. </value>
        public Suit Suit { get; set; }

        /// <summary>
        /// Gets a value indicating whether the card is a valid card.
        /// </summary>
        /// <value> True if the card has a Suit and Value that are not null. </value>
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
        /// <value> True if the card is a Heart or Diamonds. </value>
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
        /// <value> True if the card is a Club or a Spade. </value>
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
        /// Gets the value of this cards suit order.
        /// </summary>
        /// <value> An integer that is equal to the suit's current ordering value according to the SuitOrder in Settings.cs. </value>
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
        /// Compares to see which card is the worst from a list of cards.
        /// </summary>
        /// <param name="cards"> The list of cards to be compared. </param>
        /// <returns> Returns the worst card from the list. </returns>
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
        /// Compares a list of cards to see which is best.
        /// </summary>
        /// <param name="cards"> This is a list of cards that will be compared to see which is best. </param>
        /// <returns> Returns the greatest card. </returns>
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
        /// <returns> Returns true if the first card is equal to the second. </returns>
        public static bool operator ==(Card c1, Card c2)
        {
            return c1.Equals(c2);
        }

        /// <summary>
        /// Checks if two cards are not equal.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Returns true if the first card is not equal to the second. </returns>
        public static bool operator !=(Card c1, Card c2)
        {
            return !c1.Equals(c2);
        }

        /// <summary>
        /// Checks if one card is less than the second.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Returns true if the first card is less then the second. </returns>
        public static bool operator <(Card c1, Card c2)
        {
            return c1.LessThan(c2);
        }

        /// <summary>
        /// Checks if one card is greater than the second.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Returns true if the first card is greater then the second. </returns>
        public static bool operator >(Card c1, Card c2)
        {
            return c1.GreaterThan(c2);
        }

        /// <summary>
        /// Checks if one card is less than or equal to the second.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Returns true if the first card is less than or equal to the second. </returns>
        public static bool operator <=(Card c1, Card c2)
        {
            return c1.LessThan(c2) || c1 == c2;
        }

        /// <summary>
        /// Checks if one card is greater than or equal to the second.
        /// </summary>
        /// <param name="c1"> The first card. </param>
        /// <param name="c2"> The second card. </param>
        /// <returns> Returns true if the first card is greater than or equal to the second. </returns>
        public static bool operator >=(Card c1, Card c2)
        {
            return c1.GreaterThan(c2) || c1 == c2;
        }

        /// <summary>
        /// Gets the hashCode of the card.
        /// </summary>
        /// <returns> Returns the Suit to the power of the value. </returns>
        public override int GetHashCode()
        {
            return (int)Math.Pow((int)this.Suit, (int)this.Value);
        }

        /// <summary>
        /// Turns the card into a short string.
        /// </summary>
        /// <returns> Returns a string of the cards shorthand representation. </returns>
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
        /// Turns the card into a string.
        /// </summary>
        /// <returns> Returns the string that represents the Card. This will be "'Value' of 'Suit'". </returns>
        public override string ToString()
        {
            return string.Format("{0} of {1}", this.Value.ToString(), this.Suit.ToString());
        }

        /// <summary>
        /// Compares a card to another given in a parameter to see which is the greater card.
        /// </summary>
        /// <param name="c"> The card to be compared against. Is of type Card. </param>
        /// <returns> Returns true if the card the method is being run on is greater than the card in the parameter. </returns>
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
        /// Compares to see which card is lowest.
        /// </summary>
        /// <param name="c"> The card to be compared against. </param>
        /// <returns> Returns true if the card that the method is being run on is the lower card. </returns>
        public bool LessThan(Card c)
        {
            return (this.GreaterThan(c) || this.Equals(c)) ? false : true;
        }

        /// <summary>
        /// Compares to see if two cards are equal.
        /// </summary>
        /// <param name="obj"> A boxed up card that will be compared to the current card. </param>
        /// <returns> Returns true if the cards are equal. </returns>
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