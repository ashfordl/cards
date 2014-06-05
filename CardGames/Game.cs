using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames
{
    public abstract class Game<TPlayer, TInfo> 
        where TInfo : GameInfo 
        where TPlayer : Player<TInfo>
    {
        public int MaxPlayers { get; protected set; }
        protected virtual List<TPlayer> players { get; set; }

        public void AddPlayer(TPlayer p)
        {
            if (MaxPlayers <= 0)
                players.Add(p);
            else if (players.Count + 1 <= MaxPlayers)
                players.Add(p);
            else
                throw new TooManyPlayersException("Max Players " + MaxPlayers);
        }

        public abstract void Start();

        /*protected abstract void Finish();

        public delegate void GameFinishedDelegate(object sender, EventArgs args);
        public event GameFinishedDelegate GameFinished;

        protected void RaiseGameFinished(EventArgs e)
        {
            if (GameFinished != null)
                GameFinished(this, e);
        }*/
    }
}
