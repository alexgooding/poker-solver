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

            (Hand, Hand) fourAndKicker = myCards.findFour();

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
