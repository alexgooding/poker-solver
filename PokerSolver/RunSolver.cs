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
            Console.Write("( ");
            foreach (Card card in bestHand.MainHand.GetCards())
            {
                Console.Write("{0}{1} ", card.Value, card.Suit); 
            }
            Console.Write("), ( ");
            if (bestHand.KickerHand != null)
            {
                foreach (Card card in bestHand.KickerHand.GetCards())
                {
                    Console.Write("{0}{1} ", card.Value, card.Suit);
                }
            }
            Console.Write(")");
        }
    }
}
