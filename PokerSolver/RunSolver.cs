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

            (Hand, Hand) bestHand = myCards.findBestHand();

            Console.WriteLine("The best hand found is:");
            Console.Write("( ");
            foreach (Card card in bestHand.Item1.getCards())
            {
                Console.Write("{0}{1} ", card.Value, card.Suit); 
            }
            Console.Write("), ( ");
            if (bestHand.Item2 != null)
            {
                foreach (Card card in bestHand.Item2.getCards())
                {
                    Console.Write("{0}{1} ", card.Value, card.Suit);
                }
            }
            Console.Write(")");
        }
    }
}
