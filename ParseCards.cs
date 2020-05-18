using System;
using System.Collections.Generic;
using System.Globalization;

namespace PokerSolver
{
    public class ParseCards
    {
        public static List<(int, string)> parseCards(string[] cards)
        {
            var myCards = new List<(int, string)>();
            foreach (string card in cards)
            {
                myCards.Add(parseCard(card));
            }

            return myCards;
        }

        static (int, string) parseCard(string card)
        {
            var value = "";
            var suit = "";
            foreach (char c in card)
            {
                if (Char.IsDigit(c))
                {
                    value += c;
                }
                else
                {
                    suit += c;
                }
            }

            return (int.Parse(value), suit);
        }
    }
}
