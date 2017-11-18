using GaynessPopulationCalculator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaynessPopulationCalculator
{
    class Program
    {
        private static string gayMale = "Gay Male";
        private static string straigthMale = "Straight Male";
        private static string gayFemale = "Gay Female";
        private static string straigthFemale = "Straight Female";
        private static string printFormatString = "{0, -20}{1, -20}{2, -20}{3, -20}";

        // the formatting! https://docs.microsoft.com/en-us/dotnet/standard/base-types/composite-formatting
        // and https://stackoverflow.com/questions/8724861/console-write-syntax-what-does-the-format-string-0-25-mean
        //Console.WriteLine("[{0, -25
        private static GaynessCalculator GayNessCalculator;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, this is the gayness population calculator! Have a Pleasant day.");
            Console.WriteLine();
            BirthRatios birthRatios = new BirthRatios(2.36m, 2.36m);
            decimal population = AskForPopulation();
            BirthRatios birthRatio = AskForBirthRatio();
            bool answer = AskForStepByStep();

            Console.WriteLine(printFormatString, gayMale, straigthMale, straigthFemale, gayFemale);
            if (answer)
            {
                GayNessCalculator = new GaynessCalculator(population, AskForDetails(), birthRatio);

                while (true)
                {
                    Console.WriteLine(printFormatString, GayNessCalculator.Male.Gay, GayNessCalculator.Male.Straight, GayNessCalculator.Female.Straight, GayNessCalculator.Female.Gay);
                    Console.ReadLine();
                    GayNessCalculator.CalculateNextGeneration();
                }
            }
            else
            {
                bool wantToConinue = true;
                GayNessCalculator = new GaynessCalculator(population, false, birthRatio);
                Console.WriteLine("{0, -50 }", "Starting state");
                Console.WriteLine(printFormatString, GayNessCalculator.Male.Gay, GayNessCalculator.Male.Straight, GayNessCalculator.Female.Straight, GayNessCalculator.Female.Gay);

                while (wantToConinue)
                {
                    long steps = AskForHowManySteps();

                  
                    for (long i = 0; i < steps; i++)
                    {
                        GayNessCalculator.CalculateNextGeneration();
                    }
                    Console.WriteLine("{0, -50 }", String.Format("Results after {0} iterations ", steps));
                    Console.WriteLine(printFormatString, GayNessCalculator.Male.Gay, GayNessCalculator.Male.Straight, GayNessCalculator.Female.Straight, GayNessCalculator.Female.Gay);
                    Console.WriteLine();
                  wantToConinue =  AskForBoolean("Would You like to keep on going? (Y/N)");
                }
                Console.WriteLine("Thank you, come back again!");
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
        }

        private static long AskForHowManySteps()
        {
            Console.WriteLine("Please specify the amount of steps");
            string input = Console.ReadLine();
            if (input == null || input.Count() == 0)
            {
                Console.WriteLine("Invalid Input");
                return AskForHowManySteps();
            }

            long steps;
            if (long.TryParse(input, out steps))
            {
                return steps;
            }
            else
            {
                Console.WriteLine("Invalid Input");
                return AskForHowManySteps();
            }
        }

        private static bool AskForStepByStep()
        {
            Console.WriteLine("Would you like to see immediately the result of multiple generatios (G), or go step by step by pressing enter(M) ?");
            string input = Console.ReadLine();
            if (input == null || input.Count() == 0)
            {
                Console.WriteLine("Invalid Input");
                return AskForStepByStep();

            }
            var firstchar = input.First();
            if (firstchar == 'g' || firstchar == 'G')
            {
                return false;
            }
            else if (firstchar == 'm' || firstchar == 'M')
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid Input");
                return AskForStepByStep();
            }
        }

        private static BirthRatios AskForBirthRatio()
        {
            string errorMessage = "Invalid ratio, please try again";
            Console.WriteLine("What is the wanted Birth ratio (2010–2015 is 2.36, write d(D) for defaulting to it)?");
            decimal ratio = 0;
            string lineRead = Console.ReadLine();
            if (lineRead != null && lineRead.Count() != 0)
            {
                if (lineRead.First() == 'd' || lineRead.First() == 'D')
                {
                    Console.WriteLine("Defaulting to 2.36!");
                    return new BirthRatios(2.36m, 2.36m);
                }
            }

            if (decimal.TryParse(lineRead, out ratio))
            {
                if (ratio <= 0)
                {
                    Console.WriteLine(errorMessage);
                    return AskForBirthRatio();
                }
                else
                {
                    return new BirthRatios(ratio, ratio);
                }
            }
            else
            {
                Console.WriteLine(errorMessage);
                return AskForBirthRatio();
            }
        }

        /// <summary>
        /// Returns true if there are details to be shown
        /// </summary>
        /// <returns></returns>
        private static bool AskForDetails()
        {
            Console.WriteLine("Would you like to display details? (Y/N)");
            string reponse = Console.ReadLine();
            if (reponse == null || reponse.Count() == 0)
            {
                Console.WriteLine("Invalid answer, taken as a no!");
                return false;
            }
            else
            {
                char firstchar = reponse.First();
                if (firstchar == 'y' || firstchar == 'Y')
                {
                    return true;
                }
                else if (firstchar == 'n' || firstchar == 'N')
                {
                    return false;
                }
                Console.WriteLine("Invalid answer, taken as a no!");
                return false;
            }

        }

        private static bool AskForBoolean(string message) {
            Console.WriteLine(message);
            string reponse = Console.ReadLine();
            if (reponse == null || reponse.Count() == 0)
            {
                Console.WriteLine("Invalid answer, taken as a no!");
                return false;
            }
            else
            {
                char firstchar = reponse.First();
                if (firstchar == 'y' || firstchar == 'Y')
                {
                    return true;
                }
                else if (firstchar == 'n' || firstchar == 'N')
                {
                    return false;
                }
                Console.WriteLine("Invalid answer, taken as a no!");
                return false;
            }
        }

        private static decimal AskForPopulation()
        {
            Console.WriteLine("What is the initial population?");
            decimal populationNum = 0;
            string lineRead = Console.ReadLine();
            if (decimal.TryParse(lineRead, out populationNum))
            {
                if (populationNum < 0)
                {
                    Console.WriteLine("Invalid initial population, try again");
                    return AskForPopulation();
                }
                else
                {
                    return Math.Round(populationNum);
                }
            }
            else
            {
                Console.WriteLine("Invalid initial population, try again");
                return AskForPopulation();
            }
        }
    }
}
