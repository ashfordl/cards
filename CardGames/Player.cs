using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames
{
    public abstract class Player
    {
        public abstract Card MakeMove(GameInfo args);
    }

    public class ConsolePlayer : Player
    {
        List<Card> Hand;

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
                    Console.WriteLine("Trumps are CLUBS");
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
                    Console.WriteLine("Trumps are DIAMONDS");
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
                    Console.WriteLine("Trumps are SPADES");
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
                    Console.WriteLine("Trumps are HEARTS");
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

                        return PlayedCard;
                    }
                }
            }
        }
    }
}
