// Program.cs
// <copyright file="Program.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using CardsLibrary;

namespace ConsoleTesting
{
    /// <summary>
    /// The main program (for testing at the moment).
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the program.
        /// </summary>
        /// <param name="args"> Any arguments/commands that the program is run/compiled with. </param>
        public static void Main(string[] args)
        {
            List<Card> deck = CardFactory.PopulateDeck(true);

            List<CardGames.Whist.ConsolePlayer> players = new List<CardGames.Whist.ConsolePlayer>();

            Card[][] tempPlayers = CardFactory.Deal(ref deck, 2, 7);

            players.Add(new CardGames.Whist.ConsolePlayer(tempPlayers[0]));
            players.Add(new CardGames.Whist.ConsolePlayer(tempPlayers[1]));

            CardGames.Whist.WhistInfo gameInfo = new CardGames.Whist.WhistInfo();
            gameInfo.CardsInPlay = new List<Card>();
            gameInfo.Trumps = Suit.Clubs;
            gameInfo.FirstSuitLaid = Suit.Null;

            players[0].MakeMove(gameInfo);
            Console.WriteLine();
            Console.WriteLine();
            players[1].MakeMove(gameInfo);
        }
    }
}
