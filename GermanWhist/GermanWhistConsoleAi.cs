﻿// GermanWhistConsoleAi.cs
// <copyright file="GermanWhistConsoleAi.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using CardsLibrary;

namespace GermanWhist
{
    /// <summary>
    /// A <see cref="GermanWhistPlayer"/> implementation using the console for display of AI inputs.
    /// </summary>
    public class GermanWhistConsoleAi : GermanWhistConsolePlayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GermanWhistConsoleAi" /> class.
        /// </summary>
        public GermanWhistConsoleAi()
        {
            this.Hand = new List<Card>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GermanWhistConsoleAi" /> class.
        /// </summary>
        /// <param name="cards"> The cards in the player's hand. </param>
        public GermanWhistConsoleAi(IEnumerable<Card> cards)
        {
            this.Hand = cards.ToList();
        }

        /// <summary>
        /// Makes the AI play a card
        /// </summary>
        /// <param name="args"> The game info. </param>
        /// <returns> The played card. </returns>
        public override Card MakeMove(GermanWhistInfo args)
        {
            // Order the hand
            this.OrderCards();

            // Prints relevant info, eg trumps and cards already played 
            this.PrintGameInfo(args);

            // Evaluates the cards that can be played
            List<Card> valids = DetectValidCards(args);

            // If its worth winning the trick of its playing for tricks
            if (this.WorthWinning(args) || args.ToPlayFor == new Card(Value.Null, Suit.Null))
            {
                // Gets card better than ones played
                List<Card> betterCards = this.GetCards(args, valids);
                Card cardToPlay = new Card();

                // If no cards have been played
                if (args.CardsInPlay.Count == 0)
                {
                    do
                    {
                        // Will play from the 3rd best card and below if its a trump with a value of less than 4 being played for
                        if (args.ToPlayFor.SuitVal == 5 && (int)args.ToPlayFor.Value <= 4)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                betterCards.Remove(Card.HighestCardFromArray(betterCards));
                            }
                        }

                        // Pick the highest card from the list
                        cardToPlay = Card.HighestCardFromArray(betterCards);

                        // Remove that card so that it won't be picked again
                        betterCards.Remove(cardToPlay);
                    } 
                    while (cardToPlay >= args.ToPlayFor && args.ToPlayFor != new Card(Value.Null, Suit.Null)); // Until the card is less than the card being played for and if there is a card being played for
                }
                else
                {
                    // Otherwise play the lowest card possible
                    cardToPlay = Card.LowestCardFromArray(betterCards);
                }

                // Display what a human would have inputted
                return this.DisplayInput(cardToPlay, valids);
            }
            else
            {
                // Pick the lowest card possible and display what a human would have displayed
                return this.DisplayInput(Card.LowestCardFromArray(valids), valids);
            }
        }

        /// <summary>
        /// Decides if its worth winning the card being played for.
        /// </summary>
        /// <param name="args"> The game info. </param>
        /// <returns> Whether its worth winning the card. </returns>
        public bool WorthWinning(GermanWhistInfo args)
        {
            if (args.ToPlayFor.Suit == args.Trumps || (int)args.ToPlayFor.Value > 8 || (args.AceHigh && (int)args.ToPlayFor.Value == 1))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Gets a list of cards better than the ones already played.
        /// </summary>
        /// <param name="args"> The game info. </param>
        /// <param name="valids"> The valid cards. </param>
        /// <returns> A list of cards better than the cards already played. </returns>
        public List<Card> GetCards(GermanWhistInfo args, List<Card> valids)
        {
            List<Card> betterCards = new List<Card>();
            
            foreach (Card c in valids)
            {
                // Add the card to the cards in play
                args.CardsInPlay.Add(c);

                // If the card is the best out of all the other cards, add it to the list of better cards
                if (c == Card.HighestCardFromArray(args.CardsInPlay))
                {
                    betterCards.Add(c);
                }
                
                // Remove the card from the cards in play list
                args.CardsInPlay.Remove(c);
            }

            // If there were no better cards make all the valid cards be returned
            if (betterCards.Count == 0)
            {
                betterCards = valids;
            }

            return betterCards;
        }

        /// <summary>
        /// Displays the input for the card just played.
        /// </summary>
        /// <param name="card"> The card to be played. </param>
        /// <param name="valids"> The playable cards. </param>
        /// <returns> The card played. </returns>
        public Card DisplayInput(Card card, List<Card> valids)
        {                        
            // Say what was laid
            Console.WriteLine("The opponent laid: " + card.ToString() + "\n");
            return card;
        }
    }
}
