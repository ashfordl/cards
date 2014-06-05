using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames.Whist
{
    public abstract class WhistPlayer : Player<WhistInfo>
    {
        protected List<Card> DetectValidCards(WhistInfo args)
        {
            // Can the hand follow suit
            bool followSuit = Hand.Any(c => c.Suit == args.FirstSuitLaid);

            if(followSuit && args.FirstSuitLaid != Suit.Null)
            {
                // If the hand can follow suit, return only cards of the correct suit
                return Hand.Where(c => c.Suit == args.FirstSuitLaid).ToList();
            }
            else
            {
                // Else return all cards
                return Hand;
            }
        }
    }
}
