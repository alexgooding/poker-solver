using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerSolver;
using static PokerSolver.Constants;

namespace PokerSolverTests
{
    [TestClass]
    public class TestFindBestHand
    {
        [TestMethod]
        public void TestFindBestHandRoyalFlush()
        {
            string[] testCardsString = { "6h", "13d", "10d", "11d", "3s", "14d", "12d" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(14, Suit.Diamonds), new Card(13, Suit.Diamonds), new Card(12, Suit.Diamonds), new Card(11, Suit.Diamonds), new Card(10, Suit.Diamonds) });
            Hand expectedKickerHand = null;

            SortedHand actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.MainHand), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.KickerHand), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandStraightFlush()
        {
            string[] testCardsString = { "6s", "13d", "5s", "11d", "3s", "4s", "2s" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(6, Suit.Spades), new Card(5, Suit.Spades), new Card(4, Suit.Spades), new Card(3, Suit.Spades), new Card(2, Suit.Spades) });
            Hand expectedKickerHand = null;

            SortedHand actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.MainHand), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.KickerHand), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandFour()
        {
            string[] testCardsString = { "4c", "13d", "4d", "11h", "11d", "4s", "4h" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(4, Suit.Clubs), new Card(4, Suit.Diamonds), new Card(4, Suit.Spades), new Card(4, Suit.Hearts) });
            Hand expectedKickerHand = new Hand(new List<Card> { new Card(13, Suit.Diamonds) });

            SortedHand actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.MainHand), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.KickerHand), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandFullHouse()
        {
            string[] testCardsString1 = { "8s", "8d", "13c", "12d", "8h", "12s", "12c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);

            Hand expectedMainHand1 = new Hand(new List<Card> { new Card(12, Suit.Diamonds), new Card(12, Suit.Spades), new Card(12, Suit.Clubs) });
            Hand expectedKickerHand1 = new Hand(new List<Card> { new Card(8, Suit.Spades), new Card(8, Suit.Diamonds) });

            SortedHand actual1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "6s", "6d", "7c", "11d", "7h", "11s", "6h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);

            Hand expectedMainHand2 = new Hand(new List<Card> { new Card(6, Suit.Spades), new Card(6, Suit.Diamonds), new Card(6, Suit.Hearts) });
            Hand expectedKickerHand2 = new Hand(new List<Card> { new Card(11, Suit.Diamonds), new Card(11, Suit.Spades) });

            SortedHand actual2 = testCards2.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand1, actual1.MainHand), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand1, actual1.KickerHand), "Kicker hand 1 is not as expected");

            Assert.IsTrue(Hand.IsEqual(expectedMainHand2, actual2.MainHand), "Main hand 2 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand2, actual2.KickerHand), "Kicker hand 2 is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandFlush()
        {
            string[] testCardsString = { "6s", "13d", "7s", "11d", "3s", "9s", "2s" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(9, Suit.Spades), new Card(7, Suit.Spades), new Card(6, Suit.Spades), new Card(3, Suit.Spades), new Card(2, Suit.Spades) });
            Hand expectedKickerHand = null;

            SortedHand actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.MainHand), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.KickerHand), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandStraight()
        {
            string[] testCardsString1 = { "10h", "14d", "8s", "11d", "7s", "9s", "12c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);

            Hand expectedMainHand1 = new Hand(new List<Card> { new Card(12, Suit.Clubs), new Card(11, Suit.Diamonds), new Card(10, Suit.Hearts), new Card(9, Suit.Spades), new Card(8, Suit.Spades) });
            Hand expectedKickerHand1 = null;

            SortedHand actual1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "2h", "14d", "8s", "5d", "7s", "3s", "4c" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);

            Hand expectedMainHand2 = new Hand(new List<Card> { new Card(5, Suit.Diamonds), new Card(4, Suit.Clubs), new Card(3, Suit.Spades), new Card(2, Suit.Hearts), new Card(1, Suit.Diamonds) });
            Hand expectedKickerHand2 = null;

            SortedHand actual2 = testCards2.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand1, actual1.MainHand), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand1, actual1.KickerHand), "Kicker hand is not as expected");

            Assert.IsTrue(Hand.IsEqual(expectedMainHand2, actual2.MainHand), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand2, actual2.KickerHand), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandTriple()
        {
            string[] testCardsString = { "8s", "8d", "13c", "2d", "8h", "12s", "5c" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(8, Suit.Spades), new Card(8, Suit.Diamonds), new Card(8, Suit.Hearts) });
            Hand expectedKickerHand = new Hand(new List<Card> { new Card(13, Suit.Clubs), new Card(12, Suit.Spades) });

            SortedHand actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.MainHand), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.KickerHand), "Kicker hand 1 is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandTwoPair()
        {
            string[] testCardsString1 = { "8s", "8d", "13c", "12d", "13h", "12s", "10c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);

            Hand expectedMainHand1 = new Hand(new List<Card> { new Card(13, Suit.Clubs), new Card(13, Suit.Hearts), new Card(12, Suit.Diamonds), new Card(12, Suit.Spades) });
            Hand expectedKickerHand1 = new Hand(new List<Card> { new Card(10, Suit.Clubs) });

            SortedHand actual1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "6s", "6d", "8c", "11d", "8h", "9s", "2h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);

            Hand expectedMainHand2 = new Hand(new List<Card> { new Card(8, Suit.Clubs), new Card(8, Suit.Hearts), new Card(6, Suit.Spades), new Card(6, Suit.Diamonds) });
            Hand expectedKickerHand2 = new Hand(new List<Card> { new Card(11, Suit.Diamonds) });

            SortedHand actual2 = testCards2.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand1, actual1.MainHand), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand1, actual1.KickerHand), "Kicker hand 1 is not as expected");

            Assert.IsTrue(Hand.IsEqual(expectedMainHand2, actual2.MainHand), "Main hand 2 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand2, actual2.KickerHand), "Kicker hand 2 is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandPair()
        {
            string[] testCardsString = { "4s", "5d", "14c", "2d", "6h", "12s", "6c" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(6, Suit.Hearts), new Card(6, Suit.Clubs) });
            Hand expectedKickerHand = new Hand(new List<Card> { new Card(14, Suit.Clubs), new Card(12, Suit.Spades), new Card(5, Suit.Diamonds) });

            SortedHand actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.MainHand), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.KickerHand), "Kicker hand 1 is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandHighCard()
        {
            string[] testCardsString = { "4s", "5d", "13c", "2d", "6h", "12s", "8c" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(13, Suit.Clubs) });
            Hand expectedKickerHand = new Hand(new List<Card> { new Card(12, Suit.Spades), new Card(8, Suit.Clubs), new Card(6, Suit.Hearts), new Card(5, Suit.Diamonds) });

            SortedHand actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.MainHand), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.KickerHand), "Kicker hand 1 is not as expected");
        }
    }
}
