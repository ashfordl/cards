// Shed.cs
// <copyright file="Shed.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using CardsLibrary;

namespace CardGames.Shed
{
    /// <summary>
    /// An implementation of Shed.
    /// </summary>
    public class Shed : Game<ShedPlayer, ShedInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Shed" /> class.
        /// </summary>
        public Shed()
        {
            this.Players = new List<ShedPlayer>();
            this.MaxPlayers = 7;
        }

        /// <summary>
        /// Gets or sets the active players within the game.
        /// </summary>
        public List<ShedPlayer> ActivePlayers { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether the current play is clockwise.
        /// </summary>
        public bool ClockwisePlay { get; protected set; }
        
        /// <summary>
        /// Start the game.
        /// </summary>
        public override void Start()
        {
            // Initialize all the info for the game and players
            ShedInfo info = this.InitializeInfo(this.Initialize());

            this.ActivePlayers = new List<ShedPlayer>(this.Players);

            // Start playing the game
            this.PlayGame(info);
        }

        /// <summary>
        /// Initialize the player's hands and the deck.
        /// </summary>
        /// <returns> The deck. </returns>
        public List<Card> Initialize()
        {
            // Deal 3 cards to each players
            this.Deal(cards: 3);

            // Creates a deck of spare cards
            List<Card> deck = CardFactory.PopulateDeck(true);

            // Remove dealt cards from the deck
            foreach (ShedPlayer p in this.Players)
            {
                foreach (Card c in p.Hand)
                {
                    deck.Remove(c);
                }
            }

            // How many cards will be on the table per player
            int cardsOnTableCount;

            // Works out how many cards will be in the table
            if (this.Players.Count > 5)
            {
                cardsOnTableCount = 2;
            }
            else
            {
                cardsOnTableCount = 3;
            }

            // Give each player it's blind and table cards
            foreach (ShedPlayer p in this.Players)
            {
                for (int i = 0; i < cardsOnTableCount; i++)
                {
                    p.Blinds.Add(deck[0]);
                    p.Tables.Add(deck[1]);
                    deck.RemoveRange(0, 2);
                }
            }

            return deck;
        }

        /// <summary>
        /// Initialize the info for the game.
        /// </summary>
        /// <param name="deck"> The deck. </param>
        /// <returns> The game info. </returns>
        public ShedInfo InitializeInfo(List<Card> deck)
        {
            ShedInfo info = new ShedInfo();

            // Initialize all the elements to be used in the info.
            info.CardsInPlay = new List<Card>();
            info.Deck = new List<Card>(deck);
            info.PlayersLeft = this.Players.Count;
            info.RuleInPlay = Specials.None;

            // Set play to clockwise
            this.ClockwisePlay = true;

            return info;
        }

        /// <summary>
        /// Plays the game.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        public void PlayGame(ShedInfo info)
        {
            int currentTurn = 0;
            do
            {
                // Say who's turn it is
                this.WhosTurn(currentTurn);

                // Get the last card played, if there is none generate a null card
                Card lastCard;
                try
                {
                    lastCard = info.CardsInPlay.Last();
                }
                catch
                {
                    lastCard = new Card();
                }

                List<ShedPlayer> playersExcludeCurrent = new List<ShedPlayer>(this.ActivePlayers);
                playersExcludeCurrent.RemoveAt(currentTurn);
                int cardsPlayed = 1;
                Card playedCard = this.ActivePlayers[currentTurn].MakeMove(info, playersExcludeCurrent, ref cardsPlayed);

                // Add the card played into the list of played cards
                info.CardsInPlay.Add(playedCard);

                // Check if the move was valid and act accordingly
                if (this.ValidMove(info, lastCard, playedCard))
                {
                    Console.WriteLine("Valid move, well done!");
                    Console.ReadKey(true);

                    this.AddCardsToHand(info, currentTurn);

                    this.CheckSpecial(info, ref currentTurn, cardsPlayed);

                    if (this.PlayerOut(this.ActivePlayers[currentTurn]))
                    {
                        this.RemovePlayer(this.ActivePlayers[currentTurn]);
                    }

                    // If a ten was played skip the rest of the loop so play stays on them
                    if (info.CardsInPlay.Count == 0)
                    {
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Unlucky, invalid move! Pick up!");
                    Console.ReadKey(true);

                    this.GivePlayerPlayedCards(info, currentTurn);
                }

                // Change who's turn it is
                currentTurn = this.ChangeTurn(currentTurn);
            }
            while (this.ActivePlayers.Count > 1);

            this.PrintLoser();
        }

        /// <summary>
        /// Display who's turn it is.
        /// </summary>
        /// <param name="currentTurn"> The index in <see cref="this.ActivePlayers"/> of who's turn it is. </param>
        public void WhosTurn(int currentTurn)
        {
            Console.Clear();
            Console.WriteLine("Player {0}'s go...", this.ActivePlayers[currentTurn].ID);
            Console.ReadKey(true);
            Console.Clear();
            Console.WriteLine("Player {0}:", this.ActivePlayers[currentTurn].ID);
        }

        /// <summary>
        /// Checks if the card played was valid.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        /// <param name="lastCard"> The previous card played. </param>
        /// <param name="currentCard"> The new card just played. </param>
        /// <returns> Whether the played card was valid. </returns>
        public bool ValidMove(ShedInfo info, Card lastCard, Card currentCard)
        {
            if (lastCard == new Card())
            {
                // If its the first card in the list, its a valid move
                return true;
            }
            else if (info.RuleInPlay == Specials.Two || currentCard.Value == Value.Two)
            {
                // If the last played card is a two or the new card played is a two, its a valid move
                return true;
            }
            else if (info.RuleInPlay == Specials.Red_three)
            {
                if (currentCard.Value == Value.Three && currentCard.Black)
                {
                    // If the red three rule is in play and they play a black three, its a valid move
                    return true;
                }
                else
                {
                    // Otherwise remove the red three and return false
                    info.CardsInPlay.Remove(lastCard);
                    return false;
                }
            }
            else if (currentCard.Value == Value.Ten)
            {
                // If a ten was played its valid (unless on the red three), its a valid move
                return true;
            }
            else if (info.RuleInPlay == Specials.Seven && (int)currentCard.Value <= 7)
            {
                // If a number less than or equal to seven was played with the seven rule in play, its a valid move
                return true;
            }
            else if (lastCard.OneUpDownOrEqual(currentCard))
            {
                // If the played card is one up down or 
                return true;
            }

            // If it hasn't returned true, its not valid, return false
            return false;
        }

        /// <summary>
        /// Adds cards to the hand of a player if there are still cards in the deck.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        /// <param name="currentTurn"> The index of the player. </param>
        public void AddCardsToHand(ShedInfo info, int currentTurn)
        {
            while (this.ActivePlayers[currentTurn].Hand.Count < 3 && info.Deck.Count > 0)
            {
                this.ActivePlayers[currentTurn].Hand.Add(info.Deck[0]);
                info.Deck.RemoveAt(0);
            }
        }

        /// <summary>
        /// Check if a special card was played.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        /// <param name="currentPlayerIndex"> The index of the current player. </param>
        /// <param name="cardsPlayed"> The amount of cards played in the move (with ref keyword). </param> 
        public void CheckSpecial(ShedInfo info, ref int currentPlayerIndex, int cardsPlayed)
        {
            // The card that was played
            List<Card> played = new List<Card>(info.CardsInPlay);
            played.Reverse();
            played = played.Take(cardsPlayed).ToList();

            foreach (Card c in played)
            {
                // Specials that don't change the game rules:
                if (c.Value == Value.Jack)
                {
                    // Switch the direction of play
                    this.ClockwisePlay = !this.ClockwisePlay;
                }
                else if (c.Value == Value.Ten)
                {
                    // Clear the deck
                    info.CardsInPlay.Clear();
                }
                else if (c.Value == Value.Eight)
                {
                    // Skip a player
                    currentPlayerIndex = this.ChangeTurn(currentPlayerIndex);
                }

                // Specials that do change the game rules:
                if (c.Value == Value.Seven)
                {
                    // State a seven was played
                    info.RuleInPlay = Specials.Seven;
                }
                else if (c.Value == Value.Three && c.Red)
                {
                    // State a red three was played
                    info.RuleInPlay = Specials.Red_three;
                }
                else if (c.Value == Value.Two)
                {
                    // State a two was played
                    info.RuleInPlay = Specials.Two;
                }
                else
                {
                    // State nothing special that changes rules was played
                    info.RuleInPlay = Specials.None;
                }
            }
        }

        /// <summary>
        /// Checks if the player is out.
        /// </summary>
        /// <param name="p"> The player being checked. </param>
        /// <returns> Whether the player is out or not. </returns>
        public bool PlayerOut(ShedPlayer p)
        {
            return p.Blinds.Count == 0 && p.Tables.Count == 0 && p.Hand.Count == 0;
        }

        /// <summary>
        /// Removes a player from the active player list.
        /// </summary>
        /// <param name="p"> The player being removed. </param>
        public void RemovePlayer(ShedPlayer p)
        {
            this.ActivePlayers.Remove(p);
        }

        /// <summary>
        /// Gives cards in the pile of played cards to a player.
        /// </summary>
        /// <param name="info"> The current game info. </param>
        /// <param name="currentPlayer"> The index of who's getting the cards. </param>
        public void GivePlayerPlayedCards(ShedInfo info, int currentPlayer)
        {
            this.ActivePlayers[currentPlayer].Hand.AddRange(info.CardsInPlay);
            info.CardsInPlay.Clear();
            info.RuleInPlay = Specials.None;
        }
        
        /// <summary>
        /// Changes who's turn it is.
        /// </summary>
        /// <param name="currentTurn"> The current player's index. </param>
        /// <returns> The new index of the current player's turn. </returns>
        public int ChangeTurn(int currentTurn)
        {
            int nextTurn = currentTurn;

            // Add or minus one depending on which way play is going
            if (this.ClockwisePlay)
            {
                nextTurn++;
            }
            else
            {
                nextTurn--;
            }

            // Makes the integer within the range of 0 - ActivePlayers.Count
            if (nextTurn >= this.ActivePlayers.Count)
            {
                nextTurn %= this.ActivePlayers.Count;
            }
            else if (nextTurn < 0)
            {
                nextTurn = this.ActivePlayers.Count + (nextTurn % this.ActivePlayers.Count);
            }

            return nextTurn;
        }

        /// <summary>
        /// Prints who the loser is (the only element in the active players list)
        /// </summary>
        public void PrintLoser()
        {
            if (this.ActivePlayers.Count == 1)
            {
                Console.WriteLine("Player {0} is Shed!", this.ActivePlayers[0].ID);
            }
            else
            {
                Console.WriteLine("There is currently no player who is Shed!");
            }
        }
    }
}
