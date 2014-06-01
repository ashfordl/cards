using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsLibrary
{
    public static class CardFactory
    {
        public static void PopulateDeck(out List<Card> deck, bool shuffle = false, bool jokers = false)
        {
            deck = new List<Card>();
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 13; j++)
                {
                    Card c = new Card((Value)j, (Suit)i);
                    deck.Add(c);
                }
            }

            if(jokers)
            {
                deck.Add(new Card(Value.Null, Suit.Null));
                deck.Add(new Card(Value.Null, Suit.Null));
            }

            if (shuffle)
                CardFactory.Shuffle(ref deck);
        }

        public static void Shuffle(ref List<Card> deck)
        {
            List<int> numsDone = new List<int>();
            List<Card> ShuffledDeck = new List<Card>();

            for (int i = 0; i < deck.Count(); i++)
            {
                Random pickIndex = new Random();
                bool newIndex = false;
                int newPick = 0;

                while (!newIndex)
                {
                    newPick = pickIndex.Next(0, deck.Count());
                    newIndex = true;
                    foreach (int num in numsDone)
                    {
                        if (newPick == num)
                            newIndex = false;
                    }
                }
                numsDone.Add(newPick);
                ShuffledDeck.Add(deck[newPick]);
            }

            deck = ShuffledDeck;
        }

        public static void Deal(ref List<Card> deck, ref Card[][] Players, int cardsToDeal)
        {
            for (int i = 0; i < cardsToDeal; i++)
            {
                for (int ii = 0; ii < Players.Count(); ii++)
                {
                    if (deck.Count() == 0)
                        break;
                    Players[ii][i] = deck[0];
                    deck.RemoveAt(0);
                }
            }
        }

        public static void RemoveAllCards(ref List<Card> deck, ref Card[][] Players)
        {
            deck.Clear();

            for (int i = 0; i < Players.Count(); i++)
            {
                for (int ii = 0; ii < Players[i].Count(); ii++)
                {
                    Players[i][ii] = null;
                }
            }
        }
    }
}
