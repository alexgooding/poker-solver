using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerSolver;

namespace PokerSolverTests
{
    [TestClass]
    public class TestIsFirstHandBetterThanSecondHand
    {
        [TestMethod]
        public void TestIsFirstHandBetterThanSecondHandRoyalFlush()
        {
            string[] testCardsString1 = { "6h", "13d", "10d", "11d", "3s", "14d", "12d" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);
            (Hand, Hand) bestHand1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "10h", "12h", "2s", "14h", "3s", "11h", "13h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);
            (Hand, Hand) bestHand2 = testCards2.FindBestHand();

            bool? expected = null;

            bool? actual = Hand.IsFirstHandBetterThanSecondHand(bestHand1, bestHand2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIsFirstHandBetterThanSecondHandFullHouse()
        {
            string[] testCardsString1 = { "14h", "12c", "3s", "9h", "3d", "3h", "14c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);
            (Hand, Hand) bestHand1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "11s", "2d", "5d", "11d", "2s", "11h", "3h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);
            (Hand, Hand) bestHand2 = testCards2.FindBestHand();

            bool? expected = false;

            bool? actual = Hand.IsFirstHandBetterThanSecondHand(bestHand1, bestHand2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIsFirstHandBetterThanSecondHandFlush()
        {
            string[] testCardsString1 = { "6h", "13d", "5d", "11d", "3s", "14d", "12d" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);
            (Hand, Hand) bestHand1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "10h", "12h", "2s", "9h", "3s", "11h", "3h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);
            (Hand, Hand) bestHand2 = testCards2.FindBestHand();

            bool? expected = true;

            bool? actual = Hand.IsFirstHandBetterThanSecondHand(bestHand1, bestHand2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIsFirstHandBetterThanSecondHandPair()
        {
            string[] testCardsString1 = { "10h", "12h", "2s", "9h", "3d", "11h", "3c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);
            (Hand, Hand) bestHand1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "6h", "2d", "5d", "11d", "3s", "14d", "3h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);
            (Hand, Hand) bestHand2 = testCards2.FindBestHand();

            bool? expected = false;

            bool? actual = Hand.IsFirstHandBetterThanSecondHand(bestHand1, bestHand2);

            Assert.AreEqual(expected, actual);
        }
    }
}
