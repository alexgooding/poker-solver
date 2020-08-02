using System;

namespace PokerSolver
{
    class RunSolver
    {
        Hand myHand;
        Hand communityHand;

        public void runMainWorkflow(int numberOfPlayers)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var successfulInput = false;
            myHand = new Hand();
            communityHand = new Hand();

            string[] line;

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

            Console.WriteLine("Your best hand is:");
            myHand.FindBestHand().PrintSortedHand();

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
       
            Console.WriteLine("Your best hand is:");
            myHand.FindBestHand().PrintSortedHand();

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
            
            Console.WriteLine("Your best hand is:");
            myHand.FindBestHand().PrintSortedHand();

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
  
            Console.WriteLine("Your best hand is:");
            myHand.FindBestHand().PrintSortedHand();
        }
    }
}
