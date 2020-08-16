using System;
using System.Collections.Generic;

namespace PokerSolver
{
    public static class Constants
    {
        public enum Suit
        {
            Clubs,
            Diamonds,
            Hearts,
            Spades
        }

        public enum HandType
        {
            RoyalFlush,
            StraightFlush,
            FourOfAKind,
            FullHouse,
            Flush,
            Straight,
            ThreeOfAKind,
            TwoPair,
            Pair,
            HighCard
        }

        public static Dictionary<int, string> FriendlyValueNames = new Dictionary<int, string>
        {
            { 1, "A" },
            { 2, "2" },
            { 3, "3" },
            { 4, "4" },
            { 5, "5" },
            { 6, "6" },
            { 7, "7" },
            { 8, "8" },
            { 9, "9" },
            { 10, "10" },
            { 11, "J" },
            { 12, "Q" },
            { 13, "K" },
            { 14, "A" }
        };

        public static Dictionary<Suit, string> FriendlySuitNames = new Dictionary<Suit, string>
        {
            { Suit.Clubs, "♣" },
            { Suit.Diamonds, "♦" },
            { Suit.Hearts, "♥" },
            { Suit.Spades, "♠" }
        };

        public static Dictionary<HandType, string> FriendlyHandTypes = new Dictionary<HandType, string>
        {
            { HandType.RoyalFlush, "Royal Flush" },
            { HandType.StraightFlush, "Straight Flush" },
            { HandType.FourOfAKind, "Four of a Kind" },
            { HandType.FullHouse, "Full House" },
            { HandType.Flush, "Flush" },
            { HandType.Straight, "Straight" },
            { HandType.ThreeOfAKind, "Three of a Kind" },
            { HandType.TwoPair, "Two Pair" },
            { HandType.Pair, "Pair" },
            { HandType.HighCard, "High Card" }
        };
    }
}
