using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames.Whist
{
    public class WhistConsolePlayer : Player
    {
        public WhistConsolePlayer()
        {
            this.Hand = new List<Card>();
        }

        public WhistConsolePlayer(List<Card> cards)
        {
            this.Hand = cards;
        }
        
        public override Card MakeMove(WhistInfo args)
        {
            // Prints relevant info, eg trumps and first suit laid
            PrintGameInfo(args);

            // First evaluates the cards the could be played, then prints the hand with these as selectable
            List<Card> valids = DetectValidCards(args);
            PrintHand(valids);

            // Retrieves a card which is returned as the move made
            return RetrieveInput();
        }

        protected void PrintGameInfo(WhistInfo args)
        {
            Console.WriteLine("Trumps are %s", args.Trumps);
            Console.WriteLine("Suit laid is %s", args.FirstSuitLaid);

            Console.Write("Cards laid: ");
            foreach(Card laid in args.LaidCards)
                Console.Write(laid+"\t");
            Console.WriteLine();
        }

        protected List<Card> DetectValidCards(WhistInfo args)
        {
            List<Card> valids = new List<Card>();

            bool b = Hand.

            foreach (Card c in Hand)
            { 
                
            }

            return valids;
        }

        protected void PrintHand(IEnumerable<Card> valids)
        {
            Console.WriteLine("Your cards:"); // Show its going to display the cards in you hand
            for (int i = 0; i < Hand.Count; i++) // Itterate through every card
            {
                Console.Write(Hand[i].ToShortString()); // Write out the short hand of the card
                Console.Write(' '); // Put a space gap between each card
            }
            Console.WriteLine(); // End the line that your cards are in


            int NumToPrint = 1; // The number that will be printed (what you need to press to select that card above it)
            foreach (Card c in Hand) // Itterate through the cards in your hand
            {
                foreach (Card vc in valids) // For each index that is playable
                {
                    if (c == vc) // If the current index in the hand is one of the index's in the list of playable index's
                    {
                        Console.Write(NumToPrint); // Print out the number that will need to be pressed to select it
                        Console.Write("  "); // Add a two space gap so that the next number will be in line correctly
                        NumToPrint++; // Add one to the numToPrint so that it will show the next number
                    }
                    else
                        Console.Write("   "); // Otherwise put a three space gap so that the next number will be in line
                }
            }
            Console.WriteLine(); // End the current line
        }

        protected Card RetrieveInput()
        {

        }

        public override Card MakeMove(GameInfo args)
        {
            while (true) // Repeat until a card is returned since it may not be a valid input
            {
                Console.Clear(); // Clear the console 
                List<int> PlayableIndex = new List<int>(); // A list of index's that are playable
                int Playable = 0; // How many cards can be played
                bool SetSuit = false; // If the suit needs to be set via the SuitOrder class

                // Display what trumps are
                if (Settings.ClubsOrder == 5)
                    Console.WriteLine("Trumps are CLUBS");
                else if (Settings.DiamondsOrder == 5)
                    Console.WriteLine("Trumps are DIAMONDS");
                else if (Settings.SpadesOrder == 5)
                    Console.WriteLine("Trumps are SPADES");
                else if (Settings.HeartsOrder == 5)
                    Console.WriteLine("Trumps are HEARTS");
                else
                    Console.WriteLine("No trumps");

                // See what cards are playable
                if (Settings.ClubsOrder == 4)
                {
                    for (int i = 0; i < Hand.Count; i++) // Goes through the cards in the hand
                    {
                        if (Hand[i].Suit == Suit.Clubs) // If they are the correct suit
                        {
                            Playable++; // Add one to the amount of playable cards
                            PlayableIndex.Add(i); // Add the index of the playable card
                        }
                    }
                    Console.WriteLine("Suit laid is CLUBS"); // Display what the suit that has been laid

                    // The above structor is used in the next three ifs as well
                }
                else if (Settings.DiamondsOrder == 4)
                {
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        if (Hand[i].Suit == Suit.Diamonds)
                        {
                            Playable++;
                            PlayableIndex.Add(i);
                        }
                    }
                    Console.WriteLine("Suit laid is DIAMONDS");
                }
                else if (Settings.SpadesOrder == 4)
                {
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        if (Hand[i].Suit == Suit.Spades)
                        {
                            Playable++;
                            PlayableIndex.Add(i);
                        }
                    }
                    Console.WriteLine("Suit laid is SPADES");
                }
                else if (Settings.HeartsOrder == 4)
                {
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        if (Hand[i].Suit == Suit.Hearts)
                        {
                            Playable++;
                            PlayableIndex.Add(i);
                        }
                    }
                    Console.WriteLine("Suit laid is HEARTS");
                }
                else // If there is no suit laid
                {
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        Playable++;
                        PlayableIndex.Add(i);
                    }
                    Console.WriteLine("No suit laid, play a card to set the suit");
                    SetSuit = true; // Record the suit needs to be set at the end of the round
                }
                if (PlayableIndex.Count == 0) // If no cards can be played in suit, you can play any of your other cards
                {
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        Playable++;
                        PlayableIndex.Add(i);
                    }
                }

                Console.WriteLine(); // Skips a line
                Console.WriteLine("Cards in Play:"); // Show its going to display the cards in play
                foreach (Card c in args.CardsInPlay) // For every card played (passed in the WhistInfo as a the paramiter "args")
                {
                    Console.Write(c.ShorthandToString()); // Write out the shorthand of each card
                    Console.Write(' '); // Add a space wide gape between each card
                }
                Console.WriteLine(); // End the line with the cards displayed

                Console.WriteLine(); // Skips a line
                Console.WriteLine("Your cards:"); // Show its going to display the cards in you hand
                for (int i = 0; i < Hand.Count; i++) // Itterate through every card
                {
                    Console.Write(Hand[i].ShorthandToString()); // Write out the short hand of the card
                    Console.Write(' '); // Put a space gap between each card
                }
                Console.WriteLine(); // End the line that your cards are in

                int NumToPrint = 1; // The number that will be printed (what you need to press to select that card above it)
                for (int i = 0; i < Hand.Count; i++) // Itterate through the cards in your hand
                {
                    foreach (int index in PlayableIndex) // For each index that is playable
                    {
                        if (index == i) // If the current index in the hand is one of the index's in the list of playable index's
                        {
                            Console.Write(NumToPrint); // Print out the number that will need to be pressed to select it
                            Console.Write("  "); // Add a two space gap so that the next number will be in line correctly
                            NumToPrint++; // Add one to the numToPrint so that it will show the next number
                        }
                        else
                            Console.Write("   "); // Otherwise put a three space gap so that the next number will be in line
                    }
                }
                Console.WriteLine(); // End the current line

                bool Valid = true; // If the button pressed is valid
                char Input = Console.ReadKey(true).KeyChar; // The char that has been pressed to select a card
                if (!char.IsNumber(Input)) Valid = false; // If the input is not a number, its not valid

                if (Valid)
                {
                    int NumInput = (int)char.GetNumericValue(Input); // Turns the input from type char to int
                    NumInput--; // minus one so it gets the correct card in the hand index (as its 0 base as apose to 1)
                    if (NumInput <= Playable && NumInput >= 0) // If the input is above 0, and below the amount of playable cards
                    {
                        Card PlayedCard = Hand[PlayableIndex[NumInput]]; // The Card played is now equal to the selected card

                        Hand.RemoveAt(PlayableIndex[NumInput]); // Remove the played card from the hand

                        if (SetSuit) // If the suit needs to be set
                        {
                            switch (PlayedCard.Suit) 
                            {
                                case Suit.Clubs:
                                    SuitOrder.SetPlayed(Suit.Clubs); // Set suit played to clubs if the card was a club
                                    break;
                                case Suit.Diamonds:
                                    SuitOrder.SetPlayed(Suit.Diamonds); // Set suit played to diamonds if the card was a diamonds
                                    break;
                                case Suit.Spades:
                                    SuitOrder.SetPlayed(Suit.Spades); // Set suit played to spades if the card was a spade
                                    break;
                                case Suit.Hearts:
                                    SuitOrder.SetPlayed(Suit.Hearts); // Set suit played to hearts if the card was a heart
                                    break;
                            }
                        }

                        return PlayedCard; // Return the played card
                    }
                }
            }
        }
    }
}
