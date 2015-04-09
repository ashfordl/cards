// GermanWhist.cs
// <copyright file="GermanWhist.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using CardsLibrary;

namespace GermanWhist
{
    /// <summary>
    /// An implementation of german whist.
    /// </summary>
    public class GermanWhist : Game<GermanWhistPlayer, GermanWhistInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GermanWhist" /> class.
        /// </summary>
        public GermanWhist()
        {
            this.MaxPlayers = 2;
            this.Players = new List<GermanWhistPlayer>();
        }

        /// <summary>
        /// Gets or sets the deck of cards for the game.
        /// </summary>
        public List<Card> Deck { get; protected set; }

        /// <summary>
        /// Start the game.
        /// </summary>
        public override void Start()
        {
            // If there are not two players, throw a TooFewPlayersException
            if (Players.Count < 2)
            {
                throw new TooFewPlayersException("Has to have 2 players.");
            }

            // Initialize all the game info
            this.Initialize();

            GermanWhistInfo info = this.InitializeInfo();

            // First set of play (playing to change around your cards to try get a good hand)
            this.PlayPhaseOne(info);

            // Second set of play (trying to win as many tricks as possible)
            this.PlayPhaseTwo(info);

            this.DecideWinner();
        }

        /// <summary>
        /// Initialize the game's properties.
        /// </summary>
        protected void Initialize()
        {
            this.Deal(cards: 13);

            // Creates the deck
            this.Deck = CardFactory.PopulateDeck(true);

            // Remove all cards that were given to the players from the deck
            foreach (GermanWhistPlayer player in this.Players)
            {
                foreach (Card c in player.Hand)
                {
                    this.Deck.Remove(c);
                }
            }
        }

        /// <summary>
        /// Initialize the info for the game.
        /// </summary>
        /// <returns> The game info. </returns>
        protected GermanWhistInfo InitializeInfo()
        {
            GermanWhistInfo info = new GermanWhistInfo();
            info.AceHigh = true;

            // Sets the trumps to the suit of the top card left in the deck
            info.Trumps = this.Deck[0].Suit;
            info.CardsInPlay = new List<Card>();

            return info;
        }

        /// <summary>
        /// Plays the first phase of play for the game.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        protected void PlayPhaseOne(GermanWhistInfo info)
        {
            // Plays 13 rounds
            for (int i = 0; i < 13; i++, info.RoundNumber++)
            {
                // Reset the suit order
                SuitOrder.Reset();
                SuitOrder.SetTrumps(info.Trumps);

                // Sets the card being played for to the top card in the deck
                info.ToPlayFor = this.Deck[0];

                // Make each player play a card
                foreach (GermanWhistPlayer play in this.Players)
                {
                    Console.Clear();
                    Console.WriteLine("Player {0}'s turn", play.ID);
                    Console.ReadKey(true);
                    Console.Clear();

                    this.MakePlayerMove(info, play);
                }

                Console.Clear();

                // Decide who won the card and give players their new cards
                int winnerIndex = this.DecideCardWinner(info);

                Console.ReadKey(true);

                // Reset variables that need to be reset
                info.CardsInPlay.Clear();
                info.FirstSuitLaid = Suit.Null;

                // Swap the players if the second player won
                if (winnerIndex == 1)
                {
                    this.OrderPlayer();
                }
            }

            info.ToPlayFor = new Card(Value.Null, Suit.Null);
        }

        /// <summary>
        /// Plays the second phase of play for the game.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        protected void PlayPhaseTwo(GermanWhistInfo info)
        {
            // Play 13 rounds
            for (int i = 0; i < 13; i++, info.RoundNumber++)
            {
                // Reset the suit order
                SuitOrder.Reset();
                SuitOrder.SetTrumps(info.Trumps);

                // Make each player play a card
                foreach (GermanWhistPlayer play in this.Players)
                {
                    Console.Clear();
                    Console.WriteLine("Player {0}'s turn", play.ID);
                    Console.ReadKey(true);
                    Console.Clear();

                    this.MakePlayerMove(info, play);
                }

                Console.Clear();

                // Decide who won the trick
                int winnerIndex = this.DecideTrickWinner(info);

                Console.ReadKey(true);

                // Reset info
                info.CardsInPlay.Clear();
                info.FirstSuitLaid = Suit.Null;

                // Swap the players if the second player won
                if (winnerIndex == 1)
                {
                    this.OrderPlayer();
                }
            }

            Console.WriteLine("\n\n");
        }

        /// <summary>
        /// Makes a player play a card.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        /// <param name="player"> The player which is being made to make a move. </param>
        protected void MakePlayerMove(GermanWhistInfo info, GermanWhistPlayer player)
        {
            Console.Clear();
            
            // Display which player it is
            Console.WriteLine("Player: {0}\n", player.ID);
            
            // Make them play their move and remove the card from the hand and put it in the info's cards in play
            Card c = player.MakeMove(info);
            player.Hand.Remove(c);
            info.CardsInPlay.Add(c);

            // If there is no suit to play in, make the card just played become the suit to play
            if (info.FirstSuitLaid == Suit.Null)
            {
                info.FirstSuitLaid = c.Suit;
            }

            Console.WriteLine("\nPress to continue...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// Decides the winner of the card being played for and act accordingly.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        /// <returns> The index of the winner. </returns>
        protected int DecideCardWinner(GermanWhistInfo info)
        {
            // See who won
            int winnerIndex;
            if (info.CardsInPlay[0] >= info.CardsInPlay[1])
            {
                winnerIndex = 0;
            }
            else
            {
                winnerIndex = 1;
            }

            // Display who got what cards
            Console.WriteLine("Player {0} got the {1}.", this.Players[winnerIndex].ID, info.ToPlayFor.ToString());
            Console.WriteLine("Player {0} got the {1}.", this.Players[(winnerIndex + 1) % 2].ID, this.Deck[1].ToString());

            // Add the top card to the winner's hand
            this.Players[winnerIndex].Hand.Add(this.Deck[0]);

            // Add the second card to the loser's hand
            this.Players[(winnerIndex + 1) % 2].Hand.Add(this.Deck[1]);

            // Remove both the cards just given away from the deck
            this.Deck.RemoveRange(0, 2);

            return winnerIndex;
        }

        /// <summary>
        /// Orders the players.
        /// </summary>
        protected void OrderPlayer()
        {
            // Make a temporary player equal to the first player
            GermanWhistPlayer tempPlayer = this.Players[0];
            
            // The the first player become equal to the second
            this.Players[0] = this.Players[1];

            // Makes the second player equal to the original first player
            this.Players[1] = tempPlayer;
        }

        /// <summary>
        /// Decides the winner of the trick and acts accordingly.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        /// <returns> The index of the winner. </returns>
        protected int DecideTrickWinner(GermanWhistInfo info)
        {
            if (info.CardsInPlay[0] >= info.CardsInPlay[1])
            {
                this.Players[0].Score++;
                Console.WriteLine("Player {0} won the trick!", this.Players[0].ID);
                return 0;
            }
            else
            {
                this.Players[1].Score++;
                Console.WriteLine("Player {0} won the trick!", this.Players[1].ID);
                return 1;
            }
        }

        /// <summary>
        /// Decides the winner of the game.
        /// </summary>
        protected void DecideWinner()
        {            
            GermanWhistPlayer winner; 
            GermanWhistPlayer loser;

            // Which ever player has more tricks is assigned to be the winner
            if (this.Players[0].Score > this.Players[1].Score)
            {
                winner = this.Players[0];
                loser = this.Players[1];
            }
            else
            {
                winner = this.Players[1];
                loser = this.Players[0];
            }

            // Display who won
            Console.WriteLine("Player {0} won the game with {1} tricks to {2} tricks!", winner.ID, winner.Score, loser.Score);
        }
    }
}
