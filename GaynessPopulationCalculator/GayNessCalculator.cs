using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaynessPopulationCalculator
{

    class GayNessCalculator
    {
        public bool WritePopDetails ;
        public GenderPopulation Male { get; set; }
        public GenderPopulation Female { get; set; }
        Random random;

        public GayNessCalculator(long totalPopulation, bool writePopDetails)
        {
            if (totalPopulation <= 0) { throw new ArgumentException("total population cannot be less than 0"); }
            Male = new GenderPopulation(totalPopulation / 2);
            Female = new GenderPopulation(totalPopulation / 2);
            WritePopDetails = writePopDetails;
            random = new Random();

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
            // 51.9 is male. https://en.wikipedia.org/wiki/Human_sex_ratio
            long newMale = (long)Math.Round((decimal)straightBirths * (decimal)0.519);
            long newFemale = straightBirths - newMale;
            
            DistributeMale(newMale);
            DistributeFemale(newFemale);


            if (WritePopDetails) { Console.WriteLine("From straight Gay Male  : {0} , Straight Male : {1}", Male.Gay,Male.Straight); }
            if (WritePopDetails) { Console.WriteLine("From straight Gay Female  : {0} , Straight Female : {1}", Female.Gay, Female.Straight); }
            long saveStateOfFemaleGay = Female.Gay;
            long saveStateOfFemaleStraight = Female.Straight;
            // lesbo 50% lesbo rate, lesbo has 100% of femlae offspring.
            DistributeLesbiansBirths(lesboBirths);

            if (WritePopDetails) { Console.WriteLine("From lesbo Gay Female  : {0} , Straight Female : {1}", Female.Gay - saveStateOfFemaleGay, Female.Straight - saveStateOfFemaleStraight); }
        }

        private void DistributeLesbiansBirths(long lesboBirths)
        {
            for (int i = 0; i < lesboBirths; i++)
            {
                if (TrueOn50Percentage())
                {
                    Female.Gay++;
                }
                else
                {
                    Female.Straight++;
                }
            }
        }

        private void DistributeFemale(long newFemale)
        {
            for (int i = 0; i < newFemale; i++)
            {
                if (TrueOn10Percentage())
                {
                    Female.Gay++;
                }
                else
                {
                    Female.Straight++;
                }
            }
        }

        private void DistributeMale(long newMale)
        {
            for (int i = 0; i < newMale; i++)
            {
                if (TrueOn10Percentage())
                {
                    Male.Gay++;
                }
                else
                {
                    Male.Straight++;
                }
            }
        }

        private bool TrueOn10Percentage() {
            return random.Next(0, 9) == 0;
        }

        private bool TrueOn50Percentage()
        {
            return random.Next(0, 9) < 5;
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
