using System;
using System.Collections.Generic;
using System.Globalization;

namespace PokerSolver
{
    class ParseCards
    {
        public static Hand parseCards(string[] cards)
        {
            Hand myCards = new Hand();
            foreach (string card in cards)
            {
                myCards.addCard(parseCard(card));
            }

            return myCards;
        }

        private static Card parseCard(string card)
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
            Card parsedCard = new Card(int.Parse(value), suit);

            return parsedCard;
        }
    }
}
