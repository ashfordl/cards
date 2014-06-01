using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardsLibrary
{
    public class Card
    {
        public Value Value { get; set; }
        public Suit Suit { get; set; }

        public bool IsValid
        {
            get 
            {  
                if(Value != null && Value != Value.Null && Suit != null && Suit != Suit.Null)
                {
                    return (true);
                }
                return (false);
            }
        }

        public Card()
        {
            this.Value = CardsLibrary.Value.Null;
            this.Suit = CardsLibrary.Suit.Null;
        }

        public Card(Value val, Suit suit)
        {
            this.Value = val;
            this.Suit = suit;
        }

        public Card(int val, int suit)
        {
            CardsLibrary.Value Val = (CardsLibrary.Value)val;
            CardsLibrary.Suit Suit = (CardsLibrary.Suit)suit;
            this.Value = Val;
            this.Suit = Suit;
        }

        public static Card HighestCardFromArray(Card[] cards)
        {
            int bestCard = 0;
            int bestpoint = 0;

            for (int i = 0; i < cards.Count(); i++)
            {
                int iPointage = (int)cards[i].Value;
                if (Settings.AceHigh == true)
                {
                    iPointage--;
                    if (cards[i].Value == Value.Ace)
                    {
                        iPointage = 13;
                    }
                }
                if (cards[i].Suit == Suit.Clubs)
                {
                    iPointage += 13 * Settings.ClubsOrder;
                }
                if (cards[i].Suit == Suit.Diamonds)
                {
                    iPointage += 13 * Settings.DiamondsOrder;
                } 
                if (cards[i].Suit == Suit.Spades)
                {
                    iPointage += 13 * Settings.SpadesOrder;
                } 
                if (cards[i].Suit == Suit.Hearts)
                {
                    iPointage += 13 * Settings.HeartsOrder;
                } 
                if (cards[i].Suit == Suit.Null)
                {
                    iPointage += 13 * Settings.NullOrder;
                }
                if (iPointage > bestpoint)
                {
                    bestpoint = iPointage;
                    bestCard = i;
                }
            }
            return (cards[bestCard]);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Card))
                return false;
            Card c = obj as Card;

            if (c.Value != this.Value)
                return false;
            if (c.Suit != this.Suit)
                return false;

            return true;
        }

        public static bool operator ==(Card c1, Card c2)
        {
            if (c1.Value != c2.Value)
                return false;
            if (c1.Suit != c2.Suit)
                return false;

            return true;
        }

        public static bool operator !=(Card c1, Card c2)
        {
            if (c1.Value == c2.Value && c1.Suit == c2.Suit)
                return false;

            return true;
        }
    }
}
