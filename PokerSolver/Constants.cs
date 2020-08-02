
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

        public static Dictionary<Suit, string> FriendlySuitNames = new Dictionary<Suit, string>
        {
            { Suit.Clubs, "♣" },
            { Suit.Diamonds, "♦" },
            { Suit.Hearts, "♥" },
            { Suit.Spades, "♠" }
        };
    }
}
