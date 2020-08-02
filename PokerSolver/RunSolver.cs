using System;

namespace PokerSolver
{
    class RunSolver
    {
        static void Main(string[] args)
        {
            Hand myCards = ParseCards.parseCards(args);
            SortedHand myBestHand = myCards.FindBestHand();

            Console.WriteLine("My best hand is:");
            myBestHand.PrintSortedHand();

            Hand theirCards = ParseCards.parseCards(new string[] { "11h", "10h", "9h", "8h", "7h", "10d", "9d" });
            SortedHand theirBestHand = theirCards.FindBestHand();

            Console.WriteLine("Their best hand is:");
            theirBestHand.PrintSortedHand();

            var myHandBetterThanTheirs = myBestHand.IsBetterThanHand(theirBestHand);
            if (myHandBetterThanTheirs == null)
            {
                Console.WriteLine("Our hands are equally good");
            }
            else if (myHandBetterThanTheirs == true) {
                Console.WriteLine("My hand is better than theirs");
            }
            else
            {
                Console.WriteLine("Their hand is better than mine");
            }
        }
    }
}
