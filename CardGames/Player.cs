using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames
{
    public abstract class Player<I> where I : GameInfo
    {
        public abstract Card MakeMove(I args);

        public List<Card> Hand { get; set; }
    }
}
