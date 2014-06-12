// ConsolePlayer.cs
// <copyright file="ConsolePlayer.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using CardsLibrary;

namespace CardGames.Whist
{
    /// <summary>
    /// A <see cref="WhistPlayer"/> implementation using the console for input.
    /// </summary>
    public class ConsolePlayer : WhistPlayer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsolePlayer" /> class.
        /// </summary>
        public ConsolePlayer()
        {
            this.Hand = new List<Card>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsolePlayer" /> class.
        /// </summary>
        /// <param name="cards"> The cards in the player's hand. </param>
        public ConsolePlayer(IEnumerable<Card> cards)
        {
            this.Hand = cards.ToList();
        }

        /// <summary>
        /// Makes the appropriate move using the given arguments.
        /// </summary>
        /// <param name="args"> The information required to make the move. </param>
        /// <returns> The card chosen. </returns>
        public override Card MakeMove(WhistInfo args)
        {
            // Prints relevant info, eg trumps and first suit laid
            this.PrintGameInfo(args);

            // First evaluates the cards the could be played, then prints the hand with these as selectable
            List<Card> valids = DetectValidCards(args);
            this.PrintHand(valids);

            // Retrieves a card which is returned as the move made
            return this.RetrieveInput(valids, args);
        }

        /// <summary>
        /// Prints the trumps, laid suit and previous cards laid.
        /// </summary>
        /// <param name="args"> The current game info. </param>
        protected void PrintGameInfo(WhistInfo args)
        {
            Console.WriteLine("Trumps are {0}", args.Trumps);
            Console.WriteLine("Suit laid is {0}", args.FirstSuitLaid);

            Console.Write("Cards laid: ");
            foreach (Card laid in args.CardsInPlay)
            {
                Console.Write(laid + "  ");
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Prints the player's hand.
        /// </summary>
        /// <param name="valids"> The list of valid cards to play. </param>
        protected void PrintHand(List<Card> valids)
        {
            Console.WriteLine("Your hand:");

            // Iterate through every card
            foreach (Card c in this.Hand) 
            {
                if (valids.Contains(c))
                {
                    // If card can be played, display input number
                    Console.Write("({0})-", valids.IndexOf(c) + 1);  
                }

                // Display the card
                Console.Write(c.ToShortString() + ' ');     
            }

            // Reset to new line
            Console.WriteLine();    
        }

        /// <summary>
        /// Prints an error message for invalid or incorrect input.
        /// </summary>
        protected void PrintInputError()
        {
            Console.WriteLine("Error: Your input was invalid. Please try again.");
        }

        /// <summary>
        /// Retrieves the user's choice in card to lay.
        /// </summary>
        /// <param name="valids"> The list of valid cards to play. </param>
        /// <param name="args"> The current game info. </param>
        /// <returns> The card chosen by the user. </returns>
        protected Card RetrieveInput(List<Card> valids, WhistInfo args)
        {
            do
            {
                // Read the next character entered and reset to new line
                string s = Console.ReadLine();

                // Test if s is a valid number
                int i;
                try
                {
                    i = int.Parse(s.ToString());
                }
                catch
                {
                    this.PrintInputError();
                    continue;
                }

                // Make it the correct index (start at 0 as apposed to 1)
                i--; 

                // Test if i is too large or less than 0
                if (i >= valids.Count || i < 0)
                {
                    this.PrintInputError();
                    continue;
                }

                // This will return only if the input is valid
                Console.WriteLine("You Laid: " + valids.ElementAt(i) + "\n");

                return valids.ElementAt(i);
            } 
            while (true);
        }
    }
}
