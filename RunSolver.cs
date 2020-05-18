using System;
using System.Collections.Generic;
using System.Text;

namespace PokerSolver
{
    class RunSolver
    {
        static void Main(string[] args)
        {
            List<(int, string)> myCards = ParseCards.parseCards(args);

            foreach (var card in myCards)
            {
                Console.WriteLine("value: {0}, suit: {1}", card.Item1, card.Item2);
            }
        }
    }
}
