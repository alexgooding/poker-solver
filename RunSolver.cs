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

            Hand flushCards = myCards.isFlush();

            if (flushCards != null)
            {
                Console.WriteLine("A flush has been found!");
            }

        }
    }
}
