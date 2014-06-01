using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames
{
    public class GameInfo
    {
        public int RoundNumber { get; set; }

        public List<Card> CardsInPlay { get; set; }

        public Settings Settings { get; set; }
    }
}
