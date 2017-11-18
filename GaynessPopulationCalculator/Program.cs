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

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, this is the gayness population calculator! Have a Pleasant day.");
            Console.WriteLine();
            BirthRatios birthRatios = new BirthRatios(2.36m, 2.36m);
            GayNessCalculator gayNessCalculator = new GayNessCalculator(AskForPopulation(), AskForDetails(), AskForBirthRatio());
            Console.WriteLine("Press enter to go on with a generation");
            Console.WriteLine(printFormatString, gayMale,straigthMale,straigthFemale, gayFemale);
           
            while (true)
            {
                Console.WriteLine(printFormatString, gayNessCalculator.Male.Gay, gayNessCalculator.Male.Straight, gayNessCalculator.Female.Straight, gayNessCalculator.Female.Gay);
                Console.ReadLine(); 
                gayNessCalculator.CalculateNextGeneration();
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
                    return new BirthRatios(ratio,ratio);
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
           string reponse =   Console.ReadLine();
            if (reponse == null || reponse.Count() == 0)
            {
                Console.WriteLine("Invalid answer, taken as a no!");
                return false;
            }
            else
            {
             char firstchar =   reponse.First();
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
            if (decimal.TryParse(lineRead, out populationNum)){
                if (populationNum < 0)
                {
                    Console.WriteLine("Invalid initial population, try again");
                    return AskForPopulation();
                }
                else
                {
                    return populationNum;
                }
            }
            else {
                Console.WriteLine("Invalid initial population, try again");
                return AskForPopulation();
            }
        }
    }
}
