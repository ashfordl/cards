// CardsLibraryTest.cs
// <copyright file="CardsLibraryTest.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardsLibraryTest
{
    /// <summary>
    /// This class is a test class to test the CardsLibrary.
    /// </summary>
    [TestClass]
    public class CardsLibraryTest
    {
        /// <summary>
        /// This test will test the creating cards methods.
        /// </summary>
        [TestMethod]
        public void CreateCardTest() 
        {
            // arrange
            int val = 1;
            int suit = 1;

            var expectedS = CardsLibrary.Suit.Clubs;
            var expectedV = CardsLibrary.Value.Ace;

            // act
            CardsLibrary.Card c = new CardsLibrary.Card((CardsLibrary.Value)val, (CardsLibrary.Suit)suit);
            CardsLibrary.Card d = new CardsLibrary.Card(val, suit);
            var actualS1 = c.Suit;
            var actualV1 = c.Value;
            var actualS2 = d.Suit;
            var actualV2 = d.Value;

            // assert
            Assert.AreEqual(expectedS, actualS1);
            Assert.AreEqual(expectedV, actualV1);
            Assert.AreEqual(expectedS, actualS2);
            Assert.AreEqual(expectedV, actualV2);
        }
        
        /// <summary>
        /// This test will test the creating deck method.
        /// </summary>
        [TestMethod]
        public void CreateDeckTest()  
        {
            // arrange
            List<CardsLibrary.Card> deck = new List<CardsLibrary.Card>();

            // V = Value, S = Suit
            var expectedSH = CardsLibrary.Suit.Hearts;    // H = Hearts
            var expectedVH = CardsLibrary.Value.King;
            var expectedSS = CardsLibrary.Suit.Spades;    // S = Spades
            var expectedVS = CardsLibrary.Value.King;
            var expectedSD = CardsLibrary.Suit.Diamonds;  // D = Diamonds
            var expectedVD = CardsLibrary.Value.King;
            var expectedSC = CardsLibrary.Suit.Clubs;     // C = Clubs
            var expectedVC = CardsLibrary.Value.King;

            var expectedJK = new CardsLibrary.Card(CardsLibrary.Value.Null, CardsLibrary.Suit.Null);

            // act
            deck = CardsLibrary.CardFactory.PopulateDeck();
            var actualSH = deck[51].Suit;  // H = Hearts
            var actualVH = deck[51].Value;
            var actualSS = deck[38].Suit;  // S = Spades
            var actualVS = deck[38].Value;
            var actualSD = deck[25].Suit;  // D = Diamonds
            var actualVD = deck[25].Value;
            var actualSC = deck[12].Suit;  // C = Clubs
            var actualVC = deck[12].Value;

            // assert
            Assert.AreEqual(expectedSH, actualSH);
            Assert.AreEqual(expectedVH, actualVH);

            Assert.AreEqual(expectedSS, actualSS);
            Assert.AreEqual(expectedVS, actualVS);

            Assert.AreEqual(expectedSD, actualSD);
            Assert.AreEqual(expectedVD, actualVD);

            Assert.AreEqual(expectedSC, actualSC);
            Assert.AreEqual(expectedVC, actualVC);

            // act
            deck = CardsLibrary.CardFactory.PopulateDeck(false, true); // Re populates, with jokers
            actualSH = deck[51].Suit;  // H = Hearts
            actualVH = deck[51].Value;
            actualSS = deck[38].Suit;  // S = Spades
            actualVS = deck[38].Value;
            actualSD = deck[25].Suit;  // D = Diamonds
            actualVD = deck[25].Value;
            actualSC = deck[12].Suit;  // C = Clubs
            actualVC = deck[12].Value;
            var actualJK1 = deck[52];
            var actualJK2 = deck[53];

            // assert
            Assert.AreEqual(expectedSH, actualSH);
            Assert.AreEqual(expectedVH, actualVH);

            Assert.AreEqual(expectedSS, actualSS);
            Assert.AreEqual(expectedVS, actualVS);

            Assert.AreEqual(expectedSD, actualSD);
            Assert.AreEqual(expectedVD, actualVD);

            Assert.AreEqual(expectedSC, actualSC);
            Assert.AreEqual(expectedVC, actualVC);

            Assert.AreEqual(expectedJK, actualJK1);
            Assert.AreEqual(expectedJK, actualJK2);
        }

        /// <summary>
        /// This test will test the shuffling deck method.
        /// </summary>
        [TestMethod]
        public void ShuffleTest()  
        {
            // arrange
            List<CardsLibrary.Card> deck = new List<CardsLibrary.Card>();
            deck = CardsLibrary.CardFactory.PopulateDeck();

            CardsLibrary.Card notWanted = deck[0];

            // act
            deck = CardsLibrary.CardFactory.Shuffle(deck);

            CardsLibrary.Card actual = deck[0];

            // assert
            Assert.AreNotEqual(notWanted, actual);
        }

        /// <summary>
        /// This test will test the dealing method.
        /// </summary>
        [TestMethod]
        public void DealingTest() 
        {
            // arrange
            CardsLibrary.Card[][] players = new CardsLibrary.Card[5][];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new CardsLibrary.Card[7];
            }

            List<CardsLibrary.Card> deck = new List<CardsLibrary.Card>(); // Deck not shuffled so we can track what order the card will go to players
            CardsLibrary.CardFactory.PopulateDeck();

            var expected = new CardsLibrary.Card(CardsLibrary.Value.Nine, CardsLibrary.Suit.Spades);  // The last Players Last Card

            // act
            players = CardsLibrary.CardFactory.Deal(ref deck, 5, 7);

            // assert
            Assert.AreEqual(expected, players[4][6]);
        }

        /// <summary>
        /// This test will test the removing of all cards method.
        /// </summary>
        [TestMethod]
        public void ResetCardsTest() 
        {
            // arrange
            CardsLibrary.Card[][] players = new CardsLibrary.Card[5][];
            for (int i = 0; i < players.Length; i++)
            {
                players[i] = new CardsLibrary.Card[7];
            }

            List<CardsLibrary.Card> deck = new List<CardsLibrary.Card>();
            deck = CardsLibrary.CardFactory.PopulateDeck();

            CardsLibrary.Card expected1 = null;   // The value of any card in the player array
            int expected2 = 0;                    // The count of cards in the Deck

            players = CardsLibrary.CardFactory.Deal(ref deck, 5, 7);

            // act
            CardsLibrary.CardFactory.RemoveAllCards(ref deck, ref players);

            // assert
            Assert.AreEqual(expected1, players[4][6]);
            Assert.AreEqual(expected2, deck.Count());
        }

        /// <summary>
        /// This test will test the best card comparison method.
        /// </summary>
        [TestMethod]
        public void BestCardTest() 
        {
            // arrange
            List<CardsLibrary.Card> cardsList = new List<CardsLibrary.Card>();
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Ace, CardsLibrary.Suit.Spades));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Ace, CardsLibrary.Suit.Diamonds));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.King, CardsLibrary.Suit.Diamonds));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Seven, CardsLibrary.Suit.Clubs));
            CardsLibrary.Card[] cards = cardsList.ToArray();

            // arrange 1
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.Settings.AceHigh = false;

            // act
            CardsLibrary.Card actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);
            
            // arrange 2
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 3
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Spades);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 4
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Spades);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 5
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);

            // arrange 6
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);

            // arrange 7
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Clubs);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 8
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Clubs);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 9
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Hearts);
            CardsLibrary.SuitOrder.SetPlayed(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);

            // arrange 10
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Hearts);
            CardsLibrary.SuitOrder.SetPlayed(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);
        }

        /// <summary>
        /// This test will test the worst card comparison method.
        /// </summary>
        [TestMethod]
        public void WorstCardTest() 
        {
            // arrange
            List<CardsLibrary.Card> cardsList = new List<CardsLibrary.Card>();
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Ace, CardsLibrary.Suit.Spades));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Ace, CardsLibrary.Suit.Diamonds));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.King, CardsLibrary.Suit.Diamonds));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Seven, CardsLibrary.Suit.Clubs));
            CardsLibrary.Card[] cards = cardsList.ToArray();

            // arrange 1
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.Settings.AceHigh = false;

            // act
            CardsLibrary.Card actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);

            // arrange 2
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 3
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Spades);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);

            // arrange 4
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Spades);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 5
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 6
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 7
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Clubs);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);

            // arrange 8
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Clubs);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);

            // arrange 9
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Hearts);
            CardsLibrary.SuitOrder.SetPlayed(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 10
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Hearts);
            CardsLibrary.SuitOrder.SetPlayed(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);
        }

        /// <summary>
        /// This test will test the equal comparison method.
        /// </summary>
        [TestMethod]
        public void EqualsTest() 
        {
            // arrange
            CardsLibrary.Card c = new CardsLibrary.Card(1, 1);
            CardsLibrary.Card cCopy = c;
            CardsLibrary.Card d = new CardsLibrary.Card(1, 2);

            bool expectedOne = true;    // This is for 1, 3 and 6
            bool expectedTwo = false;   // This is for 2, 4 and 5

            // act
            bool actualOne = c.Equals(cCopy);
            bool actualTwo = c.Equals(d);

            bool actualThree = c == cCopy;
            bool actualFour = c == d;

            bool actualFive = c != cCopy;
            bool actualSix = c != d;

            // assert
            Assert.AreEqual(expectedOne, actualOne);
            Assert.AreEqual(expectedTwo, actualTwo);

            Assert.AreEqual(expectedOne, actualThree);
            Assert.AreEqual(expectedTwo, actualFour);

            Assert.AreEqual(expectedTwo, actualFive);
            Assert.AreEqual(expectedOne, actualSix);
        }
    }
}
