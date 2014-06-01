using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * SUIT ORDER:
 *    1 = null
 *    2 = not played and not trump
 *    3 = suit above the nothing but not played still, its bais so tends not to be used
 *    4 = card played
 *    5 = trump
 */
namespace CardsLibrary
{
    public static class SuitOrder
    {
        public static void Reset() //Sets all suit orders to 1
        {
            Settings.ClubsOrder = 1;
            Settings.DiamondsOrder = 1;
            Settings.SpadesOrder = 1;
            Settings.HeartsOrder = 1;
            Settings.NullOrder = 1;
        }

        public static void SetTrumps(Suit s) //Sets the suit specified to a trump order value
        {
            if (s == Suit.Clubs)
                Settings.ClubsOrder = 5;
            if (s == Suit.Diamonds)
                Settings.DiamondsOrder = 5;
            if (s == Suit.Spades)
                Settings.SpadesOrder = 5;
            if (s == Suit.Hearts)
                Settings.HeartsOrder = 5;
            if (s == Suit.Null)
                Settings.NullOrder = 5;
        }

        public static void SetPlayed(Suit s) //Sets the suit specified to a played order value
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

        public static void SetThird(Suit s) //Sets the suit specified to a third place order value
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

        public static void SetNull(Suit s) //Sets the suit specified to a null order value
        {
            if (s == Suit.Clubs)
                Settings.ClubsOrder = 1;
            if (s == Suit.Diamonds)
                Settings.DiamondsOrder = 1;
            if (s == Suit.Spades)
                Settings.SpadesOrder = 1;
            if (s == Suit.Hearts)
                Settings.HeartsOrder = 1;
            if (s == Suit.Null)
                Settings.NullOrder = 1;
        }
    }
}
