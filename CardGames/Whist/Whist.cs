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
        protected List<WhistPlayer> activePlayers;
        protected override List<WhistPlayer> players { get; set; }

        public Whist()
        {
            MaxPlayers = 7;
            players = new List<WhistPlayer>();
            activePlayers = new List<WhistPlayer>(players);
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

        protected List<WhistPlayer> OrderPlayers()
        {
            if (round == 1)
                return activePlayers;

            int playerOffset = round % players.Count;
            do
            {
                if (activePlayers.Contains(players[playerOffset]))
                { 
                    int firstInd = activePlayers.IndexOf(players[playerOffset]);
                    List<WhistPlayer> ordered = activePlayers.GetRange(firstInd, activePlayers.Count - firstInd);
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
