using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static PokerSolver.Constants;

namespace PokerSolver
{
    public class ParseCards
    {
        public static Hand parseCards(string[] cards)
        {
            Hand myCards = new Hand();
            foreach (string card in cards)
            {
                try
                {
                    myCards.AddCard(ParseCard(card));
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }

            return myCards;
        }

        private static Card ParseCard(string card)
        {
            try
            {
                var value = "";
                Suit suit = Suit.Hearts;
                foreach (char c in card)
                {
                    if (Char.IsDigit(c))
                    {
                        value += c;
                    }
                    else
                    {
                        switch (c)
                        {
                            case 'H':
                            case 'h':
                            case '♥':
                                suit = Suit.Hearts;
                                break;
                            case 'D':
                            case 'd':
                            case '♦':
                                suit = Suit.Diamonds;
                                break;
                            case 'S':
                            case 's':
                            case '♠':
                                suit = Suit.Spades;
                                break;
                            case 'C':
                            case 'c':
                            case '♣':
                                suit = Suit.Clubs;
                                break;
                            case 'J':
                            case 'j':
                                value += "11";
                                break;
                            case 'Q':
                            case 'q':
                                value += "12";
                                break;
                            case 'K':
                            case 'k':
                                value += "13";
                                break;
                            case 'A':
                            case 'a':
                                value += "14";
                                break;
                            default:
                                throw new ArgumentException();
                        }
                    }
                }
                int parsedValue = int.Parse(value);
                if (parsedValue >= 2 && parsedValue <= 14)
                {
                    return new Card(parsedValue, suit);
                }
                else
                {
                    throw new ValidationException();
                }
            }
            catch (ValidationException)
            {
                Console.WriteLine("Invalid card value entered.");
                throw new Exception();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid suit entered.");
                throw new Exception();
            }
        }
    }
}
