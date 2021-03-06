﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static PokerSolver.Constants;

namespace PokerSolver
{
    public class DuplicateCardException : Exception { }

    class RunSolver
    {
        Hand myHand;
        Hand newCards;
        Dictionary<HandType, List<SortedHand>> allPossibleHandsSorted;
        List<Hand> allPossibleHands;
        (SortedHand, HandType) bestHand;

        public void RunMainWorkflow(int numberOfPlayers)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.BufferHeight = 1350;
            Console.SetWindowSize(Console.LargestWindowWidth - 20, Console.LargestWindowHeight - 10);
            Console.ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("New hand");
                Console.WriteLine("----------------------------------------------");

                // Initialise all possible hands with all possible two card hands
                allPossibleHands = Hand.GenerateAllTwoCardHands();

                myHand = new Hand();
                newCards = new Hand();

                bool hasFolded = false;

                // Deal
                hasFolded = RunRound("Enter the two cards in your hand (e.g. 5d 7h)", 2);

                // Flop
                if (!hasFolded)
                {
                    hasFolded = RunRound("Enter the first three community cards", 3);
                }
                
                // Turn
                if (!hasFolded)
                {
                    hasFolded = RunRound("Enter the fourth community card", 1);
                }
                
                // River
                if (!hasFolded)
                {
                    hasFolded = RunRound("Enter the fifth community card", 1);
                }

                // Final ranking
                if (!hasFolded)
                {
                    hasFolded = RunRound("Print the final hand ranking by pressing 'p' (you can press 'p' at any point to see the current hand ranking)", 0);
                }
            }
        }

        private bool RunRound(string userInputMessage, int numberOfCardsToInput)
        {
            bool hasFolded = false;
            int handRank;

            string[] line;
            var successfulInput = false;

            while (!successfulInput)
            {
                Console.WriteLine(userInputMessage);
                line = Console.ReadLine().Split(null);
                try
                {
                    // print hand ranking list if 'p' is pressed
                    if (line[0] == "p")
                    {
                        PrintHandRankingList();
                        if (numberOfCardsToInput == 0)
                        {
                            successfulInput = true;
                        }
                        continue;
                    }
                    else if (line[0] == "f")
                    {
                        hasFolded = true;
                        return hasFolded;
                    }
                    else if (line.Length != numberOfCardsToInput)
                    {
                        throw new FormatException();
                    }
                    else
                    {
                        // Parse new cards and check there are no duplicate cards being inputted
                        newCards = ParseCards.parseCards(line);
                        if (Hand.DoShareCards(newCards, myHand) || newCards.AreDuplicateCards())
                        {
                            throw new DuplicateCardException();
                        }
                        else
                        {
                            myHand.AddCards(newCards);
                        }
                        successfulInput = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect number of cards entered.");
                }
                catch (DuplicateCardException)
                {
                    Console.WriteLine("An inputted card is already in your hand.");
                }
                catch (Exception)
                {
                    successfulInput = false;
                }
            }

            if (numberOfCardsToInput != 0)
            {
                bestHand = myHand.FindBestHand();
                Console.WriteLine("Your best hand is a " + FriendlyHandTypes[bestHand.Item2] + ":");
                Console.WriteLine(bestHand.Item1.BuildSortedHandString());
                Console.WriteLine("");

                allPossibleHandsSorted = GenerateAllPossibleHandsSorted(newCards);
                int allPossibleHandsCount = (from item in allPossibleHandsSorted.Values select item.Count).Sum();

                handRank = DetermineRankOfHand(bestHand);
                Console.WriteLine(String.Format("You best hand is rank {0} out of {1}, which is in the top {2}%.", handRank, allPossibleHandsCount, string.Format("{0:0.00}", handRank * 100.0 / allPossibleHandsCount)));
            }

            return hasFolded;
        }

        private Dictionary<HandType, List<SortedHand>> GenerateAllPossibleHandsSorted(Hand newCards)
        {
            Dictionary<HandType, List<SortedHand>> allPossibleHandsSorted = new Dictionary<HandType, List<SortedHand>>();
            for (int i = allPossibleHands.Count - 1; i >= 0; i--)
            {
                if (newCards != null)
                {
                    // Don't add player hand cards to all possible hands, as they're already in there
                    // Only keep the exact player cards in all possible hands, as all other hands containing one of those cards are not possible for any players to have
                    if (newCards.GetCards().Count == 2)
                    {
                        if (Hand.IsEqual(allPossibleHands[i], newCards)) { }
                        else if (Hand.DoShareCards(allPossibleHands[i], newCards))
                        {
                            allPossibleHands.RemoveAt(i);
                            continue;
                        }
                    }
                    else
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
                }
                (SortedHand, HandType) bestHand = allPossibleHands[i].FindBestHand();

                try
                {
                    allPossibleHandsSorted.Add(bestHand.Item2, new List<SortedHand> { bestHand.Item1 });
                }
                catch (Exception)
                {
                    if (!allPossibleHandsSorted[bestHand.Item2].Any(x => x.Equals(bestHand.Item1)))
                    {
                        allPossibleHandsSorted[bestHand.Item2].Add(bestHand.Item1);
                    }
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

        private void PrintHandRankingList()
        {
            if (allPossibleHandsSorted == null)
            {
                Console.WriteLine("Please enter your hand first.");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("{0, 20}|{1,-26}| {2}", "", "Best hand", "Number of equivalent hands");
                Console.WriteLine("{0, 20}|{1,-26}|{2}", "--------------------", "--------------------------", "----------------------------");

                for (int i = 0; i < 10; i++)
                {
                    try
                    {
                        SortedHand previousHand = null;
                        int numberOfEquivalentStrengthHands = 1;
                        foreach (SortedHand hand in SortHandsInDescendingRank(allPossibleHandsSorted[(HandType)i]))
                        {
                            
                            if (previousHand != null)
                            {
                                if (hand.IsBetterThanHand(previousHand) == null)
                                {
                                    numberOfEquivalentStrengthHands++;
                                }
                                else
                                {
                                    printHandRankingRow(previousHand, numberOfEquivalentStrengthHands);
                                    numberOfEquivalentStrengthHands = 1;
                                }
                            }
                            
                            previousHand = hand;
                        }

                        printHandRankingRow(previousHand, numberOfEquivalentStrengthHands);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }

            Console.WriteLine("");
        }

        private void printHandRankingRow(SortedHand hand, int numberOfEquivalentStrengthHands)
        {
            StringBuilder builder = new StringBuilder();
            string bestHandIndicator = "";
            string sortedHandString = "";

            // Check to see if cards can be compared
            if (hand.MainHand.GetCards().Count == bestHand.Item1.MainHand.GetCards().Count)
            {
                if (hand.IsBetterThanHand(bestHand.Item1) == null)
                {
                    bestHandIndicator = "Your best hand -> ";
                    sortedHandString = bestHand.Item1.BuildSortedHandString();
                }
            }
            if (bestHandIndicator == "")
            {
                sortedHandString = hand.BuildSortedHandString();
            }
            builder.Append(String.Format("{0, 20}", bestHandIndicator));
            builder.Append(String.Format("|{0,-26}|", sortedHandString));
            builder.Append(String.Format(" {0}", numberOfEquivalentStrengthHands));

            Console.WriteLine(builder);
        }
    }
}
