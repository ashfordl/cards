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
        public static Card BestofTwo(Card a, Card b)
        {
            //Return the first card if they are equal
            if (a == b)
                return a;

            //Return the one with the higher suit value if they are different
            if (a.SuitVal != b.SuitVal)
            {
                if (a.SuitVal > b.SuitVal)
                    return a;
                else
                    return b;
            }

            //If the suit values are the same and ace is high
            if (Settings.AceHigh)
            {
                //If a is an ace and b is not
                if (a.Value == Value.Ace && b.Value != Value.Ace)
                    return a;
                //If b is an ace and a is not
                if (a.Value != Value.Ace && b.Value == Value.Ace)
                    return b;
                //If they are both aces, return the first played card
                if (a.Value == Value.Ace && b.Value == Value.Ace)
                    return a;
            }

            //Which has the better number value is returned
            if ((int)a.Value > (int)b.Value)
                return a;
            else if ((int)a.Value < (int)b.Value)
                return b;
            else //If the cards have an equal suit value (but not the same suit) and a equal face value
                return a;

        }

        public static Card HighestCardFromArray(IEnumerable<Card> cards)
        {
            Card highest = cards.First();

            // Compares current highest against all cards in the collection
            foreach (Card c in cards)
                if (BestofTwo(highest, c) == c)
                    highest = c;

            return highest;
        }

        public static Card LowestCardFromArray(IEnumerable<Card> cards)
        {
            Card lowest = cards.First();

            // Compares current lowest against all cards in the collection
            foreach (Card c in cards)
                if (BestofTwo(lowest, c) == lowest)
                    lowest = c;

            return lowest;
        }
        #endregion

        #region Operator_Overloads
        public override bool Equals(object obj)
        {
            //If the obj getting compaired is not a card return false
            if (!(obj is Card))
                return false;
            //Otherwise turn it into a card
            Card c = obj as Card;

            //If the value is not the same return false
            if (c.Value != this.Value)
                return false;
            //If the suit is not the same return false
            if (c.Suit != this.Suit)
                return false;

            //Otherwise return true
            return true;
        }

        public static bool operator ==(Card c1, Card c2)
        {
            //If the values are not the same, return false
            if (c1.Value != c2.Value)
                return false;
            //If the suits are not the same, return false
            if (c1.Suit != c2.Suit)
                return false;

            //Otherwise return true
            return true;
        }

        public static bool operator !=(Card c1, Card c2)
        {
            //If the values and suits are the same return false
            if (c1.Value == c2.Value && c1.Suit == c2.Suit)
                return false;

            //Otherwise return true
            return true;
        }
        #endregion
    }
}