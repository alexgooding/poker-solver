using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PokerSolver
{
    class Hand : List<Card>
    {
        private List<Card> hand;
        public Hand(List<Card> hand)
        {
            this.hand = hand.OrderByDescending(o => o.Value).ToList();
        }
        public Hand()
        {
            this.hand = new List<Card>();
        }
        public void addCard(Card card)     
        {
            hand.Add(card);
            hand = hand.OrderByDescending(o => o.Value).ToList();
        }
        public void mergeHands(Hand secondHand)
        {
            foreach (Card card in secondHand)
            {
                hand.Add(card);
            }
            hand = hand.OrderByDescending(o => o.Value).ToList();
        }
        public int count()
        {
            return hand.Count;
        }
        public Hand getHighestNCards(int n)
        {
            Hand newHand = new Hand();
            for (int i = 0; i < n; i++)
            {
                newHand.addCard(hand[i]);
            }

            return newHand;
        }

        public Hand isFlush()
        {
            if (hand.Count < 5)
            {
                return null;
            }

            Hand clubCards = new Hand();
            Hand diamondCards = new Hand();
            Hand heartCards = new Hand();
            Hand spadeCards = new Hand();

            foreach (Card card in hand)
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
                return clubCards.getHighestNCards(5);
            } 
            else if (diamondCards.count() >= 5)
            {
                return diamondCards.getHighestNCards(5);
            }
            else if (heartCards.count() >= 5)
            {
                return heartCards.getHighestNCards(5);
            }
            else if (spadeCards.count() >= 5)
            {
                return spadeCards.getHighestNCards(5);
            }
            else
            {
                return null;
            }
        }

        public Dictionary<int, Hand> countByValue()
        {
            Dictionary<int, Hand> valueCount = new Dictionary<int, Hand>();
            foreach (Card card in hand)
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
    }
}
