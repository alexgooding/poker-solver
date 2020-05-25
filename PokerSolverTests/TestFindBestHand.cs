using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerSolver;

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

            Hand expectedMainHand = new Hand(new List<Card> { new Card(14, "d"), new Card(13, "d"), new Card(12, "d"), new Card(11, "d"), new Card(10, "d") });
            Hand expectedKickerHand = null;

            (Hand, Hand) actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.Item1), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.Item2), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandStraightFlush()
        {
            string[] testCardsString = { "6s", "13d", "5s", "11d", "3s", "4s", "2s" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(6, "s"), new Card(5, "s"), new Card(4, "s"), new Card(3, "s"), new Card(2, "s") });
            Hand expectedKickerHand = null;

            (Hand, Hand) actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.Item1), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.Item2), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandFour()
        {
            string[] testCardsString = { "4c", "13d", "4d", "11h", "11d", "4s", "4h" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(4, "c"), new Card(4, "d"), new Card(4, "s"), new Card(4, "h") });
            Hand expectedKickerHand = new Hand(new List<Card> { new Card(13, "d") });

            (Hand, Hand) actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.Item1), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.Item2), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandFullHouse()
        {
            string[] testCardsString1 = { "8s", "8d", "13c", "12d", "8h", "12s", "12c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);

            Hand expectedMainHand1 = new Hand(new List<Card> { new Card(12, "d"), new Card(12, "s"), new Card(12, "c") });
            Hand expectedKickerHand1 = new Hand(new List<Card> { new Card(8, "s"), new Card(8, "d") });

            (Hand, Hand) actual1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "6s", "6d", "7c", "11d", "7h", "11s", "6h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);

            Hand expectedMainHand2 = new Hand(new List<Card> { new Card(6, "s"), new Card(6, "d"), new Card(6, "h") });
            Hand expectedKickerHand2 = new Hand(new List<Card> { new Card(11, "d"), new Card(11, "s") });

            (Hand, Hand) actual2 = testCards2.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand1, actual1.Item1), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand1, actual1.Item2), "Kicker hand 1 is not as expected");

            Assert.IsTrue(Hand.IsEqual(expectedMainHand2, actual2.Item1), "Main hand 2 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand2, actual2.Item2), "Kicker hand 2 is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandFlush()
        {
            string[] testCardsString = { "6s", "13d", "7s", "11d", "3s", "9s", "2s" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(9, "s"), new Card(7, "s"), new Card(6, "s"), new Card(3, "s"), new Card(2, "s") });
            Hand expectedKickerHand = null;

            (Hand, Hand) actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.Item1), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.Item2), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandStraight()
        {
            string[] testCardsString1 = { "10h", "14d", "8s", "11d", "7s", "9s", "12c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);

            Hand expectedMainHand1 = new Hand(new List<Card> { new Card(12, "c"), new Card(11, "d"), new Card(10, "h"), new Card(9, "s"), new Card(8, "s") });
            Hand expectedKickerHand1 = null;

            (Hand, Hand) actual1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "2h", "14d", "8s", "5d", "7s", "3s", "4c" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);

            Hand expectedMainHand2 = new Hand(new List<Card> { new Card(5, "d"), new Card(4, "c"), new Card(3, "s"), new Card(2, "h"), new Card(1, "d") });
            Hand expectedKickerHand2 = null;

            (Hand, Hand) actual2 = testCards2.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand1, actual1.Item1), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand1, actual1.Item2), "Kicker hand is not as expected");

            Assert.IsTrue(Hand.IsEqual(expectedMainHand2, actual2.Item1), "Main hand is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand2, actual2.Item2), "Kicker hand is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandTriple()
        {
            string[] testCardsString = { "8s", "8d", "13c", "2d", "8h", "12s", "5c" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(8, "s"), new Card(8, "d"), new Card(8, "h") });
            Hand expectedKickerHand = new Hand(new List<Card> { new Card(13, "c"), new Card(12, "s") });

            (Hand, Hand) actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.Item1), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.Item2), "Kicker hand 1 is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandTwoPair()
        {
            string[] testCardsString1 = { "8s", "8d", "13c", "12d", "13h", "12s", "10c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);

            Hand expectedMainHand1 = new Hand(new List<Card> { new Card(13, "c"), new Card(13, "h"), new Card(12, "d"), new Card(12, "s") });
            Hand expectedKickerHand1 = new Hand(new List<Card> { new Card(10, "c") });

            (Hand, Hand) actual1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "6s", "6d", "8c", "11d", "8h", "9s", "2h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);

            Hand expectedMainHand2 = new Hand(new List<Card> { new Card(8, "c"), new Card(8, "h"), new Card(6, "s"), new Card(6, "d") });
            Hand expectedKickerHand2 = new Hand(new List<Card> { new Card(11, "d") });

            (Hand, Hand) actual2 = testCards2.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand1, actual1.Item1), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand1, actual1.Item2), "Kicker hand 1 is not as expected");

            Assert.IsTrue(Hand.IsEqual(expectedMainHand2, actual2.Item1), "Main hand 2 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand2, actual2.Item2), "Kicker hand 2 is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandPair()
        {
            string[] testCardsString = { "4s", "5d", "14c", "2d", "6h", "12s", "6c" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(6, "h"), new Card(6, "c") });
            Hand expectedKickerHand = new Hand(new List<Card> { new Card(14, "c"), new Card(12, "s"), new Card(5, "d") });

            (Hand, Hand) actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.Item1), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.Item2), "Kicker hand 1 is not as expected");
        }

        [TestMethod]
        public void TestFindBestHandHighCard()
        {
            string[] testCardsString = { "4s", "5d", "13c", "2d", "6h", "12s", "8c" };
            Hand testCards = ParseCards.parseCards(testCardsString);

            Hand expectedMainHand = new Hand(new List<Card> { new Card(13, "c") });
            Hand expectedKickerHand = new Hand(new List<Card> { new Card(12, "s"), new Card(8, "c"), new Card(6, "h"), new Card(5, "d") });

            (Hand, Hand) actual = testCards.FindBestHand();

            Assert.IsTrue(Hand.IsEqual(expectedMainHand, actual.Item1), "Main hand 1 is not as expected");
            Assert.IsTrue(Hand.IsEqual(expectedKickerHand, actual.Item2), "Kicker hand 1 is not as expected");
        }
    }
}
