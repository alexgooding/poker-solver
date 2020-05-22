﻿using System;
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
        public List<Card> getCards()
        {
            return cards;
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

        public Hand getLowestNCards(int n)
        {
            Hand newHand = new Hand();
            for (int i = cards.Count-1; i >= cards.Count-n; i--)
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

        private (Hand, Hand) findMatchingValueHands(int numberOfSameValueCards)
        {
            if (cards.Count < numberOfSameValueCards)
            {
                return (null, null);
            }

            Dictionary<int, Hand> valueCount = countByValue();

            Hand mainHand = new Hand();
            Hand kickerHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == numberOfSameValueCards)
                {
                    mainHand.addCards(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.addCards(element.Value);
                }
            }

            if (mainHand.count() != numberOfSameValueCards)
            {
                return (null, null);
            }
            else
            {
                return (mainHand, kickerHand.getHighestNCards(maxHandSize - numberOfSameValueCards));
            }
        }

        public (Hand, Hand) findRoyalFlush()
        {
            (Hand, Hand) straightFlushHand = findStraightFlush();
            if (straightFlushHand.Item1 == null)
            {
                return (null, null);
            }
            else
            {
                if (straightFlushHand.Item1.findCardsByValue(14).count() == 1)
                {
                    return straightFlushHand;
                }
                else
                {
                    return (null, null);
                }
            }
        }

        public (Hand, Hand) findStraightFlush()
        {
            (Hand, Hand) flushHand = findFlush();
            if (flushHand.Item1 == null)
            {
                return (null, null);
            }
            else
            {
                return flushHand.Item1.findStraight();
            }
        }

        public (Hand, Hand) findFour()
        {
            return findMatchingValueHands(4);
        }
        public (Hand, Hand) findFullHouse()
        {
            Dictionary<int, Hand> valueCount = countByValue();

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
                fullHouseHand = tripleHand.getHighestNCards(maxHandSize);
            }

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == 2)
                {
                    pairHand.addCards(element.Value);
                }
            }

            if (tripleHand.count() + pairHand.count() >= maxHandSize)
            {
                pairHand = pairHand.getHighestNCards(2);
                fullHouseHand.addCards(tripleHand);
                fullHouseHand.addCards(pairHand);
            }

            return (fullHouseHand, kickerHand);
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
            return findMatchingValueHands(3);
        }

        public (Hand, Hand) findTwoPair()
        {
            if (cards.Count < 4)
            {
                return (null, null);
            }

            Dictionary<int, Hand> valueCount = countByValue();

            Hand mainHand = new Hand();
            Hand kickerHand = new Hand();

            foreach (KeyValuePair<int, Hand> element in valueCount)
            {
                if (element.Value.count() == 2)
                {
                    mainHand.addCards(element.Value);
                    // Remove the element from valueCount to make calculating the kicker hand easier
                    valueCount.Remove(element.Key);
                }
                else
                {
                    kickerHand.addCards(element.Value);
                }
            }

            if (mainHand.count() < 4)
            {
                return (null, null);
            }
            else if (mainHand.count() == 4)
            {
                return (mainHand, kickerHand.getHighestNCards(1));
            }
            else
            {
                kickerHand.addCards(getLowestNCards(mainHand.count() - 4));
                mainHand = mainHand.getHighestNCards(4);

                return (mainHand, kickerHand.getHighestNCards(1));
            }
        }

        public (Hand, Hand) findPair()
        {
            return findMatchingValueHands(2);
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

        public (Hand, Hand) findBestHand()
        {
            var handCheckingMethods = new List<Func<(Hand, Hand)>> { findRoyalFlush, findStraightFlush, findFour, 
                findFullHouse, findFlush, findStraight, findTriple, findTwoPair, findPair, findHighCard };

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
