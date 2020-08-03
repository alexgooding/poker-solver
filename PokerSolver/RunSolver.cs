using System;
using static PokerSolver.Constants;

namespace PokerSolver
{
    class RunSolver
    {
        Hand myHand;
        Hand communityHand;

        public void runMainWorkflow(int numberOfPlayers)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Hand[] twoCardHands = Hand.GenerateAllTwoCardHands();

            myHand = new Hand();
            communityHand = new Hand();

            string[] line;
            var successfulInput = false;
            (SortedHand, HandType) bestHand;

            while (!successfulInput)
            {
                Console.WriteLine("Enter the two cards in your hand");
                line = Console.ReadLine().Split(null);
                try
                {
                    if (line.Length != 2)
                    {
                        throw new FormatException();
                    }
                    myHand.AddCards(ParseCards.parseCards(line));
                    successfulInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect number of cards entered.");
                }
                catch (Exception)
                {
                    successfulInput = false;
                }
            }

            bestHand = myHand.FindBestHand();
            Console.WriteLine("Your best hand is a " + FriendlyHandTypes[bestHand.Item2] + ":");
            bestHand.Item1.PrintSortedHand();

            successfulInput = false;

            while (!successfulInput)
            {
                Console.WriteLine("Enter the first three community cards");
                line = Console.ReadLine().Split(null);
                try
                {
                    if (line.Length != 3)
                    {
                        throw new FormatException();
                    }
                    myHand.AddCards(ParseCards.parseCards(line));
                    communityHand.AddCards(ParseCards.parseCards(line));
                    successfulInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect number of cards entered.");
                }
                catch (Exception)
                {
                    successfulInput = false;
                } 
            }

            bestHand = myHand.FindBestHand();
            Console.WriteLine("Your best hand is a " + FriendlyHandTypes[bestHand.Item2] + ":");
            bestHand.Item1.PrintSortedHand();

            successfulInput = false;

            while (!successfulInput)
            {
                Console.WriteLine("Enter the fourth community card");
                line = Console.ReadLine().Split(null);
                try
                {
                    if (line.Length != 1)
                    {
                        throw new FormatException();
                    }
                    myHand.AddCards(ParseCards.parseCards(line));
                    communityHand.AddCards(ParseCards.parseCards(line));
                    successfulInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect number of cards entered.");
                }
                catch (Exception)
                {
                    successfulInput = false;
                }
            }

            bestHand = myHand.FindBestHand();
            Console.WriteLine("Your best hand is a " + FriendlyHandTypes[bestHand.Item2] + ":");
            bestHand.Item1.PrintSortedHand();

            successfulInput = false;

            while (!successfulInput)
            {
                Console.WriteLine("Enter the fifth community card");
                line = Console.ReadLine().Split(null);
                try
                {
                    if (line.Length != 1)
                    {
                        throw new FormatException();
                    }
                    myHand.AddCards(ParseCards.parseCards(line));
                    communityHand.AddCards(ParseCards.parseCards(line));
                    successfulInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Incorrect number of cards entered.");
                }
                catch (Exception)
                {
                    successfulInput = false;
                }
            }

            bestHand = myHand.FindBestHand();
            Console.WriteLine("Your best hand is a " + FriendlyHandTypes[bestHand.Item2] + ":");
            bestHand.Item1.PrintSortedHand();
        }
    }
}
