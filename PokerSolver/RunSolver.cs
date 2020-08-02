using System;

namespace PokerSolver
{
    class RunSolver
    {
        Hand myHand;
        Hand communityHand;

        public void runMainWorkflow(int numberOfPlayers)
        {
            myHand = new Hand();
            communityHand = new Hand();

            string[] line;
            Console.WriteLine("Enter the two cards in your hand");
            line = Console.ReadLine().Split(null);

            myHand.AddCards(ParseCards.parseCards(line));

            Console.WriteLine("Your best hand is:");
            myHand.FindBestHand().PrintSortedHand();

            Console.WriteLine("Enter the first three community cards");
            line = Console.ReadLine().Split(null);

            myHand.AddCards(ParseCards.parseCards(line));
            communityHand.AddCards(ParseCards.parseCards(line));

            Console.WriteLine("Your best hand is:");
            myHand.FindBestHand().PrintSortedHand();

            Console.WriteLine("Enter the fourth community card");
            line = Console.ReadLine().Split(null);

            myHand.AddCards(ParseCards.parseCards(line));
            communityHand.AddCards(ParseCards.parseCards(line));

            Console.WriteLine("Your best hand is:");
            myHand.FindBestHand().PrintSortedHand();

            Console.WriteLine("Enter the fifth community card");
            line = Console.ReadLine().Split(null);

            myHand.AddCards(ParseCards.parseCards(line));
            communityHand.AddCards(ParseCards.parseCards(line));

            Console.WriteLine("Your best hand is:");
            myHand.FindBestHand().PrintSortedHand();
        }
    }
}
