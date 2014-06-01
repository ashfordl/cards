using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames.Whist
{
    class WhistInfo : GameInfo
    {
        public List<Card> LaidCards { get; set; }

        public WhistInfo(List<Card> laid)
        {
            this.LaidCards = laid;
        }
    }
}
