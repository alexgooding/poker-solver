using System;
using System.Collections.Generic;
using System.Text;

namespace PokerSolver
{
    class Hand
    {
        private List<Card> hand;
        public Hand(List<Card> hand)
        {
            this.hand = hand;
        }
        public Hand()
        {
            this.hand = new List<Card>();
        }
        public void addCard(Card card)
        {
            hand.Add(card);
        }
        public int count()
        {
            return hand.Count;
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
                return clubCards;
            } 
            else if (diamondCards.count() >= 5)
            {
                return diamondCards;
            }
            else if (heartCards.count() >= 5)
            {
                return heartCards;
            }
            else if (spadeCards.count() >= 5)
            {
                return spadeCards;
            }
            else
            {
                return null;
            }


        }
    }
}
