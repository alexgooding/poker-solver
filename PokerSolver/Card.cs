using System.ComponentModel.DataAnnotations;
using static PokerSolver.Constants;

namespace PokerSolver
{
    public class Card
    {
        public int Value { get; set; }
        public Suit Suit { get; set; }

        public Card(int value, Suit suit)
        {
            this.Value = value;
            this.Suit = suit;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Card;

            if (item == null)
            {
                return false;
            }

            if (this.Value == item.Value && this.Suit == item.Suit)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
