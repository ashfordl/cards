using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames.Whist
{
    public class ConsolePlayer : Player
    {
        public List<Card> Hand { get; set; }

        public ConsolePlayer()
        {
            this.Hand = new List<Card>();
        }

        public ConsolePlayer(List<Card> cards)
        {
            this.Hand = cards;
        }

        public override Card MakeMove(GameInfo args)
        {
            while (true)
            {
                List<int> PlayableIndex = new List<int>();
                int Playable = 0;
                bool SetSuit = false;

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

                if (Settings.ClubsOrder == 4)
                {
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        if (Hand[i].Suit == Suit.Clubs)
                        {
                            Playable++;
                            PlayableIndex.Add(i);
                        }
                    }
                    Console.WriteLine("Suit laid is CLUBS");
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
                else
                {
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        Playable++;
                        PlayableIndex.Add(i);
                    }
                    Console.WriteLine("No suit laid, play a card to set the suit");
                    SetSuit = true;
                }
                if (PlayableIndex.Count == 0) // If no cards can be played in suit, you can play any of your other cards
                {
                    for (int i = 0; i < Hand.Count; i++)
                    {
                        Playable++;
                        PlayableIndex.Add(i);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Cards in Play:");
                foreach (Card c in args.CardsInPlay)
                {
                    Console.Write(c.ShorthandToString());
                    Console.Write(' ');
                }
                Console.WriteLine();

                Console.WriteLine();
                Console.WriteLine("Your cards:");
                for (int i = 0; i < Hand.Count; i++)
                {
                    Console.Write(Hand[i].ShorthandToString());
                    Console.Write(' ');
                }
                Console.WriteLine();

                int NumToPrint = 1;
                for (int i = 0; i < Hand.Count; i++)
                {
                    foreach (int index in PlayableIndex)
                    {
                        if (index == i)
                        {
                            Console.Write(NumToPrint);
                            Console.Write("  ");
                            NumToPrint++;
                        }
                        else
                            Console.Write("   ");
                    }
                }
                Console.WriteLine();

                bool Valid = true;
                char Input = Console.ReadKey(true).KeyChar;
                if (!char.IsNumber(Input)) Valid = false;

                if (Valid)
                {
                    int NumInput = (int)char.GetNumericValue(Input);
                    NumInput--;
                    if (NumInput <= Playable && NumInput >= 0)
                    {
                        Card PlayedCard = Hand[PlayableIndex[NumInput]];

                        Hand.RemoveAt(PlayableIndex[NumInput]);

                        if (SetSuit)
                        {
                            switch (PlayedCard.Suit)
                            {
                                case Suit.Clubs:
                                    SuitOrder.SetPlayed(Suit.Clubs);
                                    break;
                                case Suit.Diamonds:
                                    SuitOrder.SetPlayed(Suit.Diamonds);
                                    break;
                                case Suit.Spades:
                                    SuitOrder.SetPlayed(Suit.Spades);
                                    break;
                                case Suit.Hearts:
                                    SuitOrder.SetPlayed(Suit.Hearts);
                                    break;
                            }
                        }

                        return PlayedCard;
                    }
                }
            }
        }
    }
}
