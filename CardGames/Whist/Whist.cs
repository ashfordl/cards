using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames.Whist
{
    public class Whist : Game
    {
        protected int round = 1;
        protected List<Player<WhistInfo>> activePlayers;
        protected List<Player<WhistInfo>> players = new List<Player<WhistInfo>>();

        public Whist()
        {
            MaxPlayers = 7;
            activePlayers = new List<Player<WhistInfo>>(players);
        }

        public override void Start()
        {
            // Each iteration is a round
            while(activePlayers.Count > 1)
            {
                List<Card> cards = new List<Card>();

                Suit trumps = Suit.Null;
                Suit first = Suit.Null;

                List<Player<WhistInfo>> plays = OrderPlayers();
                foreach(Player<WhistInfo> player in plays)
                {
                    WhistInfo roundInfo = new WhistInfo(cards, trumps, first);
                    Card card = player.MakeMove(roundInfo);
                    cards.Add(card);
                }

                round++;
            }
        }

        protected List<Player<WhistInfo>> OrderPlayers()
        {
            if (round == 1)
                return activePlayers;

            int playerOffset = round % players.Count;
            do
            {
                if (activePlayers.Contains(players[playerOffset]))
                { 
                    int firstInd = activePlayers.IndexOf(players[playerOffset]);
                    List<Player<WhistInfo>> ordered = activePlayers.GetRange(firstInd, activePlayers.Count - firstInd);
                    ordered.AddRange(activePlayers.GetRange(0, firstInd));
                    return ordered;
                }
                else
                {
                    playerOffset++;
                    continue;
                }
            } while (true);
        }
    }
}
