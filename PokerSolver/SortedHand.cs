using System;
using static PokerSolver.Constants;

namespace PokerSolver
{
    public class SortedHand
    {
        public SortedHand(Hand mainHand, Hand kickerHand)
        {
            this.MainHand = mainHand;
            this.KickerHand = kickerHand;
        }
        public SortedHand()
        {
            this.MainHand = null;
            this.KickerHand = null;
        }
        public Hand MainHand { get; set; }
        public Hand KickerHand { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as SortedHand;

            if (item == null)
            {
                return false;
            }

            if (Hand.IsEqual(this.MainHand, item.MainHand) && Hand.IsEqual(this.KickerHand, item.KickerHand))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool? IsBetterThanHand(SortedHand secondHand)
        // Compare the main hand and kicker hand of two hands of the same type (e.g. flush, pair, etc...)
        {
            for (int i = 0; i < MainHand.CardCount(); i++)
            {
                if (MainHand.GetCards()[i].Value > secondHand.MainHand.GetCards()[i].Value)
                {
                    return true;
                }
                else if (MainHand.GetCards()[i].Value < secondHand.MainHand.GetCards()[i].Value)
                {
                    return false;
                }
            }

            if (KickerHand != null)
            {
                for (int i = 0; i < KickerHand.CardCount(); i++)
                {
                    if (KickerHand.GetCards()[i].Value > secondHand.KickerHand.GetCards()[i].Value)
                    {
                        return true;
                    }
                    else if (KickerHand.GetCards()[i].Value < secondHand.KickerHand.GetCards()[i].Value)
                    {
                        return false;
                    }
                }
            }

            return null;
        }

        public void PrintSortedHand()
        {
            string sortedHandString = "(";
            if (MainHand != null && MainHand.CardCount() != 0)
            {
                foreach (Card card in MainHand.GetCards())
                {
                    sortedHandString += String.Format("{0}{1}, ", FriendlyValueNames[card.Value], Constants.FriendlySuitNames[card.Suit]);
                }
                sortedHandString = sortedHandString.Remove(sortedHandString.Length - 2);
            }         
            sortedHandString += "), (";
            if (KickerHand != null && KickerHand.CardCount() != 0)
            {
                foreach (Card card in KickerHand.GetCards())
                {
                    sortedHandString += String.Format("{0}{1}, ", FriendlyValueNames[card.Value], Constants.FriendlySuitNames[card.Suit]);
                }
                sortedHandString = sortedHandString.Remove(sortedHandString.Length - 2);
            }
            sortedHandString += ")";

            Console.Write(sortedHandString);
        }
    }
   
}
