using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerSolver
{
    public class Hand : List<Card>
    {
        public const int maxCardsInPlay = 7;
        public const int maxHandSize = 5;

        private List<Card> cards;
        public Hand(List<Card> cards)
        {
            this.cards = cards.OrderByDescending(o => o.Value).ToList();
        }
        public Hand()
        {
            this.cards = new List<Card>();
        }
        public List<Card> GetCards()
        {
            return cards;
        }
        public void AddCard(Card card)     
        {
            cards.Add(card);
            cards = cards.OrderByDescending(o => o.Value).ToList();
        }
        public void AddCards(Hand secondHand)
        {
            foreach (Card card in secondHand.cards)
            {
                AddCard(card);
            }
        }
        public int CardCount()
        {
            return cards.Count;
        }
        public static bool IsEqual(Hand firstHand, Hand secondHand)
        {
            if (firstHand == null && secondHand == null)
            {
                return true;
            }
            else if ((firstHand == null && secondHand != null) || (secondHand == null && firstHand != null))
            {
                return false;
            }
            else if (firstHand.CardCount() != secondHand.CardCount())
            {
                return false;
            }
            else
            {
                List<Card> firstHandCards = firstHand.GetCards();
                List<Card> secondHandCards = secondHand.GetCards();
                for (int i = 0; i < firstHandCards.Count; i++)
                {
                    if ((firstHandCards[i].Value != secondHandCards[i].Value) || firstHandCards[i].Suit != secondHandCards[i].Suit)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        private Hand GetHighestNCards(int n)
        {
            Hand newHand = new Hand();
            for (int i = 0; i < n; i++)
            {
                // If the number of cards requested does not exist, just return as many that do
                try
                {
                    newHand.AddCard(cards[i]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

            return newHand;
        }

        private Hand GetLowestNCards(int n)
        {
            Hand newHand = new Hand();
            for (int i = cards.Count-1; i >= cards.Count-n; i--)
            {
                // If the number of cards requested does not exist, just return as many that do
                try
                {
                    newHand.AddCard(cards[i]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

            return newHand;
        }
        private Hand FindCardsByValue(int value)
        {
            Hand valueCards = new Hand();

            foreach (Card card in cards)
            {
                if (card.Value == value)
                {
                    valueCards.AddCard(card);
                }
            }

            return valueCards;
        }
        private Dictionary<int, Hand> CountByValue()
        {
            Dictionary<int, Hand> valueCount = new Dictionary<int, Hand>();
            foreach (Card card in cards)
            {
                try
                {
                    valueCount.Add(card.Value, new Hand());
                    valueCount[card.Value].AddCard(card);
                }
                catch (ArgumentException)
                {
                    valueCount[card.Value].AddCard(card);
                }
            }

            return valueCount;
        }

        private SortedHand FindMatchingValueHands(int numberOfSameValueCards)
        {
            if (cards.Count < numberOfSameValueCards)
            {
                return new SortedHand();
            }

            Dictionary<int, Hand> valueCount = CountByValue();

            Hand mainHand = new Hand();
            Hand kickerHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.CardCount() == numberOfSameValueCards)
                {
                    mainHand.AddCards(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.AddCards(element.Value);
                }
            }

            if (mainHand.CardCount() != numberOfSameValueCards)
            {
                return new SortedHand();
            }
            else
            {
                return new SortedHand(mainHand, kickerHand.GetHighestNCards(maxHandSize - numberOfSameValueCards));
            }
        }

        private SortedHand FindRoyalFlush()
        {
            SortedHand straightFlushHand = FindStraightFlush();
            if (straightFlushHand.MainHand == null)
            {
                return new SortedHand();
            }
            else
            {
                if (straightFlushHand.MainHand.FindCardsByValue(14).CardCount() == 1)
                {
                    return straightFlushHand;
                }
                else
                {
                    return new SortedHand();
                }
            }
        }

        private SortedHand FindStraightFlush()
        {
            SortedHand flushHand = FindFlush();
            if (flushHand.MainHand == null)
            {
                return new SortedHand();
            }
            else
            {
                return flushHand.MainHand.FindStraight();
            }
        }

        private SortedHand FindFour()
        {
            return FindMatchingValueHands(4);
        }
        private SortedHand FindFullHouse()
        {
            Dictionary<int, Hand> valueCount = CountByValue();

            if (cards.Count < maxHandSize)
            {
                return new SortedHand();
            }

            Hand tripleHand = new Hand();
            Hand pairHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.CardCount() == 3)
                {
                    tripleHand.AddCards(element.Value);
                    // Remove the element from valueCount to make finding the pair easier
                    valueCount.Remove(element.Key);
                }
            }

            if (tripleHand.CardCount() == 0)
            {
                return new SortedHand();
            }
            else if (tripleHand.CardCount() == 6)
            {
                pairHand.AddCards(new Hand(tripleHand.GetCards().GetRange(3, 2)));
                tripleHand = tripleHand.GetHighestNCards(3);
            }
            else
            {
                foreach (KeyValuePair<int, Hand> element in valueCount)
                {
                    if (element.Value.CardCount() == 2)
                    {
                        pairHand.AddCards(element.Value);
                    }
                }

                if (tripleHand.CardCount() + pairHand.CardCount() >= maxHandSize)
                {
                    pairHand = pairHand.GetHighestNCards(2);
                }
                else
                {
                    return new SortedHand();
                }
            }   

            // Return pair hand in place of kicker hand for comparison purposes
            return new SortedHand(tripleHand, pairHand);
        }

        private SortedHand FindFlush()
        {
            if (cards.Count < maxHandSize)
            {
                return new SortedHand();
            }

            Hand clubCards = new Hand();
            Hand diamondCards = new Hand();
            Hand heartCards = new Hand();
            Hand spadeCards = new Hand();

            Hand flushHand = new Hand();

            foreach (Card card in cards)
            {
                switch(card.Suit)
                {
                    case "c":
                        clubCards.AddCard(card);
                        break;
                    case "d":
                        diamondCards.AddCard(card);
                        break;
                    case "h":
                        heartCards.AddCard(card);
                        break;
                    case "s":
                        spadeCards.AddCard(card);
                        break;
                }
            }
            if (clubCards.CardCount() >= maxHandSize)
            {
                flushHand = clubCards.GetHighestNCards(maxHandSize);
            } 
            else if (diamondCards.CardCount() >= maxHandSize)
            {
                flushHand = diamondCards.GetHighestNCards(maxHandSize);
            }
            else if (heartCards.CardCount() >= maxHandSize)
            {
                flushHand = heartCards.GetHighestNCards(maxHandSize);
            }
            else if (spadeCards.CardCount() >= maxHandSize)
            {
                flushHand = spadeCards.GetHighestNCards(maxHandSize);
            }
            else
            {
                return new SortedHand();
            }

            return new SortedHand(flushHand, null);
        }
        private SortedHand FindStraight()
        {
            if (cards.Count < maxHandSize)
            {
                return new SortedHand();
            }

            Hand straightHand = new Hand();
            Hand kickerHand = null;

            int previousValue = cards[0].Value + 1;

            // Treat aces as low and high
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i].Value == 14)
                {
                    cards.Add(new Card(1, cards[i].Suit));
                }
            }
            
            foreach (Card card in cards)
            {
                if (straightHand.CardCount() == maxHandSize)
                {
                    break;
                }
                else if (card.Value == previousValue - 1)
                {
                    straightHand.AddCard(card);
                    previousValue -= 1;
                }
                else if (card.Value < previousValue - 1) {
                    straightHand = new Hand();
                    straightHand.AddCard(card);
                    previousValue = card.Value;
                }
            }

            // Remove any ace lows added to cards
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                if (cards[i].Value == 1)
                {
                    cards.RemoveAt(i);
                }
            }

            if (straightHand.CardCount() < maxHandSize)
            {
                return new SortedHand();
            }
            else
            {
                return new SortedHand(straightHand, kickerHand);
            }
        }

        private SortedHand FindTriple()
        {
            return FindMatchingValueHands(3);
        }

        private SortedHand FindTwoPair()
        {
            if (cards.Count < 4)
            {
                return new SortedHand();
            }

            Dictionary<int, Hand> valueCount = CountByValue();

            Hand mainHand = new Hand();
            Hand kickerHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.CardCount() == 2)
                {
                    mainHand.AddCards(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.AddCards(element.Value);
                }
            }

            if (mainHand.CardCount() < 4)
            {
                return new SortedHand();
            }
            else if (mainHand.CardCount() == 4)
            {
                return new SortedHand(mainHand, kickerHand.GetHighestNCards(1));
            }
            else
            {
                kickerHand.AddCards(GetLowestNCards(mainHand.CardCount() - 4));
                mainHand = mainHand.GetHighestNCards(4);

                return new SortedHand(mainHand, kickerHand.GetHighestNCards(1));
            }
        }

        private SortedHand FindPair()
        {
            return FindMatchingValueHands(2);
        }

        private SortedHand FindHighCard()
        {
            Hand highestCards = GetHighestNCards(maxHandSize);

            Hand highCard = new Hand();
            highCard.AddCard(highestCards.cards[0]);
            highestCards.cards.RemoveAt(0);
            Hand kickerHand = new Hand(highestCards.cards);

            return new SortedHand(highCard, kickerHand);
        }

        public SortedHand FindBestHand()
        {
            var handCheckingMethods = new List<Func<SortedHand>> { FindRoyalFlush, FindStraightFlush, FindFour, 
                FindFullHouse, FindFlush, FindStraight, FindTriple, FindTwoPair, FindPair, FindHighCard };

            SortedHand bestHand = new SortedHand();

            foreach (var method in handCheckingMethods)
            {
                bestHand = method();
                if (bestHand.MainHand != null)
                {
                    break;
                }
            }

            return bestHand;
        }
    }
}
