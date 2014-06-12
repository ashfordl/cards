// CardsLibraryTest.cs
// <copyright file="CardsLibraryTest.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using System.Linq;
using CardGames.Whist;
using CardsLibrary;
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
        /// Tests the creating cards methods.
        /// </summary>
        [TestMethod]
        public void CreateCardTest() 
        {
            // arrange
            int val = 1;
            int suit = 1;

            var expectedS = Suit.Clubs;
            var expectedV = Value.Ace;

            // act
            Card c = new Card((Value)val, (Suit)suit);
            Card d = new Card(val, suit);
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
        /// Tests the creating deck method.
        /// </summary>
        [TestMethod]
        public void CreateDeckTest()  
        {
            // arrange
            List<Card> deck = new List<Card>();

            // V = Value, S = Suit
            var expectedSH = Suit.Hearts;    // H = Hearts
            var expectedVH = Value.King;
            var expectedSS = Suit.Spades;    // S = Spades
            var expectedVS = Value.King;
            var expectedSD = Suit.Diamonds;  // D = Diamonds
            var expectedVD = Value.King;
            var expectedSC = Suit.Clubs;     // C = Clubs
            var expectedVC = Value.King;

            var expectedJK = new Card(Value.Null, Suit.Null);

            // act
            deck = CardFactory.PopulateDeck();
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
            deck = CardFactory.PopulateDeck(false, true); // Re populates, with jokers
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
        /// Tests the shuffling deck method.
        /// </summary>
        [TestMethod]
        public void ShuffleTest()  
        {
            // arrange
            List<Card> deck = new List<Card>();
            deck = CardFactory.PopulateDeck();

            Card notWanted = deck[0];

            // act
            deck = CardFactory.Shuffle(deck);

            Card actual = deck[0];

            // assert
            Assert.AreNotEqual(notWanted, actual);
        }

        /// <summary>
        /// Tests the dealing method.
        /// </summary>
        [TestMethod]
        public void DealingTest() 
        {
            // arrange
            Whist game = new Whist();
            for (int i = 0; i < 5; i++)
            {
                game.AddPlayer(new ConsolePlayer());
            }

            var expected = new Card(Value.Nine, Suit.Spades);  // The last Players Last Card

            // act
            game.Deal(false, 7); // The deck is not shuffled so we can track where the cards go

            // assert
            Assert.AreEqual(expected, game.Players[4].Hand[6]);
        }

        /// <summary>
        /// Tests the best card comparison method.
        /// </summary>
        [TestMethod]
        public void BestCardTest() 
        {
            // arrange
            List<Card> cardsList = new List<Card>();
            cardsList.Add(new Card(Value.Ace, Suit.Spades));
            cardsList.Add(new Card(Value.Ace, Suit.Diamonds));
            cardsList.Add(new Card(Value.King, Suit.Diamonds));
            cardsList.Add(new Card(Value.Seven, Suit.Clubs));
            Card[] cards = cardsList.ToArray();

            // arrange 1
            SuitOrder.Reset();
            Settings.AceHigh = false;

            // act
            Card actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);
            
            // arrange 2
            SuitOrder.Reset();
            Settings.AceHigh = true;

            // act
            actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 3
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Spades);
            Settings.AceHigh = false;

            // act
            actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 4
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Spades);
            Settings.AceHigh = true;

            // act
            actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 5
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Diamonds);
            Settings.AceHigh = false;

            // act
            actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);

            // arrange 6
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Diamonds);
            Settings.AceHigh = true;

            // act
            actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);

            // arrange 7
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Clubs);
            Settings.AceHigh = false;

            // act
            actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 8
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Clubs);
            Settings.AceHigh = true;

            // act
            actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 9
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Hearts);
            SuitOrder.SetPlayed(Suit.Diamonds);
            Settings.AceHigh = false;

            // act
            actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);

            // arrange 10
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Hearts);
            SuitOrder.SetPlayed(Suit.Diamonds);
            Settings.AceHigh = true;

            // act
            actual = Card.HighestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);
        }

        /// <summary>
        /// Tests the worst card comparison method.
        /// </summary>
        [TestMethod]
        public void WorstCardTest() 
        {
            // arrange
            List<Card> cardsList = new List<Card>();
            cardsList.Add(new Card(Value.Ace, Suit.Spades));
            cardsList.Add(new Card(Value.Ace, Suit.Diamonds));
            cardsList.Add(new Card(Value.King, Suit.Diamonds));
            cardsList.Add(new Card(Value.Seven, Suit.Clubs));
            Card[] cards = cardsList.ToArray();

            // arrange 1
            SuitOrder.Reset();
            Settings.AceHigh = false;

            // act
            Card actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);

            // arrange 2
            SuitOrder.Reset();
            Settings.AceHigh = true;

            // act
            actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 3
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Spades);
            Settings.AceHigh = false;

            // act
            actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);

            // arrange 4
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Spades);
            Settings.AceHigh = true;

            // act
            actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 5
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Diamonds);
            Settings.AceHigh = false;

            // act
            actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 6
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Diamonds);
            Settings.AceHigh = true;

            // act
            actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);

            // arrange 7
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Clubs);
            Settings.AceHigh = false;

            // act
            actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);

            // arrange 8
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Clubs);
            Settings.AceHigh = true;

            // act
            actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);

            // arrange 9
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Hearts);
            SuitOrder.SetPlayed(Suit.Diamonds);
            Settings.AceHigh = false;

            // act
            actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);

            // arrange 10
            SuitOrder.Reset();
            SuitOrder.SetTrumps(Suit.Hearts);
            SuitOrder.SetPlayed(Suit.Diamonds);
            Settings.AceHigh = true;

            // act
            actual = Card.LowestCardFromArray(cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);
        }

        /// <summary>
        /// Tests the equal comparison method.
        /// </summary>
        [TestMethod]
        public void EqualsTest() 
        {
            // arrange
            Card c = new Card(1, 1);
            Card cCopy = c;
            Card d = new Card(1, 2);

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
