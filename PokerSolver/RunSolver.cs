using System;

namespace PokerSolver
{
    class RunSolver
    {
        static void Main(string[] args)
        {
            Hand myCards = ParseCards.parseCards(args);

            SortedHand bestHand = myCards.FindBestHand();

            Console.WriteLine("The best hand found is:");
            bestHand.PrintSortedHand();
        }
    }
}
