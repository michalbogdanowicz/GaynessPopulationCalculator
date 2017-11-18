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
            GayNessCalculator gayNessCalculator = new GayNessCalculator(AskForPopulation(), AskForDetails());
            Console.WriteLine("Press enter to go on with a generation");
            Console.WriteLine(printFormatString, gayMale,straigthMale,straigthFemale, gayFemale);
       
            while (true)
            {
                Console.WriteLine(printFormatString, gayNessCalculator.Male.Gay, gayNessCalculator.Male.Straight, gayNessCalculator.Female.Straight, gayNessCalculator.Female.Gay);
                Console.ReadLine(); // for not closing
                gayNessCalculator.CalculateNextGeneration();
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

        private static long AskForPopulation()
        {
            Console.WriteLine("What is the initial population?");
            long populationNum = 0;
            string lineRead = Console.ReadLine();
            if (long.TryParse(lineRead, out populationNum)){
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
