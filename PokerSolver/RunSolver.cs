using System;

namespace PokerSolver
{
    class RunSolver
    {
        static void Main(string[] args)
        {
            Hand myCards = ParseCards.parseCards(args);

            (Hand, Hand) bestHand = myCards.FindBestHand();

            Console.WriteLine("The best hand found is:");
            Console.Write("( ");
            foreach (Card card in bestHand.Item1.GetCards())
            {
                Console.Write("{0}{1} ", card.Value, card.Suit); 
            }
            Console.Write("), ( ");
            if (bestHand.Item2 != null)
            {
                foreach (Card card in bestHand.Item2.GetCards())
                {
                    Console.Write("{0}{1} ", card.Value, card.Suit);
                }
            }
            Console.Write(")");
        }
    }
}
