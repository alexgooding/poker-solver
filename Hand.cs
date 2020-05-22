using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PokerSolver
{
    class Hand : List<Card>
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
        public void addCard(Card card)     
        {
            cards.Add(card);
            cards = cards.OrderByDescending(o => o.Value).ToList();
        }
        public void addCards(Hand secondHand)
        {
            foreach (Card card in secondHand.cards)
            {
                addCard(card);
            }
        }
        public int count()
        {
            return cards.Count;
        }
        public Hand getHighestNCards(int n)
        {
            Hand newHand = new Hand();
            for (int i = 0; i < n; i++)
            {
                // If the number of cards requested does not exist, just return as many that do
                try
                {
                    newHand.addCard(cards[i]);
                }
                catch (ArgumentOutOfRangeException)
                {
                    break;
                }
            }

            return newHand;
        }
        private Hand findCardsByValue(int value)
        {
            Hand valueCards = new Hand();

            foreach (Card card in cards)
            {
                if (card.Value == value)
                {
                    valueCards.addCard(card);
                }
            }

            return valueCards;
        }
        private Dictionary<int, Hand> countByValue()
        {
            Dictionary<int, Hand> valueCount = new Dictionary<int, Hand>();
            foreach (Card card in cards)
            {
                try
                {
                    valueCount.Add(card.Value, new Hand());
                    valueCount[card.Value].addCard(card);
                }
                catch (ArgumentException)
                {
                    valueCount[card.Value].addCard(card);
                }
            }

            return valueCount;
        }

        public (Hand, Hand) findFour()
        {
            Dictionary<int, Hand> valueCount = countByValue();

            Hand fourHand = new Hand();
            Hand kickerHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == 4)
                {
                    fourHand.addCards(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.addCards(element.Value);
                }
            }

            if (fourHand.count() != 4)
            {
                return (null, null);
            }
            else
            {
                return (fourHand, kickerHand.getHighestNCards(1));
            }
        }
        public (Hand, Hand) findFullHouse()
        {
            Dictionary<int, Hand> valueCount = countByValue();

            if (cards.Count < maxHandSize)
            {
                return (null, null);
            }

            Hand tripleHand = new Hand();
            Hand kickerHand = null;

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == 3)
                {
                    tripleHand.addCards(element.Value);
                    // Remove the element from valueCount to make finding the pair easier
                    valueCount.Remove(element.Key);
                }
            }

            if (tripleHand.count() == 0)
            {
                return (null, null);
            }
            if (tripleHand.count() == 6)
            {
                tripleHand = tripleHand.getHighestNCards(maxHandSize);
            }

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == 2)
                {
                    tripleHand.addCards(element.Value);
                }
            }

            if (tripleHand.count() >= maxHandSize)
            {
                tripleHand = tripleHand.getHighestNCards(maxHandSize);
            }

            return (tripleHand, kickerHand);
        }

        public (Hand, Hand) findFlush()
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
                        clubCards.addCard(card);
                        break;
                    case "d":
                        diamondCards.addCard(card);
                        break;
                    case "h":
                        heartCards.addCard(card);
                        break;
                    case "s":
                        spadeCards.addCard(card);
                        break;
                }
            }
            if (clubCards.count() >= maxHandSize)
            {
                flushHand = clubCards.getHighestNCards(maxHandSize);
            } 
            else if (diamondCards.count() >= maxHandSize)
            {
                flushHand = diamondCards.getHighestNCards(maxHandSize);
            }
            else if (heartCards.count() >= maxHandSize)
            {
                flushHand = heartCards.getHighestNCards(maxHandSize);
            }
            else if (spadeCards.count() >= maxHandSize)
            {
                flushHand = spadeCards.getHighestNCards(maxHandSize);
            }
            else
            {
                return (null, kickerHand);
            }

            return (flushHand, kickerHand);
        }
        public (Hand, Hand) findStraight()
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
                if (straightHand.count() == maxHandSize)
                {
                    break;
                }
                else if (card.Value == previousValue - 1)
                {
                    straightHand.addCard(card);
                    previousValue -= 1;
                }
                else if (card.Value < previousValue - 1) {
                    straightHand = new Hand();
                    straightHand.addCard(card);
                    previousValue = card.Value;
                }
            }

            if (straightHand.count() < maxHandSize)
            {
                return (null, null);
            }
            else
            {
                return (straightHand, kickerHand);
            }
        }

        public (Hand, Hand) findTriple()
        {
            Dictionary<int, Hand> valueCount = countByValue();

            Hand tripleHand = new Hand();
            Hand kickerHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == 3)
                {
                    tripleHand.addCards(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.addCards(element.Value);
                }
            }

            if (tripleHand.count() != 3)
            {
                return (null, null);
            }
            else
            {
                return (tripleHand, kickerHand.getHighestNCards(2));
            }
        }

        public (Hand, Hand) findTwoPair()
        {
            Dictionary<int, Hand> valueCount = countByValue();

            Hand pairHand = new Hand();
            Hand kickerHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == 2)
                {
                    pairHand.addCards(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.addCards(element.Value);
                }
            }

            if (pairHand.count() != 4)
            {
                return (null, null);
            }
            else
            {
                return (pairHand, kickerHand.getHighestNCards(1));
            }
        }

        public (Hand, Hand) findPair()
        {
            Dictionary<int, Hand> valueCount = countByValue();

            Hand pairHand = new Hand();
            Hand kickerHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == 2)
                {
                    pairHand.addCards(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.addCards(element.Value);
                }
            }

            if (pairHand.count() != 2)
            {
                return (null, null);
            }
            else
            {
                return (pairHand, kickerHand.getHighestNCards(3));
            }
        }

        public (Hand, Hand) findHighCard()
        {
            Hand highestCards = getHighestNCards(maxHandSize);

            Hand highCard = new Hand();
            highCard.addCard(highestCards.cards[0]);
            highestCards.cards.RemoveAt(0);
            Hand kickerHand = new Hand(highestCards.cards);

            return (highCard, kickerHand);
        }


    }
}
