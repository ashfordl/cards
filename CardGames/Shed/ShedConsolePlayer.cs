// ShedConsolePlayer.cs
// <copyright file="ShedConsolePlayer.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using CardsLibrary;

namespace CardGames.Shed
{
    /// <summary>
    /// A <see cref="ShedPlayer"/> implementation using the console for input.
    /// </summary>
    public class ShedConsolePlayer : ShedPlayer
    {
        /// <summary>
        /// Makes the appropriate move using the given arguments.
        /// </summary>
        /// <param name="args"> The current game info. </param>
        /// <returns> The (last) card played. </returns>
        public override Card MakeMove(ShedInfo args)
        {
            // Order the hand
            this.OrderCards();

            // Print the game info
            this.PrintGameInfo(args);

            // Print the hand
            this.PrintHand();

            // Return the inputted card
            int nullInt = 0;
            return this.GetInput(args, new List<ShedPlayer>(), ref nullInt);
        }

        /// <summary>
        /// Makes the appropriate move using the given arguments.
        /// </summary>
        /// <param name="args"> The current game info. </param>
        /// <param name="otherPlayers"> The other players. </param>
        /// <param name="cardsPlayed"> The amount of cards played in the move (with ref keyword). </param>        
        /// <returns> The (last) card played. </returns>
        public override Card MakeMove(ShedInfo args, List<ShedPlayer> otherPlayers, ref int cardsPlayed)
        {
            // Order the hand
            this.OrderCards();

            // Print the game info
            this.PrintGameInfo(args, otherPlayers);

            // Print the hand
            this.PrintHand();

            // Return the inputted card
            return this.GetInput(args, otherPlayers, ref cardsPlayed);
        }
        
        /// <summary>
        /// Prints how many players are left in, how many cards are in the pile and the last card played.
        /// </summary>
        /// <param name="args"> The current game info. </param>
        /// <param name="otherPlayers"> The other players. </param>
        public void PrintGameInfo(ShedInfo args, List<ShedPlayer> otherPlayers = null)
        {
            Console.WriteLine("Players still playing: {0}", args.PlayersLeft);
            Console.WriteLine("Cards left in the deck: {0}", args.Deck.Count);
            Console.WriteLine("Cards in Play: {0}", args.CardsInPlay.Count);
            Console.WriteLine("Last Card Played: {0}", args.CardsInPlay.LastOrDefault());
            Console.WriteLine("So the rule in play is: {0}\n", args.RuleInPlay);

            // Writes the other players table cards if they are able to play them
            if (otherPlayers != null)
            {
                foreach (ShedPlayer p in otherPlayers)
                {
                    if (p.Hand.Count == 0)
                    {
                        if (p.Tables.Count != 0)
                        {
                            Console.Write("Player {0} can play their tables which are: ", p.ID);
                            foreach (Card c in p.Tables)
                            {
                                Console.Write("{0} ", c.ToUnicodeString());
                            }

                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("Player {0} can play their blinds!", p.ID);
                        }
                    }
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints the current hand the player has.
        /// </summary>
        public void PrintHand()
        {
            Console.WriteLine("Your Tables:\n");

            // Print the blind cards
            foreach (Card c in this.Blinds)
            {
                if (Hand.Count == 0 && Tables.Count == 0)
                {
                    Console.Write(" ({0})-? ", this.Blinds.IndexOf(c) + 1);
                }
                else if (Hand.Count == 0)
                {
                    Console.Write("     ? ");
                }
                else
                {
                    Console.Write(" ? ");
                }
            }

            Console.WriteLine();

            // Print the cards on the table
            foreach (Card c in this.Tables)
            {
                if (Hand.Count == 0)
                {
                    Console.Write(" ({0})-{1}", this.Tables.IndexOf(c) + 1, c.ToUnicodeString());
                }
                else
                {
                    Console.Write(" {0}", c.ToUnicodeString());
                }
            }

            Console.WriteLine("\n\nYour Hand:");

            // Print your hand
            foreach (Card c in this.Hand)
            {
                Console.Write("({0})-{1} ", (int)c.Value, c.ToUnicodeString());
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Gets the input made by the user.
        /// </summary>
        /// <param name="args"> The current game info. </param>
        /// <param name="otherPlayers"> The other players. </param>
        /// <param name="cardsPlayed"> The amount of cards played in the move (with ref keyword). </param>         
        /// <returns> The (last) card played. </returns>
        public Card GetInput(ShedInfo args, List<ShedPlayer> otherPlayers, ref int cardsPlayed)
        {
            do
            {
                // The input
                string input = Console.ReadLine();
                int? numberPicked = this.ConvertInput(input);
                
                if (numberPicked == null)
                {
                    continue;
                }

                // Picking for each stage of the game (cards in hand, table or blinds)
                if (Hand.Count != 0) 
                {
                    // Get a IEnumerable of the cards with that value
                    IEnumerable<Card> cardsAbleToPick = this.Hand.Where(c => (int)c.Value == numberPicked);

                    // Say it is not a valid input if no card has that value
                    if (cardsAbleToPick.Count() == 0)
                    {
                        Console.WriteLine("\nThat is not a valid input!");
                        continue;
                    }

                    // Return the card(s)
                    return this.GetHowManyOfValue(cardsAbleToPick, args, otherPlayers, ref cardsPlayed);
                }
                else if (Tables.Count != 0) 
                {
                    numberPicked--;

                    Card c = this.GetCardSelectedFromTable(this.Tables, (int)numberPicked, args, otherPlayers, ref cardsPlayed);

                    // Check it was a valid card
                    if (c == new Card(Value.Null, Suit.Null))
                    {
                        continue;
                    }

                    // Return the card
                    return c;
                }
                else
                {
                    numberPicked--;

                    Card c = this.GetCardSelectedFromTable(this.Blinds, (int)numberPicked, args, otherPlayers, ref cardsPlayed);

                    // Check it was a valid card
                    if (c == new Card(Value.Null, Suit.Null))
                    {
                        continue;
                    }

                    // Return the card
                    return c;
                }
            }
            while (true);
        }

        /// <summary>
        /// Gets how many cards of that value should be played.
        /// </summary>
        /// <param name="cardsAbleToPick"> The list of cards able to be played of that value. </param>
        /// <param name="args"> The current game info. </param>
        /// <param name="otherPlayers"> The other players. </param>
        /// <param name="cardsPlayed"> The amount of cards played in the move (with ref keyword). </param>          
        /// <returns> The (last) card played. </returns>
        public Card GetHowManyOfValue(IEnumerable<Card> cardsAbleToPick, ShedInfo args, List<ShedPlayer> otherPlayers, ref int cardsPlayed)
        {
            if (cardsAbleToPick.Count() == 1) 
            {
                // If there is just one card play it
                return this.OneCardAvailable(cardsAbleToPick.ToList()[0], args);
            }
            else
            {
                // Turn the IEnumerable to a list
                List<Card> cards = cardsAbleToPick.ToList();

                // If the cards are threes play the special loop for them
                if (cards[0].Value == (Value)3)
                {
                    return this.GetHowManyThrees(cards, args, otherPlayers, ref cardsPlayed);
                }

                Console.WriteLine("How many {0}s do you want to play? You have {1} avalible...", cards[0].Value, cards.Count);
                Console.WriteLine("Input 0 to go back and chose another card...");

                // Ask how many of that card is wanted to be played
                List<Card> selectedCards = this.GetNumberOfCards(cards);
                cardsPlayed = selectedCards.Count;

                // Go to the beginning of the MakeMove if told to (by a null card list)
                if (selectedCards.Count == 0)
                {
                    return this.MakeMove(args, otherPlayers, ref cardsPlayed);
                }

                // Display the move
                this.DisplayMove(selectedCards.Count, selectedCards[0]);

                // Remove selected cards from the hand
                return this.RemoveSelectedCards(selectedCards, args);
            }
        }

        /// <summary>
        /// Gets how many threes should be played
        /// </summary>
        /// <param name="cards"> The list of cards that can be played. </param>
        /// <param name="args"> The current game info. </param>
        /// <param name="otherPlayers"> The other players. </param>
        /// <param name="cardsPlayed"> The amount of cards played in the move (with ref keyword). </param>    
        /// <returns> The (last) card played. </returns>
        public Card GetHowManyThrees(List<Card> cards, ShedInfo args, List<ShedPlayer> otherPlayers, ref int cardsPlayed)
        {
            if (!cards.All(c => c.Red) && !cards.All(c => c.Black)) 
            {
                // If there is a mix of black and red cards ask whether they want to play red or black threes
                return this.GetDiferentThrees(cards, args, otherPlayers, ref cardsPlayed);
            }
            else 
            {
                // Get the colour of three it is
                string colour = this.GetColour(cards[0]);

                Console.WriteLine("How many {0} {1}s do you want to play? You have {2} avalible...", colour, cards[0].Value, cards.Count);
                Console.WriteLine("Input 0 to go back and chose another card...");

                // Ask how many threes of that colour are wanted
                List<Card> selectedCards = this.GetNumberOfCards(cards);

                // Go to the beginning of the MakeMove if told to (by a null card)
                if (selectedCards == new List<Card>())
                {
                    return this.MakeMove(args, otherPlayers, ref cardsPlayed);
                }

                // Display the move
                this.DisplayMove(selectedCards.Count, selectedCards[0], colour: colour);

                // Remove the selected cards from hand
                return this.RemoveSelectedCards(selectedCards, args);
            }
        }

        /// <summary>
        /// Gets which type of threes should be played
        /// </summary>
        /// <param name="cards"> The list of cards that can be played. </param>
        /// <param name="args"> The current game info. </param>
        /// <param name="otherPlayers"> The other players. </param>
        /// <param name="cardsPlayed"> The amount of cards played in the move (with ref keyword). </param>    
        /// <returns> The (last) card played. </returns>
        public Card GetDiferentThrees(List<Card> cards, ShedInfo args, List<ShedPlayer> otherPlayers, ref int cardsPlayed)
        {
            Console.WriteLine("Black Three(s) or Red ones? B/R");
            
            // Repeat until something is returned
            do
            {
                string input = Console.ReadLine();

                if (input.ToUpper() == "B") 
                {
                    // If "B" was inputted return the black threes
                    return this.GetHowManyThrees(cards.Where(c => c.Black).ToList(), args, otherPlayers, ref cardsPlayed);
                }
                else if (input.ToUpper() == "R") 
                {
                    // If "R" was inputted return the red threes
                    return this.GetHowManyThrees(cards.Where(c => c.Red).ToList(), args, otherPlayers, ref cardsPlayed);
                }
                else 
                {
                    // Otherwise say it wasn't a valid input
                    Console.WriteLine("That is not a valid input!");
                }
            }
            while (true);
        }

        /// <summary>
        /// Gets how many cards the user wants to play.
        /// </summary>
        /// <param name="cards"> The cards of a certain value able to be played. </param>
        /// <returns> A List of cards which is the length specified by the user. </returns>
        public List<Card> GetNumberOfCards(List<Card> cards)
        {
            // Keep repeating until it passes
            do
            {
                // Input
                string input = Console.ReadLine();
                int? cardsPicked = this.ConvertInput(input);

                // Start again if it failed to convert
                if (cardsPicked == null)
                {
                    continue;
                }

                // Return a null card if a 0 was inputted
                if (cardsPicked == 0)
                {
                    Console.Clear();
                    Console.WriteLine("Player {0}", this.ID);
                    return new List<Card>();
                }
                else
                {
                    if (cardsPicked < 0) 
                    {
                        // Try again if the amount of cards was negative
                        Console.WriteLine("That is not a valid input!");
                        continue;
                    }
                    else if (cardsPicked > cards.Count) 
                    {
                        // Try again if the amount of cards was above the amount available
                        Console.WriteLine("You only have {0} {1}s!", cards.Count, cards[0].Value);
                        continue;
                    }
                    else 
                    {
                        // Return the selected amount of cards
                        return cards.TakeWhile((c, index) => index < (int)cardsPicked).ToList();
                    }
                }
            }
            while (true);
        }

        /// <summary>
        /// Removes a list of cards from the hand of the user.
        /// </summary>
        /// <param name="selectedCards"> The selected cards to be removed. </param>
        /// <param name="args"> The current game info. </param>
        /// <returns> The (last) card played. </returns>
        public Card RemoveSelectedCards(List<Card> selectedCards, ShedInfo args)
        {
            // Keep removing until all the selected cards are removed
            while (selectedCards.Count != 0)
            {
                // Add to the cards in play
                args.CardsInPlay.Add(selectedCards[0]);

                // Remove from the hand and the selected cards list
                this.Hand.Remove(selectedCards[0]);
                selectedCards.RemoveAt(0);
            }

            // Remove (from the game info) the last card played and return it
            Card returnCard = args.CardsInPlay.Last();
            args.CardsInPlay.Remove(returnCard);
            return returnCard;
        }

        /// <summary>
        /// Gets what card should be played from the table cards.
        /// </summary>
        /// <param name="cards"> The cards from either <see cref="ShedPlayer.Blinds"/> or <see cref="ShedPlayer.Tables"/>. </param>
        /// <param name="index"> The index of the card to be selected. </param>
        /// <param name="args"> The current game info. </param>
        /// <param name="otherPlayers"> The other players. </param>
        /// <param name="cardsPlayed"> The amount of cards played in the move (with ref keyword). </param>    
        /// <returns> The card played. </returns>
        public Card GetCardSelectedFromTable(List<Card> cards, int index, ShedInfo args, List<ShedPlayer> otherPlayers, ref int cardsPlayed)
        {
            // Return a null card if the number is not valid
            if (index < 0 || index >= cards.Count)
            {
                Console.WriteLine("\nThat is not a valid input!");
                return new Card(Value.Null, Suit.Null);
            }

            // The card being returned
            Card returnCard = cards[index];

            List<Card> possibleCardsPlayable = cards.Where(c => c.Value == returnCard.Value).ToList();
            cardsPlayed = possibleCardsPlayable.Count;

            // If they can play multiple tables
            if (possibleCardsPlayable.Count > 1 && cards.SequenceEqual(this.Tables))
            {
                Console.WriteLine("You can play up to {0} {1}s, how many do you want to play...", possibleCardsPlayable.Count, returnCard.Value);
                Console.WriteLine("Input 0 to go back and chose another card...");

                // Get how many they want to play
                List<Card> cardsWantedToBePlayed = this.GetNumberOfCards(possibleCardsPlayable);

                // Go to the beginning of the MakeMove if told to (by a null card list)
                if (cardsWantedToBePlayed.Count == 0)
                {
                    return this.MakeMove(args, otherPlayers, ref cardsPlayed);
                }

                // State how many were inputted and remove them from the tables
                cardsPlayed = cardsWantedToBePlayed.Count;
                foreach (Card c in cardsWantedToBePlayed)
                {
                    this.Tables.Remove(c);
                }

                // Add the return card back in, as it gets removed again after
                this.Tables.Add(returnCard);
            }

            // Display the move
            this.DisplayMove(cardsPlayed, returnCard);

            // Removes the card from whatever list it was in (blinds or tables)
            if (cards.SequenceEqual(this.Tables))
            {
                this.Tables.Remove(returnCard);
            }
            else if (cards.SequenceEqual(this.Blinds))
            {
                this.Blinds.Remove(returnCard);
            }

            // Return the card
            return returnCard;
        }

        /// <summary>
        /// Gets the colour of a card.
        /// </summary>
        /// <param name="c"> The card. </param>
        /// <returns> The colour. </returns>
        public string GetColour(Card c)
        {
            if (c.Red)
            {
                return "Red ";
            }
            else
            {
                return "Black ";
            }
        }

        /// <summary>
        /// Plays one card if that is all that is available.
        /// </summary>
        /// <param name="c"> The card. </param>
        /// <param name="args"> The current game info. </param>
        /// <returns> The card played. </returns>
        public Card OneCardAvailable(Card c, ShedInfo args)
        {
            this.DisplayMove(1, c);
            this.Hand.Remove(c);
            return c;
        }

        /// <summary>
        /// Displays the move.
        /// </summary>
        /// <param name="numberPlayed"> The number of that value played. </param>
        /// <param name="c"> The (first) card. </param>
        /// <param name="tableCard"> If it is a card on the table (<see cref="ShedPlayer.Blinds"/> or <see cref="ShedPlayer.Tables"/>).</param>
        /// <param name="colour"> The colour of the card. </param>
        public void DisplayMove(int numberPlayed, Card c, bool tableCard = false, string colour = " ")
        {
            if (tableCard)
            {
                Console.WriteLine("You played the {0}", c.ToString());
            }
            else
            {
                Console.WriteLine("You played {0} {1}{2}s", numberPlayed, colour, c.Value);
            }

            Console.ReadKey(true);
        }

        /// <summary>
        /// Converts a string input into a integer.
        /// </summary>
        /// <param name="input"> The inputted string. </param>
        /// <returns> The converted integer. </returns>
        /// <remarks> Returns null if it failed to convert. </remarks>
        public int? ConvertInput(string input)
        {
            // Try convert the input
            try
            {
                return int.Parse(input);
            }
            catch
            {
                // Say it is not valid if it failed to convert
                Console.WriteLine("That is not a valid input!");

                // A null integer means that it failed
                return null;
            }
        }
    }
}
