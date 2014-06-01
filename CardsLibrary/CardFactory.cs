using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardsLibrary
{
    public static class CardFactory
    {
        public static void PopulateDeck(out List<Card> deck, bool shuffle = false, bool jokers = false) //Creates a deck
        {
            deck = new List<Card>(); //Creates an instance of the deck
            for (int i = 0; i < 4; i++) //Goes through each suit
            {
                for (int j = 0; j < 13; j++) //Goes throguh each card for that suit
                {
                    Card c = new Card((Value)j + 1, (Suit)i + 1); //Create the card needed
                    deck.Add(c); //Add that card to the deck
                }
            }

            if (jokers) //If jokers are in the deck, add two of them
            {
                deck.Add(new Card(Value.Null, Suit.Null));
                deck.Add(new Card(Value.Null, Suit.Null));
            }

            if (shuffle) //If the deck needs to be shuffled, shuffle it
                CardFactory.Shuffle(ref deck);
        }

        public static void Shuffle(ref List<Card> deck) //Shuffles the deck
        {
            List<int> numsDone = new List<int>(); //A list of all the index values of cards done
            List<Card> ShuffledDeck = new List<Card>(); //The new list of cards that will be return that is shuffled

            //Repeat until all cards in the deck has had its position chnaged
            for (int i = 0; i < deck.Count(); i++)
            {
                Random pickIndex = new Random(); //The random number generator that will pick a new index for the current card
                bool newIndex = false;
                int newPick = 0; //The new index it will be tacken from

                while (!newIndex) //While it hasn't found a new card
                {
                    newPick = pickIndex.Next(0, deck.Count()); //Picks a new number
                    newIndex = true; //Records that it has found a new card
                    //Checks that the new card hasn't already been picked, if it has been picked record it hasn't found a new card
                    foreach (int num in numsDone)
                    {
                        if (newPick == num)
                            newIndex = false;
                    }
                }
                numsDone.Add(newPick); //Adds the new index for the card to the list of picked index's
                ShuffledDeck.Add(deck[newPick]); //add the selected card to the new deck
            }

            deck = ShuffledDeck; //Make the deck become the shuffeled deck
        }

        public static void Deal(ref List<Card> deck, ref Card[][] Players, int cardsToDeal) //Deaks the cards in the deck to the players
        {
            //Goes through the loop of dealing the correct amount of cards
            for (int i = 0; i < cardsToDeal; i++)
            {
                //Deals one card to each player
                for (int ii = 0; ii < Players.Count(); ii++)
                {
                    //If all the cards in the deck are gone break the loop
                    if (deck.Count() == 0)
                        break;
                    Players[ii][i] = deck[0]; //Add the card at the top of the deck to the player's hand
                    deck.RemoveAt(0); //Remove the card just given away from the deck
                }
            }
        }

        public static void RemoveAllCards(ref List<Card> deck, ref Card[][] Players) //Remove all the cards in the game
        {
            //Clear cards in the deck
            deck.Clear();

            //Remove all cards in the players hands
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
