using System;
using System.Collections.Generic;
using System.Text;

namespace PokerSolver
{
    class RunSolver
    {
        static void Main(string[] args)
        {
            Hand myCards = ParseCards.parseCards(args);

            Dictionary<int, Hand> valueCount = myCards.countByValue();
            Hand tens = valueCount[10];
            Console.WriteLine("There are {0} tens in the hand.", tens.count());

            (Hand, Hand) tripleAndKickers = myCards.findTriple();

            (Hand, Hand) pairsAndKickers = myCards.findPair();

            (Hand, Hand) pairsAndKicker = myCards.findTwoPair();

            (Hand, Hand) highCardAndKickers = myCards.findHighCard();

            Hand flushCards = myCards.findFlush().Item1;

            if (flushCards != null)
            {
                Console.WriteLine("A flush has been found!");
            }

        }
    }
}
