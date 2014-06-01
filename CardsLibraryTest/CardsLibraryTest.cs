using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CardsLibraryTest
{
    [TestClass]
    public class CardsLibraryTest
    {
        [TestMethod]
        public void CreateCardTest()  //Tests both constructors with paramiters
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

        [TestMethod]
        public void CreateDeckTest()   //Test the deck creation
        {
            // arrange
            List<CardsLibrary.Card> Deck = new List<CardsLibrary.Card>();
            //V = Value, S = Suit
            var expectedSH = CardsLibrary.Suit.Hearts;    //H = Hearts
            var expectedVH = CardsLibrary.Value.King;
            var expectedSS = CardsLibrary.Suit.Spades;    //S = Spades
            var expectedVS = CardsLibrary.Value.King;
            var expectedSD = CardsLibrary.Suit.Diamonds;  //D = Diamonds
            var expectedVD = CardsLibrary.Value.King;
            var expectedSC = CardsLibrary.Suit.Clubs;     //C = Clubs
            var expectedVC = CardsLibrary.Value.King;

            var expectedJK = new CardsLibrary.Card(CardsLibrary.Value.Null, CardsLibrary.Suit.Null);

            // act
            CardsLibrary.CardFactory.PopulateDeck(out Deck);
            var actualSH = Deck[51].Suit;  //H = Hearts
            var actualVH = Deck[51].Value;
            var actualSS = Deck[38].Suit;  //S = Spades
            var actualVS = Deck[38].Value;
            var actualSD = Deck[25].Suit;  //D = Diamonds
            var actualVD = Deck[25].Value;
            var actualSC = Deck[12].Suit;  //C = Clubs
            var actualVC = Deck[12].Value;

            Assert.AreEqual(expectedSH, actualSH);
            Assert.AreEqual(expectedVH, actualVH);

            Assert.AreEqual(expectedSS, actualSS);
            Assert.AreEqual(expectedVS, actualVS);

            Assert.AreEqual(expectedSD, actualSD);
            Assert.AreEqual(expectedVD, actualVD);

            Assert.AreEqual(expectedSC, actualSC);
            Assert.AreEqual(expectedVC, actualVC);

            // act
            CardsLibrary.CardFactory.PopulateDeck(out Deck, false, true); //Re populates, with jokers
            actualSH = Deck[51].Suit;  //H = Hearts
            actualVH = Deck[51].Value;
            actualSS = Deck[38].Suit;  //S = Spades
            actualVS = Deck[38].Value;
            actualSD = Deck[25].Suit;  //D = Diamonds
            actualVD = Deck[25].Value;
            actualSC = Deck[12].Suit;  //C = Clubs
            actualVC = Deck[12].Value;
            var actualJK1 = Deck[52];
            var actualJK2 = Deck[53];

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

        [TestMethod]
        public void ShuffleTest()  //Tests the shuffling
        {
            // arrange
            List<CardsLibrary.Card> Deck = new List<CardsLibrary.Card>();
            CardsLibrary.CardFactory.PopulateDeck(out Deck);

            CardsLibrary.Card notWanted = Deck[0];

            // act
            CardsLibrary.CardFactory.Shuffle(ref Deck);

            CardsLibrary.Card actual = Deck[0];

            // assert
            Assert.AreNotEqual(notWanted, actual);
        }

        [TestMethod]
        public void DealingTest()  //Tests the Dealing
        {
            // arrange
            CardsLibrary.Card[][] Players = new CardsLibrary.Card[5][];
            for (int i = 0; i < Players.Length; i++)
            {
                Players[i] = new CardsLibrary.Card[7];
            }

            List<CardsLibrary.Card> Deck = new List<CardsLibrary.Card>();     //Deck not shuffled so we can track what order the card will go to players
            CardsLibrary.CardFactory.PopulateDeck(out Deck);

            var expected = new CardsLibrary.Card(CardsLibrary.Value.Nine, CardsLibrary.Suit.Spades);  //The last Players Last Card

            // act
            CardsLibrary.CardFactory.Deal(ref Deck, ref Players, 7);

            // assert
            Assert.AreEqual(expected, Players[4][6]);
        }

        [TestMethod]
        public void ResetCardsTest()  //Tests the Dealing
        {
            // arrange
            CardsLibrary.Card[][] Players = new CardsLibrary.Card[5][];
            for (int i = 0; i < Players.Length; i++)
            {
                Players[i] = new CardsLibrary.Card[7];
            }

            List<CardsLibrary.Card> Deck = new List<CardsLibrary.Card>();
            CardsLibrary.CardFactory.PopulateDeck(out Deck);

            CardsLibrary.Card expected1 = null;   //The value of any card in the player array
            int expected2 = 0;                    //The count of cards in the Deck

            CardsLibrary.CardFactory.Deal(ref Deck, ref Players, 7);

            // act
            CardsLibrary.CardFactory.RemoveAllCards(ref Deck, ref Players);

            // assert
            Assert.AreEqual(expected1, Players[4][6]);
            Assert.AreEqual(expected2, Deck.Count());
        }

        [TestMethod]
        public void BestCardTest()  //Tests the Picking the greatest card (includes the settings testing)
        {
            // arrange
            List<CardsLibrary.Card> cardsList = new List<CardsLibrary.Card>();
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Ace, CardsLibrary.Suit.Spades));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Ace, CardsLibrary.Suit.Diamonds));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.King, CardsLibrary.Suit.Diamonds));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Seven, CardsLibrary.Suit.Clubs));
            CardsLibrary.Card[] Cards = cardsList.ToArray();

            #region Test 1
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.Settings.AceHigh = false;

            // act
            CardsLibrary.Card actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);
            #endregion

            #region Test 2
            // arrange 2
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);
            #endregion

            #region Test 3
            // arrange 3
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Spades);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);
            #endregion

            #region Test 4
            // arrange 4
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Spades);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);
            #endregion

            #region Test 5
            // arrange 5
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);
            #endregion

            #region Test 6
            // arrange 6
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);
            #endregion

            #region Test 7
            // arrange 7
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Clubs);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);
            #endregion

            #region Test 8
            // arrange 8
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Clubs);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);
            #endregion

            #region Test 9
            // arrange 9
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Hearts);
            CardsLibrary.SuitOrder.SetPlayed(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);
            #endregion

            #region Test 10
            // arrange 10
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Hearts);
            CardsLibrary.SuitOrder.SetPlayed(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.HighestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);
            #endregion
        }
        [TestMethod]
        public void WorstCardTest()  //Tests the Picking the greatest card (includes the settings testing)
        {
            // arrange
            List<CardsLibrary.Card> cardsList = new List<CardsLibrary.Card>();
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Ace, CardsLibrary.Suit.Spades));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Ace, CardsLibrary.Suit.Diamonds));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.King, CardsLibrary.Suit.Diamonds));
            cardsList.Add(new CardsLibrary.Card(CardsLibrary.Value.Seven, CardsLibrary.Suit.Clubs));
            CardsLibrary.Card[] Cards = cardsList.ToArray();

            #region Test 1
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.Settings.AceHigh = false;

            // act
            CardsLibrary.Card actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);
            #endregion

            #region Test 2
            // arrange 2
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);
            #endregion

            #region Test 3
            // arrange 3
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Spades);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);
            #endregion

            #region Test 4
            // arrange 4
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Spades);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);
            #endregion

            #region Test 5
            // arrange 5
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);
            #endregion

            #region Test 6
            // arrange 6
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);
            #endregion

            #region Test 7
            // arrange 7
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Clubs);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[1], actual);
            #endregion

            #region Test 8
            // arrange 8
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Clubs);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[2], actual);
            #endregion

            #region Test 9
            // arrange 9
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Hearts);
            CardsLibrary.SuitOrder.SetPlayed(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = false;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[0], actual);
            #endregion

            #region Test 10
            // arrange 10
            CardsLibrary.SuitOrder.Reset();
            CardsLibrary.SuitOrder.SetTrumps(CardsLibrary.Suit.Hearts);
            CardsLibrary.SuitOrder.SetPlayed(CardsLibrary.Suit.Diamonds);
            CardsLibrary.Settings.AceHigh = true;

            // act
            actual = CardsLibrary.Card.LowestCardFromArray(Cards);

            // assert
            Assert.AreEqual(cardsList[3], actual);
            #endregion
        }

        [TestMethod]
        public void EqualsTest()  //Tests the Card.Equals Meathod overload and also the == and != overloads
        {
            // arrange
            CardsLibrary.Card c = new CardsLibrary.Card(1, 1);
            CardsLibrary.Card cCopy = c;
            CardsLibrary.Card d = new CardsLibrary.Card(1, 2);

            bool expectedOne = true;    //This is for 1, 3 and 6
            bool expectedTwo = false;   //This is for 2, 4 and 5

            // act
            bool actualOne = c.Equals(cCopy);
            bool actualTwo = c.Equals(d);

            bool actualThree = (c == cCopy);
            bool actualFour = (c == d);

            bool actualFive = (c != cCopy);
            bool actualSix = (c != d);

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
