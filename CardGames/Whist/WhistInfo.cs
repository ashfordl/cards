using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames.Whist
{
    public class WhistInfo : GameInfo
    {
        public List<Card> LaidCards { get; set; }

        public Suit Trumps { get; set; } // SuitOrder

        public Suit FirstSuitLaid { get; set; } // SuitOrder

        public WhistInfo(List<Card> laid, Suit trumps, Suit first)
        {
            this.LaidCards = laid;
            this.Trumps = trumps;
            this.FirstSuitLaid = first;
        }
    }
}
