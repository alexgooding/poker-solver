using System.ComponentModel.DataAnnotations;
using static PokerSolver.Constants;

namespace PokerSolver
{
    public class Card
    {
        [Required, RangeAttribute(2, 14)]
        public int Value { get; set; }
        public Suit Suit { get; set; }

        public Card(int value, Suit suit)
        {
            this.Value = value;
            this.Suit = suit;
        }
    }
}
