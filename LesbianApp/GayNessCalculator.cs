using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaynessPopulationCalculator
{
    public enum PupulationGrowth
    {
        /// <summary>
        /// Stable means that from two human beings make two other human beings.
        /// </summary>
        Stable = 0
    }


    class GayNessCalculator
    {
        public bool WritePopDetails ;
        public GenderPopulation Male { get; set; }
        public GenderPopulation Female { get; set; }

        public GayNessCalculator(long totalPopulation, bool writePopDetails)
        {
            if (totalPopulation <= 0) { throw new ArgumentException("total population cannot be less than 0"); }
            Male = new GenderPopulation(totalPopulation / 2);
            Female = new GenderPopulation(totalPopulation / 2);
            WritePopDetails = writePopDetails;

        }

        public void CalculateNextGeneration()
        {
            // births by straight 
            long straightBirths = CalculateBirthByStraight();
            //biths by female lesbians.
            long lesboBirths =   CalculateBirthByLesbo();
            // PURGE CURRENT POP
            Male.Purge();
            Female.Purge();
            // Calculate new population
            // half are male, half are female
            long newMale = straightBirths / 2;
            long newFemale = newMale;
          
            Male.Gay = newMale / 10;
            Male.Straight = newMale - Male.Gay;
            Female.Gay = newFemale / 10;
            Female.Straight = newFemale - Female.Gay;
            if (WritePopDetails) { Console.WriteLine("From straight Gay Male  : {0} , Straight Male : {1}", Male.Gay,Male.Straight); }
            if (WritePopDetails) { Console.WriteLine("From straight Gay Female  : {0} , Straight Female : {1}", Female.Gay, Female.Straight); }
            // lesbo 50% lesbo rate
            long halfLesbo = lesboBirths / 2;
            Female.Gay += halfLesbo;
            Female.Straight += halfLesbo;
            if (WritePopDetails) { Console.WriteLine("From lesbo Gay Female  : {0} , Straight Female : {1}", Female.Gay, Female.Straight); }
        }

        private long CalculateBirthByLesbo()
        {
            long birthnumber;
            if (Female.Gay == 0)
            {
                birthnumber = 0;
            }
            else
            {
                birthnumber = Female.Gay;
            }
            if (WritePopDetails) { Console.WriteLine("The lesbo births are : {0}", birthnumber); }
            return birthnumber;
        }

        /// <summary>
        /// Only one male for each Female
        /// </summary>
        /// <returns></returns>
        public long CalculateBirthByStraight()
        {
            long birthnumber = 0;
            if (Male.Straight == 0 || Female.Straight == 0)
            {
                birthnumber = 0;
            }
            else
            {
                long numToDouble = Math.Min(Male.Straight, Female.Straight);
                birthnumber = numToDouble * 2;
            }
            if (WritePopDetails) { Console.WriteLine("The straight births are : {0}", birthnumber); }
            return birthnumber;
        }
    }
}
