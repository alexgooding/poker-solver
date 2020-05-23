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

        private (Hand, Hand) FindMatchingValueHands(int numberOfSameValueCards)
        {
            if (cards.Count < numberOfSameValueCards)
            {
                return (null, null);
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
                return (null, null);
            }
            else
            {
                return (mainHand, kickerHand.GetHighestNCards(maxHandSize - numberOfSameValueCards));
            }
        }

        private (Hand, Hand) FindRoyalFlush()
        {
            (Hand, Hand) straightFlushHand = FindStraightFlush();
            if (straightFlushHand.Item1 == null)
            {
                return (null, null);
            }
            else
            {
                if (straightFlushHand.Item1.FindCardsByValue(14).CardCount() == 1)
                {
                    return straightFlushHand;
                }
                else
                {
                    return (null, null);
                }
            }
        }

        private (Hand, Hand) FindStraightFlush()
        {
            (Hand, Hand) flushHand = FindFlush();
            if (flushHand.Item1 == null)
            {
                return (null, null);
            }
            else
            {
                return flushHand.Item1.FindStraight();
            }
        }

        private (Hand, Hand) FindFour()
        {
            return FindMatchingValueHands(4);
        }
        private (Hand, Hand) FindFullHouse()
        {
            Dictionary<int, Hand> valueCount = CountByValue();

            if (cards.Count < maxHandSize)
            {
                return (null, null);
            }

            Hand tripleHand = new Hand();
            Hand pairHand = new Hand();
            Hand fullHouseHand = new Hand();
            Hand kickerHand = null;

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
                return (null, null);
            }
            else if (tripleHand.CardCount() == 6)
            {
                fullHouseHand = tripleHand.GetHighestNCards(maxHandSize);
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
                    fullHouseHand.AddCards(tripleHand);
                    fullHouseHand.AddCards(pairHand);
                }
                else
                {
                    return (null, null);
                }
            }   

            return (fullHouseHand, kickerHand);
        }

        private (Hand, Hand) FindFlush()
        {
            if (cards.Count < maxHandSize)
            {
                return (null, null);
            }

            Hand clubCards = new Hand();
            Hand diamondCards = new Hand();
            Hand heartCards = new Hand();
            Hand spadeCards = new Hand();

            Hand flushHand = new Hand();
            Hand kickerHand = null;

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
                return (null, kickerHand);
            }

            return (flushHand, kickerHand);
        }
        private (Hand, Hand) FindStraight()
        {
            if (cards.Count < maxHandSize)
            {
                return (null, null);
            }

            Hand straightHand = new Hand();
            Hand kickerHand = null;

            int previousValue = cards[0].Value + 1;

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

            if (straightHand.CardCount() < maxHandSize)
            {
                return (null, null);
            }
            else
            {
                return (straightHand, kickerHand);
            }
        }

        private (Hand, Hand) FindTriple()
        {
            return FindMatchingValueHands(3);
        }

        private (Hand, Hand) FindTwoPair()
        {
            if (cards.Count < 4)
            {
                return (null, null);
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
                return (null, null);
            }
            else if (mainHand.CardCount() == 4)
            {
                return (mainHand, kickerHand.GetHighestNCards(1));
            }
            else
            {
                kickerHand.AddCards(GetLowestNCards(mainHand.CardCount() - 4));
                mainHand = mainHand.GetHighestNCards(4);

                return (mainHand, kickerHand.GetHighestNCards(1));
            }
        }

        private (Hand, Hand) FindPair()
        {
            return FindMatchingValueHands(2);
        }

        private (Hand, Hand) FindHighCard()
        {
            Hand highestCards = GetHighestNCards(maxHandSize);

            Hand highCard = new Hand();
            highCard.AddCard(highestCards.cards[0]);
            highestCards.cards.RemoveAt(0);
            Hand kickerHand = new Hand(highestCards.cards);

            return (highCard, kickerHand);
        }

        public (Hand, Hand) FindBestHand()
        {
            var handCheckingMethods = new List<Func<(Hand, Hand)>> { FindRoyalFlush, FindStraightFlush, FindFour, 
                FindFullHouse, FindFlush, FindStraight, FindTriple, FindTwoPair, FindPair, FindHighCard };

            (Hand, Hand) bestHand = (null, null);

            foreach (var method in handCheckingMethods)
            {
                bestHand = method();
                if (bestHand.Item1 != null)
                {
                    break;
                }
            }

            return bestHand;
        }

    }
}
