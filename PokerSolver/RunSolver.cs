using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using static PokerSolver.Constants;

namespace PokerSolver
{
    public class DuplicateCardException : Exception { }

    class RunSolver
    {
        Hand myHand;
        Hand communityHand;
        Hand newCards;
        Dictionary<HandType, List<SortedHand>> allPossibleHandsSorted;
        List<Hand> allPossibleHands;

        public void RunMainWorkflow(int numberOfPlayers)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (true)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("New hand");
                Console.WriteLine("----------------------------------------------");

                // Initialise all possible hands with all possible two card hands
                allPossibleHands = Hand.GenerateAllTwoCardHands();

                myHand = new Hand();
                newCards = new Hand();

                // Deal
                runRound("Enter the two cards in your hand (e.g. 5d 7h)", 2);

                // Flop
                runRound("Enter the first three community cards", 3);

                // Turn
                runRound("Enter the fourth community card", 1);

                // River
                runRound("Enter the fifth community card", 1);
            }
        }

        private void runRound(string userInputMessage, int numberOfCardsToInput)
        {
            int handRank;

            string[] line;
            var successfulInput = false;
            (SortedHand, HandType) bestHand;

            while (!successfulInput)
            {
                Console.WriteLine(userInputMessage);
                line = Console.ReadLine().Split(null);
                try
                {
                    if (line.Length != numberOfCardsToInput)
                    {
                        throw new FormatException();
                    }
                    // Hand cards are already present in allPossibleHands in the first round so no need to add again
                    if (numberOfCardsToInput == 2)
                    {
                        Hand myNewCards = ParseCards.parseCards(line);
                        if (myNewCards.AreDuplicateCards())
                        {
                            throw new DuplicateCardException();
                        } 
                        else
                        {
                            myHand.AddCards(myNewCards);
                        }
                    }
                    else
                    {
                        newCards = ParseCards.parseCards(line);
                        if (Hand.DoShareCards(newCards, myHand) || newCards.AreDuplicateCards())
                        {
                            throw new DuplicateCardException();
                        }
                        else
                        {
                            myHand.AddCards(newCards);
                        }
                    }
                    successfulInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect number of cards entered.");
                }
                catch (DuplicateCardException)
                {
                    Console.WriteLine("An inputted card is already in your hand");
                }
                catch (Exception)
                {
                    successfulInput = false;
                }
            }

            bestHand = myHand.FindBestHand();
            Console.WriteLine("Your best hand is a " + FriendlyHandTypes[bestHand.Item2] + ":");
            bestHand.Item1.PrintSortedHand();

            allPossibleHandsSorted = GenerateAllPossibleHandsSorted(newCards);

            handRank = DetermineRankOfHand(bestHand);
            Console.WriteLine("Your best hand is rank " + handRank.ToString() + " out of " + allPossibleHands.Count.ToString() + ", which is in the top " + string.Format("{0:0.00}", handRank * 100.0 / allPossibleHands.Count) + "%.");
        }

        private Dictionary<HandType, List<SortedHand>> GenerateAllPossibleHandsSorted(Hand newCards)
        {
            Dictionary<HandType, List<SortedHand>> allPossibleHandsSorted = new Dictionary<HandType, List<SortedHand>>();
            for (int i = allPossibleHands.Count - 1; i >= 0; i--)
            {
                if (newCards != null)
                {
                    if (!Hand.DoShareCards(allPossibleHands[i], newCards))
                    {
                        allPossibleHands[i].AddCards(newCards);
                    }
                    else
                    {
                        allPossibleHands.RemoveAt(i);
                        continue;
                    }
                }
                (SortedHand, HandType) bestHand = allPossibleHands[i].FindBestHand();

                try
                {
                    allPossibleHandsSorted.Add(bestHand.Item2, new List<SortedHand> { bestHand.Item1 });
                }
                catch (ArgumentException)
                {
                    allPossibleHandsSorted[bestHand.Item2].Add(bestHand.Item1);
                }
            }

            return allPossibleHandsSorted;
        }
        
        private List<SortedHand> SortHandsInDescendingRank(List<SortedHand> hands)
        {
            SortedHand nextHand;

            for (int i = 0; i <= hands.Count - 2; i++)
            {
                for (int j = 0; j <= hands.Count - 2; j++)
                {
                    if (hands[j + 1].IsBetterThanHand(hands[j]) == true)
                    {
                        nextHand = hands[j + 1];
                        hands[j + 1] = hands[j];
                        hands[j] = nextHand;
                    }
                }
            }

            return hands;
        }

        private int DetermineRankOfHand((SortedHand, HandType) myHand)
        {
            int rank = 1;
            foreach (HandType handType in (HandType[])Enum.GetValues(typeof(HandType)))
            {
                try 
                {
                    if (myHand.Item2.Equals(handType))
                    {
                        foreach (SortedHand possibleHand in SortHandsInDescendingRank(allPossibleHandsSorted[handType]))
                        {
                            if (myHand.Item1.Equals(possibleHand))
                            {
                                return rank;
                            }
                            else
                            {
                                rank++;
                            }
                        }
                    }
                    else
                    {
                        rank += allPossibleHandsSorted[handType].Count;
                    }
                }
                catch (KeyNotFoundException)
                {
                    continue;
                }
            }

            return rank;
        }
    }
}
