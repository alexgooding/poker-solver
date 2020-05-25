using Microsoft.VisualStudio.TestTools.UnitTesting;
using PokerSolver;

namespace PokerSolverTests
{
    [TestClass]
    public class TestIsBetterThanHand
    {
        [TestMethod]
        public void TestIsBetterThanHandRoyalFlush()
        {
            string[] testCardsString1 = { "6h", "13d", "10d", "11d", "3s", "14d", "12d" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);
            SortedHand bestHand1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "10h", "12h", "2s", "14h", "3s", "11h", "13h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);
            SortedHand bestHand2 = testCards2.FindBestHand();

            bool? expected = null;

            bool? actual = bestHand1.IsBetterThanHand(bestHand2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIsBetterThanHandFullHouse()
        {
            string[] testCardsString1 = { "14h", "12c", "3s", "9h", "3d", "3h", "14c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);
            SortedHand bestHand1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "11s", "2d", "5d", "11d", "2s", "11h", "3h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);
            SortedHand bestHand2 = testCards2.FindBestHand();

            bool? expected = false;

            bool? actual = bestHand1.IsBetterThanHand(bestHand2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIsBetterThanHandFlush()
        {
            string[] testCardsString1 = { "6h", "13d", "5d", "11d", "3s", "14d", "12d" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);
            SortedHand bestHand1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "10h", "12h", "2s", "9h", "3s", "11h", "3h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);
            SortedHand bestHand2 = testCards2.FindBestHand();

            bool? expected = true;

            bool? actual = bestHand1.IsBetterThanHand(bestHand2);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIsBetterThanHanddPair()
        {
            string[] testCardsString1 = { "10h", "12h", "2s", "9h", "3d", "11h", "3c" };
            Hand testCards1 = ParseCards.parseCards(testCardsString1);
            SortedHand bestHand1 = testCards1.FindBestHand();

            string[] testCardsString2 = { "6h", "2d", "5d", "11d", "3s", "14d", "3h" };
            Hand testCards2 = ParseCards.parseCards(testCardsString2);
            SortedHand bestHand2 = testCards2.FindBestHand();

            bool? expected = false;

            bool? actual = bestHand1.IsBetterThanHand(bestHand2);

            Assert.AreEqual(expected, actual);
        }
    }
}
