using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardGames;
using CardsLibrary;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Card> Deck;
            CardFactory.PopulateDeck(out Deck, true);

            List<CardGames.Whist.ConsolePlayer> Players = new List<CardGames.Whist.ConsolePlayer>();

            Card[][] tempPlayers = new Card[2][];
            tempPlayers[0] = new Card[7];
            tempPlayers[1] = new Card[7];

            CardFactory.Deal(ref Deck, ref tempPlayers, 7);

            Players.Add(new CardGames.Whist.ConsolePlayer(tempPlayers[0]));
            Players.Add(new CardGames.Whist.ConsolePlayer(tempPlayers[1]));

            CardGames.Whist.WhistInfo GameInfo = new CardGames.Whist.WhistInfo(new List<Card>(), Suit.Clubs, Suit.Null);

            Players[0].MakeMove(GameInfo);
            Console.WriteLine();
            Console.WriteLine();
            Players[1].MakeMove(GameInfo);
        }
    }
}
