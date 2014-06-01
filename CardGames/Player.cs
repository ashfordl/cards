using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames
{
    public abstract class Player
    {
        public abstract Card MakeMove(GameInfo args);
    }
}
