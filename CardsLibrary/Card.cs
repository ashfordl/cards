using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsLibrary
{
    public class Card
    {
        #region Properties
        public Value Value { get; set; } //The face value of the card i.e. ace, seven, king etc.
        public Suit Suit { get; set; } //The suit value of the card i.e. Spades etc.

        public bool IsValid //If it is a valid card so has a suit value and a face value
        {
            get
            {
                if (Value != null && Value != Value.Null && Suit != null && Suit != Suit.Null)
                {
                    return true;
                }
                return false;
            }
        }

        public int SuitVal //Returns the value of the suit of the card according to the suit order
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
        #endregion

        #region Constructors
        public Card() //Creates a null card
        {
            this.Value = CardsLibrary.Value.Null;
            this.Suit = CardsLibrary.Suit.Null;
        }

        public Card(Value val, Suit suit) //Creates a card with the specified suit and value
        {
            this.Value = val;
            this.Suit = suit;
        }

        public Card(int val, int suit) //Creates a card with the specified suit and value for integers
        {
            //Converts the integers into the correct value and suit
            CardsLibrary.Value Val = (CardsLibrary.Value)val;
            CardsLibrary.Suit Suit = (CardsLibrary.Suit)suit;
            //Creates the card with the correct value and suit
            this.Value = Val;
            this.Suit = Suit;
        }
        #endregion

        #region Comparison

        public bool GreaterThan(Card c) //Returns true if the card this meathod is run on is greater. If they are equal it will say the card it is run on is greater still
        {
            //Return that this card is lower if they are equal
            if (this == c)
                return false;

            //If they have different suits
            if (this.SuitVal != c.SuitVal)
            {
                //If this cards suit is greater, return true
                if (this.SuitVal > c.SuitVal)
                    return true;
                //If the other cards suit is greater, return false
                else
                    return false;
            }

            //If the suit values are the same and ace is high
            if (Settings.AceHigh)
            {
                //If this card is an ace and card c is not
                if (this.Value == Value.Ace && c.Value != Value.Ace)
                    return true;
                //If card c is an ace and this is not
                if (this.Value != Value.Ace && c.Value == Value.Ace)
                    return false;
                //If they are both aces, return false that its lower
                if (this.Value == Value.Ace && c.Value == Value.Ace)
                    return false;
            }

            //Which has the better number value is returned
            if ((int)this.Value > (int)c.Value)
                return true;
            else if ((int)this.Value < (int)c.Value)
                return false;
            else //If the cards have an equal suit value (but not the same suit) and a equal face value
                return false;
        }

        public static Card HighestCardFromArray(IEnumerable<Card> cards)
        {
            Card highest = cards.First();

            // Compares current highest against all cards in the collection
            foreach (Card c in cards)
                if (c > highest)
                    highest = c;

            return highest;
        }

        public bool LessThan(Card c) //Returns true if the card this meathod is run on is lower. If they are equal it will say the card it is run on is greater still
        {
            return (this.GreaterThan(c) || this.Equals(c)) ? false : true;
        }

        public static Card LowestCardFromArray(IEnumerable<Card> cards)
        {
            Card lowest = cards.First();

            // Compares current lowest against all cards in the collection
            foreach (Card c in cards)
                if (c <= lowest)
                    lowest = c;

            return lowest;
        }

        public override bool Equals(object obj)
        {
            // If the obj is not a card return false
            if (!(obj is Card))
                return false;

            // obj must be a Card
            Card c = obj as Card;

            // If either suit is null, compare values
            if (this.Suit == Suit.Null || c.Suit == Suit.Null)
                return (this.Value == c.Value);

            // If either value is null, compare suits
            if (this.Value == Value.Null || c.Value == Value.Null)
                return (this.Suit == c.Suit);

            // Neither suit nor either value is null, so compare both suit and value
            return (this.Value == c.Value) && (this.Suit == c.Suit);
        }
        #endregion

        #region Operator_Overloads
        
        public static bool operator ==(Card c1, Card c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Card c1, Card c2)
        {
            return !c1.Equals(c2);
        }

        public static bool operator <(Card c1, Card c2)
        {
            return c1.LessThan(c2);
        }

        public static bool operator >(Card c1, Card c2)
        {
            return c1.GreaterThan(c2);
        }

        public static bool operator <=(Card c1, Card c2)
        {
            return c1.LessThan(c2) || c1 == c2;
        }

        public static bool operator >=(Card c1, Card c2)
        {
            return c1.GreaterThan(c2) || c1 == c2;
        }

        #endregion

        #region Utilities
        public string ToShortString()
        {
            string str = "";

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

            switch (Value)
            {
                case Value.Ace :
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
            
            return str;
        }

        public override string ToString()
        {
            return String.Format("%s of %s", this.Value.ToString(), this.Suit.ToString());
        }
        #endregion
    }
}