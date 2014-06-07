// CardFactory.cs
// <copyright file="CardFactory.cs"> This code is protected under the MIT License. </copyright>
using System;
using System.Collections.Generic;
using System.Linq;

namespace CardsLibrary
{
    /// <summary>
    /// The card factory, so where the deck is created, shuffled and dealt from.
    /// </summary>
    public static class CardFactory
    {
        /// <summary>
        /// Creates and returns a deck.
        /// </summary>
        /// <returns> Returns a deck in the form of a List of Cards </returns>
        /// <param name="shuffle"> If this is true it will shuffle the deck. </param>
        /// <param name="jokers"> If this is true it will add Jokers to the deck. </param>
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
        /// <returns> The deck that is shuffled in the form of a List of Card. </returns>
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
        /// Deal away the deck to the correct amount of players with the correct amount of cards.
        /// </summary>
        /// <returns> The hands in an array of an array of cards. </returns>
        /// <param name="deck"> The deck to be dealt. Has the "ref" so that it will change the deck once it removes cards. </param>
        /// <param name="playersToDeal"> The amount of players to be dealt to. </param>
        /// <param name="cardsToDeal"> The amount of cards to deal as an integer. </param>
        public static Card[][] Deal(ref List<Card> deck, int playersToDeal, int cardsToDeal)
        {
            Card[][] players = new Card[playersToDeal][];

            // Sets all the player card arrays to the right length
            for (int i = 0; i < playersToDeal; i++) 
            {
                players[i] = new Card[cardsToDeal];
            }

            // Goes through the loop of dealing the correct amount of cards
            for (int i = 0; i < cardsToDeal; i++)
            {
                // Deals one card to each player
                for (int ii = 0; ii < playersToDeal; ii++)
                {
                    // If all the cards in the deck are gone break the loop
                    if (deck.Count() == 0)
                    {
                        break;
                    }

                    players[ii][i] = deck[0]; // Add the card at the top of the deck to the player's hand
                    deck.RemoveAt(0); // Remove the card just given away from the deck
                }
            }

            return players;
        }

        /// <summary>
        /// Removes all the cards in the game from play
        /// </summary>
        /// <param name="deck"> The deck that will be cleared. This is a "ref" so that it is removed from the actual variable not just in the loop. </param>
        /// <param name="players"> The players that will have their hand cleared. This is a "ref" so that it is removed from the actual variable not just in the loop. </param>
        public static void RemoveAllCards(ref List<Card> deck, ref Card[][] players)
        {
            // Clear cards in the deck
            deck.Clear();

            // Remove all cards in the players hands
            for (int i = 0; i < players.Count(); i++)
            {
                for (int ii = 0; ii < players[i].Count(); ii++)
                {
                    players[i][ii] = null;
                }
            }
        }
    }
}
