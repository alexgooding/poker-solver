using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PokerSolver
{
    class Hand : List<Card>
    {
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
        public void mergeHands(Hand secondHand)
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
                newHand.addCard(cards[i]);
            }

            return newHand;
        }

        public (Hand, Hand) findFlush()
        {
            if (cards.Count < 5)
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
            if (clubCards.count() >= 5)
            {
                flushHand = clubCards.getHighestNCards(5);
            } 
            else if (diamondCards.count() >= 5)
            {
                flushHand = diamondCards.getHighestNCards(5);
            }
            else if (heartCards.count() >= 5)
            {
                flushHand = heartCards.getHighestNCards(5);
            }
            else if (spadeCards.count() >= 5)
            {
                flushHand = spadeCards.getHighestNCards(5);
            }
            else
            {
                return (null, kickerHand);
            }

            return (flushHand, kickerHand);
        }

        public Dictionary<int, Hand> countByValue()
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

        public (Hand, Hand) findTwoPair()
        {
            Dictionary<int, Hand> valueCount = countByValue();

            Hand pairHand = new Hand();
            Hand kickerHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == 2)
                {
                    pairHand.mergeHands(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.mergeHands(element.Value);
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
                    pairHand.mergeHands(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.mergeHands(element.Value);
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


    }
}
