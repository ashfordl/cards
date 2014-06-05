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

        private Suit _trumps;
        public Suit Trumps 
        {   
            get
            {
                return _trumps;
            }
            set
            {
                _trumps = value;
                SuitOrder.SetTrumps(Trumps);
            }
        }

        private Suit _firstsuitlaid;
        public Suit FirstSuitLaid
        {
            get
            {
                return _firstsuitlaid;
            }
            set
            {
                FirstSuitLaid = value;
                SuitOrder.SetPlayed(FirstSuitLaid);
            }
        }

        public WhistInfo(List<Card> laid, Suit trumps, Suit first)
        {
            this.LaidCards = laid;
            this.Trumps = trumps;
            this.FirstSuitLaid = first;
        }
    }
}
