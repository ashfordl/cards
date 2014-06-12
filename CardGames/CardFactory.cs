// CardFactory.cs
// <copyright file="CardFactory.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using CardGames;
using CardsLibrary;

namespace CardsLibrary
{
    /// <summary>
    /// Where the deck is created, shuffled and dealt from.
    /// </summary>
    public static class CardFactory
    {
        /// <summary>
        /// Creates a deck.
        /// </summary>
        /// <returns> Returns a deck. </returns>
        /// <param name="shuffle"> Whether to shuffle the deck. </param>
        /// <param name="jokers"> Whether to add jokers. </param>
        public static List<Card> PopulateDeck(bool shuffle = false, bool jokers = false)
        {
            List<Card> deck = new List<Card>(); // Creates an instance of the deck
            // Goes through each suit
            for (int i = 0; i < 4; i++)
            {
                // Goes throguh each card for that suit
                for (int j = 0; j < 13; j++)
                {
                    Card c = new Card((Value)j + 1, (Suit)i + 1); // Create the card needed
                    deck.Add(c); // Add that card to the deck
                }
            }

            // If jokers are in the deck, add two of them
            if (jokers)
            {
                deck.Add(new Card(Value.Null, Suit.Null));
                deck.Add(new Card(Value.Null, Suit.Null));
            }

            // If the deck needs to be shuffled, shuffle it
            if (shuffle)
            {
                deck = Shuffle(deck);
            }

            return deck;
        }

        /// <summary>
        /// Shuffles the deck randomly.
        /// </summary>
        /// <returns> Returns the deck after shuffling. </returns>
        /// <param name="deck"> The deck that will be shuffled. </param>
        public static List<Card> Shuffle(List<Card> deck)
        {
            List<int> numsDone = new List<int>(); // A list of all the index values of cards done
            List<Card> shuffledDeck = new List<Card>(); // The new list of cards that will be return that is shuffled

            // Repeat until all cards in the deck has had its position chnaged
            for (int i = 0; i < deck.Count(); i++)
            {
                Random pickIndex = new Random(); // The random number generator that will pick a new index for the current card
                bool newIndex = false;
                int newPick = 0; // The new index it will be tacken from

                // While it hasn't found a new card
                while (!newIndex)
                {
                    newPick = pickIndex.Next(0, deck.Count()); // Picks a new number
                    newIndex = true; // Records that it has found a new card
                    // Checks that the new card hasn't already been picked, if it has been picked record it hasn't found a new card
                    foreach (int num in numsDone)
                    {
                        if (newPick == num)
                        {
                            newIndex = false;
                        }
                    }
                }

                numsDone.Add(newPick); // Adds the new index for the card to the list of picked index's
                shuffledDeck.Add(deck[newPick]); // Add the selected card to the new deck
            }

            return shuffledDeck; // Return the shuffled deck
        }

        /// <summary>
        /// Removes all the cards in the game from play
        /// </summary>
        /// <param name="deck"> The deck that will be cleared. This is a "ref" so that it is removed from the actual variable not just in the loop. </param>
        /// <param name="players"> The players that will have their hand cleared. This is a "ref" so that it is removed from the actual variable not just in the loop. </param>
        public static void RemoveAllCards(ref List<Card> deck, ref List<Player<GameInfo>> players)
        {
            // Clear cards in the deck
            deck.Clear();

            // Remove all cards in the players hands
            foreach (Player<GameInfo> play in players)
            {
                play.Hand.Clear();
            }
        }

        /// <summary>
        /// Collects all the cards into the deck.
        /// </summary>
        /// <param name="deck"> The deck. </param>
        /// <param name="players"> The players. </param>
        /// <param name="info"> The game info. </param>
        public static void CollectAllCards(ref List<Card> deck, ref List<Player<GameInfo>> players, ref GameInfo info)
        {
            // Put all cards from the players hand into the deck
            foreach (Player<GameInfo> play in players)
            {
                foreach (Card c in play.Hand)
                {
                    deck.Add(c);
                }
            }

            // Remove all the cards from the players
            List<Card> emptyDeck = new List<Card>();
            RemoveAllCards(ref emptyDeck, ref players);

            // Put all cards in play into the deck
            foreach (Card c in info.CardsInPlay)
            {
                deck.Add(c);
            }

            info.CardsInPlay.Clear();
        }
    }
}
