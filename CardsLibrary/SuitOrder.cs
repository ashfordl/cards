using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsLibrary
{
    public static class SuitOrder
    {
        public static void Reset()
        {
            Settings.ClubsOrder = 1;
            Settings.DiamondsOrder = 1;
            Settings.SpadesOrder = 1;
            Settings.HeartsOrder = 1;
            Settings.NullOrder = 1;
        }

        public static void SetTrumps(Suit s)
        {
            if (s == Suit.Clubs)
                Settings.ClubsOrder = 4;
            if (s == Suit.Diamonds)
                Settings.DiamondsOrder = 4;
            if (s == Suit.Spades)
                Settings.SpadesOrder = 4;
            if (s == Suit.Hearts)
                Settings.HeartsOrder = 4;
            if (s == Suit.Null)
                Settings.NullOrder = 4;
        }

        public static void SetPlayed(Suit s)
        {
            if (s == Suit.Clubs)
                Settings.ClubsOrder = 3;
            if (s == Suit.Diamonds)
                Settings.DiamondsOrder = 3;
            if (s == Suit.Spades)
                Settings.SpadesOrder = 3;
            if (s == Suit.Hearts)
                Settings.HeartsOrder = 3;
            if (s == Suit.Null)
                Settings.NullOrder = 3;
        }

        public static void SetThird(Suit s)
        {
            if (s == Suit.Clubs)
                Settings.ClubsOrder = 2;
            if (s == Suit.Diamonds)
                Settings.DiamondsOrder = 2;
            if (s == Suit.Spades)
                Settings.SpadesOrder = 2;
            if (s == Suit.Hearts)
                Settings.HeartsOrder = 2;
            if (s == Suit.Null)
                Settings.NullOrder = 2;
        }

        public static void SetNull(Suit s)
        {
            if (s == Suit.Clubs)
                Settings.ClubsOrder = 0;
            if (s == Suit.Diamonds)
                Settings.DiamondsOrder = 0;
            if (s == Suit.Spades)
                Settings.SpadesOrder = 0;
            if (s == Suit.Hearts)
                Settings.HeartsOrder = 0;
            if (s == Suit.Null)
                Settings.NullOrder = 0;
        }
    }
}
